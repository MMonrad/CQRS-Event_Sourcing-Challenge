namespace PrettyMuchIncredibleBankDemo.Models;

public record Log(DateTimeOffset DateTime, string EntityName, string Message);