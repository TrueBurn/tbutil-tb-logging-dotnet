namespace TbUtil.CustomLogging;

/// <summary>
/// Specifies the meaning and relative importance of a log event.
/// </summary>
public enum LogLevel
{
    /// <summary>
    /// A message that provides verbose information for troubleshooting.
    /// </summary>
    Verbose = 0,
    /// <summary>
    /// A message that provides information about debugging. 
    /// </summary>
    Debug = 1,
    /// <summary>
    /// A message that provides additional information about the current operation.  
    /// </summary>
    Information = 2,
    /// <summary>
    /// A message that indicates an unexpected condition occurred, but the operation can continue.   
    /// </summary>
    Warning = 3,
    /// <summary>
    /// A message that indicates that an operation unexpectedly failed. 
    /// </summary>
    Error = 4,
    /// <summary>
    /// A message that indicates an unrecoverable error has occurred, and the application cannot continue.
    /// </summary>
    Fatal = 5
}

/// <summary>
/// Specifies the meaning and relative importance of an applications role
/// </summary>
public enum EntityType
{
    /// <summary>
    /// System is an API
    /// </summary>
    API,
    /// <summary>
    /// System is a serverless function
    /// </summary>
    Function,
    /// <summary>
    /// System is a background service worker
    /// </summary>
    Service,
    /// <summary>
    /// System is a web app with back end and front end
    /// </summary>
    WebApp
}

/// <summary>
/// Specifies the frequency at which the log file should roll.
/// </summary>
public enum RollingInterval
{
    /// <summary>
    /// The log file will never roll; no time period information will be appended to the log file name.
    /// </summary>
    Infinite = 0,
    /// <summary>
    /// Roll every year. File names will have a four-digit year appended in the pattern yyyy.
    /// </summary>
    Year = 1,
    /// <summary>
    /// Roll every calendar month. File names will have yyyyMM appended.
    /// </summary>
    Month = 2,
    /// <summary>
    /// Roll every day. File names will have yyyyMMdd appended.
    /// </summary>
    Day = 3,
    /// <summary>
    /// Roll every hour. File names will have yyyyMMddHH appended.
    /// </summary>
    Hour = 4,
    /// <summary>
    /// Roll every minute. File names will have yyyyMMddHHmm appended.
    /// </summary>
    Minute = 5
}