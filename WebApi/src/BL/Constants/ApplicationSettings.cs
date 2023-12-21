using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Constants
{
    public class ApplicationSettings
    {
        private const string PREFIX = "ApplicationSettings";
        public static string BrandName => $"{PREFIX}.BrandName";
        public static string HelpEmail => $"{PREFIX}.HelpEmail";
    }
}
