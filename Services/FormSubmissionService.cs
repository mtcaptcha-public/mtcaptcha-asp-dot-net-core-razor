public class FormSubmissionService
{
    private readonly MTCaptchaService _mtCaptchaService;
    private readonly IConfiguration _configuration;
    private string _privateKey;
    private string _tokenValidationURL;

    public FormSubmissionService(MTCaptchaService mtCaptchaService, IConfiguration configuration)
    {
        _mtCaptchaService = mtCaptchaService;
        _configuration = configuration;
        _privateKey = _configuration["MTCaptcha:PrivateKey"]; // Read the site key from configuration
        _tokenValidationURL = _configuration["MTCaptcha:TokenValidationURL"]; // Read the validation URL from configuration
    }

    public async Task<bool> SubmitLoginFormAsync(LoginForm formData, string mtcToken)
    {
        var isCaptchaValid = await _mtCaptchaService.IsMTCaptchaValid(_tokenValidationURL,_privateKey, mtcToken);
        if (!isCaptchaValid)
        {
            return false;  // Captcha validation failed
        }
        return AuthenticateUser(formData.Email, formData.Password);
    }

    private bool AuthenticateUser(string email, string password)
    {
        // Authentication logic here
        return email == "user@email.com" && password == "userpassword";
    }
}