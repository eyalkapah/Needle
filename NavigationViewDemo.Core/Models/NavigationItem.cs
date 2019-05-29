using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavigationViewDemo.Core.Models
{
    public class NavigationItem
    {
        public Type ViewModel { get; }

        public string Glyph { get; set; }

        public string Label { get; set; }

        public NavigationItem(Type viewModel)
        {
            ViewModel = viewModel;
        }

        public NavigationItem(int glyph, string label, Type viewModel) : this(viewModel)
        {
            Label = label;
            Glyph = char.ConvertFromUtf32(glyph);
        }
    }
}
