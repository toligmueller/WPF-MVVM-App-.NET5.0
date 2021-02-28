using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input.Wpf;
using WPF_MVVM_Base.Services;
using System;
using System.Configuration;
using System.Windows;
using Serilog;
using System.Diagnostics;

namespace WPF_MVVM_Base.ViewModels
{
    public class MainViewModel : ObservableObject
    {

        public IFrameNavigationService FrameNavigationService { get; }

        public MainViewModel(IFrameNavigationService frameNavigationService)
        {
            FrameNavigationService = frameNavigationService;

            if (Application.Current.MainWindow == null)
            {
                AppName = "Design Corp.";
            }
            else
            {
                AppName = "Corpus Dei Inc.";
            }
        }

        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                if (SetProperty(ref _isBusy, value))
                {
                    Log.Verbose("IsBusy:" + value);
                    // Raise Update for all Commands
                    OnPropertyChanged(nameof(GoBackCmd));
                    OnPropertyChanged(nameof(GoForwardCmd));
                }
            }
        }
        private bool _isBusy;

        public string AppName
        {
            get => _appName;
            set => SetProperty(ref _appName, value);
        }
        private string _appName;

        public RelayCommand GoBackCmd => new(
            () =>
            {
                IsBusy = true;
                FrameNavigationService.GoBack();
                Log.Verbose("Navigation: go back");
                IsBusy = false;
            },
            () => !IsBusy && FrameNavigationService.CanGoBack
        );
        public RelayCommand GoForwardCmd => new(
             () =>
             {
                IsBusy = true;
                FrameNavigationService.GoForward();
                 Log.Verbose("Navigation: go forward");
                 IsBusy = false;
             },
             () => !IsBusy && FrameNavigationService.CanGoForward
         );

        public RelayCommand GoMainCmd => new(
             () =>
             {
                 IsBusy = true;
                 FrameNavigationService.Navigate(new Uri("Views/MainPage.xaml", UriKind.RelativeOrAbsolute));
                 Log.Verbose("Navigation: to main page");
                 IsBusy = false;
             },
             () => !IsBusy && FrameNavigationService.CanNavigateTo(new Uri("Views/MainPage.xaml", UriKind.RelativeOrAbsolute))
         );

        public RelayCommand GoAboutCmd => new(
             () =>
             {
                 IsBusy = true;
                 FrameNavigationService.Navigate(new Uri("Views/AboutPage.xaml", UriKind.RelativeOrAbsolute));
                 Log.Verbose("Navigation: to about page");
                 IsBusy = false;
             },
             () => !IsBusy && FrameNavigationService.CanNavigateTo(new Uri("Views/AboutPage.xaml", UriKind.RelativeOrAbsolute))
         );

        public RelayCommand GoUserListCmd => new(
             () =>
             {
                 IsBusy = true;
                 FrameNavigationService.Navigate(new Uri("Views/UserListPage.xaml", UriKind.RelativeOrAbsolute));
                 Log.Verbose("Navigation: to user list page");
                 IsBusy = false;
             },
             () => !IsBusy
         );

        public RelayCommand<string> ChangeCultureCmd => new(
             culture =>
             {
                 IsBusy = true;
                 Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                 config.AppSettings.Settings["culture"].Value = culture;
                 config.Save();
                 Log.Verbose("Settings: changed culture to " + culture);
                 Log.Information("Application restart required");
                 IsBusy = false;
             },
             _ => !IsBusy
         );
    }
}
