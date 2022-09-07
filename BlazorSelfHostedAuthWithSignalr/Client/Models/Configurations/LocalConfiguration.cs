using System;

namespace BlazorSelfHostedAuthWithSignalr.Client.Models.Configurations;

public class LocalConfiguration
{
    public ChatConfiguration Chat { get; set; } = default!;
    public HttpClientConfiguration HostApi { get; set; } = default!;
    public Uri BaseAddressUri { get; internal set; } = default!;
}
