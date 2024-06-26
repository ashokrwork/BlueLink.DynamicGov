// <copyright file="TemplateRepositoryTestsTest.cs">Copyright ©  2016</copyright>

using System;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OneHub360.CMS.DAL.Tests;

namespace OneHub360.CMS.DAL.Tests.Tests
{
    [TestClass]
    [PexClass(typeof(TemplateRepositoryTests))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class TemplateRepositoryTestsTest
    {

        [PexMethod(MaxBranches = 20000)]
        public void UpdateTemplates([PexAssumeUnderTest]TemplateRepositoryTests target)
        {
            target.UpdateTemplates();
            // TODO: add assertions to method TemplateRepositoryTestsTest.UpdateTemplates(TemplateRepositoryTests)
        }
    }
}
