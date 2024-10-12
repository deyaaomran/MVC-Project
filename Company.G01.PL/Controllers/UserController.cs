using Company.G01.BLL;
using Company.G01.BLL.Interfaces;
using Company.G01.DAL.Models;
using Company.G01.PL.Helpers;
using Company.G01.PL.ViewModels;
using Company.G01.PL.ViewModels.Employee;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Company.G01.PL.Controllers
{
    [Authorize(Roles ="Admin")]

    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        //private readonly UnitOfWork _unitOfWork;

        // get,getall,add,update,delete
        // index,details,edit,delete

        public UserController(UserManager<ApplicationUser> userManager
                              //UnitOfWork unitOfWork
                             )
        {
            _userManager = userManager;
            //_unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index(string InputSearch)
        {
            var users = Enumerable.Empty<UserViewModel>();

            if (string.IsNullOrEmpty(InputSearch))
            {
                users =   _userManager.Users.Select( U => new UserViewModel()
                {
                    id = U.Id,
                    FirsName = U.FirstName,
                    LastName = U.LastName,
                    Email = U.Email,
                    Roles = _userManager.GetRolesAsync(U).Result
                }).ToListAsync().Result;

            }
            else
            {
               users =   _userManager.Users.Where(U => U.Email
                                                .ToLower()
                                                .Contains(InputSearch.ToLower()))
                                                .Select(U => new UserViewModel()
                                                {
                                                    id = U.Id,
                                                    FirsName = U.FirstName,
                                                    LastName = U.LastName,
                                                    Email = U.Email,
                                                    Roles = _userManager.GetRolesAsync(U).Result
                                                }).ToListAsync().Result;
            }
            
           

            return View(users);
        }

        public async Task<IActionResult> Details(string? id, string viewname = "Details")
        {
            if (id is null)
                return BadRequest(); //400
            

            var userFromDb = await _userManager.FindByIdAsync(id);

            if (userFromDb is null)
                 return NotFound(); //404

            var user = new UserViewModel()
            {
                id = userFromDb.Id,
                FirsName= userFromDb.FirstName,
                LastName= userFromDb.LastName,
                Email = userFromDb.Email,
                Roles = _userManager.GetRolesAsync(userFromDb).Result

            };








            return View(user);
        }
        
        [HttpGet]
        public async Task<IActionResult> Update(string? id)
        {
            return await Details(id, nameof(Update));
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // With Action Post (Request from web me only)
        public async Task<IActionResult> Update([FromRoute] string? id, UserViewModel model)
        {
                    if (id != model.id) return BadRequest();
                    if (ModelState.IsValid)
                    {

                        var userFromDb = await _userManager.FindByIdAsync(id);
                        if (userFromDb is null)
                            return NotFound(); //404

                        userFromDb.FirstName = model.FirsName;
                        userFromDb.LastName = model.LastName;
                        userFromDb.Email = model.Email;

                        await _userManager.UpdateAsync(userFromDb);

                        return RedirectToAction("Index");

                    }
            return View(model);
        }

        
        // Hard Delete : Delete from database
        // Soft Delete : Update the status of the record to inactive And Data found

        [HttpGet]
        public Task<IActionResult> Delete(string? id)
        {

            return Details(id, nameof(Delete));
        }


        [HttpPost]
        [ValidateAntiForgeryToken] // With Action Post ( Request from web me only )
        public async Task<IActionResult> Delete([FromRoute] string? id, UserViewModel model)
        {
            if (id != model.id) return BadRequest();
            if (ModelState.IsValid)
            {

                var userFromDb = await _userManager.FindByIdAsync(id);
                if (userFromDb is null)
                    return NotFound(); //404

                

                await _userManager.DeleteAsync(userFromDb);

                return RedirectToAction("Index");

            }
            return View(model);
        }
        
    }
}
