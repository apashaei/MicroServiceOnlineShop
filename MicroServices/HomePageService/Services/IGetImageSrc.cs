namespace HomPageServices.Services
{
    public interface IGetImageSrc
    {

        string Execute(string src);

    }

    public class GetImageSrc : IGetImageSrc
    {
        public string Execute(string src)
        {
            return "https://localhost:7084/" + src.Replace("\\", "//");
        }
    }
}
