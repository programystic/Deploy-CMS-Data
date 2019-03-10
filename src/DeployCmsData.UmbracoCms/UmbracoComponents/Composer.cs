using Umbraco.Core;
using Umbraco.Core.Composing;

namespace DeployCmsData.UmbracoCms.UmbracoComponents
{
    [RuntimeLevel(MinLevel = RuntimeLevel.Run)]
    public class Composer : IUserComposer
    {
        public void Compose(Composition composition)
        {
            composition.Components().Append<RunMigrations>();
            composition.Components().Append<RunOnStartup>();
        }
    }
}