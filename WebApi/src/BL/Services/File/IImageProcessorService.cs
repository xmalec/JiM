using BL.Models.File;

namespace BL.Services.File
{
    public interface IImageProcessorService : IService
    {
        public ImageCompressResult Resize(string path, int maxWidth);
        public ImageCompressResult Compress(byte[] binary, int maxWidth);
    }
}
