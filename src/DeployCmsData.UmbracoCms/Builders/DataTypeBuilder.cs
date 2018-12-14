using System;
using Umbraco.Core.Services;

namespace DeployCmsData.UmbracoCms.Builders
{
    public sealed class DataTypeBuilder
    {
        public void DeleteDataTypeByName(string dataTypeName, IDataTypeService dataTypeService)
        {
            var dataType = dataTypeService.GetDataTypeDefinitionByName(dataTypeName);
            if (dataType != null)
            {
                dataTypeService.Delete(dataType);
            }
        }

        public void DeleteDataTypeById(Guid id, IDataTypeService dataTypeService)
        {
            var dataType = dataTypeService.GetDataTypeDefinitionById(id);
            if (dataType != null)
            {
                dataTypeService.Delete(dataType);
            }
        }
    }
}