namespace DeployCmsData.UmbracoCms.Interfaces
{
    public interface IHttpServerUtility
    {
        string MapPath(string path);
        string ReadAllText(string path);
    }
}
