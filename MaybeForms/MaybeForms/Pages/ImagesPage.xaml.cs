using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaybeForms.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MaybeForms.Pages
{
    public partial class ImagesPage : ContentPage
    {
        private ImagesViewModel _viewModel;

        public ImagesPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new ImagesViewModel();
        }

        protected override void OnAppearing()
        {
            _viewModel.OnAppearing();
        }
    }
}