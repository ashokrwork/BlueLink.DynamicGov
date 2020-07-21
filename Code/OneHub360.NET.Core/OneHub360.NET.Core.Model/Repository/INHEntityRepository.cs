using NHibernate;
using System;
using System.Collections.Generic;

namespace OneHub360.NET.Core.Model
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

    public interface INHEntityRepository<T> where T : INHEntity
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
}
