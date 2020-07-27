using OneHub360.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneHub360.CMS.DAL
{
    public class CorrespondenceDocumentsRepository : NHEntityRepository<CorrespondenceDocuments>
    {
        public CorrespondenceDocumentsRepository(IDBContext context) : base(context)
        {
        }

        public CorrespondenceDocuments CreateFromDocument(Guid FK_Correspondece, Document document)
        {
            return new CorrespondenceDocuments
            {
                CreatedBy = document.CreatedBy,
                CreationDate = document.CreationDate,
                FK_Correspondence = FK_Correspondece,
                FK_Document = document.Id,
                IsDeleted = document.IsDeleted,
                LastModified = document.LastModified,
                Language = document.Language,
                Id = Guid.NewGuid()
            };
        }
    }
}
