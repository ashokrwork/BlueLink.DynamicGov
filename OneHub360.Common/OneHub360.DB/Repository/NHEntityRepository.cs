using NHibernate;
using NHibernate.Cfg;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace OneHub360.DB
{
    #region NHEntityRepository Event Delegates
    public delegate void EntityInsertingEventHandler(object sender, NHEntityEventArgs e);
    public delegate void EntityInsertedEventHandler(object sender, NHEntityEventArgs e);
    public delegate void EntityUpdatingEventHandler(object sender, NHEntityEventArgs e);
    public delegate void EntityUpdatedEventHandler(object sender, NHEntityEventArgs e);
    public delegate void EntityDeletingEventHandler(object sender, NHEntityEventArgs e);
    public delegate void EntityDeletedEventHandler(object sender, NHEntityEventArgs e);
    public delegate void EntityReadedEventHandler(object sender, NHEntityEventArgs e);
    #endregion
    public class NHEntityRepository<T> : IDisposable where T : INHEntity
    {
        #region NHEntityRepository<T> Properties
        protected IDBContext Context;
        
        protected bool IsStateless;
        #endregion

        #region NHEntityRepository<T> Events
        /// <summary>
        /// Before Inserting an Entity into the Database
        /// </summary>
        public event EntityInsertingEventHandler EntityInserting;
        protected virtual void OnEntityInserting(NHEntityEventArgs e)
        {
            if (EntityInserting != null)
                EntityInserting(this, e);
        }

        /// <summary>
        /// After Inserting an Entity into the Database
        /// </summary>
        public event EntityInsertedEventHandler EntityInserted;
        protected virtual void OnEntityInserted(NHEntityEventArgs e)
        {
            if (EntityInserted != null)
                EntityInserted(this, e);
        }

        /// <summary>
        /// Before Updating Entity into the Database
        /// </summary>
        public event EntityUpdatingEventHandler EntityUpdating;
        protected virtual void OnEntityUpdating(NHEntityEventArgs e)
        {
            if (EntityUpdating != null)
                EntityUpdating(this, e);
        }

        /// <summary>
        /// After Entity Updated into the Database
        /// </summary>
        public event EntityUpdatedEventHandler EntityUpdated;
        protected virtual void OnEntityUpdated(NHEntityEventArgs e)
        {
            if (EntityUpdated != null)
                EntityUpdated(this, e);
        }

        /// <summary>
        /// Before Deleting an Entity from the Database
        /// </summary>
        public event EntityDeletingEventHandler EntityDeleting;
        protected virtual void OnEntityDeleting(NHEntityEventArgs e)
        {
            if (EntityDeleting != null)
                EntityDeleting(this, e);
        }

        /// <summary>
        /// After Deleting an Entity from the Database
        /// </summary>
        public event EntityDeletedEventHandler EntityDeleted;
        protected virtual void OnEntityDeleted(NHEntityEventArgs e)
        {
            if (EntityDeleted != null)
                EntityDeleted(this, e);
        }

        /// <summary>
        /// After Reading an Item Details
        /// </summary>
        public event EntityReadedEventHandler EntityReaded;
        protected virtual void OnEntityReaded(NHEntityEventArgs e)
        {
            if (EntityReaded != null)
                EntityReaded(this, e);
        }
        #endregion

        #region NHEntityRepository<T> Constuctor

        public NHEntityRepository(IDBContext context)
        {
            Context = context;
            IsStateless = false;
        }

        
        #endregion

        #region NHEntityRepository<T> Public Methods

        public T InitEntity(CreationInfo creationInfo)
        {
            var entity = (T)Activator.CreateInstance(typeof(T));

            entity.CreatedBy = creationInfo.CreatedBy;
            entity.CreationDate = creationInfo.CreationDate;
            entity.Id = creationInfo.Id.HasValue ? creationInfo.Id.Value : Guid.NewGuid();
            entity.IsDeleted = creationInfo.IsDeleted;
            entity.LastModified = creationInfo.LastModified;
            entity.Language = creationInfo.Language;

            return entity;
        }

        //public void ExecuteQuery()
        //{
        //    var query = Context.Session.getna
        //    query.SetParameter("","",NHibernate.Type.StringType)
        //}

        public T InitEntity(T entity,CreationInfo creationInfo)
        {
            entity.CreatedBy = creationInfo.CreatedBy;
            entity.CreationDate = creationInfo.CreationDate;
            entity.Id = creationInfo.Id.HasValue ? creationInfo.Id.Value : Guid.NewGuid();
            entity.IsDeleted = creationInfo.IsDeleted;
            entity.LastModified = creationInfo.LastModified;
            entity.Language = creationInfo.Language;

            return entity;
        }

        /// <summary>
        /// Insert a new Entity into the Database
        /// </summary>
        /// <param name="entity">Entity to be Saved within the Database and assigned a new ID</param>
        /// <returns>Updated Entity with the ID</returns>
        public virtual T Insert(T entity)
        {
            // Initialize the NHEntityEventArgs and fire the EntityInserting Event
            NHEntityEventArgs entityEventArgs = new NHEntityEventArgs();
            entityEventArgs.Entity = entity;
            OnEntityInserting(entityEventArgs);
            if (entityEventArgs.Cancel)
                return entity;
            
            entity.Id = (Guid)Context.Session.Save(entity);

            // Initialize the NHEntityEventArgs and fire the EntityInserted Event
            entityEventArgs.Entity = entity;
            OnEntityInserted(entityEventArgs);

            return entity;
        }

        public IList<T> QueryOverIn<T,P>(Expression<Func<T, object>> expression, P[] ids) where T:class
        {
            return Context.Session.QueryOver<T>().AndRestrictionOn(expression).IsIn(ids).List();
        }

        public IList<T> QueryOverWhere<T, P>(Expression<Func<T, bool>> where,Expression<Func<T, object>> expression, P[] ids) where T : class
        {
            return Context.Session.QueryOver<T>().Where(where).AndRestrictionOn(expression).IsIn(ids).List();
        }

        /// <summary>
        /// Update and Exiting Entity within the Database
        /// </summary>
        /// <param name="entity">Object required to be Updated</param>
        public void Update(T entity)
        {
            // Initialize the NHEntityEventArgs and fire the EntityUpdating Event
            NHEntityEventArgs entityEventArgs = new NHEntityEventArgs();
            entityEventArgs.Entity = entity;
            this.OnEntityUpdating(entityEventArgs);
            if (entityEventArgs.Cancel)
                return;

            
                    entity.LastModified = DateTime.Now;
                    Context.Session.Update(entity);
                    
                
            

            // Initialize the NHEntityEventArgs and fire the EntityUpdated Event
            entityEventArgs.Entity = entity;
            this.OnEntityUpdated(entityEventArgs);
        }

        public void DynamicUpdate(Guid id, Dictionary<string, object> entityNewValues)
        {
            
                    var entity = GetById(id);
                    entity.LastModified = DateTime.Now;

                    foreach (KeyValuePair<string, object> newValue in entityNewValues)
                    {
                        PropertyInfo prop = entity.GetType().GetProperty(newValue.Key, BindingFlags.Public | BindingFlags.Instance);
                        var propertyType = prop.PropertyType;
                if(propertyType == typeof(Guid))
                     prop.SetValue(entity,Guid.Parse(newValue.Value.ToString()));

                if(propertyType == typeof(string))
                    prop.SetValue(entity, newValue.Value.ToString());

            }

            Context.Session.Update(entity);
             
        }

        

        /// <summary>
        /// Save or Update an entity within the database
        /// </summary>
        /// <param name="entity">Entity to be saved or updated</param>
        public void Save(T entity)
        {
            
                    Context.Session.SaveOrUpdate(entity);
                    
                
        }

        /// <summary>
        /// Mark Entity as Deleted, and if it is marked as deleted it will be deleted from the Database
        /// </summary>
        /// <param name="entity">Entity to be deleted</param>
        public void Delete(T entity)
        {
            this.Delete(entity.Id);
        }

        /// <summary>
        /// Mark Entity as Deleted, and if it is marked as deleted it will be deleted from the Database
        /// </summary>
        /// <param name="id">Entity ID to be Deleted</param>
        public void Delete(Guid id)
        {
            // Initialize the NHEntityEventArgs and fire the EntityDeleting Event
            NHEntityEventArgs entityEventArgs = new NHEntityEventArgs();
            entityEventArgs.EntityId = id;
            this.OnEntityDeleting(entityEventArgs);
            if (entityEventArgs.Cancel)
                return;

            
                    if (Context.Session.Delete("from " + typeof(T) + " where Id ='" + id + "' AND IsDeleted=True") == 0)
                    {
                        string updateHQL = @"update " + typeof(T) + " T set T.IsDeleted = 'True' where T.Id = '" + id + "'";
                        Context.Session.CreateQuery(updateHQL).ExecuteUpdate();
                    }
                    
                

            // Initialize the NHEntityEventArgs and fire the EntityDeleted Event
            this.OnEntityDeleted(entityEventArgs);
        }

        public void ForceDelete(Guid id)
        {
            Context.Session.Delete("from " + typeof(T) + " where Id ='" + id + "'");
        }

        /// <summary>
        /// Read an Entity from the Database
        /// </summary>
        /// <param name="id">Entity ID to be Readed</param>
        /// <returns>NHEntity Readed from the Database</returns>
        public T GetById(Guid id)
        {
            Context.Session.Flush();   

            T entity = default(T);
            
                entity = Context.Session.Get<T>(id);
            
           

            // Initialize the NHEntityEventArgs and fire the EntityReaded Event
            NHEntityEventArgs entityEventArgs = new NHEntityEventArgs();
            entityEventArgs.EntityId = id;
            this.OnEntityReaded(entityEventArgs);

            return entity;
        }

        public IList<T> GetAll()
        {
            IList<T> entities = new List<T>();
            
                entities = Context.Session.CreateQuery("from " + typeof(T) + " where IsDeleted=false").List<T>();
            
            return entities;
        }

       
        public IList<T> GetPaged(string whereClause, string orderBy, int start, int pageLength, out long totalCount)
        {
            IList<T> entities = new List<T>();
            string queryText = "from " + typeof(T);
            if (!string.IsNullOrEmpty(whereClause))
                queryText = queryText + " where IsDeleted=false AND " + whereClause;
            else
                queryText = queryText + " where IsDeleted=false";

            if (!string.IsNullOrEmpty(orderBy))
                queryText = queryText + " order by " + orderBy;

            
                IQuery query = Context.Session.CreateQuery(queryText);
                query.SetMaxResults(pageLength);
                query.SetFirstResult(start);

                entities = query.List<T>();

                queryText = "select count(*) from " + typeof(T) + ((!string.IsNullOrEmpty(whereClause)) ? " where IsDeleted=false AND " + whereClause : " where IsDeleted=false");
                totalCount = (long)Context.Session.CreateQuery(queryText).UniqueResult();
            
            return entities;
        }
        public IList<T> GetPaged(out long totalCount)
        {
            return GetPaged(null, null, 0, int.MaxValue, out totalCount);
        }

        public IList<T> GetPaged(int start, int pageLength, out long totalCount)
        {
            return GetPaged(null, null, start, pageLength, out totalCount);
        }
        public long GetTotalItems(string whereClause)
        {
           
                var queryText = "select count(*) from " + typeof(T) + ((!string.IsNullOrEmpty(whereClause)) ? " where IsDeleted=false AND " + whereClause : " where IsDeleted=false");
                return (long)Context.Session.CreateQuery(queryText).UniqueResult();
            
        }
        #endregion

        #region IDisposable Implementation
        public void Dispose()
        {
            //if(Session.IsOpen)
            //    Session.Close();
        }
        #endregion
    }
}
