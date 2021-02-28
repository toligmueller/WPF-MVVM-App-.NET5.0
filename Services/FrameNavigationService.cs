using System;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WPF_MVVM_Base.Services
{
    public interface IFrameNavigationService
    {
        public event NavigatedEventHandler Navigated;
        public event NavigationFailedEventHandler NavigationFailed;

        public Frame Frame { get; set; }
        public bool CanGoBack { get; }
        public bool CanGoForward { get; }
        public void GoBack();
        public void GoForward();
        public bool CanNavigateTo(Uri uri);
        public bool Navigate(Uri sourcePageUri, object extraData = null);
        public bool Navigate(Type sourceType);
        public bool Navigate(object sourceObject);

    }

    public class FrameNavigationService : IFrameNavigationService
    {
        public FrameNavigationService(Uri defaultPage = null)
        {
            if (defaultPage != null)
            {
                Navigate(defaultPage);
            }
        }

        public event NavigatedEventHandler Navigated;

        public event NavigationFailedEventHandler NavigationFailed;

        private Frame frame;

        public Frame Frame
        {
            get
            {
                if (frame == null)
                {
                    frame = new Frame() { NavigationUIVisibility = NavigationUIVisibility.Hidden };
                    RegisterFrameEvents();
                }

                return frame;
            }
            set
            {
                UnregisterFrameEvents();
                frame = value;
                RegisterFrameEvents();
            }
        }

        public bool CanGoBack => Frame.CanGoBack;

        public bool CanGoForward => Frame.CanGoForward;

        public bool CanNavigateTo(Uri uri) => Frame.CurrentSource != uri;

        public void GoBack() => Frame.GoBack();

        public void GoForward() => Frame.GoForward();

        public bool Navigate(Uri sourcePageUri, object extraData = null)
        {
            if (Frame.CurrentSource != sourcePageUri)
            {
                return Frame.Navigate(sourcePageUri, extraData);
            }

            return false;
        }

        public bool Navigate(Type sourceType)
        {
            if (Frame.NavigationService?.Content?.GetType() != sourceType)
            {
                return Frame.Navigate(Activator.CreateInstance(sourceType));
            }

            return false;
        }

        public bool Navigate(object sourceObject)
        {
            if (sourceObject is Page)
            {
                return Frame.Navigate(sourceObject);
            }
            return false;
        }

        public void RegisterFrameEvents()
        {
            if (frame != null)
            {
                frame.Navigated += navigated;
                frame.NavigationFailed += navigationFailed;
            }
        }

        public void UnregisterFrameEvents()
        {
            if (frame != null)
            {
                frame.Navigated -= navigated;
                frame.NavigationFailed -= navigationFailed;
            }
        }

        private void navigationFailed(object sender, NavigationFailedEventArgs e) => NavigationFailed?.Invoke(sender, e);

        private void navigated(object sender, NavigationEventArgs e) => Navigated?.Invoke(sender, e);
    }
}
