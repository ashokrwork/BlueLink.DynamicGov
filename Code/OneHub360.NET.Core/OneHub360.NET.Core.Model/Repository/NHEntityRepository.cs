using NHibernate;
using System;
using System.Collections.Generic;

namespace OneHub360.NET.Core.Model
{
    public class NHEntityRepository<T> : INHEntityRepository<T>, IDisposable where T : INHEntity
    {
        #region NHEntityRepository<T> Properties
        public ISession Session { get; set; }
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
        //public NHEntityRepository(ISessionFactory sessionFactory)
        //{
        //    SessionFactory = sessionFactory;
        //    if(Session == null)
        //        Session = SessionFactory.OpenSession();
        //}

        public NHEntityRepository(ISession session)
        {
            Session = session; 
        }

        public NHEntityRepository()
        { }
        #endregion

        #region NHEntityRepository<T> Public Methods
        /// <summary>
        /// Insert a new Entity into the Database
        /// </summary>
        /// <param name="entity">Entity to be Saved within the Database and assigned a new ID</param>
        /// <returns>Updated Entity with the ID</returns>
        public T Insert(T entity)
        {
            // Initialize the NHEntityEventArgs and fire the EntityInserting Event
            NHEntityEventArgs entityEventArgs = new NHEntityEventArgs();
            entityEventArgs.Entity = entity;
            this.OnEntityInserting(entityEventArgs);
            if (entityEventArgs.Cancel)
                return entity;

            //using (ISession session = SessionFactory.OpenSession())
            //{
                using(ITransaction transaction = Session.BeginTransaction())
                {
                    entity.Id = (Guid)Session.Save(entity);
                    transaction.Commit();
                }
            //}

            // Initialize the NHEntityEventArgs and fire the EntityInserted Event
            entityEventArgs.Entity = entity;
            this.OnEntityInserted(entityEventArgs);

            return entity;
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

            //using (ISession session = SessionFactory.OpenSession())
            //{
                using (ITransaction transaction = Session.BeginTransaction())
                {
                    Session.Update(entity);
                    transaction.Commit();
                }
            //}

            // Initialize the NHEntityEventArgs and fire the EntityUpdated Event
            entityEventArgs.Entity = entity;
            this.OnEntityUpdated(entityEventArgs);
        }
     
        /// <summary>
        /// Save or Update an entity within the database
        /// </summary>
        /// <param name="entity">Entity to be saved or updated</param>
        public void Save(T entity)
        {
            using (ITransaction transaction = Session.BeginTransaction())
            {
                Session.SaveOrUpdate(entity);
                transaction.Commit();
            }
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

            //using (ISession session = SessionFactory.OpenSession())
            //{
                using (ITransaction transaction = Session.BeginTransaction())
                {
                    if(Session.Delete("from " + typeof(T) + " where Id ='" + id + "' AND IsDeleted=True") == 0)
                    {
                        string updateHQL = @"update " + typeof(T)  + " T set T.IsDeleted = 'True' where T.Id = '" + id + "'";
                        Session.CreateQuery(updateHQL).ExecuteUpdate();
                    }
                    transaction.Commit();
                }
            //}

            // Initialize the NHEntityEventArgs and fire the EntityDeleted Event
            this.OnEntityDeleted(entityEventArgs);
        }

        /// <summary>
        /// Read an Entity from the Database
        /// </summary>
        /// <param name="id">Entity ID to be Readed</param>
        /// <returns>NHEntity Readed from the Database</returns>
        public T GetById(Guid id)
        {
            T entity = default(T);
            //using (ISession session = SessionFactory.OpenSession())
            { 
                entity = Session.Get<T>(id);
            }

            // Initialize the NHEntityEventArgs and fire the EntityReaded Event
            NHEntityEventArgs entityEventArgs = new NHEntityEventArgs();
            entityEventArgs.EntityId = id;
            this.OnEntityReaded(entityEventArgs);

            return entity;
        }

        public IList<T> GetAll()
        {
            IList<T> entities = new List<T>();
            //using (ISession session = SessionFactory.OpenSession())
            {
                entities = Session.CreateQuery("from " + typeof(T) + " where IsDeleted=false").List<T>();
            }
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

            IQuery query = Session.CreateQuery(queryText);
            query.SetMaxResults(pageLength);
            query.SetFirstResult(start);

            entities = query.List<T>();

            queryText = "select count(*) from " + typeof(T) + ((!string.IsNullOrEmpty(whereClause))? " where IsDeleted=false AND " + whereClause : " where IsDeleted=false");
            totalCount = (long)Session.CreateQuery(queryText).UniqueResult();
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
            long totalCount = 0;
            GetPaged(whereClause, null, 0, int.MaxValue, out totalCount);
            return totalCount;
        }
        #endregion

        #region IDisposable Implementation
        public void Dispose()
        {
            if(Session.IsOpen)
                Session.Close();
        }
        #endregion
    }
}
