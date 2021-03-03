using System;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using WPF_MVVM_Base.Services;

namespace WPF_MVVM_Base.ViewModels
{
    public class ViewModelLocator
    {
        public IServiceProvider Services { get; }

        public MainViewModel MainVM => Services.GetService<MainViewModel>();
        public MainPageViewModel MainPageVM => Services.GetService<MainPageViewModel>();
        public AboutPageViewModel AboutPageVM => Services.GetService<AboutPageViewModel>();
        public UserListPageViewModel UserListPageVM => Services.GetService<UserListPageViewModel>();

        public ViewModelLocator()
        {
            Services = GetServices();
        }

        private static IServiceProvider GetServices()
        {
            IServiceCollection serviceCollection = new ServiceCollection();

            serviceCollection.AddSingleton<IFrameNavigationService>(new FrameNavigationService(new Uri("Views/MainPage.xaml", UriKind.RelativeOrAbsolute)));

            if (Application.Current.MainWindow == null)
            {
                serviceCollection.AddSingleton<IDemoService, FakeDemoService>();
            }
            else
            {
                serviceCollection.AddSingleton<IDemoService>(new DemoService());
            }

            serviceCollection.AddSingleton<MainViewModel>(); // Loads on Access
            serviceCollection.AddTransient<MainPageViewModel>(); // Reloads on Access, old instances will be closed after some time, GC magic.
            serviceCollection.AddSingleton<AboutPageViewModel>(new AboutPageViewModel()); // Loads instantly
            serviceCollection.AddSingleton<UserListPageViewModel>();

            return serviceCollection.BuildServiceProvider();
        }
    }
}
