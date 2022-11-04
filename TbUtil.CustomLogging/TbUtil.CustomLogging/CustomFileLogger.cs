using Serilog;
using Serilog.Context;
using Serilog.Events;
using TbUtil.CustomLogging.Contracts;
using TbUtil.CustomLogging.Formatting;
using TbUtil.CustomLogging.Utils;

namespace TbUtil.CustomLogging;

/// <summary>
/// Custom file logger that implements a custom logger
/// </summary>
public sealed class CustomFileLogger : ICustomLogger
{

    private readonly ILogger _logger;

    /// <summary>
    /// Custom file logger constructor
    /// </summary>
    /// <param name="entityType"></param>
    /// <param name="entityName"></param>
    /// <param name="fullLogNameAndPath"></param>
    /// <param name="logLevel"></param>
    /// <param name="rollingInterval"></param>
    /// <param name="fileSizeLimitBytes"></param>
    /// <param name="frameIndex"></param>
    public CustomFileLogger(EntityType entityType,
                                string entityName,
                                string fullLogNameAndPath,
                                LogLevel logLevel = LogLevel.Warning,
                                RollingInterval rollingInterval = RollingInterval.Day,
                                long fileSizeLimitBytes = 128000000,
                                int frameIndex = 3)
    {
        AssemblyDetailsActions.SetFrameIndex(frameIndex);
        _logger = CLogger.DefaultConfig(entityType, entityName, logLevel)
                        .WriteTo.File(new CompactJsonFormatter(),
                            fullLogNameAndPath,
                            fileSizeLimitBytes: fileSizeLimitBytes,
                            rollOnFileSizeLimit: true,
                            shared: true,
                            rollingInterval: (Serilog.RollingInterval)rollingInterval)
                        .CreateLogger();
    }

    #region INTERFACE IMPLEMENATION 

    /// <summary>
    /// Determine if events at the specified level will be passed through to the log sinks
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    public bool IsEnabled(LogLevel level)
    {
        return _logger.IsEnabled((LogEventLevel)level);
    }

    /// <summary>
    /// Write a log event with the LogLevel.Debug level
    /// </summary>
    /// <param name="messageTemplate"></param>
    public void Debug(string messageTemplate)
    {
        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        _logger.Debug(messageTemplate);
    }
    /// <summary>
    /// Write a log event with the LogLevel.Debug level
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValue"></param>
    public void Debug<T>(string messageTemplate, T propertyValue)
    {
        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        _logger.Debug(messageTemplate, propertyValue);
    }
    /// <summary>
    /// Write a log event with the LogLevel.Debug level
    /// </summary>
    /// <typeparam name="T0"></typeparam>
    /// <typeparam name="T1"></typeparam>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValue0"></param>
    /// <param name="propertyValue1"></param>
    public void Debug<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    {
        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        _logger.Debug(messageTemplate, propertyValue0, propertyValue1);
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
    public void Debug<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
    {
        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        _logger.Debug(messageTemplate, propertyValue0, propertyValue1, propertyValue2);
    }
    /// <summary>
    /// Write a log event with the LogLevel.Debug level
    /// </summary>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValues"></param>
    public void Debug(string messageTemplate, params object[] propertyValues)
    {
        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        _logger.Debug(messageTemplate, propertyValues);
    }
    /// <summary>
    /// Write a log event with the LogLevel.Debug level and associated exception
    /// </summary>
    /// <param name="exception"></param>
    /// <param name="messageTemplate"></param>
    public void Debug(Exception exception, string messageTemplate)
    {
        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        _logger.Debug(exception, messageTemplate);
    }
    /// <summary>
    /// Write a log event with the LogLevel.Debug level and associated exception
    /// </summary>
    /// <param name="messageTemplate"></param>
    /// <param name="exception"></param>
    public void Debug(string messageTemplate, Exception exception)
    {
        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        _logger.Debug(exception, messageTemplate);
    }
    /// <summary>
    /// Write a log event with the LogLevel.Debug level and associated exception
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="exception"></param>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValue"></param>
    public void Debug<T>(Exception exception, string messageTemplate, T propertyValue)
    {
        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        _logger.Debug(exception, messageTemplate, propertyValue);
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
    public void Debug<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    {
        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        _logger.Debug(exception, messageTemplate, propertyValue0, propertyValue1);
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
    public void Debug<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
    {
        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        _logger.Debug(exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
    }
    /// <summary>
    /// Write a log event with the LogLevel.Debug level and associated exception
    /// </summary>
    /// <param name="exception"></param>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValues"></param>
    public void Debug(Exception exception, string messageTemplate, params object[] propertyValues)
    {
        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        _logger.Debug(exception, messageTemplate, propertyValues);
    }

    /// <summary>
    /// Write a log event with the LogLevel.Error level
    /// </summary>
    /// <param name="messageTemplate"></param>
    public void Error(string messageTemplate)
    {
        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        _logger.Error(messageTemplate);
    }
    /// <summary>
    /// Write a log event with the LogLevel.Error level
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValue"></param>
    public void Error<T>(string messageTemplate, T propertyValue)
    {
        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        _logger.Error(messageTemplate, propertyValue);
    }
    /// <summary>
    /// Write a log event with the LogLevel.Error level
    /// </summary>
    /// <typeparam name="T0"></typeparam>
    /// <typeparam name="T1"></typeparam>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValue0"></param>
    /// <param name="propertyValue1"></param>
    public void Error<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    {
        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        _logger.Error(messageTemplate, propertyValue0, propertyValue1);
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
    public void Error<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
    {
        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        _logger.Error(messageTemplate, propertyValue0, propertyValue1, propertyValue2);
    }
    /// <summary>
    /// Write a log event with the LogLevel.Error level
    /// </summary>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValues"></param>
    public void Error(string messageTemplate, params object[] propertyValues)
    {
        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        _logger.Error(messageTemplate, propertyValues);
    }
    /// <summary>
    /// Write a log event with the LogLevel.Error level and associated exception
    /// </summary>
    /// <param name="exception"></param>
    /// <param name="messageTemplate"></param>
    public void Error(Exception exception, string messageTemplate)
    {
        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        _logger.Error(exception, messageTemplate);
    }
    /// <summary>
    /// Write a log event with the LogLevel.Error level and associated exception
    /// </summary>
    /// <param name="exception"></param>
    /// <param name="messageTemplate"></param>
    public void Error(string messageTemplate, Exception exception)
    {
        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        _logger.Error(exception, messageTemplate);
    }
    /// <summary>
    /// Write a log event with the LogLevel.Error level and associated exception
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="exception"></param>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValue"></param>
    public void Error<T>(Exception exception, string messageTemplate, T propertyValue)
    {
        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        _logger.Error(exception, messageTemplate, propertyValue);
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
    public void Error<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    {
        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        _logger.Error(exception, messageTemplate, propertyValue0, propertyValue1);
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
    public void Error<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
    {
        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        _logger.Error(exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
    }
    /// <summary>
    /// Write a log event with the LogLevel.Error level and associated exception
    /// </summary>
    /// <param name="exception"></param>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValues"></param>
    public void Error(Exception exception, string messageTemplate, params object[] propertyValues)
    {
        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        _logger.Error(exception, messageTemplate, propertyValues);
    }

    /// <summary>
    /// Write a log event with the LogLevel.Fatal level
    /// </summary>
    /// <param name="messageTemplate"></param>
    public void Fatal(string messageTemplate)
    {
        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        _logger.Fatal(messageTemplate);
    }
    /// <summary>
    /// Write a log event with the LogLevel.Fatal level
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValue"></param>
    public void Fatal<T>(string messageTemplate, T propertyValue)
    {
        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        _logger.Fatal(messageTemplate, propertyValue);
    }
    /// <summary>
    /// Write a log event with the LogLevel.Fatal level
    /// </summary>
    /// <typeparam name="T0"></typeparam>
    /// <typeparam name="T1"></typeparam>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValue0"></param>
    /// <param name="propertyValue1"></param>
    public void Fatal<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    {
        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        _logger.Fatal(messageTemplate, propertyValue0, propertyValue1);
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
    public void Fatal<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
    {
        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        _logger.Fatal(messageTemplate, propertyValue0, propertyValue1, propertyValue2);
    }
    /// <summary>
    /// Write a log event with the LogLevel.Fatal level
    /// </summary>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValues"></param>
    public void Fatal(string messageTemplate, params object[] propertyValues)
    {
        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        _logger.Fatal(messageTemplate, propertyValues);
    }
    /// <summary>
    /// Write a log event with the LogLevel.Fatal level and associated exception
    /// </summary>
    /// <param name="exception"></param>
    /// <param name="messageTemplate"></param>
    public void Fatal(Exception exception, string messageTemplate)
    {
        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        _logger.Fatal(exception, messageTemplate);
    }
    /// <summary>
    /// Write a log event with the LogLevel.Fatal level and associated exception
    /// </summary>
    /// <param name="messageTemplate"></param>
    /// <param name="exception"></param>
    public void Fatal(string messageTemplate, Exception exception)
    {
        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        _logger.Fatal(exception, messageTemplate);
    }
    /// <summary>
    /// Write a log event with the LogLevel.Fatal level and associated exception
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="exception"></param>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValue"></param>
    public void Fatal<T>(Exception exception, string messageTemplate, T propertyValue)
    {
        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        _logger.Fatal(exception, messageTemplate, propertyValue);
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
    public void Fatal<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    {
        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        _logger.Fatal(exception, messageTemplate, propertyValue0, propertyValue1);
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
    public void Fatal<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
    {
        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        _logger.Fatal(exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
    }
    /// <summary>
    /// Write a log event with the LogLevel.Fatal level and associated exception
    /// </summary>
    /// <param name="exception"></param>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValues"></param>
    public void Fatal(Exception exception, string messageTemplate, params object[] propertyValues)
    {
        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        _logger.Fatal(exception, messageTemplate, propertyValues);
    }

    /// <summary>
    /// Write a log event with the LogLevel.Information
    /// </summary>
    /// <param name="messageTemplate"></param>
    public void Information(string messageTemplate)
    {
        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        _logger.Information(messageTemplate);
    }
    /// <summary>
    /// Write a log event with the LogLevel.Information
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValue"></param>
    public void Information<T>(string messageTemplate, T propertyValue)
    {
        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        _logger.Information(messageTemplate, propertyValue);
    }
    /// <summary>
    /// Write a log event with the LogLevel.Information
    /// </summary>
    /// <typeparam name="T0"></typeparam>
    /// <typeparam name="T1"></typeparam>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValue0"></param>
    /// <param name="propertyValue1"></param>
    public void Information<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    {
        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        _logger.Information(messageTemplate, propertyValue0, propertyValue1);
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
    public void Information<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
    {
        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        _logger.Information(messageTemplate, propertyValue0, propertyValue1, propertyValue2);
    }
    /// <summary>
    /// Write a log event with the LogLevel.Information
    /// </summary>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValues"></param>
    public void Information(string messageTemplate, params object[] propertyValues)
    {
        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        _logger.Information(messageTemplate, propertyValues);
    }
    /// <summary>
    /// Write a log event with the LogLevel.Information and associated exception
    /// </summary>
    /// <param name="exception"></param>
    /// <param name="messageTemplate"></param>
    public void Information(Exception exception, string messageTemplate)
    {
        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        _logger.Information(exception, messageTemplate);
    }
    /// <summary>
    /// Write a log event with the LogLevel.Information and associated exception
    /// </summary>
    /// <param name="messageTemplate"></param>
    /// <param name="exception"></param>
    public void Information(string messageTemplate, Exception exception)
    {
        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        _logger.Information(exception, messageTemplate);
    }
    /// <summary>
    /// Write a log event with the LogLevel.Information and associated exception
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="exception"></param>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValue"></param>
    public void Information<T>(Exception exception, string messageTemplate, T propertyValue)
    {
        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        _logger.Information(exception, messageTemplate, propertyValue);
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
    public void Information<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    {
        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        _logger.Information(exception, messageTemplate, propertyValue0, propertyValue1);
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
    public void Information<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
    {
        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        _logger.Information(exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
    }
    /// <summary>
    /// Write a log event with the LogLevel.Information and associated exception
    /// </summary>
    /// <param name="exception"></param>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValues"></param>
    public void Information(Exception exception, string messageTemplate, params object[] propertyValues)
    {
        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        _logger.Information(exception, messageTemplate, propertyValues);
    }

    /// <summary>
    ///  Write a log event with the LogLevel.Verbose
    /// </summary>
    /// <param name="messageTemplate"></param>
    public void Verbose(string messageTemplate)
    {
        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        _logger.Verbose(messageTemplate);
    }
    /// <summary>
    /// Write a log event with the LogLevel.Verbose
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValue"></param>
    public void Verbose<T>(string messageTemplate, T propertyValue)
    {
        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        _logger.Verbose(messageTemplate, propertyValue);
    }
    /// <summary>
    /// Write a log event with the LogLevel.Verbose
    /// </summary>
    /// <typeparam name="T0"></typeparam>
    /// <typeparam name="T1"></typeparam>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValue0"></param>
    /// <param name="propertyValue1"></param>
    public void Verbose<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    {
        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        _logger.Verbose(messageTemplate, propertyValue0, propertyValue1);
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
    public void Verbose<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
    {
        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        _logger.Verbose(messageTemplate, propertyValue0, propertyValue1, propertyValue2);
    }
    /// <summary>
    /// Write a log event with the LogLevel.Verbose
    /// </summary>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValues"></param>
    public void Verbose(string messageTemplate, params object[] propertyValues)
    {
        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        _logger.Verbose(messageTemplate, propertyValues);
    }
    /// <summary>
    /// Write a log event with the LogLevel.Verbose
    /// </summary>
    /// <param name="exception"></param>
    /// <param name="messageTemplate"></param>
    public void Verbose(Exception exception, string messageTemplate)
    {
        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        _logger.Verbose(exception, messageTemplate);
    }
    /// <summary>
    /// Write a log event with the LogLevel.Verbose
    /// </summary>
    /// <param name="exception"></param>
    /// <param name="messageTemplate"></param>
    public void Verbose(string messageTemplate, Exception exception)
    {
        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        _logger.Verbose(exception, messageTemplate);
    }
    /// <summary>
    /// Write a log event with the LogLevel.Verbose and associated exception
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="exception"></param>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValue"></param>
    public void Verbose<T>(Exception exception, string messageTemplate, T propertyValue)
    {
        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        _logger.Verbose(exception, messageTemplate, propertyValue);
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
    public void Verbose<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    {
        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        _logger.Verbose(exception, messageTemplate, propertyValue0, propertyValue1);
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
    public void Verbose<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
    {
        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        _logger.Verbose(exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
    }
    /// <summary>
    /// Write a log event with the LogLevel.Verbose and associated exception
    /// </summary>
    /// <param name="exception"></param>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValues"></param>
    public void Verbose(Exception exception, string messageTemplate, params object[] propertyValues)
    {
        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        _logger.Verbose(exception, messageTemplate, propertyValues);
    }

    /// <summary>
    /// Write a log event with the LogLevel.Warning
    /// </summary>
    /// <param name="messageTemplate"></param>
    public void Warning(string messageTemplate)
    {
        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        _logger.Warning(messageTemplate);
    }
    /// <summary>
    /// Write a log event with the LogLevel.Warning
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValue"></param>
    public void Warning<T>(string messageTemplate, T propertyValue)
    {
        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        _logger.Warning(messageTemplate, propertyValue);
    }
    /// <summary>
    /// Write a log event with the LogLevel.Warning
    /// </summary>
    /// <typeparam name="T0"></typeparam>
    /// <typeparam name="T1"></typeparam>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValue0"></param>
    /// <param name="propertyValue1"></param>
    public void Warning<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    {
        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        _logger.Warning(messageTemplate, propertyValue0, propertyValue1);
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
    public void Warning<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
    {
        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        _logger.Warning(messageTemplate, propertyValue0, propertyValue1, propertyValue2);
    }
    /// <summary>
    /// Write a log event with the LogLevel.Warning
    /// </summary>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValues"></param>
    public void Warning(string messageTemplate, params object[] propertyValues)
    {
        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        _logger.Warning(messageTemplate, propertyValues);
    }
    /// <summary>
    /// Write a log event with the LogLevel.Warning
    /// </summary>
    /// <param name="exception"></param>
    /// <param name="messageTemplate"></param>
    public void Warning(Exception exception, string messageTemplate)
    {
        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        _logger.Warning(exception, messageTemplate);
    }
    /// <summary>
    /// Write a log event with the LogLevel.Warning
    /// </summary>
    /// <param name="messageTemplate"></param>
    /// <param name="exception"></param>
    public void Warning(string messageTemplate, Exception exception)
    {
        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        _logger.Warning(exception, messageTemplate);
    }
    /// <summary>
    /// Write a log event with the LogLevel.Warning and associated exception
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="exception"></param>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValue"></param>
    public void Warning<T>(Exception exception, string messageTemplate, T propertyValue)
    {
        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        _logger.Warning(exception, messageTemplate, propertyValue);
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
    public void Warning<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
    {
        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        _logger.Warning(exception, messageTemplate, propertyValue0, propertyValue1);
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
    public void Warning<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
    {
        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        _logger.Warning(exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
    }
    /// <summary>
    /// Write a log event with the LogLevel.Warning and associated exception
    /// </summary>
    /// <param name="exception"></param>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValues"></param>
    public void Warning(Exception exception, string messageTemplate, params object[] propertyValues)
    {
        AssemblyDetails callingAssembly = AssemblyDetailsActions.GetAssemblyDetails();
        using IDisposable prop = LogContext.PushProperty("Caller", callingAssembly.GetCaller);
        LogContext.PushProperty("FilePath", callingAssembly.GetFileName);
        LogContext.PushProperty("LineNumber", callingAssembly.GetLineNumber);

        _logger.Warning(exception, messageTemplate, propertyValues);
    }

    #endregion

}