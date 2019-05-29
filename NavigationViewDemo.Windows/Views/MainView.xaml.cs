using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using NavigationViewDemo.Core.Models;
using NavigationViewDemo.Core.ViewModels;
using Needle.IoC;
using Needle.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace NavigationViewDemo.Windows.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainView : Page
    {
        public MainViewModel ViewModel => DataContext as MainViewModel;

        public static Frame ContentFrame { get; set; }

        public MainView()
        {
            this.InitializeComponent();

            ContentFrame = NestedFrame;
        }


        private void ToDashboardButton_OnClick(object sender, RoutedEventArgs e)
        {
            var navigationService = ServiceLocator.Instance.GetService<INavigationService>();
            navigationService.Navigate(typeof(DashboardViewModel));
        }
    }
}

