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
        private Bitmap _poster;
        private string _imdbID;
        public static MovieFragment NewInstance(string title, string year, string type, string poster, string imdbID)
        {
            var bundle = new Bundle();
            bundle.PutString("title", title);
            bundle.PutString("year", year);
            bundle.PutString("type", type);
            bundle.PutString("poster", poster);
            bundle.PutString("imdbID", imdbID);
            return new MovieFragment {Arguments = bundle};
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.movie_fragment, container, false);
            var title = view.FindViewById<TextView>(Resource.Id.movie_title);
            title.Text = Arguments.GetString("title");
            var year = view.FindViewById<TextView>(Resource.Id.movie_year);
            year.Text = Arguments.GetString("year");
            var type = view.FindViewById<TextView>(Resource.Id.movie_type);
            type.Text = Arguments.GetString("type");

            _imdbID = Arguments.GetString("imdbID");
            
            string posterFile = Path.Combine(Environment.ExternalStorageDirectory.Path,
                Environment.DirectoryDownloads, $"Posters/{Arguments.GetString("poster")}");

            _poster = File.Exists(posterFile)
                ? BitmapFactory.DecodeFile(posterFile)
                : Bitmap.CreateBitmap(300, 466, Bitmap.Config.Argb8888);
            var image = view.FindViewById<ImageView>(Resource.Id.movie_image);
            image.SetImageBitmap(_poster);

            view.FindViewById<ImageView>(Resource.Id.movie_image).Click += (sender, args) =>
            {
                Fragment fragment = MovieDetailsFragment.NewInstance(_poster, _imdbID);
                ((MainActivity) Activity).LoadFragment(fragment);
            };
            
            return view;
        }
    }
}