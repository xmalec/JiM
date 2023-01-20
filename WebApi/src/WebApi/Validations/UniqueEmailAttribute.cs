using BL.Services.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.DI;

namespace WebApi.Validations
{
    public class UniqueEmailAttribute : ValidationAttribute
    {

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var userService = ServiceFactory.Current.ServiceProvider.GetService<IUserService>();
            var email = value as string;
            if (email != null && !(userService.IsEmailUnique(email).GetAwaiter().GetResult())) {
                return new ValidationResult($"Email {email} is not unique.");
            }
            return ValidationResult.Success;
        }
    }
}
