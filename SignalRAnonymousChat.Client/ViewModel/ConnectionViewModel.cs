using SignalRAnonymousChat.Client.Services.ChatService;
using SignalRAnonymousChat.Client.ViewModel.Base;

namespace SignalRAnonymousChat.Client.ViewModel
{
    internal class ConnectionViewModel : BaseViewModel
    {
        protected readonly IChatService _chat;

        public ConnectionViewModel(IChatService chat)
        {
            _chat = chat;
        }
    }
}
