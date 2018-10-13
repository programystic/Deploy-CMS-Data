//using System;
//using DeployCmsData.Constants;
//using Umbraco.Core.Services;

//namespace DeployCmsData.Services
//{
//    public sealed class PropertyTypeBuilder
//    {
//        private readonly IContentTypeService _contentTypeService;
//        private string _docTypeAlias;

//        public PropertyTypeBuilder(IContentTypeService contentTypeService)
//        {
//            _contentTypeService = contentTypeService;
//        }

//        public PropertyTypeBuilder DocTypeAlias(string docTypeAlias)
//        {
//            _docTypeAlias = docTypeAlias;
//            return this;
//        }
        
//        public void CreateNewProperty(
//            string alias, 
//            string documentTypeAlias, 
//            Guid propertyTypeId, 
//            string name,
//            string description,
//            string validationExpression)
//        {
//            var documentType = _contentTypeService.GetContentType(documentTypeAlias);
//            if (documentType == null)
//            {
//                throw new ArgumentException(ExceptionMessages.DocumentTypeNotFound);
//            }

//            if (documentType.PropertyTypeExists(alias))
//            {
//                throw new ArgumentException(ExceptionMessages.PropertyAliasAlreadyExists);
//            }
//        }
//    }
//}
