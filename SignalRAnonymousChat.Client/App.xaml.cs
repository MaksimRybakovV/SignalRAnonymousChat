using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SignalRAnonymousChat.Client.Services.ChatService;
using SignalRAnonymousChat.Client.Services.NavigationService;
using SignalRAnonymousChat.Client.Services.UsernameService;
using SignalRAnonymousChat.Client.View.Windows;
using SignalRAnonymousChat.Client.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;

namespace SignalRAnonymousChat.Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .ConfigureServices(services =>
                {
                    services.AddSingleton<IChatService, ChatService>();
                    services.AddSingleton<INavigationService<UserControl>, ViewNavigationService>();
                    services.AddSingleton<IUsernameService, UsernameService>();

                    services.AddSingleton<Func<Type, UserControl>>(serviceProvider => userControl => (UserControl)serviceProvider.GetRequiredService(userControl));

                    services.AddSingleton<MainWindowViewModel>();
                    services.AddSingleton<LoginViewModel>();
                    services.AddSingleton<ChatViewModel>();

                    services.AddSingleton((services) => new MainWindow()
                    {
                        DataContext = services.GetRequiredService<MainWindowViewModel>()
                    });

                    services.AddSingleton((services) => new LoginView()
                    {
                        DataContext = services.GetRequiredService<LoginViewModel>()
                    });

                    services.AddSingleton((services) => new ChatView()
                    {
                        DataContext = services.GetRequiredService<ChatViewModel>()
                    });
                })
                .Build();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _host.Start();
            MainWindow = _host.Services.GetRequiredService<MainWindow>();
            _host.Services.GetRequiredService<MainWindowViewModel>().GetStartView();
            MainWindow.Show();

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _host.StopAsync();
            _host.Dispose();

            base.OnExit(e);
        }
    }
}
