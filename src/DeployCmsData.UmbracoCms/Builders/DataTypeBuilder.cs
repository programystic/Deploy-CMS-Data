using System;
using Umbraco.Core.Services;
using Validation;

namespace DeployCmsData.UmbracoCms.Builders
{
    public static class DataTypeBuilder
    {
        public static void DeleteDataTypeByName(string dataTypeName, IDataTypeService dataTypeService)
        {
            Requires.NotNull(dataTypeService, nameof(dataTypeService));

            var dataType = dataTypeService.GetDataTypeDefinitionByName(dataTypeName);
            if (dataType != null)
            {
                dataTypeService.Delete(dataType);
            }
        }

        public static void DeleteDataTypeById(Guid id, IDataTypeService dataTypeService)
        {
            Requires.NotNull(dataTypeService, nameof(dataTypeService));

            var dataType = dataTypeService.GetDataTypeDefinitionById(id);
            if (dataType != null)
            {
                dataTypeService.Delete(dataType);
            }
        }
    }
}