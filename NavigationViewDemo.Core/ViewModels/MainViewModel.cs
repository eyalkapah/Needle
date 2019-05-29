using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmCross.Commands;
using NavigationViewDemo.Core.Models;
using Needle.Navigation;
using Needle.ViewModels;

namespace NavigationViewDemo.Core.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private object _selectedItem;
        private readonly INavigationService _navigationService;

        public object SelectedItem
        {
            get => _selectedItem;
            set => SetProperty(ref _selectedItem, value, () => OnSelectionChanged(value));
        }

        private void OnSelectionChanged(object value)
        {
            if (value is NavigationItem item)
            {
                if (item.Label.Equals("Dashboard"))
                    _navigationService.Navigate(typeof(DashboardViewModel));
                if (item.Label.Equals("Courses"))
                    _navigationService.Navigate(typeof(CoursesViewModel));
            }
        }


        private IList<NavigationItem> _items;

        public IList<NavigationItem> Items
        {
            get => _items;
            set => SetProperty(ref _items, value);
        }

        public ICommand ToDashboardCommand { get; set; }
        public ICommand BackCommand { get; set; }

        public MainViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            _navigationService.Navigated += OnFrameNavigated;

            ToDashboardCommand = new MvxAsyncCommand(async () => await ToDashboard());
            BackCommand = new MvxAsyncCommand(async () => await Back());
        }

        public override Task OnNavigatedTo()
        {
            Items = PopulateNavigationItems().ToList();

            return Task.CompletedTask;
        }

        private async Task Back()
        {
            _navigationService.GoBack();

            await Task.CompletedTask;
        }


        private async Task ToDashboard()
        {
            SelectedItem = Items.First();

            await Task.CompletedTask;
        }

        private IEnumerable<NavigationItem> PopulateNavigationItems()
        {
            yield return new NavigationItem(0xE80F, "Dashboard", typeof(DashboardViewModel));
            yield return new NavigationItem(0xE716, "Courses", typeof(CoursesViewModel));
            yield return new NavigationItem(0xE8A1, "Orders", typeof(OrdersViewModel));
        }

        public void OnFrameNavigated(object sender, string e)
        {
            switch (e)
            {
                case "DashboardView":
                    SelectedItem = Items.FirstOrDefault();
                    break;
                case "CoursesView":
                    SelectedItem = Items[1];
                    break;
                default:
                    break;

            }
        }
    }
}
