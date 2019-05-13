using DeployCmsData.Umbraco7.Models;
using DeployCmsData.Umbraco7.Services;
using System;
using System.Linq;
using Umbraco.Core.Models;
using Umbraco.Core.Services;
using Umbraco.Web;
using Validation;
using ProperyEditors = Umbraco.Core.Constants.PropertyEditors;


namespace DeployCmsData.Umbraco7.Builders
{
    public class DataTypeBuilder
    {
        public string DataTypeAlias { get; private set; }
        public string PropertyEditorAliasValue { get; private set; }
        public Guid KeyValue { get; private set; }
        private IDataTypeService DataTypeService { get; set; }

        public DataTypeBuilder(IDataTypeService dataTypeService, string dataTypeAlias)
        {
            Setup(dataTypeService, dataTypeAlias);
        }

        public DataTypeBuilder(string dataTypeAlias)
        {
            Setup(UmbracoContext.Current.Application.Services.DataTypeService, dataTypeAlias);
        }

        private void Setup(IDataTypeService dataTypeService, string dataTypeAlias)
        {
            DataTypeService = dataTypeService;
            DataTypeAlias = dataTypeAlias;
            PropertyEditorAliasValue = ProperyEditors.TextboxAlias;
            KeyValue = Guid.NewGuid();
        }

        public DataTypeBuilder PropertyEditorAlias(string alias)
        {
            PropertyEditorAliasValue = alias;
            return this;
        }

        public DataTypeBuilder Key(Guid key)
        {
            KeyValue = key;
            return this;
        }

        public void Build()
        {
            var dataType = DataTypeService.GetDataTypeDefinitionByName(DataTypeAlias);

            if (dataType == null)
            {
                dataType = CreateNewDataType();
            }
            else
            {
                dataType.PropertyEditorAlias = PropertyEditorAliasValue;
            }

            DataTypeService.Save(dataType);
        }

        private IDataTypeDefinition CreateNewDataType()
        {
            IDataTypeDefinition dataType = new DataTypeDefinition(-1, PropertyEditorAliasValue ?? ProperyEditors.TextboxAlias)
            {
                Name = DataTypeAlias
            };
            dataType.Key = KeyValue;
            return dataType;
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