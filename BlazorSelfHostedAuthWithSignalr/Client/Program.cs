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
        var configuration = builder.Configuration.Get<LocalConfiguration>();
        var baseAddressUri = new Uri(builder.HostEnvironment.BaseAddress);

        builder.Services.AddSingleton(configuration);
        builder.RootComponents.Add<App>(selector: "#app");
        builder.RootComponents.Add<HeadOutlet>(selector: "head::after");

        builder.Services.AddHttpClient(name: configuration.Http.ClientName, client => client.BaseAddress = baseAddressUri)
            .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

        builder.Services.AddScoped(serviceProvider =>
            serviceProvider.GetRequiredService<IHttpClientFactory>().CreateClient(name: configuration.Http.ClientName));

        builder.Services.AddApiAuthorization();

        await builder.Build().RunAsync();
    }
}
