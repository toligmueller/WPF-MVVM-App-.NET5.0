using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Messaging;
using Microsoft.Toolkit.Mvvm.Input.Wpf;
using WPF_MVVM_Base.Services;
using System.Diagnostics;
using System.Windows;

namespace WPF_MVVM_Base.ViewModels
{
    public class AboutPageViewModel : ObservableRecipient
    {
        public AboutPageViewModel()
        {
            IsActive = true;

            if (Application.Current.MainWindow == null)
            {
                Message = "We are a canadian Anonimouse!";
            }
            else
            {
                Message = "We are Anonimouse Chocolate";
            }
        }
        ~AboutPageViewModel()
        {
            IsActive = false;
        }

        protected override void OnActivated()
        {
            WeakReferenceMessenger.Default.Register<Messages.TelegramChannel>(this, (r, m) => 
            {
               // Message = m.Value;
            });
        }

        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                if (SetProperty(ref _isBusy, value))
                {
                    
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

                }
            }
        }
        private string _message;
    }
}
