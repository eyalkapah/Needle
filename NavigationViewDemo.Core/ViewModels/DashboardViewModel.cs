using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Needle.ViewModels;

namespace NavigationViewDemo.Core.ViewModels
{
    public class DashboardViewModel : ViewModelBase
    {
        public string Text => "Dashboard Page";

        public override Task OnNavigatedTo()
        {
            return base.OnNavigatedTo();
        }
    }
}
