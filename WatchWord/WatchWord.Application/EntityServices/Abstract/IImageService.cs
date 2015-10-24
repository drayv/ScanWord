using System.IO;

namespace WatchWord.Application.EntityServices.Abstract
{
    public interface IImageService
    {
        byte[] CropImage(Stream imageStream, int width, int height);
    }
}