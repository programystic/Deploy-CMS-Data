using System;

namespace DeployCmsData.UmbracoCms.Builders
{
    public class PropertyBuilder
    {
        public string AliasValue { get; set; }
        public string NameValue { get; set; }
        public string TabValue { get; set; }
        public Guid DataTypeValue { get; set; }
        public string DescriptionValue { get; set; }
        public string RegularExpressionValue { get; set; }
        public bool? MandatoryValue { get; set; }
        public string PropertyEditorAliasValue { get; set; }

        public PropertyBuilder()
        {
            AliasValue = null;
            NameValue = null;
            TabValue = null;
            DataTypeValue = Guid.Empty;
            DescriptionValue = null;
            RegularExpressionValue = null;
            MandatoryValue = null;
            PropertyEditorAliasValue = null;
        }
        public PropertyBuilder(string alias)
        {
            AliasValue = alias;
        }

        public PropertyBuilder DataType(Guid propertyDataType)
        {
            DataTypeValue = propertyDataType;
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
