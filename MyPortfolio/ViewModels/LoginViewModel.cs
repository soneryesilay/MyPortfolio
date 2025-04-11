// ViewModels/LoginViewModel.cs
using System.ComponentModel.DataAnnotations;

namespace MyPortfolio.ViewModels 
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Kullanıcı adı boş bırakılamaz.")]
        [Display(Name = "Kullanıcı Adı")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Şifre boş bırakılamaz.")]
        [DataType(DataType.Password)]
        [Display(Name = "Şifre")]
        public string Password { get; set; }

        public string? ReturnUrl { get; set; } // Giriş sonrası yönlendirme için (isteğe bağlı)
    }
}