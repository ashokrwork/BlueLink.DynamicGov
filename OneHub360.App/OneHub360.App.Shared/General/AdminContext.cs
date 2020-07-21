using OneHub360.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using System.Web.Hosting;
using System.IO;
using NHibernate.Cfg;
using System.Runtime.Serialization;

namespace OneHub360.App.Shared
{
    public class AdminContext : IDBContext
    {
        public AdminContext()
        {
            InitContext(false);
        }

        public AdminContext(bool isTransactional)
        {
            InitContext(isTransactional);
        }

        private void InitContext(bool isTransactional)
        {
            var configFilePath = string.Empty;

            if (HostingEnvironment.ApplicationHost == null)
                configFilePath = Path.Combine(System.Environment.CurrentDirectory, "config\\app\\admin.config");
            else
                configFilePath = HostingEnvironment.MapPath("~/config/app/admin.config");

            sessionFactory = new Configuration().Configure(configFilePath).BuildSessionFactory();
            session = sessionFactory.OpenSession();
            session.CacheMode = CacheMode.Ignore;
            if (isTransactional)
                transaction = session.BeginTransaction();
        }

        private ISession session;
        private ISessionFactory sessionFactory;
        private ITransaction transaction;

        public ISession Session
        {
            get
            {
                return session;
            }

        }

        public ISessionFactory SessionFactory
        {
            get
            {
                return SessionFactory;
            }


        }

        public ITransaction Transaction
        {
            get
            {
                return transaction;
            }


        }

        public IStatelessSession StalelessSession
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void Dispose()
        {

            if (session.IsOpen)
                session.Close();

            if (!sessionFactory.IsClosed)
                sessionFactory.Close();

            if (transaction != null)
                transaction.Dispose();

            session.Dispose();

            sessionFactory.Dispose();
        }
    }

    [Serializable()]
    public abstract partial class AdminNHEntity : IAdminNHEntity
    {

        #region NHEntity<Key> Public Extensibility Methods

        /// <summary>
        /// There are no comments for OnCreated in the schema.
        /// </summary>
        partial void OnCreated();

        public override bool Equals(object obj)
        {
            NHEntity toCompare = obj as NHEntity;

            if (toCompare == null)
            {
                return false;
            }

            if (!Object.Equals(this.Id, toCompare.Id))
                return false;

            return true;
        }

        public override int GetHashCode()
        {
            int hashCode = 13;
            hashCode = (hashCode * 7) + Id.GetHashCode();
            return hashCode;
        }

        #endregion

        #region NHEntity<Key> Public Constructors
        public AdminNHEntity()
        {
            OnCreated();
            CreationDate = DateTime.Now;
            LastModified = DateTime.Now;
            IsDeleted = false;
        }
        #endregion

        #region NHEntity<Key> Public Properties
        public virtual Guid Id { get; set; }

        public virtual DateTime CreationDate { get; set; }

        public virtual string CreatedBy { get; set; }

        public virtual DateTime LastModified { get; set; }

        public virtual string LastModifiedBy { get; set; }

        [IgnoreDataMember]
        public virtual bool IsDeleted { get; set; }
        #endregion
    }

    public interface IAdminNHEntity
    {
        Guid Id { get; set; }
        DateTime CreationDate { get; set; }
        string CreatedBy { get; set; }
        DateTime LastModified { get; set; }
        string LastModifiedBy { get; set; }
        bool IsDeleted { get; set; }
    }

    #region NHEntityRepository Event Delegates
    public delegate void EntityInsertingEventHandler(object sender, AdminNHEntityEventArgs e);
    public delegate void EntityInsertedEventHandler(object sender, AdminNHEntityEventArgs e);
    public delegate void EntityUpdatingEventHandler(object sender, AdminNHEntityEventArgs e);
    public delegate void EntityUpdatedEventHandler(object sender, AdminNHEntityEventArgs e);
    public delegate void EntityDeletingEventHandler(object sender, AdminNHEntityEventArgs e);
    public delegate void EntityDeletedEventHandler(object sender, AdminNHEntityEventArgs e);
    public delegate void EntityReadedEventHandler(object sender, AdminNHEntityEventArgs e);
    #endregion

    public interface IAdminNHEntityRepository<T> where T : IAdminNHEntity
    {
        #region NHEntityRepository<T> Events
        /// <summary>
        /// Before Inserting an Entity into the Database
        /// </summary>
        event EntityInsertingEventHandler EntityInserting;

        /// <summary>
        /// After Inserting an Entity into the Database
        /// </summary>
        event EntityInsertedEventHandler EntityInserted;

        /// <summary>
        /// Before Updating Entity into the Database
        /// </summary>
        event EntityUpdatingEventHandler EntityUpdating;

        /// <summary>
        /// After Entity Updated into the Database
        /// </summary>
        event EntityUpdatedEventHandler EntityUpdated;

        /// <summary>
        /// Before Deleting an Entity from the Database
        /// </summary>
        event EntityDeletingEventHandler EntityDeleting;

        /// <summary>
        /// After Deleting an Entity from the Database
        /// </summary>
        event EntityDeletedEventHandler EntityDeleted;

        /// <summary>
        /// After Reading an Item Details
        /// </summary>
        event EntityReadedEventHandler EntityReaded;
        #endregion

        #region NHEntityRepository<T> Public Methods
        /// <summary>
        /// Insert a new Entity into the Database
        /// </summary>
        /// <param name="entity">Entity to be Saved within the Database and assigned a new ID</param>
        /// <returns>Updated Entity with the ID</returns>
        T Insert(T entity);

        /// <summary>
        /// Update and Exiting Entity within the Database
        /// </summary>
        /// <param name="entity">Object required to be Updated</param>
        void Update(T entity);

        /// <summary>
        /// Save or Update an entity within the database
        /// </summary>
        /// <param name="entity">Entity to be saved or updated</param>
        void Save(T entity);

        /// <summary>
        /// Mark Entity as Deleted, and if it is marked as deleted it will be deleted from the Database
        /// </summary>
        /// <param name="entity">Entity to be deleted</param>
        void Delete(T entity);

        /// <summary>
        /// Mark Entity as Deleted, and if it is marked as deleted it will be deleted from the Database
        /// </summary>
        /// <param name="id">Entity ID to be Deleted</param>
        void Delete(Guid id);

        /// <summary>
        /// Read an Entity from the Database
        /// </summary>
        /// <param name="id">Entity ID to be Readed</param>
        /// <returns>NHEntity Readed from the Database</returns>
        T GetById(Guid id);

        IList<T> GetAll();

        IList<T> GetPaged(string whereClause, string orderBy, int start, int pageLength, out long totalCount);

        IList<T> GetPaged(out long totalCount);


        IList<T> GetPaged(int start, int pageLength, out long totalCount);

        long GetTotalItems(string whereClause);

        #endregion
    }

    public class AdminNHEntityRepository<T> : IAdminNHEntityRepository<T>, IDisposable where T : IAdminNHEntity
    {
        #region NHEntityRepository<T> Properties
        public ISession Session { get; set; }
        #endregion

        #region NHEntityRepository<T> Events
        /// <summary>
        /// Before Inserting an Entity into the Database
        /// </summary>
        public event EntityInsertingEventHandler EntityInserting;
        protected virtual void OnEntityInserting(AdminNHEntityEventArgs e)
        {
            if (EntityInserting != null)
                EntityInserting(this, e);
        }

        /// <summary>
        /// After Inserting an Entity into the Database
        /// </summary>
        public event EntityInsertedEventHandler EntityInserted;
        protected virtual void OnEntityInserted(AdminNHEntityEventArgs e)
        {
            if (EntityInserted != null)
                EntityInserted(this, e);
        }

        /// <summary>
        /// Before Updating Entity into the Database
        /// </summary>
        public event EntityUpdatingEventHandler EntityUpdating;
        protected virtual void OnEntityUpdating(AdminNHEntityEventArgs e)
        {
            if (EntityUpdating != null)
                EntityUpdating(this, e);
        }

        /// <summary>
        /// After Entity Updated into the Database
        /// </summary>
        public event EntityUpdatedEventHandler EntityUpdated;
        protected virtual void OnEntityUpdated(AdminNHEntityEventArgs e)
        {
            if (EntityUpdated != null)
                EntityUpdated(this, e);
        }

        /// <summary>
        /// Before Deleting an Entity from the Database
        /// </summary>
        public event EntityDeletingEventHandler EntityDeleting;
        protected virtual void OnEntityDeleting(AdminNHEntityEventArgs e)
        {
            if (EntityDeleting != null)
                EntityDeleting(this, e);
        }

        /// <summary>
        /// After Deleting an Entity from the Database
        /// </summary>
        public event EntityDeletedEventHandler EntityDeleted;
        protected virtual void OnEntityDeleted(AdminNHEntityEventArgs e)
        {
            if (EntityDeleted != null)
                EntityDeleted(this, e);
        }

        /// <summary>
        /// After Reading an Item Details
        /// </summary>
        public event EntityReadedEventHandler EntityReaded;
        protected virtual void OnEntityReaded(AdminNHEntityEventArgs e)
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

        public AdminNHEntityRepository(IDBContext context)
        {
            Session = context.Session;
        }

        public AdminNHEntityRepository()
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
            // Initialize the AdminNHEntityEventArgs and fire the EntityInserting Event
            AdminNHEntityEventArgs entityEventArgs = new AdminNHEntityEventArgs();
            entityEventArgs.Entity = entity;
            this.OnEntityInserting(entityEventArgs);
            if (entityEventArgs.Cancel)
                return entity;

            //using (ISession session = SessionFactory.OpenSession())
            //{
            using (ITransaction transaction = Session.BeginTransaction())
            {
                entity.Id = (Guid)Session.Save(entity);
                transaction.Commit();
            }
            //}

            // Initialize the AdminNHEntityEventArgs and fire the EntityInserted Event
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
            // Initialize the AdminNHEntityEventArgs and fire the EntityUpdating Event
            AdminNHEntityEventArgs entityEventArgs = new AdminNHEntityEventArgs();
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

            // Initialize the AdminNHEntityEventArgs and fire the EntityUpdated Event
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
            // Initialize the AdminNHEntityEventArgs and fire the EntityDeleting Event
            AdminNHEntityEventArgs entityEventArgs = new AdminNHEntityEventArgs();
            entityEventArgs.EntityId = id;
            this.OnEntityDeleting(entityEventArgs);
            if (entityEventArgs.Cancel)
                return;

            //using (ISession session = SessionFactory.OpenSession())
            //{
            using (ITransaction transaction = Session.BeginTransaction())
            {
                if (Session.Delete("from " + typeof(T) + " where Id ='" + id + "' AND IsDeleted=True") == 0)
                {
                    string updateHQL = @"update " + typeof(T) + " T set T.IsDeleted = 'True' where T.Id = '" + id + "'";
                    Session.CreateQuery(updateHQL).ExecuteUpdate();
                }
                transaction.Commit();
            }
            //}

            // Initialize the AdminNHEntityEventArgs and fire the EntityDeleted Event
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

            // Initialize the AdminNHEntityEventArgs and fire the EntityReaded Event
            AdminNHEntityEventArgs entityEventArgs = new AdminNHEntityEventArgs();
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

            queryText = "select count(*) from " + typeof(T) + ((!string.IsNullOrEmpty(whereClause)) ? " where IsDeleted=false AND " + whereClause : " where IsDeleted=false");
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
            if (Session.IsOpen)
                Session.Close();
        }
        #endregion
    }

    public class AdminNHEntityEventArgs : EventArgs
    {
        public AdminNHEntityEventArgs()
        {
            Cancel = false;
        }
        public IAdminNHEntity Entity { get; set; }
        public Guid EntityId { get; set; }
        public bool Cancel { get; set; }

    }
}
