using DeployCmsData.Core.Attributes;
using DeployCmsData.Core.Interfaces;
using DeployCmsData.Umbraco7.Builders;
using System.Linq;
using Validation;

namespace DeployCmsData.UpgradeScripts_7.UpgradeScripts
{
    [DoNotAutoRun]
    public class DocTypeInheritance : IUpgradeScript
    {
        public bool RunScript()
        {
            var builder1 = new DocumentTypeBuilder("websiteRoot");
            builder1.AddField("Title")
                .Tab("Website Root");
            builder1.Update();

            var builder2 = new DocumentTypeBuilder("website2");
            builder2.AddField("Title 2")
                .Tab("Web Site 2");
            builder2.BuildWithParent("websiteRoot");

            var builder3 = new DocumentTypeBuilder("website3");
            builder3.AddField("Title 3")
                .Tab("Web Site 3");
            builder3.BuildWithParent("website2");

            var docType = new DocumentTypeBuilder("inheritedDocTypeTest").
                BuildWithParent("website3");

            Verify.Operation(docType.CompositionAliases().Count() == 3, "We should have 3 composition aliases");

            return true;
        }
    }
}
