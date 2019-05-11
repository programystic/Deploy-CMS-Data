using System;
using Umbraco.Core.Services;
using Validation;

namespace DeployCmsData.Umbraco7.Builders
{
    public class DataTypeBuilder
    {
        public string DataTypeAlias { get; private set; }
        public DataTypeBuilder(string dataTypeAlias)
        {
            DataTypeAlias = dataTypeAlias;
        }

        public void Build()
        {

        }

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