using System;

namespace DeployCmsData.Umbraco7.Builders
{
    public class PropertyBuilder
    {
        public string AliasValue { get; set; }
        public string NameValue { get; set; }
        public string TabValue { get; set; }
        public string DataTypeAliasValue { get; set; }
        public Guid DataTypeIdValue { get; set; }

        public string DescriptionValue { get; set; }
        public string RegularExpressionValue { get; set; }
        public bool? MandatoryValue { get; set; }
        public string PropertyEditorAliasValue { get; set; }

        public PropertyBuilder()
        {
            AliasValue = null;
            NameValue = null;
            TabValue = null;
            DataTypeAliasValue = null;
            DescriptionValue = null;
            RegularExpressionValue = null;
            MandatoryValue = null;
            PropertyEditorAliasValue = null;
        }
        public PropertyBuilder(string alias)
        {
            AliasValue = alias;
        }

        public PropertyBuilder DataTypeAlias(string alias)
        {
            DataTypeAliasValue = alias;
            DataTypeIdValue = Guid.Empty;
            return this;
        }

        public PropertyBuilder DataTypeId(string id)
        {
            DataTypeIdValue = new Guid(id);
            DataTypeAliasValue = null;
            return this;
        }

        public PropertyBuilder DataTypeId(Guid id)
        {
            DataTypeIdValue = id;
            DataTypeAliasValue = null;

            return this;
        }

        public PropertyBuilder PropertyEditorAlias(string alias)
        {
            PropertyEditorAliasValue = alias;
            return this;
        }

        public PropertyBuilder Name(string fieldName)
        {
            NameValue = fieldName;
            return this;
        }

        public PropertyBuilder Description(string fieldDescription)
        {
            DescriptionValue = fieldDescription;
            return this;
        }

        public PropertyBuilder RegularExpression(string fieldRegularExpression)
        {
            RegularExpressionValue = fieldRegularExpression;
            return this;
        }

        public PropertyBuilder Tab(string fieldTab)
        {
            TabValue = fieldTab;
            return this;
        }

        public PropertyBuilder IsMandatory()
        {
            MandatoryValue = true;
            return this;
        }
    }
}
