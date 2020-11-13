using MaybeForms.Model;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Text.Json;

namespace MaybeForms.Services
{
    public class DataStore : IDataStore<Movie>
    {
        private List<Movie> _movies;

        public DataStore(string resourcesPath)
        {
            ResourcesPath = resourcesPath;
            var titlesJson = File.ReadAllText($"{ResourcesPath}/MoviesList.json");
            _movies = JsonSerializer.Deserialize<List<Movie>>(titlesJson);
        }

        public string ResourcesPath { get; set; }

        public async Task<bool> AddItemAsync(Movie movie)
        {
            _movies.Add(movie);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(Movie movie)
        {
            var oldMovie = _movies.FirstOrDefault(m => m == movie);
            if (oldMovie == null) return await Task.FromResult(false);
            _movies.Remove(oldMovie);
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Movie movie)
        {
            var oldIMovie = _movies.Where((Movie m) => m.imdbID == movie.imdbID).FirstOrDefault();
            if (oldIMovie == null) return await Task.FromResult(false);

            _movies.Remove(oldIMovie);
            _movies.Add(movie);

            return await Task.FromResult(true);
        }

        public async Task<Movie> GetItemAsync(string imdbID)
            => await Task.FromResult(_movies.FirstOrDefault((Movie m) => m.imdbID == imdbID));

        public async Task<IEnumerable<Movie>> GetItemsAsync(bool forceRefresh = false)
            => await Task.FromResult(_movies);

        public async Task<string> ReadFileAsync(string path)
            => await Task.FromResult(File.ReadAllText($"{ResourcesPath}/{path}"));

        public ObservableCollection<Movie> GetSearchResults(string query)
        {
            query = query.ToLower();
            return new ObservableCollection<Movie>(_movies.Where(m => m.Title.ToLowerInvariant().Contains(query)));
        }
    }
}