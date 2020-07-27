using Microsoft.VisualStudio.TestTools.UnitTesting;
using OneHub360.CMS.DAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneHub360.CMS.DAL.Tests
{
    [TestClass()]
    public class ExternalOrganizationsRepositoryTests
    {
        [TestMethod()]
        public void OneHub()
        {
            using (var context = new CMSContext(true))
            {
                var externalOrganizationsRepository = new ExternalOrganizationsRepository(context);

                externalOrganizationsRepository.Insert(new ExternalOrganizations
                {
                    CreatedBy = "ashokr",
                    CreationDate = DateTime.Now,
                    G2GSiteID = 1,
                    IsActive = true,
                    IsDeleted = false,
                    IsG2G = true,
                    Language = DB.Language.AR,
                    LastModified = DateTime.Now,
                    Title = "شركة OneHub360",
                    Logo =
                    File.ReadAllBytes(
                        string.Format("{0}{1}",
                        AppDomain.CurrentDomain.BaseDirectory,
                        @"\img\onehub.png"))
                });

                context.Transaction.Commit();

            }
        }


        [TestMethod()]
        public void FillExternalOrganizations()
        {
            using (var context = new CMSContext(true))
            {

                var externalOrganizationsRepository = new ExternalOrganizationsRepository(context);

                externalOrganizationsRepository.Insert(new ExternalOrganizations
                {
                    CreatedBy = "ashokr",
                    CreationDate = DateTime.Now,
                    G2GSiteID = 1,
                    IsActive = true,
                    IsDeleted = false,
                    IsG2G = true,
                    Language = DB.Language.AR,
                    LastModified = DateTime.Now,
                    Title = "وزارة الدفاع",
                    Logo =
                    File.ReadAllBytes(
                        string.Format("{0}{1}",
                        AppDomain.CurrentDomain.BaseDirectory,
                        @"\img\MODKW.jpg"))
                });

                externalOrganizationsRepository.Insert(new ExternalOrganizations
                {
                    CreatedBy = "ashokr",
                    CreationDate = DateTime.Now,
                    G2GSiteID = 2,
                    IsActive = true,
                    IsDeleted = false,
                    IsG2G = true,
                    Language = DB.Language.AR,
                    LastModified = DateTime.Now,
                    Title = "وزارة الصحة",
                    Logo =
                    File.ReadAllBytes(
                        string.Format("{0}{1}",
                        AppDomain.CurrentDomain.BaseDirectory,
                        @"\img\MOHKW.jpg"))
                });

                externalOrganizationsRepository.Insert(new ExternalOrganizations
                {
                    CreatedBy = "ashokr",
                    CreationDate = DateTime.Now,
                    G2GSiteID = 3,
                    IsActive = true,
                    IsDeleted = false,
                    IsG2G = true,
                    Language = DB.Language.AR,
                    LastModified = DateTime.Now,
                    Title = "وزارة العدل",
                    Logo =
                    File.ReadAllBytes(
                        string.Format("{0}{1}",
                        AppDomain.CurrentDomain.BaseDirectory,
                        @"\img\MOJKW.jpg"))
                });

                externalOrganizationsRepository.Insert(new ExternalOrganizations
                {
                    CreatedBy = "ashokr",
                    CreationDate = DateTime.Now,
                    IsActive = true,
                    IsDeleted = false,
                    IsG2G = false,
                    Language = DB.Language.AR,
                    LastModified = DateTime.Now,
                    Title = "السفارة الإيطالية"
                });

                context.Transaction.Commit();
            }
        }

        [TestMethod()]
        public void NumberAutoComplete()
        {
            using (var context = new CMSContext())
            {
                var repository = new OutgoingLetterNumberAutoCompeleteRepository(context);

                var result = repository.GetAll();
            }
        }

        [TestMethod()]
        public void Demo()
        {
            using (var cms = new CMSContext(true))
            {
                var externalOrgRepository = new ExternalOrganizationsRepository(cms);

                var externalUnit = externalOrgRepository.GetById(Guid.Parse("013D0A17-7DC0-40B3-819A-A693000F2BFC"));

                externalUnit.Logo = File.ReadAllBytes(@"C:\Users\administrator.ONEHUB360\Desktop\Profile\mohamedabd.jpg");

                externalOrgRepository.Update(externalUnit);

                cms.Transaction.Commit();
            }
        }
    }
}