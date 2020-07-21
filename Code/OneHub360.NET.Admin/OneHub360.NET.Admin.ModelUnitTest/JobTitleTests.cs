using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using NHibernate.Cfg;
using OneHub360.NET.Admin.Model;
using OneHub360.NET.Core.Model;
using System;

namespace OneHub360.NET.Admin.ModelUnitTest
{
    [TestClass()]
    public class JobTitleTests
    {
        [TestMethod()]
        public void InsertSampleDataUsers()
        {
            // Initialize session
            var sessionFactory = new Configuration().Configure().BuildSessionFactory();


            var ministerJobTitle = new JobTitle { Title = "وزير", CreatedBy = "demo\\mameen", Description = "Inserting for testing decription" };
            var undJobTitle = new JobTitle { Title = "وكيل", CreatedBy = "demo\\mameen" };
            var assUndJobTitle = new JobTitle { Title = "وكيل مساعد", CreatedBy = "demo\\mameen" };
            var managerJobTitle = new JobTitle { Title = "مدير إدارة", CreatedBy = "demo\\mameen" };
            var controlerJobTitle = new JobTitle { Title = "مراقب", CreatedBy = "demo\\mameen", Description = "Inserting for testing decription" };
            var sectionHeadJobTitle = new JobTitle { Title = "رئيس قسم", CreatedBy = "demo\\mameen" };
            var employeeJobTitle = new JobTitle { Title = "مطور نظم", CreatedBy = "demo\\mameen", Description = "Inserting for testing decription" };

            using (ISession sess = sessionFactory.OpenSession())
            using (ITransaction tx = sess.BeginTransaction())
            {
                sess.Save(ministerJobTitle);
                sess.Save(undJobTitle);
                sess.Save(assUndJobTitle);
                sess.Save(managerJobTitle);
                sess.Save(controlerJobTitle);
                sess.Save(sectionHeadJobTitle);
                sess.Save(employeeJobTitle);
                tx.Commit();
            }


            //jobTitleRepository.Insert(ministerJobTitle);
            //jobTitleRepository.Insert(undJobTitle);
            //jobTitleRepository.Insert(assUndJobTitle);
            //jobTitleRepository.Insert(managerJobTitle);
            //jobTitleRepository.Insert(controlerJobTitle);
            //jobTitleRepository.Insert(sectionHeadJobTitle);
            //jobTitleRepository.Insert(employeeJobTitle);

            // Insert sample employees
            // TODO: Change office phone to string
            //var minister = new UserInfo { About = "سمو معالي الوزير ", BirthDate = new Nullable<DateTime>(new DateTime(1975, 01, 01)), ArabicFullName = "الشيخ خالد الجراح الصباح", Email = "minister@demo.gov.kw", JobTitle = jobTitles[0], LatinFullName = "Shaikh Khaled Al Garah Al Sabah", LoginName = "mnd.mod.gov.kw", Mobile = "90084822", OrganizationUnit = ministerOrgUnit, CreatedBy = "demo\\mameen" };
            //userInfoRepository.Insert(minister);

            //var und = new UserInfo { About = "السيد وكيل الوزارة ", BirthDate = new Nullable<DateTime>(new DateTime(1975, 01, 01)), ArabicFullName = "الشيخ محمد الجراح الصباح", Email = "und@demo.gov.kw", JobTitle = jobTitles[1], LatinFullName = "Shaikh Mohammed Al Garah Al Sabah", LoginName = "und.mod.gov.kw", Mobile = "90084822", OrganizationUnit = undOrgUnit, CreatedBy = "demo\\mameen" };
            //userInfoRepository.Insert(und);

            //var assUnd = new UserInfo { About = "السيد الوكيل المساعد للشئون الإدارية و القانونية", BirthDate = new Nullable<DateTime>(new DateTime(1975, 01, 01)), ArabicFullName = "عبد الله علي الغانم", Email = "uga@demo.gov.kw", JobTitle = jobTitles[2], LatinFullName = "Mohammed Abdullah Al Ghanim", LoginName = "uga.mod.gov.kw", Mobile = "90084822", OrganizationUnit = assUndOrgUnit, CreatedBy = "demo\\mameen" };
            //userInfoRepository.Insert(assUnd);

            //var departmentManager = new UserInfo { About = "مدير إدارة مركز المعلومات الآلي", BirthDate = new Nullable<DateTime>(new DateTime(1975, 01, 01)), ArabicFullName = "أحمد فاروق البسيوني", Email = "ccd@demo.gov.kw", JobTitle = jobTitles[3], LatinFullName = "Ahmed Farook", LoginName = "ccd.mod.gov.kw", Mobile = "90084822", OrganizationUnit = departmentOrgUnit, CreatedBy = "demo\\mameen" };
            //userInfoRepository.Insert(departmentManager);

            //var controler = new UserInfo { About = "مراقب تطوير و صيانة النظم", BirthDate = new Nullable<DateTime>(new DateTime(1975, 01, 01)), ArabicFullName = "مجدي محمد أمين", Email = "mdc@demo.gov.kw", JobTitle = jobTitles[4], LatinFullName = "Magdy Ameed", LoginName = "mdc.mod.gov.kw", Mobile = "90084822", OrganizationUnit = controlerOrgUnit, CreatedBy = "demo\\mameen" };
            //userInfoRepository.Insert(controler);

            //var sectionHead = new UserInfo { About = "رئيس قسم مراقبة الجودة", BirthDate = new Nullable<DateTime>(new DateTime(1975, 01, 01)), ArabicFullName = "وائل مصطفي", Email = "qss@demo.gov.kw", JobTitle = jobTitles[5], LatinFullName = "Magdy Ameed", LoginName = "mdc.mod.gov.kw", Mobile = "90084822", OrganizationUnit = sectionOrgUnit, CreatedBy = "demo\\mameen" };
            //userInfoRepository.Insert(sectionHead);

            //var employee = new UserInfo { About = "مطور نظم", BirthDate = new Nullable<DateTime>(new DateTime(1975, 01, 01)), ArabicFullName = "احمد شكر", Email = "emp@demo.gov.kw", JobTitle = jobTitles[6], LatinFullName = "Ahmed Shokr", LoginName = "emp.mod.gov.kw", Mobile = "90084822", OrganizationUnit = sectionOrgUnit, CreatedBy = "demo\\mameen" };
            //userInfoRepository.Insert(employee);

        }
    }
}