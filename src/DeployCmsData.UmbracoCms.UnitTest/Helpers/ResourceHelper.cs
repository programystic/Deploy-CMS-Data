using System.IO;
using System.Reflection;
using System.Linq;

namespace DeployCmsData.UmbracoCms.UnitTest.Helpers
{
    internal static class ResourceHelper
    {
        public static string GetEmbeddedResource(string resourceName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var allResourceNames = assembly.GetManifestResourceNames();
            var fullResourceName = allResourceNames.FirstOrDefault(x => x.ToLower().EndsWith(resourceName.ToLower()));

            using (Stream stream = assembly.GetManifestResourceStream(fullResourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}