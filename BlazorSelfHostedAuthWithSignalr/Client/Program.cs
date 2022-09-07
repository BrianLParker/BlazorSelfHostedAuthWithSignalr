using System;
using System.Net.Http;
using System.Threading.Tasks;
using BlazorSelfHostedAuthWithSignalr.Client.Models.Configurations;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorSelfHostedAuthWithSignalr.Client;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);

        //Hard Coded configuration
        //LocalConfiguration configuration = GetDefaultConfiguration();

        //Load configuration from appsettings.json
        LocalConfiguration configuration = builder.Configuration.Get<LocalConfiguration>();

        configuration.BaseAddressUri = new Uri(builder.HostEnvironment.BaseAddress);

        builder.Services.AddSingleton(configuration);
        builder.RootComponents.Add<App>(selector: "#app");
        builder.RootComponents.Add<HeadOutlet>(selector: "head::after");
        builder.Services.AddHttpClient(
            name: configuration.HostApi.ClientName,
            client => client.BaseAddress = configuration.BaseAddressUri)
            .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

        builder.Services.AddScoped(typeof(AccountClaimsPrincipalFactory<RemoteUserAccount>), typeof(RolesAccountClaimsPrincipalFactory));

        builder.Services.AddScoped(serviceProvider =>
            serviceProvider.GetRequiredService<IHttpClientFactory>().CreateClient(name: configuration.HostApi.ClientName));

        builder.Services.AddApiAuthorization();
        await builder.Build().RunAsync();
    }

    private static LocalConfiguration GetDefaultConfiguration()
    {
        return new()
        {
            Chat = new() { Endpoint = "chathub" },
            HostApi = new() { ClientName = "SelfHost.ServerAPI" }
        };
    }
}
