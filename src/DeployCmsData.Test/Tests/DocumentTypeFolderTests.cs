using System;
using DeployCmsData.Constants;
using DeployCmsData.Interfaces;
using DeployCmsData.Services;
using DeployCmsData.Test.Services;
using Moq;
using NUnit.Framework;
using Umbraco.Core.Models;
using Umbraco.Core.Models.EntityBase;
using Umbraco.Core.Services;


namespace DeployCmsData.Test.Tests
{
    [TestFixture]
    public class DocumentTypeFolderTests
    {
        private Mock<IUmbracoFactory> _umbracoFactory;
        private Mock<IContentTypeService> _contentTypeService;

        [SetUp]
        public void Initialise()
        {
            _umbracoFactory = new Mock<IUmbracoFactory>();
            _contentTypeService = new Mock<IContentTypeService>();
        }

        [Test]
        public void DocumentTypeFolderCreateAtRoot()
        {
            const string name = "myFolder";
            const int id = 102;

            var testBuilder = new DocumentTypeTestBuilder();

            var builder = testBuilder
                .CreateDocumentTypeFolderAtRoot(id, name)
                .Build();

            var container = builder.CreateFolderAtRoot(name);

            testBuilder.UmbracoFactory.Verify(x => x.NewContainer(CmsContentValues.RootFolder, name, 1), Times.Once);
            Assert.AreEqual(id, container.Id);
        }

        [Test]
        public void DocumentTypeFolderCreate()
        {
            const string name = "myFolder";
            const string parentName = "myParentFolder";
            const int parentId = 101;
            const int id = 102;

            var newEntity = new Mock<IUmbracoEntity>();
            newEntity.SetupAllProperties();
            newEntity.Setup(x => x.Id).Returns(id);

            var testBuilder = new DocumentTypeTestBuilder();
            testBuilder.CreateDocumentTypeFolderAtRoot(id, name);            

            // TODO: Fix this test

            //var builder = new DocumentTypeBuilder(_contentTypeService.Object, _umbracoFactory.Object);
            //var container = builder.CreateFolder(name, parentName, 1);

            //testBuilder.UmbracoFactory.Verify(x => x.NewContainer(parentId, name, 1), Times.Once);
            //Assert.AreEqual(id, container.Id);
        }

        [Test]
        public void DocumentTypeFolderCreateWithInvalidParent()
        {
            const string name = "myFolder";
            const string parentAlias = "myParentAlias";

            _contentTypeService.Setup(x => x.GetContentType(parentAlias)).Returns((IContentType)null);
            var builder = new CreateDocumentTypeBuilder(_contentTypeService.Object, _umbracoFactory.Object);

            Assert.Throws<ArgumentException>(() => builder.CreateFolder(name, parentAlias, 1));
        }

    }
}
