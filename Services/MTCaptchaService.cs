using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;


public class MTCaptchaService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly string _privateKey;
    private readonly string _tokenValidationURL;
    public MTCaptchaService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        // _configuration = configuration;
        _privateKey = configuration["MTCaptcha:PrivateKey"];
        _tokenValidationURL = configuration["MTCaptcha:TokenValidationURL"];
    }

    public async Task<bool> IsMTCaptchaValid(string tokenValidationURL, string privateKey, string mtcToken)
    {
        // Construct the URL with query parameters
        var url = $"{tokenValidationURL}?privatekey={privateKey}&token={mtcToken}";
        
        // Send a GET request to the URL
        var response = await _httpClient.GetAsync(url);
        
        // Read the response as JSON
        var MTCaptchaResponse = await response.Content.ReadFromJsonAsync<MTCaptchaResponse>();
        
        // Check if the response was successful and return accordingly
        return MTCaptchaResponse?.Success ?? false;
    }

    private class MTCaptchaResponse
    {
        public bool Success { get; set; }
    }
}