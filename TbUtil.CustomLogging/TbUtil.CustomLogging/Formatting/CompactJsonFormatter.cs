using Serilog.Events;
using Serilog.Formatting;
using Serilog.Formatting.Json;

namespace TbUtil.CustomLogging.Formatting;

/// <summary>
/// Custom formatter. Sets as single line json
/// </summary>
public sealed class CompactJsonFormatter : ITextFormatter
{
    private readonly JsonValueFormatter _valueFormatter;

    /// <summary>
    /// Custom formatter constructor
    /// </summary>
    /// <param name="valueFormatter"></param>
    public CompactJsonFormatter(JsonValueFormatter valueFormatter = null)
    {
        this._valueFormatter = valueFormatter ?? new JsonValueFormatter(typeTagName: "$type");
    }

    /// <summary>
    /// Returning a log-level desc
    /// </summary>
    /// <param name="logLevel"></param>
    /// <returns></returns>
    private static string GetLogLevel(LogEventLevel logLevel) => logLevel switch
    {
        LogEventLevel.Verbose => "verbose",
        LogEventLevel.Debug => "debug",
        LogEventLevel.Information => "information",
        LogEventLevel.Warning => "warning",
        LogEventLevel.Error => "error",
        LogEventLevel.Fatal => "fatal",
        _ => "none",
    };

    /// <summary>
    /// Formats the log event and outputs based on the provided writer
    /// </summary>
    /// <param name="logEvent"></param>
    /// <param name="output"></param>
    public void Format(LogEvent logEvent, TextWriter output)
    {

        ArgumentNullException.ThrowIfNull(logEvent);
        ArgumentNullException.ThrowIfNull(output);
        ArgumentNullException.ThrowIfNull(_valueFormatter);

        //----------------------------[ @timestamp ]------------------------
        output.Write("{\"@timestamp\":\"");
        output.Write(logEvent.Timestamp.UtcDateTime.ToString("O"));

        //----------------------------[ CurrentDateTime ]------------------------
        output.Write("\",\"LocalDateTime\":\"");
        output.Write(logEvent.Timestamp.DateTime.ToString("O"));

        //----------------------------[ Message ]------------------------
        output.Write("\",\"Message\":");
        string message = logEvent.MessageTemplate.Render(logEvent.Properties);
        JsonValueFormatter.WriteQuotedJsonString(message, output);

        //----------------------------[ Guid or IDX ]------------------------
        //output.Write(",\"Guid\":\"");
        //var id = Guid.NewGuid();
        //output.Write(id.ToString());
        //output.Write('"');

        //----------------------------[ LogLevel ]------------------------
        output.Write(",\"LogLevel\":\"");
        output.Write(GetLogLevel(logEvent.Level));
        output.Write('\"');

        //--(s)------------------------[ Metadata section ]------------------------
        output.Write(",\"Metadata\":{");

        long numberOfPropMapped = 0;
        foreach (KeyValuePair<string, LogEventPropertyValue> property in logEvent.Properties)
        {
            numberOfPropMapped++;

            string name = property.Key;
            if (name.Length > 0 && name[0] == '@')
            {
                // Escape first '@' by doubling
                name = '@' + name;
            }

            JsonValueFormatter.WriteQuotedJsonString(name, output);
            output.Write(':');
            _valueFormatter.Format(property.Value, output);

            if (numberOfPropMapped < logEvent.Properties.Count)
            {
                output.Write(',');
            }
        }

        if (logEvent.Exception is not null)
        {
            output.Write(",\"Exception\":");
            JsonValueFormatter.WriteQuotedJsonString(logEvent.Exception.ToString(), output);
        }

        output.Write("}");
        //--(e)------------------------[ Metadata section ]------------------------

        output.Write('}');
        output.WriteLine();
    }

}
