using System;
using Umbraco.Core.Services;

namespace DeployCmsData.UmbracoCms.Builders
{
    public static class DataTypeBuilder
    {
        public static void DeleteDataTypeByName(string dataTypeName, IDataTypeService dataTypeService)
        {
            if (dataTypeService == null)
            {
                throw new ArgumentNullException(nameof(dataTypeService));
            }

            var dataType = dataTypeService.GetDataTypeDefinitionByName(dataTypeName);
            if (dataType != null)
            {
                dataTypeService.Delete(dataType);
            }
        }

        public static void DeleteDataTypeById(Guid id, IDataTypeService dataTypeService)
        {
            if (dataTypeService == null)
            {
                throw new ArgumentNullException(nameof(dataTypeService));
            }

            var dataType = dataTypeService.GetDataTypeDefinitionById(id);
            if (dataType != null)
            {
                dataTypeService.Delete(dataType);
            }
        }
    }
}