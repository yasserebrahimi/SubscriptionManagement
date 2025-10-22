using Serilog.Core;
using Serilog.Events;

namespace SubscriptionManagement.WebAPI.Logging;

public class MaskingEnricher : ILogEventEnricher
{
    private static readonly HashSet<string> Sensitive = new(StringComparer.OrdinalIgnoreCase)
    {
        "Authorization", "authorization", "access_token", "refresh_token", "token", "email", "password"
    };

    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        // Mask headers if logged as a property named "RequestHeaders" or similar
        foreach (var propName in logEvent.Properties.Keys.ToList())
        {
            if (logEvent.Properties[propName] is StructureValue sv)
            {
                var masked = new List<LogEventProperty>();
                foreach (var p in sv.Properties)
                {
                    if (Sensitive.Contains(p.Name))
                    {
                        masked.Add(new LogEventProperty(p.Name, new ScalarValue("***")));
                    }
                    else
                    {
                        masked.Add(p);
                    }
                }
                logEvent.RemovePropertyIfPresent(propName);
                logEvent.AddPropertyIfAbsent(new LogEventProperty(propName, new StructureValue(masked, sv.TypeTag)));
            }
        }

        // Normalize correlation id if present
        if (!logEvent.Properties.ContainsKey("CorrelationId") &&
            logEvent.Properties.TryGetValue("X-Correlation-ID", out var cid))
        {
            logEvent.AddPropertyIfAbsent(new LogEventProperty("CorrelationId", cid));
        }
    }
}
