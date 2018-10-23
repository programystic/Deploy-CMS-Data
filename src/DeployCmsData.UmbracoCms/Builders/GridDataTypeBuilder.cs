using Umbraco.Core.Services;

namespace DeployCmsData.UmbracoCms.Builders
{
    public class GridDataTypeBuilder
    {
        public GridDataTypeBuilder(IDataTypeService dataTypeService)
        {
        }

        public GridDataTypeBuilder AddRow()
        {
            return this;
        }

        public GridDataTypeBuilder AddToolbarOption()
        {
            return this;
        }
    }
}
