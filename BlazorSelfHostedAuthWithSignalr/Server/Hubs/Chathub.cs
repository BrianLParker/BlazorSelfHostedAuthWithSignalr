using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace BlazorSelfHostedAuthWithSignalr.Server.Hubs;

[Authorize(Roles = "Manager")]
public class ChatHub : Hub
{
    [Authorize(Roles = "Administrator")]
    public async Task SendMessage(string user, string message)
    {
        var userEmail = GetUserEmail();
        var authUser = $"{user}({userEmail})";

        await Clients.All.SendAsync(method: "ReceiveMessage", authUser, message);
    }

    private string GetUserEmail() => Context.User.FindFirstValue(ClaimTypes.Email);
}

