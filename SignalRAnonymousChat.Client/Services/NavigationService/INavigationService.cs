namespace SignalRAnonymousChat.Client.Services.NavigationService
{
    public interface INavigationService<T>
    {
        public T? CurrentView { get; set; }
        public void NavigateTo<V>() where V : T;
    }
}
