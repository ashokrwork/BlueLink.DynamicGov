using Microsoft.VisualStudio.TestTools.UnitTesting;
using OneHub360.App.DAL;
using OneHub360.App.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneHub360.App.DAL.Tests
{
    [TestClass()]
    public class UserActionRepositoryTests
    {
        [TestMethod()]
        public void InsertActionType()
        {
            using (var context = new OneHubContext(true))
            {
                var userActionTypeRepository = new UserActionTypeRepository(context);

                var actionType = new UserActionType
                {
                    ActionCssClass = "fa fa-paper-plane",
                    CreatedBy = "E0050903-2B1A-4DBB-A5AC-0015F36C0F25",
                    CreationDate = DateTime.Now,
                    Id = Guid.Parse("DD797965-6F99-4DF2-B5AB-09F6FE8B15B6"),
                    IsDeleted = false,
                    Language = DB.Language.AR,
                    LastModified = DateTime.Now,
                    Name = "CMS.SendDaftLetter",
                    Message = "إعتماد مسودة كتاب",
                    TemplateUrl = "/partials/modules/cms/action/senddraftletter.html"
                };

                userActionTypeRepository.Insert(actionType);
                context.Transaction.Commit();
            }
        }
    }
}