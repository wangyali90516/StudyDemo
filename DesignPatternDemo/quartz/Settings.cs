using System.Configuration;

namespace DesignPatternDemo.quartz
{
    public class Settings
    {
        public static string ExcuteTime => ConfigurationManager.AppSettings["ExcuteTime"];
        public static string ListenPoint => ConfigurationManager.AppSettings["ListenPoint"];

        public static string RedisConnectionString => ConfigurationManager.AppSettings["RedisConnectionString"];
        public static int RedisDb => int.Parse(ConfigurationManager.AppSettings["RedisDb"]);

        public static string SqlDiagnosticsDb => ConfigurationManager.AppSettings["SqlDiagnosticsDb"];
    }
}