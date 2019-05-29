using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Needle.Navigation
{
    public interface INavigationService
    {
        event EventHandler<string> Navigated;
        bool CanGoBack { get; }
        void Initialize(object frame);
        void GoBack();

        Task Navigate(Type viewModel, object parameter = null);
        //void Navig
    }
}
