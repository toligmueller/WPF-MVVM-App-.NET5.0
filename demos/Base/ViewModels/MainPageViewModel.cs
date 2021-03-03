using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Messaging;
using Microsoft.Toolkit.Mvvm.Input.Wpf;
using WPF_MVVM_Base.Services;
using System;
using System.Windows;
using WPF_MVVM_Base.Models;

namespace WPF_MVVM_Base.ViewModels
{
    public class MainPageViewModel : ObservableRecipient
    {

        private readonly IDemoService demoService;

        public MainPageViewModel(IDemoService demoService)
        {
            this.demoService = demoService;
            IsActive = true;
            if (Application.Current.MainWindow == null)
            {
                Message = "...*";
                Messages = new Models.Messages();
                Messages.Add(new Message() { MessageId = Guid.NewGuid(), CreateAt = DateTime.Now, Text = "test" });
            }
            else
            {
                Message = "...";
                Messages = demoService.Read();
            }
        }
        ~MainPageViewModel()
        {
            IsActive = false;
        }

        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                if (SetProperty(ref _isBusy, value))
                {
                    OnPropertyChanged(nameof(TalkCmd));
                    OnPropertyChanged(nameof(WriteCmd));
                }
            }
        }
        private bool _isBusy;

        public string Message
        {
            get => _message;
            set 
            {
                if (SetProperty(ref _message, value)) 
                {
                    WeakReferenceMessenger.Default.Send(new Messages.TelegramChannel(value));
                } 
            }
        }
        private string _message;

        public string NewMessage
        {
            get => _newMessage;
            set => SetProperty(ref _newMessage, value);
        }
        private string _newMessage;

        public Models.Messages Messages
        {
            get => _messages;
            set => SetProperty(ref _messages, value);
        }
        private Models.Messages _messages;

        public AsyncRelayCommand<string> TalkCmd => new(
            async timeout =>
            {
                IsBusy = true;
                Message = await demoService.Talk();
                //throw new Exception("Test");
                IsBusy = false;
            },
            timeout => !IsBusy
        );

        public AsyncRelayCommand WriteCmd => new(
            async () =>
            {
                IsBusy = true;
                await demoService.Write(NewMessage);
                Messages = demoService.Read();
                NewMessage = string.Empty;
                IsBusy = false;
            },
            () => !IsBusy
        );
    }
}
