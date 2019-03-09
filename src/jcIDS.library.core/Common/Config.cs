namespace jcIDS.library.core.Common
{
    public class Config
    {
        public string DBFileName { get; set; }

        public Config()
        {
            DBFileName = Common.Constants.DEFAULT_FILENAME_DATABASE;
        }
    }
}