using Microsoft.Extensions.Options;

namespace PMI.Services;

public class WebhookService
{
    private readonly HttpClient _httpClient;
    private readonly IOptionsSnapshot<WebhookOptions> _options;

    public WebhookService(IOptionsSnapshot<WebhookOptions> options, IHttpClientFactory httpClientFactory)
    {
        _options = options;
        _httpClient = httpClientFactory.CreateClient();
        _httpClient.BaseAddress = new Uri(_options.Value.Uri);
    }


    public async Task<HttpResponseMessage> SendHook(string title, string payload)
    {
        var uriBuilder = new UriBuilder(_httpClient.BaseAddress!);
        var query = QueryString.Create([
            new KeyValuePair<string, string?>(_options.Value.KeyParameter, _options.Value.ApiKey),
            new KeyValuePair<string, string?>(_options.Value.TitleParam, title),
            new KeyValuePair<string, string?>(_options.Value.ContentParam, payload)
        ]);

        uriBuilder.Query = query.ToString();
        var result = await _httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, uriBuilder.ToString()));
        result.EnsureSuccessStatusCode();
        return result;
    }
}