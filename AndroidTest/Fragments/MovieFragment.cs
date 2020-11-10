using Android.Graphics;
using Android.OS;
using Android.Views;
using Android.Widget;
using System.IO;
using Android.App;
using Path = System.IO.Path;

namespace AndroidTest.Fragments
{
    public class MovieFragment : Fragment
    {
        public static MovieFragment NewInstance(string title, string year, string type, string poster)
        {
            var bundle = new Bundle();
            bundle.PutString("title", title);
            bundle.PutString("year", year);
            bundle.PutString("type", type);
            bundle.PutString("poster", poster);
            return new MovieFragment {Arguments = bundle};
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            if (container == null) return null;
            
            View view = inflater.Inflate(Resource.Layout.movie_fragment, container, false);
            var title = view.FindViewById<TextView>(Resource.Id.movie_title);
            title.Text = Arguments.GetString("title");
            var year = view.FindViewById<TextView>(Resource.Id.movie_year);
            year.Text = Arguments.GetString("year");
            var type = view.FindViewById<TextView>(Resource.Id.movie_type);
            type.Text = Arguments.GetString("type");

            string posterFile = Path.Combine(Environment.ExternalStorageDirectory.Path,
                Environment.DirectoryDownloads, $"Posters/{Arguments.GetString("poster")}");

            var poster = File.Exists(posterFile)
                ? BitmapFactory.DecodeFile(posterFile)
                : Bitmap.CreateBitmap(300, 466, Bitmap.Config.Argb8888);
            var image = view.FindViewById<ImageView>(Resource.Id.movie_image);
            image.SetImageBitmap(poster);

            return view;
        }
    }
}