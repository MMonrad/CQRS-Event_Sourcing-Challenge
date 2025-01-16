namespace PMI.Services;

public class WebhookOptions
{
    public string Uri { get; set; }
    public string ApiKey { get; set; }
    public string KeyParameter { get; set; }
    public string TitleParam { get; set; }
    public string ContentParam { get; set; }
}