using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessor.Services
{
    public class ImageProcessorService : IImageProcessorService
    {
        public byte[] Resize(byte[] data, int maxWidth, int maxHeight)
        {
            IImageFormat format= null;
            using(var image = Image.Load(data, out format)) {
                var ratio = Math.Min(1, Math.Min(maxWidth / image.Width, maxHeight / image.Height));
                image.Mutate(x => x
                     .Resize(image.Width * ratio, image.Height * ratio));
                using (var ms = new MemoryStream())
                {
                    image.Save(ms, format);
                    return ms.ToArray();
                }
            }
        }
    }
}
