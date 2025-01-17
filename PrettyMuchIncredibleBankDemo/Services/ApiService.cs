using Microsoft.Extensions.Options;
using PMI.RequestModels;
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
    public async Task<List<AccountStatement>?> SearchAccountsAsync(string? query)
    {
        var response = await _httpClient.GetAsync($"/Api/accounts/search?query={query}");
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
        if (response.StatusCode == System.Net.HttpStatusCode.NotFound || response.StatusCode == System.Net.HttpStatusCode.NoContent)
        {
            return null;
        }
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<AccountStatement>();
    }

    public async Task<AccountStatement?> Deposit(string id, decimal amount)
    {
        var response = await _httpClient
            .PostAsJsonAsync("/Api/accounts/deposit", new DepositRequestModel(id, amount));
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<AccountStatement>();
    }
    public async Task<AccountStatement?> Withdraw(string id, decimal amount)
    {
        var response = await _httpClient
            .PostAsJsonAsync("/Api/accounts/withdraw", new WithdrawalRequestModel(id, amount));
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<AccountStatement>();
    }
    public async Task<List<AccountStatement>?> Transfer(string from, string to, decimal amount)
    {
        var response = await _httpClient
            .PostAsJsonAsync("/Api/accounts/transfer", new TransferRequestModel(from, to, amount));
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<AccountStatement>>();
    }
}
