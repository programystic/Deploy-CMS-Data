//using System;
//using DeployCmsData.Constants;
//using DeployCmsData.Interfaces;
//using DeployCmsData.Services;
//using DeployCmsData.Test.Services;
//using Moq;
//using NUnit.Framework;
//using Umbraco.Core.Models;
//using Umbraco.Core.Models.EntityBase;
//using Umbraco.Core.Services;

//namespace DeployCmsData.Test.Tests
//{
//    [TestFixture]
//    public class DocumentTypeFolderTests
//    {
//        [Test]
//        public void DocumentTypeFolderCreateAtRoot()
//        {
//            const string name = "myFolder";
//            const int id = 102;

//            var builderSetup = new Services.DocumentTypeBuilderBuilder();

//            builderSetup
//                .CreateDocumentTypeFolderAtRoot(id, name);            

//            var container = builder
//                .Name(name)
//                .bu

//            builderSetup.UmbracoFactory.Verify(x => x.NewContainer(FolderValues.RootFolder, name, 1), Times.Once);
//            Assert.AreEqual(id, container.Id);
//        }

//        [Test]
//        public void DocumentTypeFolderCreateWithParent()
//        {
//            const string name = "myFolder";
//            const string parentName = "myParentFolder";
//            const int parentId = 101;
//            const int id = 102;

//            var newEntity = new Mock<IUmbracoEntity>();
//            newEntity.SetupAllProperties();
//            newEntity.Setup(x => x.Id).Returns(id);

//            var testBuilder = new Services.DocumentTypeBuilderBuilder();
//            var builder = testBuilder
//                .CreateDocumentTypeFolderAtRoot(id, name)
//                .Build();

//            var container = builder.CreateFolder(name, parentName, 1);

//            testBuilder.UmbracoFactory.Verify(x => x.NewContainer(parentId, name, 1), Times.Once);
//            Assert.AreEqual(id, container.Id);
//            Assert.AreEqual(id, container.ParentId, parentId);
//        }

//        [Test]
//        public void DocumentTypeFolderCreateWithInvalidParent()
//        {
//            //const string name = "myFolder";
//            //const string parentAlias = "myParentAlias";

//            //_contentTypeService.Setup(x => x.GetContentType(parentAlias)).Returns((IContentType)null);
//            //var builder = new CreateDocumentTypeBuilder(_contentTypeService.Object, _umbracoFactory.Object);

//            //Assert.Throws<ArgumentException>(() => builder.CreateFolder(name, parentAlias, 1));
//        }

//    }
//}