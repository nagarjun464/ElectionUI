using System.Net.Http.Json;
using Electionapp.UI.Models; 

namespace Electionapp.UI.Services;

public class PresidentCountyApiClient
{
    private readonly HttpClient _http;
    public PresidentCountyApiClient(HttpClient http) => _http = http;

    // List all records
    public async Task<List<PresidentCountyDto>> ListAsync(int page = 1, int pageSize = 50, string? search = null)
    {
        var url = $"api/PresidentCounty?page={page}&pageSize={pageSize}";

        if (!string.IsNullOrWhiteSpace(search))
            url += $"&search={Uri.EscapeDataString(search)}";

        return await _http.GetFromJsonAsync<List<PresidentCountyDto>>(url)
               ?? new List<PresidentCountyDto>();
    }


    //public async Task<List<PresidentCountyDto>> ListAsync(int limit = 50, CancellationToken ct = default)
    //{
    //    var url = $"api/PresidentCounty?limit={limit}";
    //    var items = await _http.GetFromJsonAsync<List<PresidentCountyDto>>(url, cancellationToken: ct);
    //    return items ?? new List<PresidentCountyDto>();
    //}

    // Get by Id
    public async Task<PresidentCountyDto> GetAsync(string id, CancellationToken ct = default)
    {
        var item = await _http.GetFromJsonAsync<PresidentCountyDto>($"api/PresidentCounty/{id}", ct);
        return item!;
    }

    // Create
    public async Task<PresidentCountyDto> CreateAsync(CreatePresidentCountyDto dto, CancellationToken ct = default)
    {
        var resp = await _http.PostAsJsonAsync("api/PresidentCounty", dto, cancellationToken: ct);
        resp.EnsureSuccessStatusCode();
        return (await resp.Content.ReadFromJsonAsync<PresidentCountyDto>(cancellationToken: ct))!;
    }

    // Update
    public async Task UpdateAsync(string id, UpdatePresidentCountyDto dto, CancellationToken ct = default)
    {
        var resp = await _http.PutAsJsonAsync($"api/PresidentCounty/{id}", dto, ct);
        Console.WriteLine($"UpdateAsync response status code: {resp.StatusCode}");
        resp.EnsureSuccessStatusCode();
    }

    // Delete
    public async Task DeleteAsync(string id, CancellationToken ct = default)
    {
        var resp = await _http.DeleteAsync($"api/PresidentCounty/{id}", ct);
        resp.EnsureSuccessStatusCode();
    }
}
