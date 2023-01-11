using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Enums
{
    public enum EmailType
    {
        NewUser
    }

    public static class EmailTypeExtensions
    {
        public static string GetTemplateName(this EmailType emailType)
        {
            return $"{nameof(emailType)}.cshtml";
        }
    }
    
}
