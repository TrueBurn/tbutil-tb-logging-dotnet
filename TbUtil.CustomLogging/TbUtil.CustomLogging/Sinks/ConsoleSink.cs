using Serilog.Configuration;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting;
using Serilog;
using Serilog.Formatting.Compact;

namespace TbUtil.CustomLogging.Sinks;

/// <summary>
/// We using the RenderedCompactJsonFormatter to allow the fields as top-level fields - that way we can inforce a policy with set mini fields required in JSON model
/// Handy lnk to look at - https://github.com/serilog/serilog-formatting-compact regarding the JSON formatter and options
/// </summary>
[System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0052:Remove unread private members", Justification = "<Pending>")]
public sealed class ConsoleSink : ILogEventSink
{
    private readonly IFormatProvider _formatProvider;
    private readonly ITextFormatter _formatter;

    /// <summary>
    /// Override to extend the Console output to be fixed JSON
    /// </summary>
    /// <param name="formatProvider"></param>
    public ConsoleSink(IFormatProvider formatProvider)
    {
        _formatProvider = formatProvider;
        //_formatter = new RenderedCompactJsonFormatter();                            // enforce to write to JSON - don't allow overrides on this
        _formatter = new CompactJsonFormatter();
    }

    /// <summary>
    /// Write an event to console in JSON
    /// </summary>
    /// <param name="logEvent"></param>
    public void Emit(LogEvent logEvent)
    {
        StringWriter sw = new();
        _formatter.Format(logEvent, sw);
        string message = sw.ToString();

        Console.WriteLine(message);
    }

}

/// <summary>
/// Extensions for the sink
/// </summary>
public static class SinkExtensions
{
    /// <summary>
    /// Writes to console
    /// </summary>
    /// <param name="loggerConfiguration"></param>
    /// <param name="formatProvider"></param>
    /// <returns></returns>
    public static LoggerConfiguration ConsoleLog(this LoggerSinkConfiguration loggerConfiguration, IFormatProvider formatProvider = null)
    {
        return loggerConfiguration.Sink(new ConsoleSink(formatProvider));
    }

}
