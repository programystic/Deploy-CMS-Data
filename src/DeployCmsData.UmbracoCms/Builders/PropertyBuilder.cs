using System;

namespace DeployCmsData.UmbracoCms.Builders
{
    public class PropertyBuilder
    {
        private string aliasValue;
        private string nameValue;
        private string tabValue;
        private Guid dataTypeValue;
        private string descriptionValue;
        private string regularExpressionValue;
        private bool mandatoryValue;
        private string propertyEditorAliasValue;

        public string AliasValue { get => aliasValue; set => aliasValue = value; }
        public string NameValue { get => nameValue; set => nameValue = value; }
        public string TabValue { get => tabValue; set => tabValue = value; }
        public Guid DataTypeValue { get => dataTypeValue; set => dataTypeValue = value; }
        public string DescriptionValue { get => descriptionValue; set => descriptionValue = value; }
        public string RegularExpressionValue { get => regularExpressionValue; set => regularExpressionValue = value; }
        public bool MandatoryValue { get => mandatoryValue; set => mandatoryValue = value; }
        public string PropertyEditorAliasValue { get => propertyEditorAliasValue; set => propertyEditorAliasValue = value; }

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
