using Umbraco.Core.Persistence;
using Umbraco.Core.Persistence.DatabaseAnnotations;

namespace DeployCmsData.UmbracoCms.Models
{
    [TableName(DeployCmsData.Constants.Data.LogsTableName)]
    [PrimaryKey(nameof(Id), autoIncrement = true)]
    public class UmbracoUpgradeLog : DeployCmsData.Models.UpgradeLog
    {
        [PrimaryKeyColumn(AutoIncrement = true)]
        public override long Id { get; set; }

        [NullSetting(NullSetting = NullSettings.Null)]
        [SpecialDbType(SpecialDbTypes.NTEXT)]
        public override string Exception { get; set; }
    }
}