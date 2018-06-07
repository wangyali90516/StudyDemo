using System.Configuration;

namespace DesignPatternDemo.quartz
{
    public class Settings
    {
        public static string ExcuteTime => ConfigurationManager.AppSettings["ExcuteTime"];
        public static string ListenPoint => ConfigurationManager.AppSettings["ListenPoint"];
    }
}