using System;
using System.IO;
using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V4.Content;
using Xamarin.Forms;
using Environment = Android.OS.Environment;

namespace MaybeForms.Android
{
    [Activity(Label = "MaybeForms", Theme = "@style/MainTheme", MainLauncher = true, Icon = "@drawable/icon",
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            var recourcesPath = Path.Combine(Environment.ExternalStorageDirectory.Path,
                Environment.DirectoryDownloads,
                "Programming Assignment");
            recourcesPath = (Directory.Exists(recourcesPath)) ? recourcesPath : String.Empty;

            if ((ContextCompat.CheckSelfPermission(ApplicationContext, Manifest.Permission.WriteExternalStorage) !=
                 (int) Permission.Granted)
                || (ContextCompat.CheckSelfPermission(ApplicationContext, Manifest.Permission.ReadExternalStorage) !=
                    (int) Permission.Granted))
            {
                RequestPermissions(
                    new[]
                        {Manifest.Permission.ReadExternalStorage, Manifest.Permission.WriteExternalStorage}, 1);
            }
            
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            Forms.SetFlags("SwipeView_Experimental");
            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App(recourcesPath));
        }
    }
}