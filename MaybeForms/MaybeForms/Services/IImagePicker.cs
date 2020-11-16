using System.IO;
using System.Threading.Tasks;

namespace MaybeForms.Services
{
    public interface IImagePicker
    {
        Task<string> GetImageStreamAsync();
    }
}