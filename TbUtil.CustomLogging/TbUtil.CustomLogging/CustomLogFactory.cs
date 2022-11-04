using TbUtil.CustomLogging.Contracts;

namespace TbUtil.CustomLogging;

/// <summary>
/// Factory to create the desired type of custom logger
/// </summary>
public static class CustomLogFactory
{
    /// <summary>
    /// Create an instance of a custom console logger
    /// </summary>
    /// <param name="entityType"></param>
    /// <param name="entityName"></param>
    /// <param name="logLevel"></param>
    /// <param name="debugConsole"></param>
    /// <param name="frameIndex"></param>
    /// <returns></returns>
    public static ICustomLogger CreateLogger(EntityType entityType,
                                             string entityName,
                                             LogLevel logLevel = LogLevel.Warning,
                                             bool debugConsole = false,
                                             int frameIndex = 3)
    {
        return new CustomConsoleLogger(entityType, entityName, logLevel, debugConsole, frameIndex);
    }

    /// <summary>
    /// Create an instance of a custom file logger
    /// </summary>
    /// <param name="entityType"></param>
    /// <param name="entityName"></param>
    /// <param name="fullLogNameAndPath"></param>
    /// <param name="logLevel"></param>
    /// <param name="rollingInterval"></param>
    /// <param name="fileSizeLimitBytes"></param>
    /// <param name="frameIndex"></param>
    /// <returns></returns>
    public static ICustomLogger CreateLogger(EntityType entityType,
                                string entityName,
                                string fullLogNameAndPath,
                                LogLevel logLevel = LogLevel.Warning,
                                RollingInterval rollingInterval = RollingInterval.Day,
                                long fileSizeLimitBytes = 128000000,
                                int frameIndex = 3)
    {
        return new CustomFileLogger(entityType, entityName, fullLogNameAndPath, logLevel, rollingInterval, fileSizeLimitBytes, frameIndex);
    }

}
