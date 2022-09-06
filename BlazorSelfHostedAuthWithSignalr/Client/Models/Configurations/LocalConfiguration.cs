namespace BlazorSelfHostedAuthWithSignalr.Client.Models.Configurations;

public class LocalConfiguration
{
    public ChatConfiguration Chat { get; set; } = default!;
    public HttpClientConfiguration Http { get; set; } = default!;
}
