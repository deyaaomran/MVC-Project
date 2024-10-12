using System.ComponentModel.DataAnnotations;

namespace Company.G01.PL.ViewModels.Auth
{
	public class SignUpViewModel
	{
		[Required(ErrorMessage = "UserName is Required !!")]
		public string UserName { get; set; }

		[Required(ErrorMessage = "FirstName is Required !!")]
		public string FirstName { get; set; }

		[Required(ErrorMessage = "LastName is Required !!")]
		public string LastName { get; set; }



		[Required(ErrorMessage = "LastName is Required !!")]
		[EmailAddress(ErrorMessage = "Invaled Email !!")]
		public string Email { get; set; }



		[Required(ErrorMessage = "Password is Required !!")]
		[DataType(DataType.Password)]
		
		public string Password { get; set; }

		[Required(ErrorMessage = "Password is Required !!")]
		[DataType(DataType.Password)]
		[Compare(nameof(Password),ErrorMessage = "Confimed Password dose not match Password !! ")]
		public string ConfimedPassword { get; set; }

        public bool IsAgree { get; set; }	





    }
}
