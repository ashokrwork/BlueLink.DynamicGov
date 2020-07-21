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
    public class FeedTypeRepositoryTests
    {
        [TestMethod()]
        public void UpdateMOIFeedTypes()
        {
            using (var context = new OneHubContext(true))
            {

                var feedTypeRepository = new FeedTypeRepository(context);

                var moiReport = new FeedTypes { ShowInNewItemStrip = true, EntityId = Guid.NewGuid(),Title="بلاغ",LastModified = DateTime.Now,CreatedBy= "OneHub360", CreationDate = DateTime.Now,GroupTitle ="نظام البلاغات والتحقيقات",Id = Guid.NewGuid() ,TemplateUrl = "/partials/modules/cms/feeder/moireportitem" };

                feedTypeRepository.Insert(moiReport);

                context.Transaction.Commit();
            }
        }
            
        [TestMethod()]
        public void UpdateFeedTypes()
        {
            using(var context = new OneHubContext(true)){

                var feedTypeRepository = new FeedTypeRepository(context);

                // Draft Memo
                var draftMemoType = feedTypeRepository.GetById(Guid.Parse("23722766-6DD5-4CB3-8989-22655038300E"));
                if (draftMemoType != null)
                {
                    feedTypeRepository.ForceDelete(Guid.Parse("23722766-6DD5-4CB3-8989-22655038300E"));
                }
                draftMemoType = new FeedTypes
                {
                    Id = Guid.Parse("23722766-6DD5-4CB3-8989-22655038300E"),
                    CreatedBy = "OneHub360",
                    CreationDate = DateTime.Now,
                    IsDeleted = false,
                    Language = DB.Language.AR,
                    LastModified = DateTime.Now,
                    NewFormUrl = "Modal.createdraftmemo",
                    NewStripImageUrl = "/img/modules/cms/draftmemo259.png",
                    ShowInHome = true,
                    ShowInNewItemStrip = true,
                    TemplateUrl = "/partials/modules/cms/draftmemoitem.html",
                    Title = "مسودة مذكرة",
                    GroupTitle = "المراسلات"
                };
                feedTypeRepository.Insert(draftMemoType);

                // Outgoing Memo
                var outgoingMemoType = feedTypeRepository.GetById(Guid.Parse("38C15649-3D62-42A7-8B53-979A763F4055"));
                if (outgoingMemoType != null)
                {
                    feedTypeRepository.ForceDelete(Guid.Parse("38C15649-3D62-42A7-8B53-979A763F4055"));
                }
                outgoingMemoType = new FeedTypes
                {
                    Id = Guid.Parse("38C15649-3D62-42A7-8B53-979A763F4055"),
                    CreatedBy = "OneHub360",
                    CreationDate = DateTime.Now,
                    IsDeleted = false,
                    Language = DB.Language.AR,
                    LastModified = DateTime.Now,
                    ShowInHome = true,
                    TemplateUrl = "/partials/modules/cms/outgoingmemoitem.html",
                    Title = "مذكرة صادرة",
                    GroupTitle = "المراسلات"
                };
                feedTypeRepository.Insert(outgoingMemoType);

                // Incoming Memo
                var incomingMemoType = feedTypeRepository.GetById(Guid.Parse("98B2986D-542D-4959-9317-3F3B398CCF3E"));
                if (incomingMemoType != null)
                {
                    feedTypeRepository.ForceDelete(Guid.Parse("98B2986D-542D-4959-9317-3F3B398CCF3E"));
                }
                incomingMemoType = new FeedTypes
                {
                    Id = Guid.Parse("98B2986D-542D-4959-9317-3F3B398CCF3E"),
                    CreatedBy = "OneHub360",
                    CreationDate = DateTime.Now,
                    IsDeleted = false,
                    Language = DB.Language.AR,
                    LastModified = DateTime.Now,
                    ShowInHome = true,
                    TemplateUrl = "/partials/modules/cms/incomingmemoitem.html",
                    Title = "مذكرة واردة",
                    GroupTitle = "المراسلات"
                };
                feedTypeRepository.Insert(incomingMemoType);

                // Draft Letter
                var draftLetterType = feedTypeRepository.GetById(Guid.Parse("3D2ABCA4-40DD-4B07-8782-B287F2002CD3"));
                if (draftLetterType != null)
                {
                    feedTypeRepository.ForceDelete(Guid.Parse("3D2ABCA4-40DD-4B07-8782-B287F2002CD3"));
                }
                draftLetterType = new FeedTypes
                {
                    Id = Guid.Parse("3D2ABCA4-40DD-4B07-8782-B287F2002CD3"),
                    CreatedBy = "OneHub360",
                    CreationDate = DateTime.Now,
                    IsDeleted = false,
                    Language = DB.Language.AR,
                    LastModified = DateTime.Now,
                    NewFormUrl = "Modal.createdraftletter",
                    NewStripImageUrl = "/img/modules/cms/draftletter259.png",
                    ShowInHome = true,
                    ShowInNewItemStrip = true,
                    TemplateUrl = "/partials/modules/cms/draftletteritem.html",
                    Title = "مسودة كتاب",
                    GroupTitle = "المراسلات"
                };
                feedTypeRepository.Insert(draftLetterType);

                // Outgoing Letter
                var outgoingLetterType = feedTypeRepository.GetById(Guid.Parse("16E26CF7-39DC-4962-859C-B6A20B4D0FFF"));
                if (outgoingLetterType != null)
                {
                    feedTypeRepository.ForceDelete(Guid.Parse("16E26CF7-39DC-4962-859C-B6A20B4D0FFF"));
                }
                outgoingLetterType = new FeedTypes
                {
                    Id = Guid.Parse("16E26CF7-39DC-4962-859C-B6A20B4D0FFF"),
                    CreatedBy = "OneHub360",
                    CreationDate = DateTime.Now,
                    IsDeleted = false,
                    Language = DB.Language.AR,
                    LastModified = DateTime.Now,
                    ShowInHome = true,
                    TemplateUrl = "/partials/modules/cms/outgoingletteritem.html",
                    Title = "كتاب صادر",
                    GroupTitle = "المراسلات"
                };
                feedTypeRepository.Insert(outgoingLetterType);


                // Draft Letter
                var incomingLetterType = feedTypeRepository.GetById(Guid.Parse("C6C08AF1-DDB2-4ED0-A0B3-F5C79BD85F29"));
                if (incomingLetterType != null)
                {
                    feedTypeRepository.ForceDelete(Guid.Parse("C6C08AF1-DDB2-4ED0-A0B3-F5C79BD85F29"));
                }
                incomingLetterType = new FeedTypes
                {
                    Id = Guid.Parse("C6C08AF1-DDB2-4ED0-A0B3-F5C79BD85F29"),
                    CreatedBy = "OneHub360",
                    CreationDate = DateTime.Now,
                    IsDeleted = false,
                    Language = DB.Language.AR,
                    LastModified = DateTime.Now,
                    NewFormUrl = "Modal.createincomingletter",
                    NewStripImageUrl = "/img/modules/cms/incomingletter.png",
                    ShowInHome = true,
                    ShowInNewItemStrip = true,
                    TemplateUrl = "/partials/modules/cms/incomingletteritem.html",
                    Title = "تسجيل كتاب وارد",
                    GroupTitle = "المراسلات"
                };
                feedTypeRepository.Insert(incomingLetterType);


                context.Transaction.Commit();
            }

        }
    }
}