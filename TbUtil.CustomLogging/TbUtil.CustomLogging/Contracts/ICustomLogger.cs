namespace TbUtil.CustomLogging.Contracts;

/// <summary>
/// Contract for the custom logger
/// </summary>
public interface ICustomLogger
{
    /// <summary>
    /// Determine if events at the specified level will be passed through to the log sinks
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    bool IsEnabled(LogLevel level);

    #region Debug

    /// <summary>
    /// Write a log event with the LogLevel.Debug level
    /// </summary>
    /// <param name="messageTemplate"></param>
    void Debug(string messageTemplate);
    /// <summary>
    /// Write a log event with the LogLevel.Debug level
    /// </summary>
    /// <typeparam name="T0"></typeparam>
    /// <typeparam name="T1"></typeparam>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValue0"></param>
    /// <param name="propertyValue1"></param>
    void Debug<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1);
    /// <summary>
    /// Write a log event with the LogLevel.Debug level
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValue"></param>
    void Debug<T>(string messageTemplate, T propertyValue);
    /// <summary>
    /// Write a log event with the LogLevel.Debug level and associated exception
    /// </summary>
    /// <param name="exception"></param>
    /// <param name="messageTemplate"></param>
    void Debug(Exception exception, string messageTemplate);
    /// <summary>
    /// Write a log event with the LogLevel.Debug level and associated exception
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="exception"></param>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValue"></param>
    void Debug<T>(Exception exception, string messageTemplate, T propertyValue);
    /// <summary>
    /// Write a log event with the LogLevel.Debug level and associated exception
    /// </summary>
    /// <typeparam name="T0"></typeparam>
    /// <typeparam name="T1"></typeparam>
    /// <param name="exception"></param>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValue0"></param>
    /// <param name="propertyValue1"></param>
    void Debug<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1);
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
    void Debug<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2);
    /// <summary>
    /// Write a log event with the LogLevel.Debug level and associated exception
    /// </summary>
    /// <param name="exception"></param>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValues"></param>
    void Debug(Exception exception, string messageTemplate, params object[] propertyValues);
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
    void Debug<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2);
    /// <summary>
    /// Write a log event with the LogLevel.Debug level
    /// </summary>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValues"></param>
    void Debug(string messageTemplate, params object[] propertyValues);

    #endregion Debug

    #region Error

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
    void Error<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2);
    /// <summary>
    /// Write a log event with the LogLevel.Error level and associated exception
    /// </summary>
    /// <param name="exception"></param>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValues"></param>
    void Error(Exception exception, string messageTemplate, params object[] propertyValues);
    /// <summary>
    /// Write a log event with the LogLevel.Error level
    /// </summary>
    /// <param name="messageTemplate"></param>
    void Error(string messageTemplate);
    /// <summary>
    /// Write a log event with the LogLevel.Error level
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValue"></param>
    void Error<T>(string messageTemplate, T propertyValue);
    /// <summary>
    /// Write a log event with the LogLevel.Error level
    /// </summary>
    /// <typeparam name="T0"></typeparam>
    /// <typeparam name="T1"></typeparam>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValue0"></param>
    /// <param name="propertyValue1"></param>
    void Error<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1);
    /// <summary>
    /// Write a log event with the LogLevel.Error level and associated exception
    /// </summary>
    /// <param name="exception"></param>
    /// <param name="messageTemplate"></param>
    void Error(Exception exception, string messageTemplate);
    /// <summary>
    /// Write a log event with the LogLevel.Error level
    /// </summary>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValues"></param>
    void Error(string messageTemplate, params object[] propertyValues);
    /// <summary>
    /// Write a log event with the LogLevel.Error level and associated exception
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="exception"></param>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValue"></param>
    void Error<T>(Exception exception, string messageTemplate, T propertyValue);
    /// <summary>
    /// Write a log event with the LogLevel.Error level and associated exception
    /// </summary>
    /// <typeparam name="T0"></typeparam>
    /// <typeparam name="T1"></typeparam>
    /// <param name="exception"></param>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValue0"></param>
    /// <param name="propertyValue1"></param>
    void Error<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1);
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
    void Error<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2);

    #endregion Error

    #region Fatal

    /// <summary>
    /// Write a log event with the LogLevel.Fatal level
    /// </summary>
    /// <param name="messageTemplate"></param>
    void Fatal(string messageTemplate);
    /// <summary>
    /// Write a log event with the LogLevel.Fatal level
    /// </summary>
    /// <typeparam name="T0"></typeparam>
    /// <typeparam name="T1"></typeparam>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValue0"></param>
    /// <param name="propertyValue1"></param>
    void Fatal<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1);
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
    void Fatal<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2);
    /// <summary>
    /// Write a log event with the LogLevel.Fatal level
    /// </summary>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValues"></param>
    void Fatal(string messageTemplate, params object[] propertyValues);
    /// <summary>
    /// Write a log event with the LogLevel.Fatal level and associated exception
    /// </summary>
    /// <param name="exception"></param>
    /// <param name="messageTemplate"></param>
    void Fatal(Exception exception, string messageTemplate);
    /// <summary>
    /// Write a log event with the LogLevel.Fatal level and associated exception
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="exception"></param>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValue"></param>
    void Fatal<T>(Exception exception, string messageTemplate, T propertyValue);
    /// <summary>
    /// Write a log event with the LogLevel.Fatal level and associated exception
    /// </summary>
    /// <typeparam name="T0"></typeparam>
    /// <typeparam name="T1"></typeparam>
    /// <param name="exception"></param>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValue0"></param>
    /// <param name="propertyValue1"></param>
    void Fatal<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1);
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
    void Fatal<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2);
    /// <summary>
    /// Write a log event with the LogLevel.Fatal level and associated exception
    /// </summary>
    /// <param name="exception"></param>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValues"></param>
    void Fatal(Exception exception, string messageTemplate, params object[] propertyValues);
    /// <summary>
    /// Write a log event with the LogLevel.Fatal level
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValue"></param>
    void Fatal<T>(string messageTemplate, T propertyValue);

    #endregion Fatal

    #region Information

    /// <summary>
    /// Write a log event with the LogLevel.Information
    /// </summary>
    /// <typeparam name="T0"></typeparam>
    /// <typeparam name="T1"></typeparam>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValue0"></param>
    /// <param name="propertyValue1"></param>
    void Information<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1);
    /// <summary>
    /// Write a log event with the LogLevel.Information and associated exception
    /// </summary>
    /// <param name="exception"></param>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValues"></param>
    void Information(Exception exception, string messageTemplate, params object[] propertyValues);
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
    void Information<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2);
    /// <summary>
    /// Write a log event with the LogLevel.Information and associated exception
    /// </summary>
    /// <typeparam name="T0"></typeparam>
    /// <typeparam name="T1"></typeparam>
    /// <param name="exception"></param>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValue0"></param>
    /// <param name="propertyValue1"></param>
    void Information<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1);
    /// <summary>
    /// Write a log event with the LogLevel.Information and associated exception
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="exception"></param>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValue"></param>
    void Information<T>(Exception exception, string messageTemplate, T propertyValue);
    /// <summary>
    /// Write a log event with the LogLevel.Information and associated exception
    /// </summary>
    /// <param name="exception"></param>
    /// <param name="messageTemplate"></param>
    void Information(Exception exception, string messageTemplate);
    /// <summary>
    /// Write a log event with the LogLevel.Information
    /// </summary>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValues"></param>
    void Information(string messageTemplate, params object[] propertyValues);
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
    void Information<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2);
    /// <summary>
    /// Write a log event with the LogLevel.Information
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValue"></param>
    void Information<T>(string messageTemplate, T propertyValue);
    /// <summary>
    /// Write a log event with the LogLevel.Information
    /// </summary>
    /// <param name="messageTemplate"></param>
    void Information(string messageTemplate);

    #endregion Information

    #region Verbose

    /// <summary>
    ///  Write a log event with the LogLevel.Verbose
    /// </summary>
    /// <param name="messageTemplate"></param>
    void Verbose(string messageTemplate);
    /// <summary>
    /// Write a log event with the LogLevel.Verbose
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValue"></param>
    void Verbose<T>(string messageTemplate, T propertyValue);
    /// <summary>
    /// Write a log event with the LogLevel.Verbose
    /// </summary>
    /// <typeparam name="T0"></typeparam>
    /// <typeparam name="T1"></typeparam>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValue0"></param>
    /// <param name="propertyValue1"></param>
    void Verbose<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1);
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
    void Verbose<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2);
    /// <summary>
    /// Write a log event with the LogLevel.Verbose and associated exception
    /// </summary>
    /// <param name="exception"></param>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValues"></param>
    void Verbose(Exception exception, string messageTemplate, params object[] propertyValues);
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
    void Verbose<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2);
    /// <summary>
    /// Write a log event with the LogLevel.Verbose and associated exception
    /// </summary>
    /// <typeparam name="T0"></typeparam>
    /// <typeparam name="T1"></typeparam>
    /// <param name="exception"></param>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValue0"></param>
    /// <param name="propertyValue1"></param>
    void Verbose<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1);
    /// <summary>
    /// Write a log event with the LogLevel.Verbose and associated exception
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="exception"></param>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValue"></param>
    void Verbose<T>(Exception exception, string messageTemplate, T propertyValue);
    /// <summary>
    /// Write a log event with the LogLevel.Verbose
    /// </summary>
    /// <param name="exception"></param>
    /// <param name="messageTemplate"></param>
    void Verbose(Exception exception, string messageTemplate);
    /// <summary>
    /// Write a log event with the LogLevel.Verbose
    /// </summary>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValues"></param>
    void Verbose(string messageTemplate, params object[] propertyValues);

    #endregion Verbose

    #region Warning

    /// <summary>
    /// Write a log event with the LogLevel.Warning and associated exception
    /// </summary>
    /// <typeparam name="T0"></typeparam>
    /// <typeparam name="T1"></typeparam>
    /// <param name="exception"></param>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValue0"></param>
    /// <param name="propertyValue1"></param>
    void Warning<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1);
    /// <summary>
    /// Write a log event with the LogLevel.Warning and associated exception
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="exception"></param>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValue"></param>
    void Warning<T>(Exception exception, string messageTemplate, T propertyValue);
    /// <summary>
    /// Write a log event with the LogLevel.Warning
    /// </summary>
    /// <param name="messageTemplate"></param>
    void Warning(string messageTemplate);
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
    void Warning<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2);
    /// <summary>
    /// Write a log event with the LogLevel.Warning
    /// </summary>
    /// <param name="exception"></param>
    /// <param name="messageTemplate"></param>
    void Warning(Exception exception, string messageTemplate);
    /// <summary>
    /// Write a log event with the LogLevel.Warning and associated exception
    /// </summary>
    /// <param name="exception"></param>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValues"></param>
    void Warning(Exception exception, string messageTemplate, params object[] propertyValues);
    /// <summary>
    /// Write a log event with the LogLevel.Warning
    /// </summary>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValues"></param>
    void Warning(string messageTemplate, params object[] propertyValues);
    /// <summary>
    /// Write a log event with the LogLevel.Warning
    /// </summary>
    /// <typeparam name="T0"></typeparam>
    /// <typeparam name="T1"></typeparam>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValue0"></param>
    /// <param name="propertyValue1"></param>
    void Warning<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1);
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
    void Warning<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2);
    /// <summary>
    /// Write a log event with the LogLevel.Warning
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="messageTemplate"></param>
    /// <param name="propertyValue"></param>
    void Warning<T>(string messageTemplate, T propertyValue);

    #endregion Warning

}