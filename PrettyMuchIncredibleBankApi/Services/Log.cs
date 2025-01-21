namespace PMI.Services;

public record Log(DateTimeOffset DateTime, string EntityName, string Message);