using System.Collections.Generic;
using Umbraco.Core.Models;

namespace DeployCmsData.Services
{
    public class FieldBuilder
    {
        private string _alias;
        private string _tab;
        private string _type;
        private string _help;
        private string _description;
        private readonly DocumentTypeBuilder _documentTypeBuilder;

        public FieldBuilder(string alias, string tab, string type, DocumentTypeBuilder documentTypeBuilder)
        {
            _alias = alias;
            _tab = tab;
            _type = type;
            _documentTypeBuilder = documentTypeBuilder;
            _documentTypeBuilder.FieldList.Add(this);
        }

        public FieldBuilder FieldDescription(string description)
        {
            _description = description;
            return this;
        }

        public FieldBuilder FieldHelp(string help)
        {
            _help = help;
            return this;
        }

        public FieldBuilder AddField(string alias, string tab, string type)
        {
            return new FieldBuilder(alias, tab, type, _documentTypeBuilder);
        }

        public FieldBuilder UpdateField(string alias, string tab, string type)
        {
            return new FieldBuilder(alias, tab, type, _documentTypeBuilder);
        }

        public IContentType Build()
        {
            return _documentTypeBuilder.Build();
        }
    }
}
