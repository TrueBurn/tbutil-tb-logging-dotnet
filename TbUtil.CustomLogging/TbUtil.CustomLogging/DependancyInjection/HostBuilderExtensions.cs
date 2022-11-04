using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace TbUtil.CustomLogging.DependancyInjection;

/// <summary>
/// Contains <see cref="IHostBuilder"/> extension methods used by <see cref="CLogger"/> 
/// </summary>
public static class HostBuilderExtensions
{
    /// <summary>
    /// Configures <see cref="CLogger"/> instance using logging data specified in the application's configuration. 
    /// </summary>
    /// <param name="hostBuilder"></param>
    /// <param name="entityType"></param>
    public static IHostBuilder UseCustomLogger(this IHostBuilder hostBuilder, EntityType entityType)
    {
        hostBuilder.ConfigureServices((hostBuilderContext, services) =>
        {
            //get application entity name from application configurations
            string entityName = hostBuilderContext.Configuration.GetValue<string>("Application:EntityName");

            //get application log level from the application configurations
            LogLevel customLogLevel = hostBuilderContext.Configuration.GetValue<LogLevel>($"Logging:LogLevel:{entityType}");

            //configure logger based on the application's environment 
            if (hostBuilderContext.HostingEnvironment.IsDevelopment())
            {
                CLogger.SetupDebugConsole(
                    entityType: entityType,
                    entityName: entityName,
                    logLevel: customLogLevel
                );
            }
            else
            {
                CLogger.SetupConsole(
                    entityType: entityType,
                    entityName: entityName,
                    logLevel: customLogLevel
                );
            }
        });

        //add Serilog to the IHostBuilder
        return hostBuilder.UseSerilog();
    }

}