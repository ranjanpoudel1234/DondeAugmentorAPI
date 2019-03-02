using Microsoft.Extensions.Configuration;
using NLog;
using System.Collections.Generic;

namespace Donde.Augmentor.Web.AwsEnvironmentConfiguration
{
    public static class AwsEnvironmentConfigurationExtensions
    {
        /// <summary>
        /// Gets conncetion string for RDS based on the environment variables in AWS
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static string GetRdsConnectionString(this IConfigurationRoot configuration)
        {
            string hostname = configuration.GetValue<string>(AwsEnvironmentVariableKeys.RDS_HOST_NAME);
            string port = configuration.GetValue<string>(AwsEnvironmentVariableKeys.RDS_PORT);
            string dbname = configuration.GetValue<string>(AwsEnvironmentVariableKeys.RDS_DB_NAME);
            string username = configuration.GetValue<string>(AwsEnvironmentVariableKeys.RDS_USERNAME);
            string password = configuration.GetValue<string>(AwsEnvironmentVariableKeys.RDS_PASSWORD);

            return $"Server={hostname};Port={port};Database={dbname};Username={username};Password={password}";
        }

        /// <summary>
        /// Gets the NLog labels to disable based on the configuration in AWS environment for that key
        /// regarding which log levels should be disabled for that environment
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static List<LogLevel> GetLogLevelsToDisable(this IConfigurationRoot configuration)
        {
            string logLevelsConfiguredToDisable = configuration.GetValue<string>(AwsEnvironmentVariableKeys.DISABLE_NLOG_LEVELS);
            var individualLogLevelsConfiguredToDisable = logLevelsConfiguredToDisable.Split(";");
            var nLogLevelsToDisable = new List<LogLevel>();
            foreach (var logLevel in individualLogLevelsConfiguredToDisable)
            {
                switch(logLevel)
                {
                    case "Info":
                        nLogLevelsToDisable.Add(LogLevel.Info);
                        break;

                    case "Debug":
                        nLogLevelsToDisable.Add(LogLevel.Debug);
                        break;
                    case "Trace":
                        nLogLevelsToDisable.Add(LogLevel.Trace);
                        break;
                    case "Warn":
                        nLogLevelsToDisable.Add(LogLevel.Warn);
                        break;
                }
            }

            return nLogLevelsToDisable;
        }     
    }
}
