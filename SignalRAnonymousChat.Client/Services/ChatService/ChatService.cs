using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace SignalRAnonymousChat.Client.Services.ChatService
{
    internal class ChatService : IChatService
    {
        private HubConnection? _connection;

        public event Action<string, string> MessageReceived;

        public void Construct(HubConnection connection)
        {
            _connection = connection;
            _connection.On<string, string>("ReceiveMessage", (username, message)
                => MessageReceived?.Invoke(username, message));
        }

        public void SetSendMessageEvent(Action<string, string> action)
        {
            MessageReceived += action;
        }

        public async Task Connect()
        {
            await _connection!.StartAsync();
        }

        public async Task SendMessage(string username, string message)
        {
            await _connection!.SendAsync("SendMessageToChat", username, message);
        }
    }
}
