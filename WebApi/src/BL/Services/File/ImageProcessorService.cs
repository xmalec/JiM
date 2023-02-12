using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.File
{
    public class ImageProcessorService : IImageProcessorService
    {
        public byte[] Resize(string path, float maxWidth)
        {
            IImageFormat? format = null;
            using (var image = Image.Load(path, out format))
            {
                var ratio = Math.Min(1, maxWidth / image.Width);
                image.Mutate(x => x
                     .Resize((int)(image.Width * ratio), (int)(image.Height * ratio)));
                using (var ms = new MemoryStream())
                {
                    image.Save(ms, format);
                    return ms.ToArray();
                }
            }
        }
    }
}
