using DeployCmsData.UmbracoCms.Constants;

namespace DeployCmsData.UmbracoCms.Builders
{
    public class PropertyBuilder
    {
        internal string AliasValue;
        internal string NameValue;
        internal string TabValue;
        internal string DataTypeValue;
        internal string DescriptionValue;
        internal string RegularExpressionValue;
        internal bool MandatoryValue;
        internal string PropertyEditorAliasValue;

        public PropertyBuilder DataType(CmsDataType dataTypeName)
        {
            DataTypeValue = dataTypeName.ToString();
            return this;
        }

        public PropertyBuilder DataType(string dataTypeName)
        {
            DataTypeValue = dataTypeName;
            return this;
        }

        public PropertyBuilder PropertyEditorAlias(string propertyEditorAlias)
        {
            PropertyEditorAliasValue = propertyEditorAlias;
            return this;
        }

        public PropertyBuilder Alias(string fieldAlias)
        {
            AliasValue = fieldAlias;
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
