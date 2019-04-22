using DeployCmsData.UmbracoCms.UnitTest.Builders;
using Moq;
using NUnit.Framework;
using System;
using Umbraco.Core.Models;

namespace DeployCmsData.UmbracoCms.UnitTest.Tests
{
    public static class Templates
    {
        [Test]
        public static void CreateTemplateWithNoMaster()
        {
            const string templateAlias = "Template1";
            const string masterTemplateAlias = "Master";

            var testBuilder = new TemplateTestBuilder();

            var templateBuilder = testBuilder
                .NewTemplate(templateAlias)
                .FindsTemplate(masterTemplateAlias)
                .Build();

            var template = templateBuilder
                .Build();

            Assert.IsNotNull(template);
            testBuilder.Template.Verify(x => x.SetMasterTemplate(It.IsAny<ITemplate>()), Times.Never);
            testBuilder.FileService.Verify(x => x.SaveTemplate(testBuilder.Template.Object, It.IsAny<int>()), Times.Once);
            testBuilder.UmbracoFactory.Verify(x => x.NewTemplate(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Test]
        public static void CreateTemplateWithMaster()
        {
            const string templateAlias = "Template1";
            const string masterTemplateAlias = "Master";

            var testBuilder = new TemplateTestBuilder();

            var templateBuilder = testBuilder
                .NewTemplate(templateAlias)
                .FindsTemplate(masterTemplateAlias)
                .Build();

            var template = templateBuilder
                .WithMasterTemplate(masterTemplateAlias)
                .Build();

            Assert.IsNotNull(template);
            testBuilder.Template.Verify(x => x.SetMasterTemplate(It.IsAny<ITemplate>()), Times.Once);
            testBuilder.FileService.Verify(x => x.SaveTemplate(testBuilder.Template.Object, It.IsAny<int>()), Times.Once);
            testBuilder.UmbracoFactory.Verify(x => x.NewTemplate(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Test]
        public static void CreateTemplateWithInvalidMaster()
        {
            const string templateAlias = "Template1";
            const string masterTemplateAlias = "Master";

            var testBuilder = new TemplateTestBuilder();

            var templateBuilder = testBuilder
                .NewTemplate(templateAlias)
                .Build();

            Assert.Throws<InvalidOperationException>(() =>
                templateBuilder
                    .WithMasterTemplate(masterTemplateAlias)
                    .Build());
        }

        [Test]
        public static void UpdateExistingTemplateNewMaster()
        {
            const string templateAlias = "Template1";
            const string masterTemplateAlias = "Master";

            var testBuilder = new TemplateTestBuilder();

            var templateBuilder = testBuilder
                .ExistingTemplate(templateAlias)
                .FindsTemplate(masterTemplateAlias)
                .Build();

            var template = templateBuilder
                .WithMasterTemplate(masterTemplateAlias)
                .Build();

            Assert.IsNotNull(template);
            testBuilder.Template.Verify(x => x.SetMasterTemplate(It.IsAny<ITemplate>()), Times.Once);
            testBuilder.FileService.Verify(x => x.SaveTemplate(testBuilder.Template.Object, It.IsAny<int>()), Times.Once);
            testBuilder.UmbracoFactory.Verify(x => x.NewTemplate(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Test]
        public static void UpdateExistingTemplateNoUpdates()
        {
            const string templateAlias = "Template1";
            const string masterTemplateAlias = "Master";

            var testBuilder = new TemplateTestBuilder();

            var templateBuilder = testBuilder
                .ExistingTemplateWithMaster(templateAlias, masterTemplateAlias)
                .FindsTemplate(masterTemplateAlias)
                .Build();

            var template = templateBuilder
                .WithMasterTemplate(masterTemplateAlias)
                .Build();

            Assert.IsNotNull(template);
            testBuilder.Template.Verify(x => x.SetMasterTemplate(It.IsAny<ITemplate>()), Times.Never);
            testBuilder.FileService.Verify(x => x.SaveTemplate(testBuilder.Template.Object, It.IsAny<int>()), Times.Never);
            testBuilder.UmbracoFactory.Verify(x => x.NewTemplate(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }
    }
}
