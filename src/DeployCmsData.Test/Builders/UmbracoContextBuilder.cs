using Moq;
using System.Collections.Generic;
using System.Web;
using Umbraco.Core;
using Umbraco.Core.Configuration.UmbracoSettings;
using Umbraco.Core.Logging;
using Umbraco.Core.Profiling;
using Umbraco.Web;
using Umbraco.Web.Routing;
using Umbraco.Web.Security;

namespace DeployCmsData.Test.Builders
{
    class UmbracoContextBuilder
    {
        protected UmbracoContext UmbracoContext { get; private set; }
        private Mock<HttpContextBase> HttpContext { get; set; }

        public UmbracoContextBuilder()
        {
            var cacheHelper = new Mock<CacheHelper>();
            var logger = new Mock<ILogger>();
            var profiler = new Mock<IProfiler>();
            var profilingLogger = new ProfilingLogger(logger.Object, profiler.Object);

            HttpContext = new Mock<HttpContextBase>();

            var applicationContext = new ApplicationContext(
                cacheHelper.Object,
                profilingLogger);
            
            var webSecurity = new Mock<WebSecurity>(
                HttpContext.Object,
                applicationContext);

            var settings = new Mock<IUmbracoSettingsSection>();
            var urlProviders = new Mock<IEnumerable<IUrlProvider>>();

            UmbracoContext = UmbracoContext.EnsureContext(HttpContext.Object, applicationContext, webSecurity.Object, settings.Object, urlProviders.Object, false);
        }
    }
}
