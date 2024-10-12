using System.ComponentModel.DataAnnotations;

namespace Company.G01.PL.ViewModels.Auth
{
	public class ResetPasswordViewModel
	{
		[Required(ErrorMessage = "Password is Required !!")]
		[DataType(DataType.Password)]

		public string Password { get; set; }

		[Required(ErrorMessage = "Password is Required !!")]
		[DataType(DataType.Password)]
		[Compare(nameof(Password), ErrorMessage = "Confimed Password dose not match Password !! ")]
		public string ConfimedPassword { get; set; }
	}
}
