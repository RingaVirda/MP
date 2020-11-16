using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MaybeForms.Annotations;
using MaybeForms.Model;
using MaybeForms.Pages;
using MaybeForms.Services;
using Xamarin.Forms;

namespace MaybeForms.ViewModel
{
    public class MoviesListVewModel : ViewModelBase
    {
        private ObservableCollection<Movie> _movies;

        public MoviesListVewModel()
        {
            PageTitle = "Movies";

            LoadMoviesCommand = new Command(async () => await LoadMovies());
            MovieTapped = new Command<Movie>(OnMovieSelected);
            AddMovieCommand = new Command(OnAddMovie);
            DeleteMovie = new Command<Movie>(OnDeleteMovie);

            Movies = new ObservableCollection<Movie>();
        }


        public Command LoadMoviesCommand { get; }
        public Command MovieTapped { get; }
        public Command AddMovieCommand { get; }
        public Command DeleteMovie { get; }

        public ObservableCollection<Movie> Movies
        {
            get => _movies;
            set => SetProperty(ref _movies, value);
        }

        private async Task LoadMovies()
        {
            IsBusy = true;
            try
            {
                Movies.Clear();
                var movies = await MovieStore.GetItemsAsync();
                foreach (var movie in movies)
                {
                    if (movie.imdbID != null)
                    {
                        movie.Poster = Path.Combine(((MoviesStore) MovieStore).ResourcesPath,
                            "Posters",
                            movie.Poster);
                    }

                    Movies.Add(movie);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
        }

        private async void OnMovieSelected(Movie movie)
        {
            if (movie == null)
                return;

            await Shell.Current.GoToAsync(
                $"{nameof(MovieDetailsPage)}?{nameof(MovieDetailsViewModel.ImdbID)}={movie.imdbID}");
        }

        private async void OnAddMovie(object o)
        {
            await Shell.Current.GoToAsync(nameof(NewMoviePage));
        }

        private async void OnDeleteMovie(Movie movie)
        {
            await MovieStore.DeleteItemAsync(movie);
            await LoadMovies();
        }

        public void OnSearch(string query)
        {
            Movies = ((MoviesStore)MovieStore).GetSearchResults(query);
        }
    }
}