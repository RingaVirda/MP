using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MaybeForms.Model;

namespace MaybeForms.Services
{
    public class ImagesStore : IDataStore<Images>
    {
        private List<Images> _images;

        public ImagesStore()
        {
            _images = new List<Images>();
            _images.Add(new Images());
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