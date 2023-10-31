using Microsoft.AspNetCore.SignalR;

namespace SignalRAnonymousChat.Web.Hubs
{
    public class ChatHub : Hub
    {
        private static Dictionary<string, string> Users = new();

        public override async Task OnConnectedAsync()
        {
            string? username = Context.GetHttpContext()?.Request.Query["username"];
            Users.Add(Context.ConnectionId, username!);
            await SendMessageToChat(string.Empty, $"{username} joined the chat room.");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            string? username = Users.GetValueOrDefault(Context.ConnectionId);
            Users.Remove(Context.ConnectionId);
            await SendMessageToChat(string.Empty, $"{username} left the chat room.");
            await base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessageToChat(string username, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", username, message);
        }
    }
}
