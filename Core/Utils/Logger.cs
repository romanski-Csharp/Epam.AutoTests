using log4net;
using log4net.Config;
using System.IO;
using System.Reflection;

namespace Core.Utils
{
    public static class Logger
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static void InitLogger()
        {
            var logRepository = LogManager.GetRepository(Assembly.GetExecutingAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
            log.Info("Логер ініціалізовано. Початок виконання тестів");
        }

        public static void Info(string message)
        {
            log.Info(message);
        }

        public static void Error(string message)
        {
            log.Error(message);
        }

        public static void Debug(string message)
        {
            log.Debug(message);
        }
    }
}