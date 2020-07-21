using System;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;

namespace OneHub360.NET.Core.Model
{
    [Serializable()]
    public abstract partial class NHEntity : INHEntity 
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
        public NHEntity()
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
}
