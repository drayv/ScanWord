using System.Drawing;
using System.IO;
using ImageProcessor;
using ImageProcessor.Imaging;
using WatchWord.Application.EntityServices.Abstract;

namespace WatchWord.Application.EntityServices.Concrete
{
    public class ImageService : IImageService
    {
        public byte[] CropImage(Stream imageStream, int width, int height)
        {
            byte[] imageBytes;
            using (var outStream = new MemoryStream())
            {
                using (var imageFactory = new ImageFactory())
                {
                    var format = new ImageProcessor.Imaging.Formats.JpegFormat();
                    var size = new Size(width, height);
                    // sets Crop, but u can use Stretch for example
                    imageFactory.Load(imageStream).Resize(new ResizeLayer(size, ResizeMode.Crop)).Format(format).Save(outStream);
                }

                imageBytes = outStream.ToArray();
            }

            return imageBytes;
        }
    }
}