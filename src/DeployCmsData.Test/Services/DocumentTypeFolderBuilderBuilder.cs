//using DeployCmsData.Interfaces;
//using DeployCmsData.Services;
//using Moq;
//using Umbraco.Core.Services;

//namespace DeployCmsData.Test.Services
//{
//    internal class DocumentTypeFolderBuilderSetup
//    {
//        private readonly DocumentTypeFolderBuilder _documentTypeFolderBuilder;
//        private readonly Mock<IContentTypeService> _contentTypeService;
//        public Mock<IUmbracoFactory> UmbracoFactory { get; }

//        public DocumentTypeFolderBuilderSetup()
//        {
//            UmbracoFactory = new Mock<IUmbracoFactory>();
//            _contentTypeService = new Mock<IContentTypeService>();
//            _documentTypeFolderBuilder = new DocumentTypeFolderBuilder(_contentTypeService.Object, UmbracoFactory.Object);
//        }
//    }
//}
