using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web.Script.Serialization;

namespace OneHub360.DB
{
    [Serializable()]
    public abstract partial class NHEntity : INHEntity, ICloneable {
    
        #region Extensibility Method Definitions
        
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
        public NHEntity()
        {
            OnCreated();
            CreationDate = DateTime.Now;
            LastModified = DateTime.Now;
            EntityId = Guid.NewGuid();
            IsDeleted = false;
        }
    
        public virtual Guid Id { get; set; }
        public virtual DateTime CreationDate { get; set; }
        [ScriptIgnore]
        public virtual bool IsDeleted { get; set; }
        [ScriptIgnore]
        public virtual Language Language { get; set; }
        public virtual DateTime LastModified { get; set; }
        public virtual string CreatedBy { get; set; }
        [ScriptIgnore]
        public virtual Guid EntityId { get; set; }

        #region ICloneable Members
        public virtual object Clone()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();
            using (stream)
            {
                formatter.Serialize(stream, this);
                stream.Seek(0, SeekOrigin.Begin);
                return formatter.Deserialize(stream);
            }
        }
        #endregion
    }
}
