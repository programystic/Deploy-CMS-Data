using DeployCmsData.Test.Services;
using DeployCmsData.UmbracoCms.Services;
using Moq;
using NUnit.Framework;
using System;
using Umbraco.Core.Services;

namespace DeployCmsData.Test.Tests
{
    [TestFixture]
    class DeleteDocumentType
    {
        private const string Alias = "myAlias";

        [Test]
        public void DeleteDocumentTypeFailsWhenItDoesNotExist()
        {
            var builder = new DocumentTypeBuilderSetup()
                .Build();

            Assert.Throws<ArgumentException>(
                () => builder.DeleteDocumentType(Alias));
        }

        [Test]
        public void DeleteExistingDocumentType()
        {
            var builder = new DocumentTypeBuilderSetup()
                .ReturnsExistingContentType(Alias)
                .Build();

            builder.DeleteDocumentType(Alias);
        }

        //[Test]
        //public void EmptyListOfDocumentTypes()
        //{
        //    var contentTypeService = new Mock<IContentTypeService>();
        //    var cmsCollections = new CmsLibrary(contentTypeService.Object);

        //    var aliases = cmsCollections.GetAllDocumentTypeAliases();

        //    Assert.AreEqual(0, aliases.Count);
        //}

        //[Test]
        //public void GetListOfDocumentTypes()
        //{
        //    var contentTypeService = new Mock<IContentTypeService>();
        //    var cmsCollections = new CmsLibrary(contentTypeService.Object);

        //    var list = new List<string>();
        //    list.Add("alias1");
        //    list.Add("alias2");
        //    list.Add("alias3");

        //    contentTypeService.Setup(x => x.GetAllContentTypeAliases(It.IsAny<Guid>())).Returns(list);

        //    var aliases = cmsCollections.GetAllDocumentTypeAliases();

        //    Assert.AreEqual(3, aliases.Count);
        //}

        [Test]
        public void DeleteAllDocumentTypes()
        {
            var contentTypeService = new Mock<IContentTypeService>();
            var cmsCollections = new CmsLibrary(contentTypeService.Object);

            cmsCollections.DeleteAllDocumentTypes();
        }
    }
}