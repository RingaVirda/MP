using System;
using System.IO;
using System.Threading.Tasks;
using Android;
using Android.App;
using Android.Provider;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Support.V4.Content;
using Xamarin.Forms;
using Environment = Android.OS.Environment;
using Uri = Android.Net.Uri;

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

            Instance = this;
        }

        public static readonly int PickImageId = 1000;

        public TaskCompletionSource<string> PickImageTaskCompletionSource { set; get; }
        internal static MainActivity Instance { get; private set; }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent intent)
        {
            base.OnActivityResult(requestCode, resultCode, intent);

            if (requestCode == PickImageId)
            {
                if ((resultCode == Result.Ok) && (intent != null))
                {
                    Uri uri = intent.Data;

                    PickImageTaskCompletionSource.SetResult(GetPathToImage(uri));
                }
                else
                {
                    PickImageTaskCompletionSource.SetResult(null);
                }
            }
        }

        private string GetPathToImage(Uri uri)
        {
            string doc_id = "";
            using (var c1 = ContentResolver.Query(uri, null, null, null, null))
            {
                c1.MoveToFirst();
                string document_id = c1.GetString(0);
                doc_id = document_id.Substring(document_id.LastIndexOf(":") + 1);
            }

            string path = null;

            string selection = MediaStore.Images.Media.InterfaceConsts.Id + " =? ";
            using (var cursor = ContentResolver.Query(MediaStore.Images.Media.ExternalContentUri, null, selection,
                new string[] {doc_id}, null))
            {
                if (cursor == null) return path;
                var columnIndex = cursor.GetColumnIndexOrThrow(MediaStore.Images.Media.InterfaceConsts.Data);
                cursor.MoveToFirst();
                path = cursor.GetString(columnIndex);
            }

            return path;
        }
    }
}