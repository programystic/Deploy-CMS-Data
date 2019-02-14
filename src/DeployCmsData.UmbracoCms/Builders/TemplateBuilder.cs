using DeployCmsData.UmbracoCms.Constants;
using DeployCmsData.UmbracoCms.Interfaces;
using DeployCmsData.UmbracoCms.Services;
using System.Web;
using Umbraco.Core.Models;
using Umbraco.Core.Services;
using Umbraco.Web;
using Validation;

namespace DeployCmsData.UmbracoCms.Builders
{
    public class TemplateBuilder
    {
        private readonly string _alias;
        private string _masterTemplateAlias;
        private IFileService _fileService;
        private IUmbracoFactory _umbracoFactory;
        private IHttpServerUtility _httpServerUtility;

        public TemplateBuilder(string alias)
        {
            var applicationContext = UmbracoContext.Current.Application;
            _umbracoFactory = new UmbracoFactory(applicationContext.Services.ContentTypeService);
            _fileService = applicationContext.Services.FileService;
            _httpServerUtility = new Server();

            _alias = alias;
        }

        public TemplateBuilder(IFileService fileService, IUmbracoFactory umbracoFactory, IHttpServerUtility httpServerUtility, string alias)
        {
            _umbracoFactory = umbracoFactory;
            _fileService = fileService;
            _httpServerUtility = httpServerUtility;

            _alias = alias;
        }

        public TemplateBuilder WithMasterTemplate(string masterTemplateAlias)
        {
            _masterTemplateAlias = masterTemplateAlias;
            return this;
        }

        public ITemplate Build()
        {
            return GetTemplate(_alias, _masterTemplateAlias);
        }

        private ITemplate GetTemplate(string alias, string masterAlias)
        {
            var newTemplate = false;
            var newMasterTemplate = false;

            var template = _fileService.GetTemplate(alias);
            if (template == null)
            {
                template = _umbracoFactory.NewTemplate(alias, alias);
                template.Content = _httpServerUtility.ReadAllText($"~/Views/{alias}.cshtml");
                newTemplate = true;
            }

            newMasterTemplate = SetMasterTemplate(masterAlias, template);

            if (newTemplate || newMasterTemplate)
            {
                _fileService.SaveTemplate(template);
            }

            return template;
        }

        private bool SetMasterTemplate(string masterAlias, ITemplate template)
        {
            if (!string.IsNullOrEmpty(masterAlias) && template.MasterTemplateAlias != masterAlias)
            {
                var masterTemplate = _fileService.GetTemplate(masterAlias);
                Verify.Operation(masterTemplate != null, ExceptionMessages.TemplateNotFound);

                template.SetMasterTemplate(masterTemplate);
                return true;
            }

            return false;
        }
    }
}