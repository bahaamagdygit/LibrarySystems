using Library.Context;
using Library.Dtos.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Library.Mvc.Controllers
{
    
    public class AccountController : Controller
    {
        // هنا بعمل انجكت لل يوزر منجر والساين ان مانجر
        private readonly UserManager<NewUser> userManager;
        private readonly SignInManager<NewUser> signInManager;

      
        public AccountController(UserManager<NewUser> _userManager, SignInManager<NewUser> _signInManager, RoleManager<IdentityRole> _roleManager)
        {
            userManager = _userManager;
            signInManager = _signInManager;
          



        }



		[HttpGet]
		public IActionResult Registration()
		{

			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Registration(RegistrationDTO RegModel)
		{

			if (ModelState.IsValid)
			{
				NewUser user = new NewUser();
				user.Email = RegModel.Email;
				user.UserName = RegModel.Name;
				user.PhoneNumber = RegModel.PhoneNumber;
				user.UserName = RegModel.UserName;
				user.IsDeleted = false;
				//هنا بيعمل هاش للباسورد قبل مبيسيفه فالداتا بيز 
				var result = await userManager.CreateAsync(user, RegModel.Password);
				if (result.Succeeded)
				{
					//create cookie 
					// the false here for persistant
					// ياعني لو كانتر بترو هيبقا ليها كوكي مدتها كام يوم لاكن لو كانت فولس هتبقا الكوكي عل السشن

					var res = await userManager.AddToRoleAsync(user, RegModel.RoleName.ToString().ToLower());
					await signInManager.SignInAsync(user, false);
					return RedirectToAction("Login", "Account");
				}
				else
				{
					// هنا بنلف علي الايرور  
					foreach (var errItem in result.Errors)
					{
						ModelState.AddModelError("err", errItem.Description);
					}

					return View(RegModel);
				}



			}
			return View(RegModel);
		}




		[HttpGet]
        public async Task<IActionResult> Login(string returnUrl = null)
        {

          //ممكن هنا نوجه اليوزر للصفحه الي كان اوذ يروح ليها 
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO login, string returnUrl = null)
        {


            if (!ModelState.IsValid)
            {
                return View(login);
            }
            NewUser user = await userManager.FindByNameAsync(login.UserName);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Username/Email or Password incorrect");
                return View();
            }
            var result = signInManager.CheckPasswordSignInAsync(user, login.Password, true).Result;
            if (result.IsLockedOut)
            {
                ModelState.AddModelError(string.Empty, "Try again later"); return View(result);
            }
            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Username/Email or Password incorrect");
            }
            await signInManager.SignInAsync(user, login.remember);
            var userROle = await userManager.GetRolesAsync(user);
            return RedirectToAction("index", "book");

        }
       

        public async Task<IActionResult> logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }


    }
}
