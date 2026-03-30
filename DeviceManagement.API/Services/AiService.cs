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
}
