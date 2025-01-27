using Company.G01.DAL.Models;
using Company.G01.PL.Helpers;
using Company.G01.PL.ViewModels.Auth;
using Humanizer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Email = Company.G01.DAL.Models.Email;

namespace Company.G01.PL.Controllers
{
    public class AccountController : Controller
    {
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AccountController(
			UserManager<ApplicationUser> userManager,
			SignInManager<ApplicationUser> signInManager)
        {
			_userManager = userManager;
			_signInManager = signInManager;
		}

		#region SingUp
		// SingUp

		[HttpGet] // Acocunt/SingUp
		public IActionResult SignUp()
		{
			return View();
		}

		[HttpPost] // Acocunt/SingUp
		public async Task<IActionResult> SignUp(SignUpViewModel model)
		{
			if (ModelState.IsValid)
			{
				// P@ssw0rd
				// SingUp
				try
				{
					var user = await _userManager.FindByNameAsync(model.UserName);
					if (user is null)
					{
						user = await _userManager.FindByEmailAsync(model.Email);
						if (user is null)
						{
							user = new ApplicationUser()
							{
								UserName = model.UserName,
								FirstName = model.FirstName,
								LastName = model.LastName,
								Email = model.Email,
								IsAgree = model.IsAgree
							};
							var Result = await _userManager.CreateAsync(user, model.Password);

							if (Result.Succeeded)
							{
								return RedirectToAction("SignIn");
							}

							foreach (var error in Result.Errors)
							{
								ModelState.AddModelError(string.Empty, error.Description);
							}
						}
						else
						{
                            ModelState.AddModelError(string.Empty, "Email is already exists !!");
                        }

					}
					else
					{
                        ModelState.AddModelError(string.Empty, "UserName is already exists !!");
                    }
					
				}
				catch (Exception ex)
				{

					ModelState.AddModelError(string.Empty, ex.Message);
				}
			}
			return View(model);
		}

		#endregion

		#region SignIn
		// SignIn
		[HttpGet]
		public IActionResult SignIn()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> SignIn(SignInViewModel model)
		{
			if (ModelState.IsValid)
			{
				try
				{
					var user = await _userManager.FindByEmailAsync(model.Email);
					if (user is not null)
					{
						// chesk password
						var flag = await _userManager.CheckPasswordAsync(user, model.Password);
						if (flag)
						{
							// SignIn
							var Resulr = await _signInManager.PasswordSignInAsync(user,model.Password,model.RememberMe,false);
							if (Resulr.Succeeded)
							{
								return RedirectToAction("Index", "Home");
							}
						}

					}
					ModelState.AddModelError(string.Empty, "Invaled Login !!");

				}
				catch (Exception ex)
				{

					ModelState.AddModelError(string.Empty, ex.Message);
				}
			}

			return View(model);
		}
		#endregion

		#region SignOut
		public async new Task<IActionResult> SignOut()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction(nameof(SignIn));
		}
		#endregion

		#region Forget Password

		[HttpGet]
		public IActionResult FrogetPassword()
		{
			return View();
		}

		//[HttpPost]
		public async Task<IActionResult> FrogetPassword(ForgetPasswordViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user =await _userManager.FindByEmailAsync(model.Email);
				if(user is not null)
				{
					// Create Token
					var token = await _userManager.GeneratePasswordResetTokenAsync(user);

					// Create URL
					var url = Url.Action("ResetPassword", "Account",new {email = model.Email , token = token },Request.Scheme);


					var email = new Email()
					{
						To = model.Email,
						Subject = "Reset Password",
						Body = url
					};

					EmailSetting.SendEmail(email);

					return RedirectToAction(nameof(CheckYourInbox));
					


				}
				ModelState.AddModelError(string.Empty, "Invaled Operation, Please try again !!");
			}
			return View(model);
		}

		


		[HttpGet]
		public IActionResult CheckYourInbox()
		{
			return View();
		}


		[HttpGet]


		public IActionResult ResetPassword(string email , string token)
		{
			TempData["Email"] = email;
			TempData["token"] = token;
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
		{
			if (ModelState.IsValid)
			{
				var email = TempData["Email"] as string;
				var token = TempData["token"] as string;

				var user = await _userManager.FindByEmailAsync(email);
				if (user is not null)
				{
					var result = await _userManager.ResetPasswordAsync(user, token, model.Password);
					if (result.Succeeded)
					{
						return RedirectToAction(nameof(SignIn));
					}

				}
			}
			ModelState.AddModelError(string.Empty, "Invaled Operation, Please try again !!");

			return View(model);
		}

		public IActionResult AccessDenied()
		{
			return View();
		}











		#endregion


	}
}
