namespace SnackHub.Domain.Models.Gateways;

public class SignUpRequest
{
    public SignUpRequest(string name, string userName, string password, string email)
    {
        Name = name;
        Username = userName;
        Password = password;
        Email = email;
    }

    public string Name { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
}