using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaybeForms.Pages;
using Xamarin.Forms;

namespace MaybeForms
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(MoviesListPage), typeof(MoviesListPage));
            Routing.RegisterRoute(nameof(MovieDetailsPage), typeof(MovieDetailsPage));
            Routing.RegisterRoute(nameof(NewMoviePage), typeof(NewMoviePage));
        }
    }
}