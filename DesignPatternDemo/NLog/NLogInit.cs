using System;
using System.Text;
using NLog;
using NLog.Config;
using NLog.Layouts;
using NLog.Targets;

namespace DesignPatternDemo.NLog
{
    public class BaseLog
    {
        /// <summary>
        ///     The application logger
        /// </summary>
        private static readonly Lazy<ILogger> ApplicationLogger = new Lazy<ILogger>(() => InitApplicationLogger("ApplicationLogger"));

        /// <summary>
        ///     The application logger
        /// </summary>
        private static readonly Lazy<ILogger> BankApplicationLogger = new Lazy<ILogger>(() => InitApplicationLogger("BankApplicationLogger"));

        /// <summary>
        ///     The application logger
        /// </summary>
        private static readonly Lazy<ILogger> SpecialApplicationLogger = new Lazy<ILogger>(() => InitApplicationLogger("SpecialApplicationLogger"));

        /// <summary>
        ///     Initializes the <see cref="BaseLog" /> class.
        /// </summary>
        static BaseLog()
        {
            CreateCommonLog();
        }

        /// <summary>
        ///     Gets the logger.
        /// </summary>
        /// <value>The logger.</value>
        protected static ILogger Logger => ApplicationLogger.Value;

        private static FileTarget BuildTarget(string postFix)
        {
            return new FileTarget
            {
                AutoFlush = true,
                CreateDirs = true,
                Encoding = Encoding.UTF8,
                FileName = Layout.FromString(AppDomain.CurrentDomain.BaseDirectory + "Logs\\${shortdate}" + postFix + ".log"),
                Layout = Layout.FromString("${date} [${level:format=FirstCharacter}] ${message} ${onexception:${exception:format=tostring}"),
                ArchiveAboveSize = 1024000 * 10,
                ArchiveNumbering = ArchiveNumberingMode.Rolling,
                ArchiveFileName = "${basedir}/Logs/archives/${shortdate}.{#####}" + postFix + ".log"
            };
        }

        private static void CreateCommonLog()
        {
            LoggingConfiguration config = new LoggingConfiguration();

            FileTarget commonTarget = BuildTarget(null);
            config.AddTarget("ApplicationTarget", commonTarget);

            config.LoggingRules.Add(new LoggingRule("ApplicationLogger", LogLevel.Info, commonTarget));

            LogManager.Configuration = config;
        }

        /// <summary>
        ///     Initializes the application logger.
        /// </summary>
        /// <returns>NLog.ILogger.</returns>
        private static ILogger InitApplicationLogger(string loggerName)
        {
            return LogManager.GetLogger(loggerName);
        }
    }

    public class NLogInit : BaseLog
    {
        public static void Main()
        {
            Logger.Info("1111");

            Console.ReadKey();
        }
    }
}