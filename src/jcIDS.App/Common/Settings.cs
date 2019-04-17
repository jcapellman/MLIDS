namespace jcIDS.app.Common
{
    public class Settings
    {
        public string WebServiceURL { get; set; }

        public Settings()
        {
            WebServiceURL = Constants.DEFAULT_WEBSERVICE_URL;
        }
    }
}