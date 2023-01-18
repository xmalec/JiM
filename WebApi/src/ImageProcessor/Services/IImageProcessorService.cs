using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessor.Services
{
    public interface IImageProcessorService
    {
        public byte[] Resize(byte[] data, int maxWidth, int maxHeight);
    }
}
