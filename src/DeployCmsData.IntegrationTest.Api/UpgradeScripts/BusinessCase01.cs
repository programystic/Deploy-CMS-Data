using DeployCmsData.Core.Interfaces;
using DeployCmsData.UmbracoCms.Builders;
using DeployCmsData.UmbracoCms.Constants;
using DeployCmsData.UmbracoCms.Services;
using System;

namespace DeployCmsData.IntegrationTest.Api.UpgradeScripts
{
    public class BusinessCase01 : UmbracoUpgradeScript
    {
        private Guid bikePickerId = new Guid("{77F278EB-9D34-42E4-8C3B-7444BEC29A11}");

        public Guid BikePickerId { get => bikePickerId; set => bikePickerId = value; }

        public override bool RunScript(IUpgradeLogRepository upgradeLog)
        {
            CreateBikeModelPage();
            NewSubTypeBikeSelector();
            NewTabsForBikeModelPage();

            new DocumentTypeBuilder("myLovelyNewDocumentType").BuildAtRoot();

            return true;
        }

        private static void CreateBikeModelPage()
        {
            var builder = new DocumentTypeBuilder("bikeModelPage");
            builder.BuildAtRoot();
        }

        private void NewSubTypeBikeSelector()
        {
            var builder = new MultiNodeTreePickerBuilder(BikePickerId);

            builder
                .Name("Bike Picker Name")
                .AllowItemsOfType("BikeModelPage")
                .ShowOpenButton()
                .StartNodeXpath("$root/BikeListingPage")
                .Build();
        }

        private void NewTabsForBikeModelPage()
        {
            var builder = new DocumentTypeBuilder("bikeModelPage");

            ModelLevelSpecificationTab(builder);
            SubModuleDefinitionTab(builder);

            builder.Update();
        }

        private void ModelLevelSpecificationTab(DocumentTypeBuilder builder)
        {
            const string tabName = "Model Level Specification";

            builder.AddField("isLeadingModel")
                .Description("Check if this is main model for submodels")
                .DataType(DataType.CheckBox)
                .Tab(tabName);

            builder.AddField("subTypes")
                .DataType(BikePickerId)
                .Tab(tabName);
        }

        private static void SubModuleDefinitionTab(DocumentTypeBuilder builder)
        {
            const string tabName = "Sub Model Definition";

            builder.AddField("bikeImage")
                .Description("Select image to be displayed on a card")
                .DataType(DataType.MediaPicker)
                .Tab(tabName);
            builder.AddField("bikeImage")
                            .Description("Select image to be displayed on a card")
                            .DataType(DataType.MediaPicker)
                            .Tab(tabName);

            builder.AddField("overrideFromLabel")
                .Tab(tabName);

            builder.AddField("overrideSpecificationLabel")
                .Tab(tabName);

            builder.AddField("specificationContent")
                .DataType(DataType.RichTextEditor)
                .Tab(tabName);

            builder.AddField("propertyDefaultTest");
            builder.AddField("propertyDefaultTest");
            builder.AddField("propertyDefaultTest");
        }
    }
}
