using System.ComponentModel.DataAnnotations;
using WebApi.Validations;

namespace WebApi.Models.User
{
    public class UserCreateModel
    {
        [UniqueEmail]
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsAdmin { get; set; }
    }
}
