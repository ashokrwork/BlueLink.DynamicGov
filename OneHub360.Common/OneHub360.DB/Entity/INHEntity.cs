using System;

namespace OneHub360.DB
{
    public interface INHEntity
    {
        Guid Id { get; set; }
        DateTime CreationDate { get; set; }
        DateTime LastModified { get; set; }
        bool IsDeleted { get; set; }

        string CreatedBy { get; set; }

        Language Language { get; set; }
    }
}
