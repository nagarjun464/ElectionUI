using System.Net.Http.Json;
using Electionapp.UI.Models;

namespace Electionapp.UI.Services;

public class ElectionApiClient
{
    private readonly HttpClient _http;
    public ElectionApiClient(HttpClient http) => _http = http;

    public async Task<List<ElectionDto>> ListAsync(int limit = 50, string? status = null, CancellationToken ct = default)
    {
        var url = status is null or "" ? $"api/elections?limit={limit}" : $"api/elections?limit={limit}&status={Uri.EscapeDataString(status)}";
        var items = await _http.GetFromJsonAsync<List<ElectionDto>>(url, cancellationToken: ct);
        return items ?? new List<ElectionDto>();
    }

    public async Task<ElectionDto> CreateAsync(CreateElectionDto dto, CancellationToken ct = default)
    {
        var resp = await _http.PostAsJsonAsync("api/elections", dto, cancellationToken: ct);
        resp.EnsureSuccessStatusCode();
        return (await resp.Content.ReadFromJsonAsync<ElectionDto>(cancellationToken: ct))!;
    }

    public async Task UpdateAsync(string id, UpdateElectionDto dto, CancellationToken ct = default)
    {
        var resp = await _http.PutAsJsonAsync($"api/elections/{id}", dto, cancellationToken: ct);
        resp.EnsureSuccessStatusCode();
    }

    public async Task DeleteAsync(string id, CancellationToken ct = default)
    {
        var resp = await _http.DeleteAsync($"api/elections/{id}", ct);
        resp.EnsureSuccessStatusCode();
    }

    public async Task<ElectionDto> GetAsync(string id, CancellationToken ct = default)
    {
        var item = await _http.GetFromJsonAsync<ElectionDto>($"api/elections/{id}", ct);
        return item!;
    }

}
