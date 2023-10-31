using SignalRAnonymousChat.Client.Infrastructure.ObservableObject;

namespace SignalRAnonymousChat.Client.Services.UsernameService
{
    internal class UsernameService : ObservableObject, IUsernameService
    {
        private string _currentUsername = string.Empty;

        public string CurrentUsername
        {
            get => _currentUsername;
            private set
            {
                if (value == "")
                {
                    _currentUsername = "Anonymous";
                    OnPropertyChanged();
                    return;
                }
                _currentUsername = value;
                OnPropertyChanged();
            }
        }

        public void SetCurrentUsername(string currentUsername)
        {
            CurrentUsername = currentUsername;
        }
    }
}
