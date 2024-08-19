using Microsoft.AspNetCore.Http.HttpResults;
using System.ComponentModel.DataAnnotations;

namespace ProjectMS.Models
{
    public class Users
    {
        public long UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailID { get; set; }
        public string Password { get; set; }
        public string PhoneNo { get; set; }
        public DateTime Joiningdate { get; set; }
        public int RoleID { get; set; }
        public string Role { get; set; }
        public int UserGroupID { get; set; }
        public bool Status { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class ForgotPasswordModel
    {
        public int Id { get; set; }
        public string? Token { get; set; }
        public string? Email { get; set; }
        public string? IPAddress { get; set; }
        public string? FullName { get; set; }
        public DateTime CreatedAT { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class ResetPasswordModel
    {
        public string? Token { get; set; }
        public string? Email { get; set; }
        public string? FullName { get; set; }

        [Required(ErrorMessage ="Required")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\W).{8,20}$", ErrorMessage = "Password must be 8-20 characters long, with at least one uppercase letter, one lowercase letter, and one special character.")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Required")]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string? ConfirmPassword { get; set; }

    }
}
