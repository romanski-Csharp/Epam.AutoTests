using log4net;
using log4net.Core;
using log4net.Repository.Hierarchy;
using Core.Configuration;
using System;
using System.IO;

namespace Core.Utils
{
    public static class Logger
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(Logger));

        static Logger()
        {
            log4net.Config.XmlConfigurator.Configure(new FileInfo("log4net.config"));
            SetLogLevel(ConfigManager.Instance.ApiMinLogLevel);
        }

        private static void SetLogLevel(string levelName)
        {
            var repository = (Hierarchy)LogManager.GetRepository();
            var level = repository.LevelMap[levelName.ToUpper()];

            if (level != null)
            {
                repository.Root.Level = level;
                repository.RaiseConfigurationChanged(EventArgs.Empty);
                _logger.Info($"[CONFIG] Minimum log level set to: {level.Name}");
            }
            else
            {
                _logger.Warn($"[CONFIG] Unknown log level '{levelName}'. Using default.");
            }
        }

        public static void Info(string message) => _logger.Info(message);
        public static void Debug(string message) => _logger.Debug(message);
        public static void Error(string message, Exception ex = null) => _logger.Error(message, ex);
        public static void Warn(string message) => _logger.Warn(message);
    }
}