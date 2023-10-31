using Microsoft.AspNetCore.SignalR.Client;
using SignalRAnonymousChat.Client.Infrastructure.Commands;
using SignalRAnonymousChat.Client.Services.ChatService;
using SignalRAnonymousChat.Client.Services.NavigationService;
using SignalRAnonymousChat.Client.Services.UsernameService;
using SignalRAnonymousChat.Client.View.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SignalRAnonymousChat.Client.ViewModel
{
    internal class LoginViewModel : MainWindowViewModel
    {
        public string Name { get; set; } = string.Empty;

        public ICommand LoginCommand { get; }

        public LoginViewModel(IChatService chat, INavigationService<UserControl> navigation, IUsernameService username) : base(chat, navigation, username)
        {
            LoginCommand = new RelayCommand(OnLoginCommandCommand, CanLoginCommandCommand);
        }

        private bool CanLoginCommandCommand(object parameter) => true;

        private async void OnLoginCommandCommand(object parameter)
        {
            Username?.SetCurrentUsername(Name);

            var connection = new HubConnectionBuilder()
                .WithUrl($"https://localhost:7249/chathub?username={Username?.CurrentUsername}")
                .Build();
            _chat.Construct(connection);
            await _chat.Connect();

            Navigation?.NavigateTo<ChatView>();
        }
    }
}
