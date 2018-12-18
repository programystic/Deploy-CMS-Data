using System;
using DeployCmsData.Core.Interfaces;
using DeployCmsData.UmbracoCms.Builders;
using DeployCmsData.UmbracoCms.Constants;
using DeployCmsData.UmbracoCms.Services;

namespace DeployCmsData.IntegrationTest.Api.UpgradeScripts
{
    public class BusinessCase01 : UmbracoUpgradeScript
    {
        public const string BikePickerName = "SubType Bike Picker";

        public override bool RunScript(IUpgradeLogRepository upgradeLog)
        {
            CreateBikeModelPage();
            NewSubTypeBikeSelector();
            NewTabsForBikeModelPage();

            return true;
        }

        private void CreateBikeModelPage()
        {
            var builder = new DocumentTypeBuilder();
            builder
                .Alias("BikeModelPage")
                .Name("Bike Model Page")
                .Icon(Icons.Document)
                .BuildAtRoot();
        }

        private void NewSubTypeBikeSelector()
        {
            var id = Guid.Parse("{77F278EB-9D34-42E4-8C3B-7444BEC29A11}");
            var builder = new MultiNodeTreePickerBuilder(id);

            builder
                .Name(BikePickerName)
                .AllowItemsOfType("BikeModelPage")
                .ShowOpenButton()
                .StartNodeXPath("$root/BikeListingPage")
                .Build();
        }

        private void NewTabsForBikeModelPage()
        {
            var builder = new DocumentTypeBuilder();
            builder.Alias("BikeModelPage");

            ModelLevelSpecificationTab(builder);
            SubModuleDefinitionTab(builder);

            builder.Build();
        }

        private void ModelLevelSpecificationTab(DocumentTypeBuilder builder)
        {
            const string tabName = "Model Level Specification";

            builder.AddField()
                .Alias("isLeadingModel")
                .Name("Is leading model")
                .Description("Check if this is main model for submodels")
                .DataType(CmsDataType.Checkbox)
                .Tab(tabName);

            builder.AddField()
                .Alias("subTypes")
                .Name("Sub types")
                .DataType(BikePickerName)
                .Tab("Model Level Specification");
        }

        private void SubModuleDefinitionTab(DocumentTypeBuilder builder)
        {
            const string tabName = "Sub Model Definition";

            builder.AddField()
                .Alias("bikeImage")
                .Name("Bike Image")
                .Description("Select image to be displayed on a card")
                .DataType(CmsDataType.MediaPicker2)
                .Tab(tabName);

            builder.AddField()
                .Alias("overrideFromLabel")
                .Name("Override from label")
                .DataType(CmsDataType.TextString)
                .Tab(tabName);

            builder.AddField()
                .Alias("overrideSpecificationLabel")
                .Name("Override specification label")
                .DataType(CmsDataType.TextString)
                .Tab(tabName);

            builder.AddField()
                .Alias("specificationContent")
                .Name("Specification content")
                .DataType(CmsDataType.RichTextEditor)
                .Tab(tabName);
        }
    }
}
