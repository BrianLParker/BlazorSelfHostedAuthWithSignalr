// ----------------------------------------------------
// Copyright ©️ 2022, Brian Parker. All rights reserved.
// ----------------------------------------------------

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;

namespace BlazorSelfHostedAuthWithSignalr.Server.Models.Configurations;

public class ConfigureChatHubBearerToken : IPostConfigureOptions<JwtBearerOptions>
{
    private readonly LocalConfiguration config;

    public ConfigureChatHubBearerToken(IConfiguration configuration) =>
        config = configuration.Get<LocalConfiguration>();

    public void PostConfigure(string name, JwtBearerOptions options)
    {
        Func<MessageReceivedContext, Task> originalOnMessageReceived = options.Events.OnMessageReceived;

        options.Events.OnMessageReceived = async context =>
        {
            await originalOnMessageReceived(context);

            if (IsNullOrEmpty(context.Token))
            {
                StringValues accessToken = context.Request.Query[key: "access_token"];
                PathString requestPath = context.HttpContext.Request.Path;
                string endPoint = $"/{config.Chat.Endpoint}";

                if (NotNullOrEmpty(accessToken) && PathStartsWith(requestPath, endPoint))
                {
                    context.Token = accessToken;
                }
            }
        };
    }

    private static bool PathStartsWith(PathString requestPath, string endPoint) =>
        requestPath.StartsWithSegments(endPoint);

    private static bool IsNullOrEmpty(StringValues accessToken) =>
        string.IsNullOrEmpty(accessToken);

    private static bool NotNullOrEmpty(StringValues accessToken) =>
        !IsNullOrEmpty(accessToken);
}
