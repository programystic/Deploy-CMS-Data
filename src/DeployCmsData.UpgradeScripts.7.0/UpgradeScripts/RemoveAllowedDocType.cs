using DeployCmsData.Core.Attributes;
using DeployCmsData.Core.Interfaces;
using DeployCmsData.Umbraco7.Builders;
using DeployCmsData.Umbraco7.Constants;
using DeployCmsData.UpgradeScripts_7.Constants;

namespace DeployCmsData.UpgradeScripts_7.UpgradeScripts
{
    [DoNotAutoRun]
    public class RemoveAllowedDocType : IUpgradeScript
    {
        public bool RunScript()
        {
            new DocumentTypeBuilder("allowedChildNodeType1").BuildInFolder("Pages");
            new DocumentTypeBuilder("allowedChildNodeType2").BuildInFolder("Pages");
            new DocumentTypeBuilder("allowedChildNodeType3").BuildInFolder("Pages");

            var builder = new DocumentTypeBuilder("removeAllowedDocType");

            builder
                .Icon(Icons.Home)
                .AddComposition("pageMetaData")
                .AddComposition("contentBase")
                .AddComposition("navigationBase")
                .AddAllowedChildNodeType("allowedChildNodeType1")
                .AddAllowedChildNodeType("allowedChildNodeType2")
                .AddAllowedChildNodeType("allowedChildNodeType3");

            builder.AddField("mainContent")
                .DataTypeAlias(LocalDataTypes.Grid)
                .Tab("Content");

            builder.BuildInFolder("Pages");


            new DocumentTypeBuilder("removeAllowedDocType")
                .RemoveAllowedChildNodeType("allowedChildNodeType2")
                .Update();

            return true;
        }
    }
}
