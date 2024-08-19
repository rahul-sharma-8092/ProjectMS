using System.ComponentModel.DataAnnotations;

namespace ProjectMS.Models
{
    public class Login
    {
        public string? Role { get; set; }

        [Display(Name = "Email ID")]
        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid")]
        public string EmailId { get; set; }


        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\W).{8,20}$", ErrorMessage ="Invalid")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
