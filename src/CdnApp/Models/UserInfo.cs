namespace CdnApp.Models;

public record UserInfo
{
    public string Token { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
}