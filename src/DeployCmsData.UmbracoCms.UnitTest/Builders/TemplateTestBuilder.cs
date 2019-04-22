using DeployCmsData.UmbracoCms.Builders;
using DeployCmsData.UmbracoCms.Interfaces;
using Moq;
using System.Web;
using Umbraco.Core.Models;
using Umbraco.Core.Services;

namespace DeployCmsData.UmbracoCms.UnitTest.Builders
{

    internal class TemplateTestBuilder
    {
        public Mock<ITemplate> Template;
        public Mock<IFileService> FileService;
        public Mock<IUmbracoFactory> UmbracoFactory;

        private Mock<IHttpServerUtility> _serverUtility;
        private TemplateBuilder _templateBuilder;
        private string _templateAlias;

        internal TemplateTestBuilder()
        {
            UmbracoFactory = new Mock<IUmbracoFactory>();
            FileService = new Mock<IFileService>();
            _serverUtility = new Mock<IHttpServerUtility>();
        }

        internal TemplateTestBuilder NewTemplate(string templateAlias)
        {
            Template = new Mock<ITemplate>();

            UmbracoFactory.Setup(x => x.NewTemplate(templateAlias, templateAlias)).Returns(Template.Object);
            _templateAlias = templateAlias;

            return this;
        }

        internal TemplateTestBuilder ExistingTemplate(string templateAlias)
        {
            Template = new Mock<ITemplate>();
            FileService.Setup(x => x.GetTemplate(templateAlias)).Returns(Template.Object);
            _templateAlias = templateAlias;

            return this;
        }

        internal TemplateTestBuilder ExistingTemplateWithMaster(string templateAlias, string masterTemplateAlias)
        {
            ExistingTemplate(templateAlias);
            Template.Setup(x => x.MasterTemplateAlias).Returns(masterTemplateAlias);

            return this;
        }

        internal TemplateTestBuilder FindsTemplate(string templateAlias)
        {
            var template = new Mock<ITemplate>();
            FileService.Setup(x => x.GetTemplate(templateAlias)).Returns(template.Object);

            return this;
        }

        internal TemplateBuilder Build()
        {
            _templateBuilder = new TemplateBuilder(FileService.Object, UmbracoFactory.Object, _serverUtility.Object, _templateAlias);
            return _templateBuilder;
        }
    }
}
