using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Messaging;
using Microsoft.Toolkit.Mvvm.Input.Wpf;
using WPF_MVVM_Base.Services;
using System;
using System.Windows;

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
            }
            else
            {
                Message = "...";
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
    }
}
