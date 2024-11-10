namespace SnackHub.Domain.Models.Gateways.Models;

public class AuthenticationResult
{
    public string IdToken { get; set; }
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}