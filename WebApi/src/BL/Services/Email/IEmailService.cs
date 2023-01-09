using BL.Models.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.Email
{
    public interface IEmailService : IService
    {
        Task SendEmailAsync(EmailRequest mailRequest);
    }
}
