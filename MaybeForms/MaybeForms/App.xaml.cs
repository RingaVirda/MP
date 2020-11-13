using System;
using MaybeForms.Model;
using MaybeForms.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace MaybeForms
{
    public partial class App : Application
    {
        public static IDataStore<Movie> MovieStore;
        
        public App(string resourcesPath)
        {
            InitializeComponent();
            
            MovieStore = new DataStore(resourcesPath);
            
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}