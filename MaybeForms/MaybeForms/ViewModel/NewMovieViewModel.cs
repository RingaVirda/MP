using System;
using MaybeForms.Model;
using MaybeForms.Pages;
using Xamarin.Forms;

namespace MaybeForms.ViewModel
{
    public class NewMovieViewModel : ViewModelBase
    {
        private readonly NewMoviePage _newMoviePage;
        private string _title;
        private string _year;
        private string _type;

        public NewMovieViewModel(NewMoviePage newMoviePage)
        {
            _newMoviePage = newMoviePage;
            PageTitle = "Add";

            AddCommand = new Command(OnAdd, ValidateProperties);

            PropertyChanged +=
                (_, __) => AddCommand.ChangeCanExecute();
        }

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public string Year
        {
            get => _year;
            set => SetProperty(ref _year, value);
        }

        public string Type
        {
            get => _type;
            set => SetProperty(ref _type, value);
        }

        public Command AddCommand { get; }

        private async void OnAdd()
        {
            await MovieStore.AddItemAsync(new Movie() {Title = Title, Year = Year, Type = Type});
            await _newMoviePage.Navigation.PopAsync();
        }

        private bool ValidateProperties()
            => int.TryParse(Year, out _) && !string.IsNullOrWhiteSpace(Title) && !string.IsNullOrWhiteSpace(Year) &&
               !string.IsNullOrWhiteSpace(Type);
    }
}