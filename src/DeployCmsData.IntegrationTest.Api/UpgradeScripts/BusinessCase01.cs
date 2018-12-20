using System;
using DeployCmsData.Core.Interfaces;
using DeployCmsData.UmbracoCms.Builders;
using DeployCmsData.UmbracoCms.Constants;
using DeployCmsData.UmbracoCms.Services;

namespace DeployCmsData.IntegrationTest.Api.UpgradeScripts
{
    public class BusinessCase01 : UmbracoUpgradeScript
    {
        public Guid BikePickerId = new Guid("{77F278EB-9D34-42E4-8C3B-7444BEC29A11}");

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
                .Name("bike Model Page")
                .Icon(Icons.Document)
                .BuildAtRoot();
        }

        private void NewSubTypeBikeSelector()
        {
            var builder = new MultiNodeTreePickerBuilder(BikePickerId);

            builder
                .Name("Bike Picker Name")
                .AllowItemsOfType("BikeModelPage")
                .ShowOpenButton()
                .StartNodeXPath("$root/BikeListingPage")
                .Build();
        }

        private void NewTabsForBikeModelPage()
        {
            var builder = new DocumentTypeBuilder();
            builder.Alias("bikeModelPage");

            ModelLevelSpecificationTab(builder);
            SubModuleDefinitionTab(builder);

            builder.Build();
        }

        private void ModelLevelSpecificationTab(DocumentTypeBuilder builder)
        {
            const string tabName = "Model Level Specification";

            builder.AddField("isLeadingModel")
                .Description("Check if this is main model for submodels")
                .DataType(DataType.Checkbox)
                .Tab(tabName);

            builder.AddField("subTypes")
                .DataType(BikePickerId)
                .Tab("Model Level Specification");
        }

        private void SubModuleDefinitionTab(DocumentTypeBuilder builder)
        {
            const string tabName = "Sub Model Definition";

            builder.AddField("bikeImage")
                .Description("Select image to be displayed on a card")
                .DataType(DataType.MediaPicker)
                .Tab(tabName);

            builder.AddField("overrideFromLabel")
                .Tab(tabName);

            builder.AddField("overrideSpecificationLabel")
                .Tab(tabName);

            builder.AddField("specificationContent")
                .DataType(DataType.Richtexteditor)
                .Tab(tabName);

            builder.AddField("propertyDefaultTest");
        }
    }
}
