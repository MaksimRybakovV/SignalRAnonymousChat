﻿using SignalRAnonymousChat.Client.Infrastructure.Commands;
using SignalRAnonymousChat.Client.Services.ChatService;
using SignalRAnonymousChat.Client.Services.NavigationService;
using SignalRAnonymousChat.Client.Services.UsernameService;
using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace SignalRAnonymousChat.Client.ViewModel
{
    internal class ChatViewModel : MainWindowViewModel
    {
        public ObservableCollection<string> Messages { get; }

        public string Message { get; set; } = string.Empty;

        public ICommand SendMessageCommand { get; }

        public ChatViewModel(IChatService chat, INavigationService<UserControl> navigation, IUsernameService username)
            : base(chat, navigation, username)
        {
            _chat.SetSendMessageEvent(SendMessageHandler);
            SendMessageCommand = new RelayCommand(OnSendMessageCommand, CanSendMessageCommand);
            Messages = new ObservableCollection<string>();
        }

        private bool CanSendMessageCommand(object parameter) => true;

        private async void OnSendMessageCommand(object parameter)
        {
            await _chat.SendMessage(Username!.CurrentUsername, Message);
        }

        private void SendMessageHandler(string username, string message)
        {
            App.Current.Dispatcher.BeginInvoke((Action)delegate ()
            {
                Messages.Add(Message = username == "" ? $"{message}" : $"{username}: {message}");
            });
        }
    }
}
