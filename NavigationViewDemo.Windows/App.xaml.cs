using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using NavigationViewDemo.Core.ViewModels;
using NavigationViewDemo.Windows.Views;
using Needle.IoC;
using Needle.Navigation;
using Needle.Platforms.Uap.Services;

namespace NavigationViewDemo.Windows
{
    sealed partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
            ApplicationView.PreferredLaunchViewSize = new Size(1280, 840);
        }

        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            var frame = Window.Current.Content as Frame;
            if (frame == null)
            {
                var startup = new Startup();
                startup.ConfigureServices();

                //var setup = new Setup();
                //var viewModels = setup.GetViewModels();

                // get all view models
                // get all views
                // make auto registrations pairs

                NavigationService.Register<MainViewModel, MainView>();
                NavigationService.Register<DashboardViewModel, DashboardView>();
                NavigationService.Register<CoursesViewModel, CoursesView>();


                NavigationService.StartApp<MainViewModel>();

                var navigationService = ServiceLocator.Instance.GetService<INavigationService>();
                navigationService.Initialize(MainView.ContentFrame);
            }
            else
            {
            }

            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                AppViewBackButtonVisibility.Collapsed;
        }
    }



}
