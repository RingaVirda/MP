using System.Collections.Generic;
using System.IO;
using Xamarin.Forms;

namespace MaybeForms.Model
{
    public class Images
    {
        private List<string> _imageContainer = new List<string>();

        public string Image0
        {
            get => _imageContainer.Count >= 1 ? _imageContainer[0] : null;
        }

        public string Image1
        {
            get => _imageContainer.Count >= 2 ? _imageContainer[1] : null;
        }

        public string Image2
        {
            get => _imageContainer.Count >= 3 ? _imageContainer[2] : null;
        }

        public string Image3
        {
            get => _imageContainer.Count >= 4 ? _imageContainer[3] : null;
        }

        public string Image4
        {
            get => _imageContainer.Count >= 5 ? _imageContainer[4] : null;
        }

        public string Image5
        {
            get => _imageContainer.Count >= 6 ? _imageContainer[5] : null;
        }

        public string Image6
        {
            get => _imageContainer.Count >= 7 ? _imageContainer[6] : null;
        }

        public string Image7
        {
            get => _imageContainer.Count >= 8 ? _imageContainer[7] : null;
        }

        public string Image8
        {
            get => _imageContainer.Count >= 9 ? _imageContainer[8] : null;
        }

        public string Image9
        {
            get => _imageContainer.Count >= 10 ? _imageContainer[9] : null;
        }

        public bool IsFull() => _imageContainer.Count == 10;

        public bool AddImage(string image)
        {
            if (!IsFull())
            {
                _imageContainer.Add(image);
                return true;
            }

            return false;
        }
    }
}