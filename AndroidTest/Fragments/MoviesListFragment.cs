using System.IO;
using System.Text.Json;
using Android;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Support.V4.Content;
using Android.Views;
using Android.Widget;
using AndroidTest.Model;

namespace AndroidTest.Fragments
{
    public class MoviesListFragment : Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.movies_list_fragment, container, false);
            var titlesNameFile = Path.Combine(Environment.ExternalStorageDirectory.Path, Environment.DirectoryDownloads,
                "MoviesList.json");
            var test = view.FindViewById<TextView>(Resource.Id.test_view);

            if (!File.Exists(titlesNameFile))
            {
                test.Text = "File with movie titles not found. It should be in downloads folder.";
                return view;
            }

            if ((ContextCompat.CheckSelfPermission(Context, Manifest.Permission.WriteExternalStorage) !=
                 (int) Permission.Granted)
                || (ContextCompat.CheckSelfPermission(Context, Manifest.Permission.ReadExternalStorage) !=
                    (int) Permission.Granted))
            {
                RequestPermissions(
                    new[]
                        {Manifest.Permission.ReadExternalStorage, Manifest.Permission.WriteExternalStorage}, 1);
            }

            var titlesJson = File.ReadAllText(titlesNameFile);
            var movies = JsonSerializer.Deserialize<SearchJson>(titlesJson);

            foreach (var movie in movies.Search)
            {
                MovieFragment fragment = MovieFragment.NewInstance(movie.Title, movie.Year, movie.Type, movie.Poster);

                var tx = FragmentManager.BeginTransaction();
                tx.Add(Resource.Id.movieContainer, fragment);
                tx.Commit();
            }

            return view;
        }

        public static Fragment NewInstance()
        {
            var bundle = new Bundle();
            return new MoviesListFragment {Arguments = bundle};
        }
    }
}