using Microsoft.Extensions.Options;
using PrettyMuchIncredibleBankDemo.Models;

namespace PrettyMuchIncredibleBankDemo.Services;

public class ApiService
{
    private readonly HttpClient _httpClient;
    private readonly IOptionsSnapshot<PmiOptions> _pmiOptions;

    public ApiService(IOptionsSnapshot<PmiOptions> pmiOptions, IHttpClientFactory httpClientFactory)
    {
        _pmiOptions = pmiOptions;
        _httpClient = httpClientFactory.CreateClient();
        _httpClient.BaseAddress = new Uri(_pmiOptions.Value.ApiUrl);
    }

    public async Task<List<AccountStatement>?> GetAccountsAsync()
    {
        var response = await _httpClient.GetAsync("/Api/accounts/list");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<AccountStatement>>();
    }

    public async Task<string> CreateAccountAsync()
    {
        var response = await _httpClient.PostAsync("/Api/accounts", null);
        response.EnsureSuccessStatusCode();
        var model = await response.Content.ReadAsStringAsync();
        return model;
    }

    public async Task<AccountStatement?> GetAccountAsync(string id)
    {
        var response = await _httpClient.GetAsync($"/Api/accounts/{id}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<AccountStatement>();
    }
}
