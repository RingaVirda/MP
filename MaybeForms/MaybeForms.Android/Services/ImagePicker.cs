using System.IO;
using System.Threading.Tasks;
using Android.Content;
using MaybeForms.Android.Services;
using MaybeForms.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(ImagePicker))]

namespace MaybeForms.Android.Services
{
    public class ImagePicker : IImagePicker
    {
        public Task<string> GetImageStreamAsync()
        {
            Intent intent = new Intent();
            intent.SetType("image/*");
            intent.SetAction(Intent.ActionGetContent);

            MainActivity.Instance.StartActivityForResult(
                Intent.CreateChooser(intent, "Select Picture"),
                MainActivity.PickImageId);

            MainActivity.Instance.PickImageTaskCompletionSource = new TaskCompletionSource<string>();

            return MainActivity.Instance.PickImageTaskCompletionSource.Task;
        }
    }
}