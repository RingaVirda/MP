using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaybeForms.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MaybeForms.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChartsPage : ContentPage
    {
        public ChartsPage()
        {
            InitializeComponent();
            
            BindingContext = new ChartsViewModel();
        }
    }
}