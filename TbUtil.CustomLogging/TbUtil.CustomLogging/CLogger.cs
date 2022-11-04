using Serilog;
using Serilog.Context;
using Serilog.Core;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Sinks.SystemConsole.Themes;
using TbUtil.CustomLogging.Formatting;
using TbUtil.CustomLogging.Sinks;
using TbUtil.CustomLogging.Utils;

namespace TbUtil.CustomLogging;

/// <summary>
/// Creates an instance of the custom logger
/// </summary>
public sealed class CLogger
{

    #region Custom IMPLEMENTATION

    private static LogLevel _logLevel = LogLevel.Error;

    /// <summary>
    /// Returns a valid LogLevel enum based on string input
    /// </summary>
    /// <param name="logLevel"></param>
    /// <returns></returns>
    private static LogLevel GetLoggingLevel(string logLevel) => logLevel.ToLower() switch
    {
        "trace" or "verbose" => LogLevel.Verbose,
        "debug" => LogLevel.Debug,
        "info" or "information" => LogLevel.Information,
        "warn" or "warning" => LogLevel.Warning,
        "fatal" => LogLevel.Fatal,
        _ => LogLevel.Error,
    };

    /// <summary>
    /// Setup the logging framework to write to console using JSON format
    /// </summary>
    /// <param name="logLevel"></param>
    /// <param name="entityType"></param>
    /// <param name="entityName"></param>
    /// <param name="frameIndex"></param>
    public static void SetupConsole(EntityType entityType, string entityName, LogLevel logLevel = LogLevel.Warning, int frameIndex = 3)
    {
        _logLevel = logLevel;
        AssemblyDetailsActions.SetFrameIndex(frameIndex);
        Log.Logger = DefaultConfig(entityType, entityName, logLevel)
                        .WriteTo.ConsoleLog()
                        .CreateLogger();
    }

    /// <summary>
    /// Setup the logging framework to write to console using JSON format
    /// </summary>
    /// <param name="entityType"></param>
    /// <param name="entityName"></param>
    /// <param name="logLevel"></param>
    /// <param name="frameIndex"></param>
    public static void SetupConsole(EntityType entityType, string entityName, string logLevel = "Warning", int frameIndex = 3)
    {
        SetupConsole(entityType, entityName, GetLoggingLevel(logLevel), frameIndex);
    }

    /// <summary>
    /// Setup the logging framework to write to a rolling logfile with JSON format
    /// </summary>
    /// <param name="logLevel"></param>
    /// <param name="entityType"></param>
    /// <param name="entityName"></param>
    /// <param name="fullLogNameAndPath"></param>
    /// <param name="fileSizeLimitBytes"></param>
    /// <param name="rollingInterval"></param>
    /// <param name="frameIndex"></param>
    public static void SetupFile(EntityType entityType,
                                string entityName,
                                string fullLogNameAndPath,
                                LogLevel logLevel = LogLevel.Warning,
                                RollingInterval rollingInterval = RollingInterval.Day,
                                long fileSizeLimitBytes = 128000000,
                                int frameIndex = 3)
    {
        _logLevel = logLevel;
        AssemblyDetailsActions.SetFrameIndex(frameIndex);
        Log.Logger = DefaultConfig(entityType, entityName, logLevel)
                        .WriteTo.File(new CompactJsonFormatter(),
                            fullLogNameAndPath,
                            fileSizeLimitBytes: fileSizeLimitBytes,
                            rollOnFileSizeLimit: true,
                            shared: true,
                            rollingInterval: (Serilog.RollingInterval)rollingInterval)
                        .CreateLogger();
    }

    /// <summary>
    /// Setup the logging framework to write to a rolling logfile with JSON format
    /// </summary>
    /// <param name="logLevel"></param>
    /// <param name="entityType"></param>
    /// <param name="entityName"></param>
    /// <param name="fullLogNameAndPath"></param>
    /// <param name="fileSizeLimitBytes"></param>
    /// <param name="rollingInterval"></param>
    /// <param name="frameIndex"></param>
    public static void SetupFile(EntityType entityType,
                                string entityName,
                                string fullLogNameAndPath,
                                string logLevel = "Warning",
                                RollingInterval rollingInterval = RollingInterval.Day,
                                long fileSizeLimitBytes = 128000000,
                                int frameIndex = 3)
    {
        SetupFile(entityType, entityName, fullLogNameAndPath, GetLoggingLevel(logLevel), rollingInterval, fileSizeLimitBytes, frameIndex);
    }

    /// <summary>
    /// Setup the logging framework to write to the ColorConsole for debugging purposes 
    /// </summary>
    /// <param name="logLevel"></param>
    /// <param name="entityType"></param>
    /// <param name="entityName"></param>
    /// <param name="frameIndex"></param>
    public static void SetupDebugConsole(EntityType entityType, string entityName, LogLevel logLevel = LogLevel.Debug, int frameIndex = 3)
    {
        _logLevel = logLevel;
        AssemblyDetailsActions.SetFrameIndex(frameIndex);
        Log.Logger = DefaultConfig(entityType, entityName, logLevel)
                        .WriteTo.Console(outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss}] [{Level:u3}] [{AssemblyName}] {Message} {Exception} {NewLine}", theme: AnsiConsoleTheme.Code)
                        .CreateLogger();
    }

    /// <summary>
    /// Setup the logging framework to write to the ColorConsole for debugging purposes 
    /// </summary>
    /// <param name="logLevel"></param>
    /// <param name="entityType"></param>
    /// <param name="entityName"></param>
    /// <param name="frameIndex"></param>
    public static void SetupDebugConsole(EntityType entityType, string entityName, string logLevel = "Debug", int frameIndex = 3)
    {
        SetupDebugConsole(entityType, entityName, GetLoggingLevel(logLevel), frameIndex);
    }

    /// <summary>
    /// Default Serilog config with custom minimum fields required for logging
    /// </summary>
    /// <param name="logLevel"></param>
    /// <param name="entityType"></param>
    /// <param name="entityName"></param>
    /// <returns></returns>
    public static LoggerConfiguration DefaultConfig(EntityType entityType, string entityName, LogLevel logLevel)
    {
        LoggerConfiguration config = new LoggerConfiguration()
                        .Enrich.FromLogContext()
                        .Enrich.WithAssemblyName()
                        .Enrich.WithAssemblyVersion()
                        .Enrich.WithExceptionDetails()
                        .Enrich.WithMemoryUsage()
                        .Enrich.WithProperty("EntityType", entityType)
                        .Enrich.WithProperty("EntityName", entityName);

        config.MinimumLevel.ControlledBy(new LoggingLevelSwitch((LogEventLevel)logLevel));

        return config;
    }

    #endregion

    #region SERILOG LOG INTERFACE OVERRIDE 

    /// <summary>
    /// Determine if events at the specified level will be passed through to the log sinks
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    public static bool IsEnabled(LogLevel level)
    {
        return Log.IsEnabled((LogEventLevel)level);
    }
    /// <summary>
    /// Resets Log.Logger to the default and disposes the original if possible
    /// </summary>
    public static void CloseAndFlush()
    {
        Log.CloseAndFlush();
    }

    /// <summary>
    /// Write a log event with the LogLevel.Debug level
    /// </summary>
    /// <param name="messageTemplate"></param>
    public static void Debug(string messageTemplate)
    {
        if (LogLevel.Debug < _logLevel)
        {
            return;
        }

        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        Log.Debug(messageTemplate);
    }
    /// <summary>
    /// Write a log event with the LogLevel.Debug level
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValue"></param>
    public static void Debug<T>(string messageTemplate, T propertyValue)
    {
        if (LogLevel.Debug < _logLevel)
        {
            return;
        }

        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        Log.Debug(messageTemplate, propertyValue);
    }
    /// <summary>
    /// Write a log event with the LogLevel.Debug level
    /// </summary>
    /// <typeparam name="T0"></typeparam>
    /// <typeparam name="T1"></typeparam>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValue0"></param>
    /// <param name="propertyValue1"></param>
    public static void Debug<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    {
        if (LogLevel.Debug < _logLevel)
        {
            return;
        }

        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        Log.Debug(messageTemplate, propertyValue0, propertyValue1);
    }
    /// <summary>
    /// Write a log event with the LogLevel.Debug level
    /// </summary>
    /// <typeparam name="T0"></typeparam>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValue0"></param>
    /// <param name="propertyValue1"></param>
    /// <param name="propertyValue2"></param>
    public static void Debug<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
    {
        if (LogLevel.Debug < _logLevel)
        {
            return;
        }

        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        Log.Debug(messageTemplate, propertyValue0, propertyValue1, propertyValue2);
    }
    /// <summary>
    /// Write a log event with the LogLevel.Debug level
    /// </summary>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValues"></param>
    public static void Debug(string messageTemplate, params object[] propertyValues)
    {
        if (LogLevel.Debug < _logLevel)
        {
            return;
        }

        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        Log.Debug(messageTemplate, propertyValues);
    }
    /// <summary>
    /// Write a log event with the LogLevel.Debug level and associated exception
    /// </summary>
    /// <param name="exception"></param>
    /// <param name="messageTemplate"></param>
    public static void Debug(Exception exception, string messageTemplate)
    {
        if (LogLevel.Debug < _logLevel)
        {
            return;
        }

        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        Log.Debug(exception, messageTemplate);
    }
    /// <summary>
    /// Write a log event with the LogLevel.Debug level and associated exception
    /// </summary>
    /// <param name="messageTemplate"></param>
    /// <param name="exception"></param>
    public static void Debug(string messageTemplate, Exception exception)
    {
        if (LogLevel.Debug < _logLevel)
        {
            return;
        }

        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        Log.Debug(exception, messageTemplate);
    }
    /// <summary>
    /// Write a log event with the LogLevel.Debug level and associated exception
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="exception"></param>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValue"></param>
    public static void Debug<T>(Exception exception, string messageTemplate, T propertyValue)
    {
        if (LogLevel.Debug < _logLevel)
        {
            return;
        }

        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        Log.Debug(exception, messageTemplate, propertyValue);
    }
    /// <summary>
    /// Write a log event with the LogLevel.Debug level and associated exception
    /// </summary>
    /// <typeparam name="T0"></typeparam>
    /// <typeparam name="T1"></typeparam>
    /// <param name="exception"></param>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValue0"></param>
    /// <param name="propertyValue1"></param>
    public static void Debug<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    {
        if (LogLevel.Debug < _logLevel)
        {
            return;
        }

        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        Log.Debug(exception, messageTemplate, propertyValue0, propertyValue1);
    }
    /// <summary>
    /// Write a log event with the LogLevel.Debug level and associated exception
    /// </summary>
    /// <typeparam name="T0"></typeparam>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <param name="exception"></param>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValue0"></param>
    /// <param name="propertyValue1"></param>
    /// <param name="propertyValue2"></param>
    public static void Debug<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
    {
        if (LogLevel.Debug < _logLevel)
        {
            return;
        }

        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        Log.Debug(exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
    }
    /// <summary>
    /// Write a log event with the LogLevel.Debug level and associated exception
    /// </summary>
    /// <param name="exception"></param>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValues"></param>
    public static void Debug(Exception exception, string messageTemplate, params object[] propertyValues)
    {
        if (LogLevel.Debug < _logLevel)
        {
            return;
        }

        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        Log.Debug(exception, messageTemplate, propertyValues);
    }

    /// <summary>
    /// Write a log event with the LogLevel.Error level
    /// </summary>
    /// <param name="messageTemplate"></param>
    public static void Error(string messageTemplate)
    {
        if (LogLevel.Error < _logLevel)
        {
            return;
        }

        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        Log.Error(messageTemplate);
    }
    /// <summary>
    /// Write a log event with the LogLevel.Error level
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValue"></param>
    public static void Error<T>(string messageTemplate, T propertyValue)
    {
        if (LogLevel.Error < _logLevel)
        {
            return;
        }

        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        Log.Error(messageTemplate, propertyValue);
    }
    /// <summary>
    /// Write a log event with the LogLevel.Error level
    /// </summary>
    /// <typeparam name="T0"></typeparam>
    /// <typeparam name="T1"></typeparam>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValue0"></param>
    /// <param name="propertyValue1"></param>
    public static void Error<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    {
        if (LogLevel.Error < _logLevel)
        {
            return;
        }

        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        Log.Error(messageTemplate, propertyValue0, propertyValue1);
    }
    /// <summary>
    /// Write a log event with the LogLevel.Error level
    /// </summary>
    /// <typeparam name="T0"></typeparam>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValue0"></param>
    /// <param name="propertyValue1"></param>
    /// <param name="propertyValue2"></param>
    public static void Error<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
    {
        if (LogLevel.Error < _logLevel)
        {
            return;
        }

        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        Log.Error(messageTemplate, propertyValue0, propertyValue1, propertyValue2);
    }
    /// <summary>
    /// Write a log event with the LogLevel.Error level
    /// </summary>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValues"></param>
    public static void Error(string messageTemplate, params object[] propertyValues)
    {
        if (LogLevel.Error < _logLevel)
        {
            return;
        }

        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        Log.Error(messageTemplate, propertyValues);
    }
    /// <summary>
    /// Write a log event with the LogLevel.Error level and associated exception
    /// </summary>
    /// <param name="exception"></param>
    /// <param name="messageTemplate"></param>
    public static void Error(Exception exception, string messageTemplate)
    {
        if (LogLevel.Error < _logLevel)
        {
            return;
        }

        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        Log.Error(exception, messageTemplate);
    }
    /// <summary>
    /// Write a log event with the LogLevel.Error level and associated exception
    /// </summary>
    /// <param name="exception"></param>
    /// <param name="messageTemplate"></param>
    public static void Error(string messageTemplate, Exception exception)
    {
        if (LogLevel.Error < _logLevel)
        {
            return;
        }

        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        Log.Error(exception, messageTemplate);
    }
    /// <summary>
    /// Write a log event with the LogLevel.Error level and associated exception
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="exception"></param>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValue"></param>
    public static void Error<T>(Exception exception, string messageTemplate, T propertyValue)
    {
        if (LogLevel.Error < _logLevel)
        {
            return;
        }

        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        Log.Error(exception, messageTemplate, propertyValue);
    }
    /// <summary>
    /// Write a log event with the LogLevel.Error level and associated exception
    /// </summary>
    /// <typeparam name="T0"></typeparam>
    /// <typeparam name="T1"></typeparam>
    /// <param name="exception"></param>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValue0"></param>
    /// <param name="propertyValue1"></param>
    public static void Error<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    {
        if (LogLevel.Error < _logLevel)
        {
            return;
        }

        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        Log.Error(exception, messageTemplate, propertyValue0, propertyValue1);
    }
    /// <summary>
    /// Write a log event with the LogLevel.Error level and associated exception
    /// </summary>
    /// <typeparam name="T0"></typeparam>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <param name="exception"></param>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValue0"></param>
    /// <param name="propertyValue1"></param>
    /// <param name="propertyValue2"></param>
    public static void Error<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
    {
        if (LogLevel.Error < _logLevel)
        {
            return;
        }

        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        Log.Error(exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
    }
    /// <summary>
    /// Write a log event with the LogLevel.Error level and associated exception
    /// </summary>
    /// <param name="exception"></param>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValues"></param>
    public static void Error(Exception exception, string messageTemplate, params object[] propertyValues)
    {
        if (LogLevel.Error < _logLevel)
        {
            return;
        }

        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        Log.Error(exception, messageTemplate, propertyValues);
    }

    /// <summary>
    /// Write a log event with the LogLevel.Fatal level
    /// </summary>
    /// <param name="messageTemplate"></param>
    public static void Fatal(string messageTemplate)
    {
        if (LogLevel.Fatal < _logLevel)
        {
            return;
        }

        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        Log.Fatal(messageTemplate);
    }
    /// <summary>
    /// Write a log event with the LogLevel.Fatal level
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValue"></param>
    public static void Fatal<T>(string messageTemplate, T propertyValue)
    {
        if (LogLevel.Fatal < _logLevel)
        {
            return;
        }

        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        Log.Fatal(messageTemplate, propertyValue);
    }
    /// <summary>
    /// Write a log event with the LogLevel.Fatal level
    /// </summary>
    /// <typeparam name="T0"></typeparam>
    /// <typeparam name="T1"></typeparam>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValue0"></param>
    /// <param name="propertyValue1"></param>
    public static void Fatal<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    {
        if (LogLevel.Fatal < _logLevel)
        {
            return;
        }

        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        Log.Fatal(messageTemplate, propertyValue0, propertyValue1);
    }
    /// <summary>
    /// Write a log event with the LogLevel.Fatal level
    /// </summary>
    /// <typeparam name="T0"></typeparam>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValue0"></param>
    /// <param name="propertyValue1"></param>
    /// <param name="propertyValue2"></param>
    public static void Fatal<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
    {
        if (LogLevel.Fatal < _logLevel)
        {
            return;
        }

        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        Log.Fatal(messageTemplate, propertyValue0, propertyValue1, propertyValue2);
    }
    /// <summary>
    /// Write a log event with the LogLevel.Fatal level
    /// </summary>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValues"></param>
    public static void Fatal(string messageTemplate, params object[] propertyValues)
    {
        if (LogLevel.Fatal < _logLevel)
        {
            return;
        }

        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        Log.Fatal(messageTemplate, propertyValues);
    }
    /// <summary>
    /// Write a log event with the LogLevel.Fatal level and associated exception
    /// </summary>
    /// <param name="exception"></param>
    /// <param name="messageTemplate"></param>
    public static void Fatal(Exception exception, string messageTemplate)
    {
        if (LogLevel.Fatal < _logLevel)
        {
            return;
        }

        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        Log.Fatal(exception, messageTemplate);
    }
    /// <summary>
    /// Write a log event with the LogLevel.Fatal level and associated exception
    /// </summary>
    /// <param name="messageTemplate"></param>
    /// <param name="exception"></param>
    public static void Fatal(string messageTemplate, Exception exception)
    {
        if (LogLevel.Fatal < _logLevel)
        {
            return;
        }

        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        Log.Fatal(exception, messageTemplate);
    }
    /// <summary>
    /// Write a log event with the LogLevel.Fatal level and associated exception
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="exception"></param>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValue"></param>
    public static void Fatal<T>(Exception exception, string messageTemplate, T propertyValue)
    {
        if (LogLevel.Fatal < _logLevel)
        {
            return;
        }

        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        Log.Fatal(exception, messageTemplate, propertyValue);
    }
    /// <summary>
    /// Write a log event with the LogLevel.Fatal level and associated exception
    /// </summary>
    /// <typeparam name="T0"></typeparam>
    /// <typeparam name="T1"></typeparam>
    /// <param name="exception"></param>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValue0"></param>
    /// <param name="propertyValue1"></param>
    public static void Fatal<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    {
        if (LogLevel.Fatal < _logLevel)
        {
            return;
        }

        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        Log.Fatal(exception, messageTemplate, propertyValue0, propertyValue1);
    }
    /// <summary>
    /// Write a log event with the LogLevel.Fatal level and associated exception
    /// </summary>
    /// <typeparam name="T0"></typeparam>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <param name="exception"></param>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValue0"></param>
    /// <param name="propertyValue1"></param>
    /// <param name="propertyValue2"></param>
    public static void Fatal<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
    {
        if (LogLevel.Fatal < _logLevel)
        {
            return;
        }

        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        Log.Fatal(exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
    }
    /// <summary>
    /// Write a log event with the LogLevel.Fatal level and associated exception
    /// </summary>
    /// <param name="exception"></param>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValues"></param>
    public static void Fatal(Exception exception, string messageTemplate, params object[] propertyValues)
    {
        if (LogLevel.Fatal < _logLevel)
        {
            return;
        }

        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        Log.Fatal(exception, messageTemplate, propertyValues);
    }

    /// <summary>
    /// Write a log event with the LogLevel.Information
    /// </summary>
    /// <param name="messageTemplate"></param>
    public static void Information(string messageTemplate)
    {
        if (LogLevel.Information < _logLevel)
        {
            return;
        }

        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        Log.Information(messageTemplate);
    }
    /// <summary>
    /// Write a log event with the LogLevel.Information
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValue"></param>
    public static void Information<T>(string messageTemplate, T propertyValue)
    {
        if (LogLevel.Information < _logLevel)
        {
            return;
        }

        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        Log.Information(messageTemplate, propertyValue);
    }
    /// <summary>
    /// Write a log event with the LogLevel.Information
    /// </summary>
    /// <typeparam name="T0"></typeparam>
    /// <typeparam name="T1"></typeparam>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValue0"></param>
    /// <param name="propertyValue1"></param>
    public static void Information<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    {
        if (LogLevel.Information < _logLevel)
        {
            return;
        }

        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        Log.Information(messageTemplate, propertyValue0, propertyValue1);
    }
    /// <summary>
    /// Write a log event with the LogLevel.Information
    /// </summary>
    /// <typeparam name="T0"></typeparam>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValue0"></param>
    /// <param name="propertyValue1"></param>
    /// <param name="propertyValue2"></param>
    public static void Information<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
    {
        if (LogLevel.Information < _logLevel)
        {
            return;
        }

        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        Log.Information(messageTemplate, propertyValue0, propertyValue1, propertyValue2);
    }
    /// <summary>
    /// Write a log event with the LogLevel.Information
    /// </summary>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValues"></param>
    public static void Information(string messageTemplate, params object[] propertyValues)
    {
        if (LogLevel.Information < _logLevel)
        {
            return;
        }

        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        Log.Information(messageTemplate, propertyValues);
    }
    /// <summary>
    /// Write a log event with the LogLevel.Information and associated exception
    /// </summary>
    /// <param name="exception"></param>
    /// <param name="messageTemplate"></param>
    public static void Information(Exception exception, string messageTemplate)
    {
        if (LogLevel.Information < _logLevel)
        {
            return;
        }

        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        Log.Information(exception, messageTemplate);
    }
    /// <summary>
    /// Write a log event with the LogLevel.Information and associated exception
    /// </summary>
    /// <param name="messageTemplate"></param>
    /// <param name="exception"></param>
    public static void Information(string messageTemplate, Exception exception)
    {
        if (LogLevel.Information < _logLevel)
        {
            return;
        }

        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        Log.Information(exception, messageTemplate);
    }
    /// <summary>
    /// Write a log event with the LogLevel.Information and associated exception
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="exception"></param>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValue"></param>
    public static void Information<T>(Exception exception, string messageTemplate, T propertyValue)
    {
        if (LogLevel.Information < _logLevel)
        {
            return;
        }

        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        Log.Information(exception, messageTemplate, propertyValue);
    }
    /// <summary>
    /// Write a log event with the LogLevel.Information and associated exception
    /// </summary>
    /// <typeparam name="T0"></typeparam>
    /// <typeparam name="T1"></typeparam>
    /// <param name="exception"></param>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValue0"></param>
    /// <param name="propertyValue1"></param>
    public static void Information<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    {
        if (LogLevel.Information < _logLevel)
        {
            return;
        }

        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        Log.Information(exception, messageTemplate, propertyValue0, propertyValue1);
    }
    /// <summary>
    /// Write a log event with the LogLevel.Information and associated exception
    /// </summary>
    /// <typeparam name="T0"></typeparam>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <param name="exception"></param>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValue0"></param>
    /// <param name="propertyValue1"></param>
    /// <param name="propertyValue2"></param>
    public static void Information<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
    {
        if (LogLevel.Information < _logLevel)
        {
            return;
        }

        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        Log.Information(exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
    }
    /// <summary>
    /// Write a log event with the LogLevel.Information and associated exception
    /// </summary>
    /// <param name="exception"></param>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValues"></param>
    public static void Information(Exception exception, string messageTemplate, params object[] propertyValues)
    {
        if (LogLevel.Information < _logLevel)
        {
            return;
        }

        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        Log.Information(exception, messageTemplate, propertyValues);
    }

    /// <summary>
    ///  Write a log event with the LogLevel.Verbose
    /// </summary>
    /// <param name="messageTemplate"></param>
    public static void Verbose(string messageTemplate)
    {
        if (LogLevel.Verbose < _logLevel)
        {
            return;
        }

        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        Log.Verbose(messageTemplate);
    }
    /// <summary>
    /// Write a log event with the LogLevel.Verbose
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValue"></param>
    public static void Verbose<T>(string messageTemplate, T propertyValue)
    {
        if (LogLevel.Verbose < _logLevel)
        {
            return;
        }

        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        Log.Verbose(messageTemplate, propertyValue);
    }
    /// <summary>
    /// Write a log event with the LogLevel.Verbose
    /// </summary>
    /// <typeparam name="T0"></typeparam>
    /// <typeparam name="T1"></typeparam>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValue0"></param>
    /// <param name="propertyValue1"></param>
    public static void Verbose<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    {
        if (LogLevel.Verbose < _logLevel)
        {
            return;
        }

        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        Log.Verbose(messageTemplate, propertyValue0, propertyValue1);
    }
    /// <summary>
    /// Write a log event with the LogLevel.Verbose
    /// </summary>
    /// <typeparam name="T0"></typeparam>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValue0"></param>
    /// <param name="propertyValue1"></param>
    /// <param name="propertyValue2"></param>
    public static void Verbose<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
    {
        if (LogLevel.Verbose < _logLevel)
        {
            return;
        }

        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        Log.Verbose(messageTemplate, propertyValue0, propertyValue1, propertyValue2);
    }
    /// <summary>
    /// Write a log event with the LogLevel.Verbose
    /// </summary>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValues"></param>
    public static void Verbose(string messageTemplate, params object[] propertyValues)
    {
        if (LogLevel.Verbose < _logLevel)
        {
            return;
        }

        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        Log.Verbose(messageTemplate, propertyValues);
    }
    /// <summary>
    /// Write a log event with the LogLevel.Verbose
    /// </summary>
    /// <param name="exception"></param>
    /// <param name="messageTemplate"></param>
    public static void Verbose(Exception exception, string messageTemplate)
    {
        if (LogLevel.Verbose < _logLevel)
        {
            return;
        }

        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        Log.Verbose(exception, messageTemplate);
    }
    /// <summary>
    /// Write a log event with the LogLevel.Verbose
    /// </summary>
    /// <param name="exception"></param>
    /// <param name="messageTemplate"></param>
    public static void Verbose(string messageTemplate, Exception exception)
    {
        if (LogLevel.Verbose < _logLevel)
        {
            return;
        }

        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        Log.Verbose(exception, messageTemplate);
    }
    /// <summary>
    /// Write a log event with the LogLevel.Verbose and associated exception
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="exception"></param>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValue"></param>
    public static void Verbose<T>(Exception exception, string messageTemplate, T propertyValue)
    {
        if (LogLevel.Verbose < _logLevel)
        {
            return;
        }

        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        Log.Verbose(exception, messageTemplate, propertyValue);
    }
    /// <summary>
    /// Write a log event with the LogLevel.Verbose and associated exception
    /// </summary>
    /// <typeparam name="T0"></typeparam>
    /// <typeparam name="T1"></typeparam>
    /// <param name="exception"></param>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValue0"></param>
    /// <param name="propertyValue1"></param>
    public static void Verbose<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    {
        if (LogLevel.Verbose < _logLevel)
        {
            return;
        }

        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        Log.Verbose(exception, messageTemplate, propertyValue0, propertyValue1);
    }
    /// <summary>
    /// Write a log event with the LogLevel.Verbose and associated exception
    /// </summary>
    /// <typeparam name="T0"></typeparam>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <param name="exception"></param>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValue0"></param>
    /// <param name="propertyValue1"></param>
    /// <param name="propertyValue2"></param>
    public static void Verbose<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
    {
        if (LogLevel.Verbose < _logLevel)
        {
            return;
        }

        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        Log.Verbose(exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
    }
    /// <summary>
    /// Write a log event with the LogLevel.Verbose and associated exception
    /// </summary>
    /// <param name="exception"></param>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValues"></param>
    public static void Verbose(Exception exception, string messageTemplate, params object[] propertyValues)
    {
        if (LogLevel.Verbose < _logLevel)
        {
            return;
        }

        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        Log.Verbose(exception, messageTemplate, propertyValues);
    }

    /// <summary>
    /// Write a log event with the LogLevel.Warning
    /// </summary>
    /// <param name="messageTemplate"></param>
    public static void Warning(string messageTemplate)
    {
        if (LogLevel.Warning < _logLevel) return;

        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        Log.Warning(messageTemplate);
    }
    /// <summary>
    /// Write a log event with the LogLevel.Warning
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValue"></param>
    public static void Warning<T>(string messageTemplate, T propertyValue)
    {
        if (LogLevel.Warning < _logLevel) return;

        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        Log.Warning(messageTemplate, propertyValue);
    }
    /// <summary>
    /// Write a log event with the LogLevel.Warning
    /// </summary>
    /// <typeparam name="T0"></typeparam>
    /// <typeparam name="T1"></typeparam>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValue0"></param>
    /// <param name="propertyValue1"></param>
    public static void Warning<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    {
        if (LogLevel.Warning < _logLevel) return;

        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        Log.Warning(messageTemplate, propertyValue0, propertyValue1);
    }
    /// <summary>
    /// Write a log event with the LogLevel.Warning
    /// </summary>
    /// <typeparam name="T0"></typeparam>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValue0"></param>
    /// <param name="propertyValue1"></param>
    /// <param name="propertyValue2"></param>
    public static void Warning<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
    {
        if (LogLevel.Warning < _logLevel) return;

        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        Log.Warning(messageTemplate, propertyValue0, propertyValue1, propertyValue2);
    }
    /// <summary>
    /// Write a log event with the LogLevel.Warning
    /// </summary>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValues"></param>
    public static void Warning(string messageTemplate, params object[] propertyValues)
    {
        if (LogLevel.Warning < _logLevel) return;

        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        Log.Warning(messageTemplate, propertyValues);
    }
    /// <summary>
    /// Write a log event with the LogLevel.Warning
    /// </summary>
    /// <param name="exception"></param>
    /// <param name="messageTemplate"></param>
    public static void Warning(Exception exception, string messageTemplate)
    {
        if (LogLevel.Warning < _logLevel) return;

        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        Log.Warning(exception, messageTemplate);
    }
    /// <summary>
    /// Write a log event with the LogLevel.Warning
    /// </summary>
    /// <param name="messageTemplate"></param>
    /// <param name="exception"></param>
    public static void Warning(string messageTemplate, Exception exception)
    {
        if (LogLevel.Warning < _logLevel) return;

        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        Log.Warning(exception, messageTemplate);
    }
    /// <summary>
    /// Write a log event with the LogLevel.Warning and associated exception
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="exception"></param>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValue"></param>
    public static void Warning<T>(Exception exception, string messageTemplate, T propertyValue)
    {
        if (LogLevel.Warning < _logLevel) return;

        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        Log.Warning(exception, messageTemplate, propertyValue);
    }
    /// <summary>
    /// Write a log event with the LogLevel.Warning and associated exception
    /// </summary>
    /// <typeparam name="T0"></typeparam>
    /// <typeparam name="T1"></typeparam>
    /// <param name="exception"></param>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValue0"></param>
    /// <param name="propertyValue1"></param>
    public static void Warning<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    {
        if (LogLevel.Warning < _logLevel) return;

        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        Log.Warning(exception, messageTemplate, propertyValue0, propertyValue1);
    }
    /// <summary>
    /// Write a log event with the LogLevel.Warning and associated exception
    /// </summary>
    /// <typeparam name="T0"></typeparam>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <param name="exception"></param>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValue0"></param>
    /// <param name="propertyValue1"></param>
    /// <param name="propertyValue2"></param>
    public static void Warning<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
    {
        if (LogLevel.Warning < _logLevel)
        {
            return;
        }

        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        Log.Warning(exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
    }
    /// <summary>
    /// Write a log event with the LogLevel.Warning and associated exception
    /// </summary>
    /// <param name="exception"></param>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValues"></param>
    public static void Warning(Exception exception, string messageTemplate, params object[] propertyValues)
    {
        if (LogLevel.Warning < _logLevel) return;

        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        Log.Warning(exception, messageTemplate, propertyValues);
    }

    /// <summary>
    /// Create a logger that enriches log events via the provided enrichers.
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static ILogger ForContext(Type source)
    {
        return Log.Logger.ForContext(source);
    }
    /// <summary>
    /// Create a logger that marks log events as being from the specified source type.
    /// </summary>
    /// <param name="enricher"></param>
    /// <returns></returns>
    public static ILogger ForContext(ILogEventEnricher enricher)
    {
        return Log.Logger.ForContext(enricher);
    }
    /// <summary>
    /// A logger that will enrich log events as specified.
    /// </summary>
    /// <param name="enrichers"></param>
    /// <returns></returns>
    public static ILogger ForContext(IEnumerable<ILogEventEnricher> enrichers)
    {
        return Log.Logger.ForContext(enrichers);
    }
    /// <summary>
    /// Create a logger that enriches log events with the specified property.
    /// </summary>
    /// <param name="propertyName"></param>
    /// <param name="value"></param>
    /// <param name="destructureObjects"></param>
    /// <returns></returns>
    public static ILogger ForContext(string propertyName, object value, bool destructureObjects = false)
    {
        return Log.Logger.ForContext(propertyName, value, destructureObjects);
    }

    /// <summary>
    ///     Uses configured scalar conversion and destructuring rules to bind a set of properties
    ///     to a message template. Returns false if the template or values are invalid (ILogger
    ///     methods never throw exceptions).
    /// </summary>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValues"></param>
    /// <param name="parsedTemplate"></param>
    /// <param name="boundProperties"></param>
    /// <returns></returns>
    public static bool BindMessageTemplate(string messageTemplate, object[] propertyValues, out MessageTemplate parsedTemplate, out IEnumerable<LogEventProperty> boundProperties)
    {
        return Log.BindMessageTemplate(messageTemplate, propertyValues, out parsedTemplate, out boundProperties);
    }
    /// <summary>
    /// Uses configured scalar conversion and destructuring rules to bind a property value to its captured representation.
    /// </summary>
    /// <param name="propertyName"></param>
    /// <param name="value"></param>
    /// <param name="destructureObjects"></param>
    /// <param name="property"></param>
    /// <returns></returns>
    public static bool BindProperty(string propertyName, object value, bool destructureObjects, out LogEventProperty property)
    {
        return Log.BindProperty(propertyName, value, destructureObjects, out property);
    }

    /// <summary>
    /// Write an event to the log.
    /// </summary>
    /// <param name="logEvent"></param>
    public static void Write(LogEvent logEvent)
    {
        Log.Write(logEvent);
    }
    /// <summary>
    /// Write a log event with the specified level
    /// </summary>
    /// <param name="level"></param>
    /// <param name="messageTemplate"></param>
    public static void Write(LogEventLevel level, string messageTemplate)
    {
        Log.Write(level, messageTemplate);
    }
    /// <summary>
    /// Write a log event with the specified level
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="level"></param>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValue"></param>
    public static void Write<T>(LogEventLevel level, string messageTemplate, T propertyValue)
    {
        Log.Write(level, messageTemplate, propertyValue);
    }
    /// <summary>
    /// Write a log event with the specified level
    /// </summary>
    /// <typeparam name="T0"></typeparam>
    /// <typeparam name="T1"></typeparam>
    /// <param name="level"></param>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValue0"></param>
    /// <param name="propertyValue1"></param>
    public static void Write<T0, T1>(LogEventLevel level, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    {
        Log.Write(level, messageTemplate, propertyValue0, propertyValue1);
    }
    /// <summary>
    /// Write a log event with the specified level
    /// </summary>
    /// <typeparam name="T0"></typeparam>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <param name="level"></param>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValue0"></param>
    /// <param name="propertyValue1"></param>
    /// <param name="propertyValue2"></param>
    public static void Write<T0, T1, T2>(LogEventLevel level, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
    {
        Log.Write(level, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
    }
    /// <summary>
    /// Write a log event with the specified level
    /// </summary>
    /// <param name="level"></param>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValues"></param>
    public static void Write(LogEventLevel level, string messageTemplate, params object[] propertyValues)
    {
        Log.Write(level, messageTemplate, propertyValues);
    }
    /// <summary>
    /// Write a log event with the specified level and associated exception 
    /// </summary>
    /// <param name="level"></param>
    /// <param name="exception"></param>
    /// <param name="messageTemplate"></param>
    public static void Write(LogEventLevel level, Exception exception, string messageTemplate)
    {
        Log.Write(level, exception, messageTemplate);
    }
    /// <summary>
    /// Write a log event with the specified level and associated exception 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="level"></param>
    /// <param name="exception"></param>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValue"></param>
    public static void Write<T>(LogEventLevel level, Exception exception, string messageTemplate, T propertyValue)
    {
        Log.Write(level, exception, messageTemplate, propertyValue);
    }
    /// <summary>
    /// Write a log event with the specified level and associated exception 
    /// </summary>
    /// <typeparam name="T0"></typeparam>
    /// <typeparam name="T1"></typeparam>
    /// <param name="level"></param>
    /// <param name="exception"></param>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValue0"></param>
    /// <param name="propertyValue1"></param>
    public static void Write<T0, T1>(LogEventLevel level, Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    {
        Log.Write(level, exception, messageTemplate, propertyValue0, propertyValue1);
    }
    /// <summary>
    /// Write a log event with the specified level and associated exception 
    /// </summary>
    /// <typeparam name="T0"></typeparam>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <param name="level"></param>
    /// <param name="exception"></param>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValue0"></param>
    /// <param name="propertyValue1"></param>
    /// <param name="propertyValue2"></param>
    public static void Write<T0, T1, T2>(LogEventLevel level, Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
    {
        Log.Write(level, exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
    }
    /// <summary>
    /// Write a log event with the specified level and associated exception 
    /// </summary>
    /// <param name="level"></param>
    /// <param name="exception"></param>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValues"></param>
    public static void Write(LogEventLevel level, Exception exception, string messageTemplate, params object[] propertyValues)
    {
        Log.Write(level, exception, messageTemplate, propertyValues);
    }

    #endregion

}