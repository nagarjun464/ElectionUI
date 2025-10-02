using System.Net.Http.Json;
using Electionapp.UI.Models;

namespace Electionapp.UI.Services;
public class LoginAPI
{
    private readonly HttpClient _http;

    public LoginAPI(HttpClient http)
    {
        _http = http;
    }

    public async Task<(bool Success, Dictionary<string, string[]> Errors)> LoginAsync(LoginDto dto)
    {
        var response = await _http.PostAsJsonAsync("api/Auth/login", dto);

        if (response.IsSuccessStatusCode)
            return (true, new Dictionary<string, string[]>());

        var errorString = await response.Content.ReadAsStringAsync();
        var errors = new Dictionary<string, string[]>();

        try
        {
            var json = System.Text.Json.JsonDocument.Parse(errorString);

            if (json.RootElement.TryGetProperty("errors", out var errorObj))
            {
                foreach (var prop in errorObj.EnumerateObject())
                {
                    errors[prop.Name] = prop.Value.EnumerateArray()
                        .Select(x => x.GetString() ?? "")
                        .ToArray();
                }
            }
            
            else if (json.RootElement.TryGetProperty("error", out var msg))
            {
                errors["General"] = new[] { msg.GetString() ?? "Invalid credentials" };
            }

            else if (json.RootElement.TryGetProperty("title", out var title))
            {
                // Show only the "title" if no detailed errors exist
                errors["General"] = new[] { title.GetString() ?? "Something went wrong." };
            }
        }
        catch
        {
            errors["General"] = new[] { "Unexpected error occurred" };
        }

        return (false, errors);
    }
}
