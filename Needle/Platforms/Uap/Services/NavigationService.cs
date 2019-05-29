using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Needle.IoC;
using Needle.Navigation;
using Needle.ViewModels;

namespace Needle.Platforms.Uap.Services
{
    public class NavigationService : INavigationService
    {
        private static readonly ConcurrentDictionary<Type, Type> ViewModelToViewMap;

        public Frame MainFrame { get; set; }

        public bool CanGoBack => MainFrame.CanGoBack;

        public NavigationService()
        {
        }

        static NavigationService()
        {
            ViewModelToViewMap = new ConcurrentDictionary<Type, Type>();
        }

        public void Initialize(object frame)
        {
            MainFrame = frame as Frame;

            if (MainFrame != null)
            {
                MainFrame.Navigated += FrameOnNavigated;
            }
        }

        private void FrameOnNavigated(object sender, NavigationEventArgs e)
        {
            Navigated?.Invoke(MainFrame, e.SourcePageType.Name);
        }

        public static object StartApp<TViewModel>()
        {
            var frame = new Frame();

            var view = ViewModelToViewMap[typeof(TViewModel)];
            frame.Navigate(view);

            var viewModel = ServiceLocator.Instance.GetService(typeof(TViewModel));
            frame.DataContext = viewModel;

            if (viewModel is ViewModelBase vm)
            {
                vm.OnNavigatedTo();
            }

            Window.Current.Content = frame;
            Window.Current.Activate();

            return frame.Content;
        }

        public void GoBack()
        {
            if (MainFrame.CanGoBack)
                MainFrame.GoBack();
        }

        public async Task Navigate(Type viewModel, object parameter = null)
        {
            if (MainFrame.DataContext is ViewModelBase vm)
            {
                await vm.OnNavigatedFrom();
            }

            MainFrame.Navigate(GetView(viewModel), parameter);

            vm = ServiceLocator.Instance.GetService(viewModel) as ViewModelBase;

            if (vm != null)
            {
                await vm.OnNavigatedTo();
            }
        }


        private Type GetView(Type viewModel)
        {
            try
            {
                var view = ViewModelToViewMap[viewModel];
                return view;
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("View not registered for the view model");
            }
        }

        public static void Register<TViewModel, TView>() where TView: Page
        {
            ViewModelToViewMap.TryAdd(typeof(TViewModel), typeof(TView));

        }

        public event EventHandler<string> Navigated;

    

    }
}
