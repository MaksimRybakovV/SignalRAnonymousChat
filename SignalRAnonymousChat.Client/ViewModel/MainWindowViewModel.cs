using SignalRAnonymousChat.Client.Services.ChatService;
using SignalRAnonymousChat.Client.Services.NavigationService;
using SignalRAnonymousChat.Client.Services.UsernameService;
using SignalRAnonymousChat.Client.View.Windows;
using System.Windows.Controls;

namespace SignalRAnonymousChat.Client.ViewModel
{
    class MainWindowViewModel : ConnectionViewModel
    {
        private INavigationService<UserControl>? _navigation;
        private IUsernameService? _username;

        public INavigationService<UserControl>? Navigation
        {
            get => _navigation;
            set
            {
                _navigation = value;
                OnPropertyChanged(nameof(Navigation));
            }
        }

        public IUsernameService? Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public MainWindowViewModel(IChatService chat, INavigationService<UserControl> navigation, IUsernameService username) : base(chat)
        {
            Navigation = navigation;
            Username = username;
        }

        public void GetStartView()
        {
            Navigation!.NavigateTo<LoginView>();
        }
    }
}
