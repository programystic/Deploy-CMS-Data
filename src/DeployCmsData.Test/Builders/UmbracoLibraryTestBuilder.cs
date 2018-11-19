using DeployCmsData.UmbracoCms.Interfaces;
using DeployCmsData.UmbracoCms.Services;
using Moq;
using System.Collections.Generic;
using Umbraco.Core.Models;
using Umbraco.Core.Services;

namespace DeployCmsData.Test.Builders
{
    class UmbracoLibraryTestBuilder
    {
        UmbracoLibrary _umbracoLibrary;
        Mock<IUmbracoFactory> _umbracoFactory;
        public readonly Mock<IContentService> ContentService;

        public UmbracoLibraryTestBuilder()
        {
            var contentTypeService = new Mock<IContentTypeService>();
            ContentService = new Mock<IContentService>();
            _umbracoFactory = new Mock<IUmbracoFactory>();

            _umbracoLibrary = new UmbracoLibrary(contentTypeService.Object, ContentService.Object, _umbracoFactory.Object);
        }

        public UmbracoLibraryTestBuilder SetupRootContent(int childCount)
        {
            var root = new Mock<IContent>();
            var rootChildren = new List<IContent>();
            for (int i = 0; i < childCount; i++)
            {
                var childContent = new Mock<IContent>();
                rootChildren.Add(childContent.Object);
            }

            _umbracoFactory.Setup(x => x.GetChildren(root.Object)).Returns(rootChildren);            

            var rootContent = new List<IContent>();
            rootContent.Add(root.Object);

            ContentService.Setup(x => x.GetRootContent()).Returns(rootContent);

            return this;
        }

        public UmbracoLibrary Build()
        {            
            return _umbracoLibrary;
        }
    }
}
