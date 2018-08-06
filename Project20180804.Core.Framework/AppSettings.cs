namespace Project20180804.Core.Framework
{
    public class AppSettings
    {
        public string MySqlConnectionString { get; set; }
        public QiNiuSetting QiNiuSetting { get; set; }
    }

    public class QiNiuSetting
    {
        public string AccessKey { get; set; }
        public string SecretKey { get; set; }
        public string Bucket { get; set; }
    }
}
