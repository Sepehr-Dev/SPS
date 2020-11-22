using System.ComponentModel.DataAnnotations;

namespace SPS.ViewModels.Account
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "نام کاربری")]
        public string UserName { get; set; }
        [Required]
        [Display(Name = "رمز عبور")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        [Display(Name = "مرا به خاطر بسپار")]
        public bool Remember { get; set; }
    }
}
