using System;

namespace DeployCmsData.UmbracoCms.Builders
{
    public class PropertyBuilder
    {
        public string AliasValue;
        public string NameValue;
        public string TabValue;
        public Guid DataTypeValue;
        public string DescriptionValue;
        public string RegularExpressionValue;
        public bool MandatoryValue;
        public string PropertyEditorAliasValue;

        public PropertyBuilder(string alias)
        {
            AliasValue = alias;
        }

        public PropertyBuilder DataType(Guid dataType)
        {
            DataTypeValue = dataType;
            return this;
        }

        public PropertyBuilder PropertyEditorAlias(string propertyEditorAlias)
        {
            PropertyEditorAliasValue = propertyEditorAlias;
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
