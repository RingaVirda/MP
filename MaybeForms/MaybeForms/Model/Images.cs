using System;
using System.Collections.Generic;
using System.IO;
using Xamarin.Forms;

namespace MaybeForms.Model
{
    public class Images
    {
        private List<ImageSource> _imageContainer = new List<ImageSource>();

        public ImageSource Image0
        {
            get => _imageContainer.Count >= 1 ? _imageContainer[0] : null;
        }

        public ImageSource Image1
        {
            get => _imageContainer.Count >= 2 ? _imageContainer[1] : null;
        }

        public ImageSource Image2
        {
            get => _imageContainer.Count >= 3 ? _imageContainer[2] : null;
        }

        public ImageSource Image3
        {
            get => _imageContainer.Count >= 4 ? _imageContainer[3] : null;
        }

        public ImageSource Image4
        {
            get => _imageContainer.Count >= 5 ? _imageContainer[4] : null;
        }

        public ImageSource Image5
        {
            get => _imageContainer.Count >= 6 ? _imageContainer[5] : null;
        }

        public ImageSource Image6
        {
            get => _imageContainer.Count >= 7 ? _imageContainer[6] : null;
        }

        public ImageSource Image7
        {
            get => _imageContainer.Count >= 8 ? _imageContainer[7] : null;
        }

        public ImageSource Image8
        {
            get => _imageContainer.Count >= 9 ? _imageContainer[8] : null;
        }

        public ImageSource Image9
        {
            get => _imageContainer.Count >= 10 ? _imageContainer[9] : null;
        }

        public bool IsFull() => _imageContainer.Count == 10;

        public bool AddImage(string image)
        {
            if (!IsFull())
            {
                if (image.Contains("http")) _imageContainer.Add(new UriImageSource {Uri = new Uri(image)});
                else _imageContainer.Add(new FileImageSource {File = image});
                return true;
            }

            return false;
        }
    }
}