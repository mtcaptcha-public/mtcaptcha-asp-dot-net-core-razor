public class AppConfigService
{
    public string SiteKey { get; set; }
    public string PrivateKey { get; set; }
    public string TokenValidationURL { get; set; }

    public AppConfigService(IConfiguration configuration)
    {
        SiteKey = configuration["MTCaptcha:SiteKey"] ?? throw new InvalidOperationException("SiteKey is not configured.");
        PrivateKey = configuration["MTCaptcha:PrivateKey"] ?? throw new InvalidOperationException("PrivateKey is not configured.");
        TokenValidationURL = configuration["MTCaptcha:TokenValidationURL"] ?? throw new InvalidOperationException("TokenValidationURL is not configured.");
    }
}