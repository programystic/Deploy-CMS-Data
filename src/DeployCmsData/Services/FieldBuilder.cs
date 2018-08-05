using DeployCmsData.Constants;

namespace DeployCmsData.Services
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

        public FieldBuilder DataType(CmsDataType dataType)
        {
            DataTypeValue = dataType.ToString();
            return this;
        }

        public FieldBuilder DataType(string dataType)
        {
            DataTypeValue = dataType;
            return this;
        }

        public FieldBuilder Alias(string alias)
        {
            AliasValue = alias;
            return this;
        }

        public FieldBuilder Name(string name)
        {
            NameValue = name;
            return this;
        }

        public FieldBuilder Description(string description)
        {
            DescriptionValue = description;
            return this;
        }

        public FieldBuilder RegularExpression(string regularExpression)
        {
            RegularExpressionValue = regularExpression;
            return this;
        }

        public FieldBuilder Tab(string tab)
        {
            TabValue = tab;
            return this;
        }

        public FieldBuilder IsMandatory()
        {
            MandatoryValue = true;
            return this;
        }
    }
}
