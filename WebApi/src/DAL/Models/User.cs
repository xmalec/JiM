namespace DAL.Models
{
    public class User : BaseEntity
    {
        public bool IsAdmin { get; set; }
        public string PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}