using System;
using OneHub360.NET.Core.Model;

namespace OneHub360.NET.Admin.Model
{
    [Serializable]
    public partial class Invitation :NHEntity
    {
        public Invitation()
        {
            Status = InvitationStatus.Sent;
        }
        /// <summary>
        /// There are no comments for ReplayDate in the schema.
        /// </summary>
        public virtual Nullable<DateTime> ReplayDate
        {
            get;
            set;
        }

        /// <summary>
        /// There are no comments for Status in the schema.
        /// </summary>
        public virtual InvitationStatus Status
        {
            get;
            set;
        }

        /// <summary>
        /// There are no comments for UserInfoFrom in the schema.
        /// </summary>
        public virtual UserInfo UserInfoFrom
        {
            get;
            set;
        }

        /// <summary>
        /// There are no comments for UserInfoTo in the schema.
        /// </summary>
        public virtual UserInfo UserInfoTo
        {
            get;
            set;
        }
    }

}
