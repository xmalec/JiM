using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Models.Email
{
    public class LayoutModel : EmailBodyModel
    {
        public const string TemplateName = "emailTemplate.cshtml";

        public string Content { get; set; }
    }
}
