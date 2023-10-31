namespace SignalRAnonymousChat.Client.Services.UsernameService
{
    public interface IUsernameService
    {
        public string CurrentUsername { get; }

        public void SetCurrentUsername(string currentUsername);
    }
}
