using Microsoft.AspNetCore.SignalR;

namespace BlazorSelfHostedAuthWithSignalr.Server.Hubs;

public class ChatHub : Hub
{
    public async Task SendMessage(string user, string message)
    {
        await Clients.All.SendAsync(method: "ReceiveMessage", user, message);
    }
}