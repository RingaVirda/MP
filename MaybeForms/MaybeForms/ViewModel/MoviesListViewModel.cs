using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using MaybeForms.Model;
using MaybeForms.Pages;
using MaybeForms.Services;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

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
        
        public bool IsLoading { get; set; }
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
            IsLoading = true;
            try
            {
                Movies.Clear();
                var movies = await MovieStore.GetItemsAsync();
                if (movies.Count() == 0)
                {
                    return;
                }
                movies.ForEach(m => Movies.Add(m));
                IsLoading = false;
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

        public async void OnSearch(string query)
        {
            IsLoading = true;
            await ((MoviesStore)MovieStore).GetSearchResults(query, this);
        }
    }
}