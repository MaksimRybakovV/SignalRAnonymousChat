using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace SignalRAnonymousChat.Client.Services.ChatService
{
    public interface IChatService
    {
        public void Construct(HubConnection connection);
        public void SetSendMessageEvent(Action<string, string> action);
        public Task Connect();
        public Task SendMessage(string username, string message);
    }
}
