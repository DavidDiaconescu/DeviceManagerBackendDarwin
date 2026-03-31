using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using DeviceManagement.API.Models.DTOs;
using DeviceManagement.API.Services.Interfaces;

namespace DeviceManagement.API.Services;

public class AiService : IAiService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;
    private readonly string _model;

    public AiService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _apiKey = configuration["Groq:ApiKey"]!;
        _model = configuration["Groq:Model"]!;
    }

    public async Task<string> GenerateDeviceDescriptionAsync(DeviceDto device)
    {
        var prompt =
            $"Generate a concise, professional one-sentence description for this mobile device: " +
            $"Name: {device.Name}, Manufacturer: {device.Manufacturer}, Type: {device.Type}, " +
            $"OS: {device.OperatingSystem} {device.OSVersion}, RAM: {device.RAM}GB, Processor: {device.Processor}. " +
            $"Return only the description sentence, no extra text.";

        var requestBody = new
        {
            model = _model,
            messages = new[]
            {
                new { role = "user", content = prompt }
            },
            max_tokens = 100
        };

        var json = JsonSerializer.Serialize(requestBody);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var url = "https://api.groq.com/openai/v1/chat/completions";

        try
        {
            using var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
            request.Content = content;

            var response = await _httpClient.SendAsync(request);
            var responseJson = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Groq API error {response.StatusCode}: {responseJson}");
            }

            using var doc = JsonDocument.Parse(responseJson);
            return doc.RootElement
                .GetProperty("choices")[0]
                .GetProperty("message")
                .GetProperty("content")
                .GetString() ?? string.Empty;
        }
        catch (Exception ex)
        {
            throw new Exception($"AiService error: {ex.Message}");
        }
    }

    public async Task<string> GenerateProcurementSuggestionAsync(decimal budgetPerEmployee, int employeeCount, string currency)
    {
        var prompt =
            $"You are a mobile device procurement specialist.\n" +
            $"A company needs to equip {employeeCount} employees with mobile devices.\n" +
            $"The budget per employee is {budgetPerEmployee} {currency}.\n\n" +
            $"Please suggest the best combination of:\n" +
            $"1. A smartphone (with model name and estimated price)\n" +
            $"2. A protective case (with type and estimated price)\n" +
            $"3. A screen protector (with type and estimated price)\n\n" +
            $"Make sure the total for all 3 items fits within the budget per employee.\n" +
            $"Also mention if the budget is tight or comfortable for the suggested items.\n" +
            $"Be specific with model names and realistic prices in {currency}.\n" +
            $"Format the response clearly with sections for each item.";

        var requestBody = new
        {
            model = _model,
            messages = new[]
            {
                new { role = "user", content = prompt }
            },
            max_tokens = 500
        };

        var json = JsonSerializer.Serialize(requestBody);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var url = "https://api.groq.com/openai/v1/chat/completions";

        try
        {
            using var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
            request.Content = content;

            var response = await _httpClient.SendAsync(request);
            var responseJson = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Groq API error {response.StatusCode}: {responseJson}");
            }

            using var doc = JsonDocument.Parse(responseJson);
            return doc.RootElement
                .GetProperty("choices")[0]
                .GetProperty("message")
                .GetProperty("content")
                .GetString() ?? string.Empty;
        }
        catch (Exception ex)
        {
            throw new Exception($"AiService error: {ex.Message}");
        }
    }
}
