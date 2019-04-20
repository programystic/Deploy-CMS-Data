namespace DeployCmsData.Umbraco7.Interfaces
{
    public interface IHttpServerUtility
    {
        string MapPath(string path);
        string ReadAllText(string path);
    }
}
