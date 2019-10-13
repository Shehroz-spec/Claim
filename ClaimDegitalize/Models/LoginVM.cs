using System.ComponentModel.DataAnnotations;

namespace ClaimDigitalize.Models
{
    public class LoginVM
    {
        [Required]
        [Display(Name = "UserName")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public string RememberMe { get; set; }

        public string GrantType { get; set; }
    }
}