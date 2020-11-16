using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaybeForms.Model;
using MaybeForms.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MaybeForms.Pages
{
    public partial class MoviesListPage : ContentPage
    {
        private readonly MoviesListVewModel _viewModel;

        public MoviesListPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new MoviesListVewModel();
        }

        protected override void OnAppearing()
        {
            _viewModel.OnAppearing();
        }

        private void SearchBar_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            _viewModel.OnSearch(SearchBar.Text);
        }
    }
}