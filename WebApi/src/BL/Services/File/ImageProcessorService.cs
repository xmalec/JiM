﻿using BL.Models.File;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.File
{
    public class ImageProcessorService : IImageProcessorService
    {
        public ImageCompressResult Compress(byte[] binary, int maxWidth)
        {
            using (var image = Image.Load(binary.AsSpan()))
            {
                return ResizeInternal(image, maxWidth);
            }
        }

        public ImageCompressResult Resize(string path, int maxWidth)
        {
            using (var image = Image.Load(path))
            {
                return ResizeInternal(image, maxWidth);
            }
        }

        private ImageCompressResult ResizeInternal(Image image, float maxWidth)
        {
            var ratio = Math.Min(1, maxWidth / image.Width);
            var result = new ImageCompressResult()
            {
                Width = (int)(image.Width * ratio),
                Height = (int)(image.Height * ratio)
            };
            image.Mutate(x => x
                 .Resize(result.Width, result.Height));
            using (var ms = new MemoryStream())
            {
                image.Save(ms, image.Metadata.DecodedImageFormat);
                result.Binary = ms.ToArray();
                return result;
            }
        }
    }
}
