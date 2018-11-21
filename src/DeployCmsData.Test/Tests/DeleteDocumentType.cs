using System;
using DeployCmsData.Test.Builders;
using Moq;
using NUnit.Framework;
using Umbraco.Core.Models;

namespace DeployCmsData.Test.Tests
{
    [TestFixture]
    public class DeleteDocumentType
    {
        private const string Alias = "myAlias";

        [Test]
        public void DeleteDocumentTypeFailsWhenItDoesNotExist()
        {
            var builder = new DocumentTypeTestBuilder()
                .Build();

            Assert.Throws<ArgumentException>(
                () => builder.DeleteDocumentType(Alias));
        }

        [Test]
        public void DeleteExistingDocumentType()
        {
            var builder = new DocumentTypeTestBuilder()
                .ReturnsExistingContentType(Alias)
                .Build();

            builder.DeleteDocumentType(Alias);
        }

        [Test]
        public void DeleteAllDocumentTypes()
        {
            var builder = new UmbracoLibraryTestBuilder();

            var library = builder
                .Build();

            library.DeleteAllDocumentTypes();
        }

        [Test]
        public void DeleteAllContent()
        {
            const int childCount = 10;
            var builder = new UmbracoLibraryTestBuilder();

            var library = builder
                .SetupRootContent(childCount)
                .Build();

            library.DeleteAllContent();

            builder.ContentService.Verify(x => x.Delete(It.IsAny<IContent>(), 0), Times.Exactly(childCount + 1));
        }
    }
}