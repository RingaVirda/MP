using MaybeForms.Model;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Text.Json;
using MaybeForms.ViewModel;

namespace MaybeForms.Services
{
    public class MoviesStore : IDataStore<Movie>
    {
        private List<Movie> _movies;
        private List<Movie> _createdMovies;
        private bool _useHttp = true;
        private string url = "http://www.omdbapi.com/?apikey=7e9fe69e&s={0}&page=1";

        public MoviesStore(string resourcesPath)
        {
            _createdMovies = new List<Movie>();
            if (_useHttp)
            {
                _movies = new List<Movie>();
            }
            else
            {
                ResourcesPath = resourcesPath;
                var titlesJson = File.ReadAllText($"{ResourcesPath}/MoviesList.json");
                _movies = JsonSerializer.Deserialize<List<Movie>>(titlesJson);
            }
        }

        public string ResourcesPath { get; set; }

        public async Task<bool> AddItemAsync(Movie movie)
        {
            _createdMovies.Add(movie);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(Movie movie)
        {
            var oldMovie = _movies.FirstOrDefault(m => m == movie);
            if (oldMovie == null)
            {
                oldMovie = _createdMovies.FirstOrDefault(m => m == movie);
                if (oldMovie == null) return await Task.FromResult(false);
            }

            _movies.Remove(oldMovie);
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Movie movie)
        {
            var oldIMovie = _movies.FirstOrDefault(m => m.imdbID == movie.imdbID);
            if (oldIMovie == null) return await Task.FromResult(false);

            _movies.Remove(oldIMovie);
            _movies.Add(movie);

            return await Task.FromResult(true);
        }

        public async Task<Movie> GetItemAsync(string imdbID)
            => await Task.FromResult(_movies.FirstOrDefault(m => m.imdbID == imdbID));

        public async Task<IEnumerable<Movie>> GetItemsAsync(bool forceRefresh = false)
        {
            var res = new List<Movie>(_movies);
            res.AddRange(_createdMovies);
            return await Task.FromResult(res);
        }

        public async Task<string> ReadFileAsync(string path)
            => await Task.FromResult(File.ReadAllText($"{ResourcesPath}/{path}"));

        private MoviesListVewModel _moviesList;

        public async Task GetSearchResults(string query, MoviesListVewModel moviesListVewModel)
        {
            _moviesList = moviesListVewModel;
            query = query.Replace(' ', '+').ToLower();
            try
            {
                using (WebClient client = new WebClient())
                {
                    var uri = new Uri(string.Format(url, query));
                    client.DownloadStringCompleted += OnDownloadCompleted;
                    client.DownloadStringAsync(uri);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    
        private async void OnDownloadCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            var jsonString = e.Result;
            if (jsonString.Contains("Error"))
            {
                return;
            }

            var start = jsonString.IndexOf('[');
            var end =jsonString.IndexOf(']');
            jsonString = jsonString.Substring(start, end - start + 1);
            _movies = JsonSerializer.Deserialize<List<Movie>>(jsonString);
            _moviesList.Movies = new ObservableCollection<Movie>(_movies);
            _moviesList.IsLoading = false;
        }
    }
}