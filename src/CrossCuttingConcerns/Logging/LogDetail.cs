namespace CrossCuttingConcerns.Logging;

public class LogDetail
{
    public string? TraceId { get; set; }
    public string? LogType { get; set; } // 1: Info, 2: Error, 3: Debug

    public string? RequestIpAddress { get; set; }
    public string? RequestPath { get; set; }
    public string? RequestQuery { get; set; }
    public object? RequestHeader { get; set; }
    public object? RequestBody { get; set; }
    public string? HttpMethod { get; set; }
    
    public int? StatusCode { get; set; }
    public object? ResponseBody { get; set; }

    public string? Exception { get; set; }
    public string? StackTrace { get; set; }
    public string? User { get; set; }

    public DateTime LogDate { get; set; } = DateTime.UtcNow;
}

