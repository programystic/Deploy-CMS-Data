using DeployCmsData.Constants;

namespace DeployCmsData.Builders
{
    public class FieldBuilder
    {
        internal string AliasValue;
        internal string NameValue;
        internal string TabValue;
        internal string DataTypeValue;
        internal string DescriptionValue;
        internal string RegularExpressionValue;
        internal bool MandatoryValue;

        public FieldBuilder DataType(CmsDataType dataTypeName)
        {
            DataTypeValue = dataTypeName.ToString();
            return this;
        }

        public FieldBuilder DataType(string dataTypeName)
        {
            DataTypeValue = dataTypeName;
            return this;
        }

        public FieldBuilder Alias(string fieldAlias)
        {
            AliasValue = fieldAlias;
            return this;
        }

        public FieldBuilder Name(string fieldName)
        {
            NameValue = fieldName;
            return this;
        }

        public FieldBuilder Description(string fieldDescription)
        {
            DescriptionValue = fieldDescription;
            return this;
        }

        public FieldBuilder RegularExpression(string fieldRegularExpression)
        {
            RegularExpressionValue = fieldRegularExpression;
            return this;
        }

        public FieldBuilder Tab(string fieldTab)
        {
            TabValue = fieldTab;
            return this;
        }

        public FieldBuilder IsMandatory()
        {
            MandatoryValue = true;
            return this;
        }
    }
}
