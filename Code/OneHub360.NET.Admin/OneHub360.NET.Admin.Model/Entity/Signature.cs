using OneHub360.NET.Core.Model;
using System;

namespace OneHub360.NET.Admin.Model
{
    [Serializable]
    public partial class Signature : NHEntity
    {
        public Signature()
        {
            Status = SignatureStatus.Active;
        }
        /// <summary>
        /// There are no comments for Title in the schema.
        /// </summary>
        public virtual string Title
        {
            get;
            set;
        }
    
        /// <summary>
        /// There are no comments for Image in the schema.
        /// </summary>
        public virtual byte[] Image
        {
            get;
            set;
        }

        /// <summary>
        /// There are no comments for EnrollmentDate in the schema.
        /// </summary>
        public virtual DateTime EnrollmentDate
        {
            get;
            set;
        }
    
        /// <summary>
        /// There are no comments for NotAfter in the schema.
        /// </summary>
        public virtual DateTime NotAfter
        {
            get;
            set;
        }
    
        /// <summary>
        /// There are no comments for NotBefore in the schema.
        /// </summary>
        public virtual DateTime NotBefore
        {
            get;
            set;
        }

        /// <summary>
        /// There are no comments for Certificate in the schema.
        /// </summary>
        public virtual string Certificate
        {
            get;
            set;
        }

        /// <summary>
        /// There are no comments for Status in the schema.
        /// </summary>
        public virtual SignatureStatus Status
        {
            get;
            set;
        }

        /// <summary>
        /// There are no comments for SMSActivationCode in the schema.
        /// </summary>
        public virtual string SMSActivationCode
        {
            get;
            set;
        }

        /// <summary>
        /// There are no comments for EmailActivationCode in the schema.
        /// </summary>
        public virtual string EmailActivationCode
        {
            get;
            set;
        }

        /// <summary>
        /// There are no comments for ActivationFailureCount in the schema.
        /// </summary>
        public virtual int ActivationFailureCount
        {
            get;
            set;
        }

        public virtual bool IncludePrivateKey
        {
            get;
            set;
        }
    }
}
