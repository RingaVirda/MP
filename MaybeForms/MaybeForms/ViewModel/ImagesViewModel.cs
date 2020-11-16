using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using MaybeForms.Model;
using MaybeForms.Services;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace MaybeForms.ViewModel
{
    public class ImagesViewModel : ViewModelBase
    {
        private ObservableCollection<Images> _images;

        public ImagesViewModel()
        {
            PageTitle = "Images";
            _images = new ObservableCollection<Images>();
            LoadImagesCommand = new Command(async () => await LoadImages());
            AddImageCommand = new Command(OnAddImage);
        }


        public ObservableCollection<Images> Images
        {
            get => _images;
            set => SetProperty(ref _images, value);
        }

        public Command LoadImagesCommand { get; set; }
        public Command AddImageCommand { get; }

        private async Task LoadImages()
        {
            IsBusy = true;
            try
            {
                Images.Clear();
                var images = await ImagesStore.GetItemsAsync();
                images.ForEach(i => _images.Add(i));
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

        private async void OnAddImage(object obj)
        {
            var path = await DependencyService.Get<IImagePicker>().GetImageStreamAsync();
            var images = await ((ImagesStore) ImagesStore).GetLastImages();
            if (images == null || !images.AddImage(path))
            {
                images = new Images();
                images.AddImage(path);
                await ImagesStore.AddItemAsync(images);
            }
        }

        public async void OnAppearing()
        {
            await LoadImages();
        }
    }
}