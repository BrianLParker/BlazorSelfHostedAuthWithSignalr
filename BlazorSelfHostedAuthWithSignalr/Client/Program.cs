using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorSelfHostedAuthWithSignalr.Client;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>(selector: "#app");
        builder.RootComponents.Add<HeadOutlet>(selector: "head::after");

        builder.Services.AddHttpClient(name: "BlazorSelfHostedAuthWithSignalr.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
            .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

        // Supply HttpClient instances that include access tokens when making requests to the server project
        builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient(name: "BlazorSelfHostedAuthWithSignalr.ServerAPI"));

        builder.Services.AddApiAuthorization();

        await builder.Build().RunAsync();
    }
}
