using BL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.File
{
    public interface IImageProcessorService : IService
    {
        public byte[] Resize(byte[] data, float maxWidth);
    }
}
