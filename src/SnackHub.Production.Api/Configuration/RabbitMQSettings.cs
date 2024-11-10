namespace SnackHub.Production.Api.Configuration;

public record RabbitMQSettings
{
    public required string Host { get; set; }
    public required string UserName { get; set; }
    public required string Password { get; set; }
}