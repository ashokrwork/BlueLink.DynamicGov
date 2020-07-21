using Microsoft.VisualStudio.TestTools.UnitTesting;
using OneHub360.App.DAL;
using OneHub360.App.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneHub360.App.DAL.Tests
{
    //[TestClass()]
    //public class UserInfoRepositoryTests
    //{
    //    [TestMethod()]
    //    public void TestTest()
    //    {
    //        var img = File.ReadAllBytes(@"C:\Y\s.png");

    //        var context = new OneHubContext(true);

    //        var userInfoRepository = new UserInfoRepository(context);

    //        var userInfo = new UserInfos {
    //            Id = Guid.NewGuid(),
    //            About = "سكرتيرة",
    //            ArabicFullName = "مني أحمد",
    //            CreatedBy = "E0050903-2B1A-4DBB-A5AC-0015F36C0F25",
    //            CreationDate = DateTime.Now,
    //            Domain = "onehub",
    //            IsDeleted = false,
    //            LoginName = "mona",
    //            Language = DB.Language.AR,
    //            JobTitles = new JobTitles { Id = Guid.Parse("e0081416-1f11-48b7-b596-a05e14cd843c") },
    //            LatinFullName = "Mona Ahmed",
    //            Password = "1234",
    //            PasswordSalt = "sss",
    //            Mobile = "90084822",
    //            Email = "mona@onehub.com",
    //            OrganizationUnits = new OrganizationUnits { Id=Guid.Parse("E0050903-2B1A-44BB-9BC6-7A3D44C3A788") },
    //            LastModified = DateTime.Now,
    //            Photo = img,
    //            Username = "mona"
    //        };

    //        userInfoRepository.Insert(userInfo);

    //        context.Transaction.Commit();
    //        context.Dispose();
    //    }

    //    [TestMethod()]
    //    public void EblaDemo()
    //    {
            
    //        var context = new OneHubContext(true);

    //        var userRepository = new UserInfoRepository(context);
    //        // EBLA
    //        var hilal = userRepository.GetById(Guid.Parse("E0050903-2B1A-4DBB-A5AC-0015F36C0F25"));
    //        hilal.Photo = File.ReadAllBytes(@"C:\Users\ashokr.CCMOF\Desktop\profile\Hilal.jpg");
    //        hilal.About = "المدير العام";
    //        hilal.ArabicFullName = "هلال أرناؤوط";
    //        hilal.LoginName = "hilal";
    //        hilal.Username = "hilal";
    //        hilal.Mobile = "";
    //        //hilal.OrganizationUnits = new OrganizationUnits { Id = Guid.Parse("E005020C-0402-4386-9D72-4FE5ADF69516") };
    //        userRepository.Update(hilal);

    //        var farah = userRepository.GetById(Guid.Parse("A65B95F3-C45D-47A9-9AE9-1256AD598EBA"));
    //        farah.Photo = File.ReadAllBytes(@"C:\Users\ashokr.CCMOF\Desktop\profile\Farah.jpg");
    //        farah.About = "مدير عملاء";
    //        farah.ArabicFullName = "فرح أرناؤوط";
    //        farah.LoginName = "farah";
    //        farah.Username = "farah";
    //        farah.Mobile = "96599610706";
    //        //farah.OrganizationUnits = new OrganizationUnits { Id = Guid.Parse("E005020C-0402-4386-9D72-4FE5ADF69516") };
    //        userRepository.Update(farah);

    //        var nabil = userRepository.GetById(Guid.Parse("E005020C-050D-4126-B7A7-687DD409F384"));
    //        nabil.Photo = File.ReadAllBytes(@"C:\Users\ashokr.CCMOF\Desktop\profile\nabil.jpg");
    //        nabil.About = "مدير إقليمي";
    //        nabil.ArabicFullName = "نبيل نوصيبا";
    //        nabil.LoginName = "nabil";
    //        nabil.Username = "nabil";
    //        nabil.Mobile = "97455863297";
    //        //nabil.OrganizationUnits = new OrganizationUnits { Id = Guid.Parse("E005020C-0402-4386-9D72-4FE5ADF69516") };
    //        userRepository.Update(nabil);

    //        var saad = userRepository.GetById(Guid.Parse("E005080A-3430-4C78-A57B-F296D1AF4DCC"));
    //        saad.Photo = File.ReadAllBytes(@"C:\Users\ashokr.CCMOF\Desktop\profile\saad.jpg");
    //        saad.About = "مدير بيع القطاع الحكومي";
    //        saad.ArabicFullName = "سعد سميرة";
    //        saad.LoginName = "saad";
    //        saad.Username = "saad";
    //        saad.Mobile = "96594999818";
    //        //saad. = new OrganizationUnits { Id = Guid.Parse("E005020C-0402-4386-9D72-4FE5ADF69516") };
    //        userRepository.Update(saad);

    //        context.Transaction.Commit();

    //        context.Dispose();
    //    }

    //    [TestMethod()]
    //    public void MOCIDemo()
    //    {

    //        var context = new OneHubContext(true);

    //        var userRepository = new UserInfoRepository(context);
           
    //        var yousif = userRepository.GetById(Guid.Parse("E0050903-2B1A-4DBB-A5AC-0015F36C0F25"));
    //        yousif.Photo = File.ReadAllBytes(@"C:\Users\administrator.ONEHUB360\Desktop\Profile\minister.png");
    //        yousif.About = "وزير التجارة و الصناعة";
    //        yousif.ArabicFullName = "خالد الرضوان";
    //        yousif.LoginName = "minister";
    //        yousif.Username = "minister";
    //        yousif.Mobile = "96590084822";
    //        //hilal.OrganizationUnits = new OrganizationUnits { Id = Guid.Parse("E005020C-0402-4386-9D72-4FE5ADF69516") };
    //        userRepository.Update(yousif);

    //        context.Transaction.Commit();

    //        context.Dispose();

    //        return;

    //        var khalid = userRepository.GetById(Guid.Parse("A65B95F3-C45D-47A9-9AE9-1256AD598EBA"));
    //        khalid.Photo = File.ReadAllBytes(@"C:\Users\ashokr.CCMOF\Desktop\profile\khalid.png");
    //        khalid.About = "وكيل وزارة التجارة و الصناعة";
    //        khalid.ArabicFullName = "خالد الشمالي";
    //        khalid.LoginName = "khalid";
    //        khalid.Username = "khalid";
    //        khalid.Mobile = "96590084822";
    //        //farah.OrganizationUnits = new OrganizationUnits { Id = Guid.Parse("E005020C-0402-4386-9D72-4FE5ADF69516") };
    //        userRepository.Update(khalid);

    //        var abdalziz = userRepository.GetById(Guid.Parse("E005020C-050D-4126-B7A7-687DD409F384"));
    //        abdalziz.Photo = File.ReadAllBytes(@"C:\Users\ashokr.CCMOF\Desktop\profile\nopic.png");
    //        abdalziz.About = "مدير مكتب وكيل الوزارة";
    //        abdalziz.ArabicFullName = "عبد العزيز العازمي";
    //        abdalziz.LoginName = "aziz";
    //        abdalziz.Username = "aziz";
    //        abdalziz.Mobile = "96590084822";
    //        //nabil.OrganizationUnits = new OrganizationUnits { Id = Guid.Parse("E005020C-0402-4386-9D72-4FE5ADF69516") };
    //        userRepository.Update(abdalziz);

    //        var nimer = userRepository.GetById(Guid.Parse("E005080A-3430-4C78-A57B-F296D1AF4DCC"));
    //        nimer.Photo = File.ReadAllBytes(@"C:\Users\ashokr.CCMOF\Desktop\profile\nemer.jpg");
    //        nimer.About = "الوكيل المساعد لشئون التجارة الخارجية";
    //        nimer.ArabicFullName = "الشيخ نمر فهد الصباح";
    //        nimer.LoginName = "nimer";
    //        nimer.Username = "nimer";
    //        nimer.Mobile = "96590084822";
    //        //saad. = new OrganizationUnits { Id = Guid.Parse("E005020C-0402-4386-9D72-4FE5ADF69516") };
    //        userRepository.Update(nimer);

            
    //    }

    //    [TestMethod()]
    //    public void Demo()
    //    {

    //        var context = new OneHubContext(true);

    //        var userRepository = new UserInfoRepository(context);


    //        var mohamed = userRepository.GetById(Guid.Parse("A65B95F3-C45D-47A9-9AE9-1256AD598EBA"));
    //        mohamed.Photo = File.ReadAllBytes(@"C:\Users\administrator.ONEHUB360\Desktop\Profile\tura1.png");
    //        mohamed.About = "الوكيل المساعد لشؤون نظم المعلومات";
    //        mohamed.ArabicFullName = "محمد التورة";
    //        mohamed.LoginName = "mohamed";
    //        mohamed.Username = "mohamed";
    //        mohamed.Mobile = "96590084822";
    //        //farah.OrganizationUnits = new OrganizationUnits { Id = Guid.Parse("E005020C-0402-4386-9D72-4FE5ADF69516") };
    //        userRepository.Update(mohamed);

    //        context.Transaction.Commit();

    //        context.Dispose();

    //        return;

    //        var salem = userRepository.GetById(Guid.Parse("E0050903-2B1A-4DBB-A5AC-0015F36C0F25"));
    //        salem.Photo = File.ReadAllBytes(@"C:\Users\administrator.ONEHUB360\Desktop\Profile\salem.jpg");
    //        salem.About = "رئيس مجلس ادارة هيئة الاتصالات";
    //        salem.ArabicFullName = "سالم الاذينة";
    //        salem.LoginName = "salem";
    //        salem.Username = "salem";
    //        salem.Mobile = "96590084822";
    //        //hilal.OrganizationUnits = new OrganizationUnits { Id = Guid.Parse("E005020C-0402-4386-9D72-4FE5ADF69516") };
    //        userRepository.Update(salem);

            

            

            

    //        var abdalziz = userRepository.GetById(Guid.Parse("E005020C-050D-4126-B7A7-687DD409F384"));
    //        abdalziz.Photo = File.ReadAllBytes(@"C:\Users\administrator.ONEHUB360\Desktop\Profile\nopic.png");
    //        abdalziz.About = "مدير مكتب رئيس مجلس ادارة هيئة الاتصالات";
    //        abdalziz.ArabicFullName = "عبد العزيز العازمي";
    //        abdalziz.LoginName = "aziz";
    //        abdalziz.Username = "aziz";
    //        abdalziz.Mobile = "96590084822";
    //        //nabil.OrganizationUnits = new OrganizationUnits { Id = Guid.Parse("E005020C-0402-4386-9D72-4FE5ADF69516") };
    //        userRepository.Update(abdalziz);

    //        var haya = userRepository.GetById(Guid.Parse("E005080A-3430-4C78-A57B-F296D1AF4DCC"));
    //        haya.Photo = File.ReadAllBytes(@"C:\Users\administrator.ONEHUB360\Desktop\Profile\nopic.png");
    //        haya.About = "الوكيل المساعد للشؤون المالية والادارية";
    //        haya.ArabicFullName = "هيا الودعاني";
    //        haya.LoginName = "haya";
    //        haya.Username = "haya";
    //        haya.Mobile = "96590084822";
    //        //saad. = new OrganizationUnits { Id = Guid.Parse("E005020C-0402-4386-9D72-4FE5ADF69516") };
    //        userRepository.Update(haya);

    //        var iqbal = new UserInfos()
    //        {
    //            Id = Guid.NewGuid(),
    //            Photo = File.ReadAllBytes(@"C:\Users\administrator.ONEHUB360\Desktop\Profile\nopic.png"),
    //            About = "مدير قسم تكنولوجيا المعلومات",
    //            ArabicFullName = "إقبال العوضي",
    //            LoginName = "iqbal",
    //            Username = "iqbal",
    //            Mobile = "96590084822",
    //            CreatedBy = "E0050903-2B1A-4DBB-A5AC-0015F36C0F25",
    //            CreationDate = DateTime.Now,
    //            Domain = "onehub",
    //            IsDeleted = false,
    //            Language = DB.Language.AR,
    //            JobTitles = new JobTitles { Id = Guid.Parse("e0081416-1f11-48b7-b596-a05e14cd843c") },
    //            LatinFullName = "Iqbal Al Awady",
    //            Password = "1234",
    //            PasswordSalt = "sss",
    //            Email = "iqbal@onehub.com",
    //            OrganizationUnits = new OrganizationUnits { Id = Guid.Parse("E0050903-2B1A-44BB-9BC6-7A3D44C3A788") },
    //            LastModified = DateTime.Now
    //        };

    //        userRepository.Insert(iqbal);

    //        var yousif = new UserInfos()
    //        {
    //            Id = Guid.NewGuid(),
    //            Photo = File.ReadAllBytes(@"C:\Users\administrator.ONEHUB360\Desktop\Profile\nopic.png"),
    //            About = "مدير قسم المشاريع الوطنية",
    //            ArabicFullName = "يوسف القطاني",
    //            LoginName = "yousif",
    //            Username = "yousif",
    //            Mobile = "96590084822",
    //            CreatedBy = "E0050903-2B1A-4DBB-A5AC-0015F36C0F25",
    //            CreationDate = DateTime.Now,
    //            Domain = "onehub",
    //            IsDeleted = false,
    //            Language = DB.Language.AR,
    //            JobTitles = new JobTitles { Id = Guid.Parse("e0081416-1f11-48b7-b596-a05e14cd843c") },
    //            LatinFullName = "Yousif Al Qatani",
    //            Password = "1234",
    //            PasswordSalt = "sss",
    //            Email = "yousif@onehub.com",
    //            OrganizationUnits = new OrganizationUnits { Id = Guid.Parse("E0050903-2B1A-44BB-9BC6-7A3D44C3A788") },
    //            LastModified = DateTime.Now
    //        };

    //        userRepository.Insert(yousif);

            
    //    }
    //}
}