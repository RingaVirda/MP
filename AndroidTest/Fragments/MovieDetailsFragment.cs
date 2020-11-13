using System.IO;
using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Views;
using System.Text.Json;
using Android.Widget;
using AndroidTest.Model;
using Path = System.IO.Path;

namespace AndroidTest.Fragments
{
    public class MovieDetailsFragment : Fragment
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.movie_details_fragment, container, false);
            view.FindViewById<ImageView>(Resource.Id.movie_details_poster)
                .SetImageBitmap((Bitmap) Arguments.GetParcelable("poster"));
            
            string detailsFile = Path.Combine(Environment.ExternalStorageDirectory.Path,
                Environment.DirectoryDownloads, $"{Arguments.GetString("imdbID")}.json");
            var movieDetails = (File.Exists(detailsFile))
                ? JsonSerializer.Deserialize<MovieDetails>(File.ReadAllText(detailsFile))
                : null;
            if (movieDetails != null)
            {
                view.FindViewById<TextView>(Resource.Id.movie_details_title).Text = $"Title: {movieDetails.Title}";
                view.FindViewById<TextView>(Resource.Id.movie_details_year).Text = $"Year: {movieDetails.Year}";
                view.FindViewById<TextView>(Resource.Id.movie_details_genre).Text = $"Genre: {movieDetails.Genre}";
                view.FindViewById<TextView>(Resource.Id.movie_details_director).Text = $"Director: {movieDetails.Director}";
                view.FindViewById<TextView>(Resource.Id.movie_details_actors).Text = $"Actors: {movieDetails.Actors}";
                view.FindViewById<TextView>(Resource.Id.movie_details_country).Text = $"Country: {movieDetails.Country}";
                view.FindViewById<TextView>(Resource.Id.movie_details_language).Text = $"Language: {movieDetails.Language}";
                view.FindViewById<TextView>(Resource.Id.movie_details_production).Text = $"Production: {movieDetails.Production}";
                view.FindViewById<TextView>(Resource.Id.movie_details_released).Text = $"Released: {movieDetails.Released}";
                view.FindViewById<TextView>(Resource.Id.movie_details_runtime).Text = $"Runtime: {movieDetails.Runtime}";
                view.FindViewById<TextView>(Resource.Id.movie_details_awards).Text = $"Awards: {movieDetails.Awards}";
                view.FindViewById<TextView>(Resource.Id.movie_details_rating).Text = $"Rating: {movieDetails.Rated}";
                view.FindViewById<TextView>(Resource.Id.movie_details_plot).Text = $"Plot: {movieDetails.Plot}";
            }

            return view;
        }

        public static MovieDetailsFragment NewInstance(Bitmap poster, string imdbID)
        {
            var bundle = new Bundle();
            bundle.PutParcelable("poster", poster);
            bundle.PutString("imdbID", imdbID);
            return new MovieDetailsFragment {Arguments = bundle};
        }
    }
}