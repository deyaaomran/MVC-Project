using System.ComponentModel.DataAnnotations;

namespace Company.G01.PL.ViewModels.Auth
{
	public class SignInViewModel
	{
        
		[Required(ErrorMessage = "LastName is Required !!")]
		[EmailAddress(ErrorMessage = "Invaled Email !!")]
		public string Email { get; set; }



		[Required(ErrorMessage = "Password is Required !!")]
		[DataType(DataType.Password)]

		public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
