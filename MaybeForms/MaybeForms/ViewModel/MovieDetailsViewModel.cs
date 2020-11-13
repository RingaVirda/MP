using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using MaybeForms.Model;
using MaybeForms.Services;
using Xamarin.Forms;

namespace MaybeForms.ViewModel
{
    [QueryProperty(nameof(ImdbID), nameof(ImdbID))]
    public class MovieDetailsViewModel : ViewModelBase
    {
        private readonly Frame _posterFrame;
        private string _imdbID;
        private MovieDetails _details;

        public MovieDetailsViewModel(Frame posterFrame)
        {
            _posterFrame = posterFrame;
            PageTitle = "Details";
            _details = new MovieDetails();
        }

        public MovieDetails Details
        {
            get => _details;
            set => SetProperty(ref _details, value);
        }

        public string ImdbID
        {
            get { return _imdbID; }
            set
            {
                _imdbID = value;
                LoadDetails(value);
            }
        }

        public async Task LoadDetails(string imdbID)
        {
            if (imdbID == string.Empty)
            {
                _posterFrame.IsVisible = false;
                return;
            }  
            var jsonString = await MovieStore.ReadFileAsync($"{imdbID}.json");
            var details = JsonSerializer.Deserialize<MovieDetails>(jsonString);
            if (details.Poster == string.Empty)
            {
                _posterFrame.IsVisible = false;
            }
            else
            {
                details.Poster = Path.Combine(((DataStore) MovieStore).ResourcesPath, "Posters", details.Poster);
            }
            Details = details;
        }
    }
}