namespace PMI.RequestModels;

public record TransferRequestModel(string SourceAccountId, string TargetAccountId, decimal Amount);