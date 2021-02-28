using Serilog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace WPF_MVVM_Base
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    { 
        public App()
        {
            // loading UI/UX culture
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            WPF_MVVM_Base.Properties.Resources.Culture = new CultureInfo(config.AppSettings.Settings["culture"].Value);

            // global exception handler
            AppDomain.CurrentDomain.UnhandledException += AppDomainCurrentDomainUnhandledExceptionHandler;
            Dispatcher.UnhandledException += DispatcherUnhandledExceptionHandler;
            Application.Current.DispatcherUnhandledException += ApplicationCurrentDispatcherUnhandledExceptionHandler;
            TaskScheduler.UnobservedTaskException += TaskSchedulerUnobservedTaskExceptionHandler;

            // logging
            //Serilog.Debugging.SelfLog.Enable(msg => Debug.WriteLine(msg));
            string outputTemplate = "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}";
            Log.Logger = new LoggerConfiguration()
                    .ReadFrom.AppSettings()
                    .WriteTo.Debug(outputTemplate: outputTemplate)
                    .WriteTo.File("logs/app.log", outputTemplate: outputTemplate, rollingInterval: RollingInterval.Day, rollOnFileSizeLimit: true)
                    .CreateLogger();
    }

        #region global exception handling
        private void TaskSchedulerUnobservedTaskExceptionHandler(object sender, UnobservedTaskExceptionEventArgs e)
        {
            GlobalExceptionHandler(e.Exception); 
        }

        private void ApplicationCurrentDispatcherUnhandledExceptionHandler(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            GlobalExceptionHandler(e.Exception);
        }

        private void DispatcherUnhandledExceptionHandler(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            GlobalExceptionHandler(e.Exception);
        }

        private void AppDomainCurrentDomainUnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs e)
        {
            GlobalExceptionHandler(e.ExceptionObject.ToString());
        }

        private void GlobalExceptionHandler(Exception ex)
        {
            Log.Error(ex, ex.Message);
        }
        private void GlobalExceptionHandler(string message)
        {
            Log.Error(message);
        }
        #endregion
    }
}
