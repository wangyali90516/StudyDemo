using System;
using System.Text;
using NLog;
using NLog.Config;
using NLog.Layouts;
using NLog.Targets;

namespace HangFire
{
    public class JYMNLogger
    {
        /// <summary>
        ///     The application logger
        /// </summary>
        private static readonly Lazy<ILogger> ApplicationLogger = new Lazy<ILogger>(() => InitApplicationLogger("ApplicationLogger"));

        /// <summary>
        ///
        /// </summary>
        static JYMNLogger()
        {
            CreateCommonLog();
        }

        /// <summary>
        ///     Gets the logger.
        /// </summary>
        /// <value>The logger.</value>
        protected ILogger Logger => ApplicationLogger.Value;

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
}