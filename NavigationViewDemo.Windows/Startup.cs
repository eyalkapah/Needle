using Microsoft.Extensions.DependencyInjection;
using NavigationViewDemo.Core.ViewModels;
using Needle.IoC;
using Needle.Navigation;
using Needle.Platforms.Uap.Services;

namespace NavigationViewDemo.Windows
{
    public class Startup
    {
        public Startup()
        {
        }

        public void ConfigureServices()
        {
            var services = new ServiceCollection();

            // Services
            services.AddScoped<INavigationService, NavigationService>();

            // ViewModels
            services.AddTransient<MainViewModel>();
            services.AddTransient<DashboardViewModel>();
            services.AddTransient<CoursesViewModel>();
            services.AddTransient<OrdersViewModel>();

            ServiceLocator.Init(services.BuildServiceProvider());
        }
    }
}
