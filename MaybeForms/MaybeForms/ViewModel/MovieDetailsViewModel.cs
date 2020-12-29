using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using MaybeForms.Model;
using Xamarin.Forms;

namespace MaybeForms.ViewModel
{
    [QueryProperty(nameof(ImdbID), nameof(ImdbID))]
    public class MovieDetailsViewModel : ViewModelBase
    {
        private string url = "http://www.omdbapi.com/?apikey=7e9fe69e&i={0}";
        private readonly Frame _posterFrame;
        private string _imdbID;
        private MovieDetails _details;

        public MovieDetailsViewModel(Frame posterFrame)
        {
            _posterFrame = posterFrame;
            PageTitle = "Details";
            _details = new MovieDetails();
        }

        public bool IsLoading { get; set; }
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
            IsLoading = true;
            if (imdbID == string.Empty)
            {
                _posterFrame.IsEnabled = false;
                return;
            }

            try
            {
                using (WebClient client = new WebClient())
                {
                    var jsonString = client.DownloadString(string.Format(url, imdbID));
                    var details = JsonSerializer.Deserialize<MovieDetails>(jsonString);
                    if (details.Poster == string.Empty || details.Poster == "N/A")
                    {
                        _posterFrame.IsEnabled = false;
                    }
                    else
                    {
                        details.Poster = details.Poster;
                    }

                    Details = details;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}