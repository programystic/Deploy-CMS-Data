using System;
using DeployCmsData.Umbraco7.Builders;
using Moq;
using Umbraco.Core.Services;

namespace DeployCmsData.Umbraco7.UnitTest.Builders
{
    internal class GridDataTypeTestBuilder
    {
        private readonly GridDataTypeBuilder _gridDataTypeBuilder;
        public Mock<IDataTypeService> DataTypeService { get; }

        public GridDataTypeTestBuilder(Guid key)
        {
            DataTypeService = new Mock<IDataTypeService>();
            _gridDataTypeBuilder = new GridDataTypeBuilder(DataTypeService.Object, key);
        }

        public GridDataTypeBuilder Build()
        {
            return _gridDataTypeBuilder;
        }
    }
}
