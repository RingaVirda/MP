using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using MaybeForms.Model;

namespace MaybeForms.Services
{
    public class ImagesStore : IDataStore<Images>
    {
        private List<Images> _images;

        private string url =
            "https://pixabay.com/api/?key=19193969-87191e5db266905fe8936d565&q=fun+party&image_type=photo&per_page=30";

        public ImagesStore()
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    _images = new List<Images>();
                    _images.Add(new Images());
                    var jsonString = client.DownloadString(url);
                    var start = jsonString.IndexOf('[');
                    var end = jsonString.IndexOf(']');
                    jsonString = jsonString.Substring(start, end - start + 1);
                    var imageList = JsonSerializer.Deserialize<List<ImageSs>>(jsonString, new JsonSerializerOptions {PropertyNameCaseInsensitive = true});
                    foreach (var image in imageList)
                    {
                        var images = _images.Last();
                        if (images == null || !images.AddImage(image.LargeImageURL))
                        {
                            images = new Images();
                            images.AddImage(image.LargeImageURL);
                            _images.Add(images);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<bool> AddItemAsync(Images images)
        {
            _images.Add(images);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(Images images)
            => throw new NotImplementedException();

        public async Task<bool> UpdateItemAsync(Images images)
            => throw new NotImplementedException();

        public async Task<Images> GetItemAsync(string id)
            => throw new NotImplementedException();

        public async Task<IEnumerable<Images>> GetItemsAsync(bool forceRefresh = false)
            => await Task.FromResult(_images);

        public async Task<Images> GetLastImages()
            => await Task.FromResult(_images.Last());
    }
}