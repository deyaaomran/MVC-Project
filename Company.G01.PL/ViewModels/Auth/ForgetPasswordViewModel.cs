using System.ComponentModel.DataAnnotations;

namespace Company.G01.PL.ViewModels.Auth
{
    public class ForgetPasswordViewModel
    {
        [Required(ErrorMessage = "LastName is Required !!")]
        [EmailAddress(ErrorMessage = "Invaled Email !!")]
        public string Email { get; set; }
    }
}
