using System;

namespace OneHub360.NET.Core.Model
{
    public interface INHEntity
    {
        Guid Id { get; set; }
        DateTime CreationDate { get; set; }
        string CreatedBy { get; set; }
        DateTime LastModified { get; set; }
        string LastModifiedBy { get; set; }
        bool IsDeleted { get; set; }
    }
}
