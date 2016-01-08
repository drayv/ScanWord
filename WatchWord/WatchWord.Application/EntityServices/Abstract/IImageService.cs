using System.IO;

namespace WatchWord.Application.EntityServices.Abstract
{
    /// <summary>Represents a layer for work with images.</summary>
    public interface IImageService
    {
        /// <summary>Crop original image from stream, to the specific width and heigth.</summary>
        /// <param name="imageStream">Original image stream.</param>
        /// <param name="width">Result width.</param>
        /// <param name="height">Result height.</param>
        /// <returns>Cropped image bytes.</returns>
        byte[] CropImage(Stream imageStream, int width, int height);
    }
}