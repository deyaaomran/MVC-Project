using Company.G01.DAL.Models;
using Company.G01.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Company.G01.PL.Controllers
{
    [Authorize(Roles ="Admin")]
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;


        // get,getall,add,update,delete
        // index,details,edit,delete,create

        public RolesController(RoleManager<IdentityRole> roleManager,
                               UserManager<ApplicationUser> userManager
                             )
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(RolesViewModel model)
        {
            if(!ModelState.IsValid)
            {
                var role = new IdentityRole()
                {
                    Name=model.RoleName
                };
                await _roleManager.CreateAsync(role);
                return RedirectToAction("Index");
            }
            return View();
        }
        public async Task<IActionResult> Index(string InputSearch)
        {
            var roles = Enumerable.Empty<RolesViewModel>();

            if (string.IsNullOrEmpty(InputSearch))
            {
                roles = await _roleManager.Roles.Select(R => new RolesViewModel()
                {
                    Id = R.Id,
                    RoleName = R.Name
                   
                }).ToListAsync();

            }
            else
            {
                roles = await _roleManager.Roles.Where(R => R.Name
                                                 .ToLower()
                                                 .Contains(InputSearch.ToLower()))
                                                 .Select(R => new RolesViewModel()
                                                 {
                                                     Id = R.Id,
                                                     RoleName = R.Name

                                                 }).ToListAsync();
            }



            return View(roles);
        }
        [HttpGet]
        public async Task<IActionResult> Details(string? id, string viewname = "Details")
        {
            if (id is null)
                return BadRequest(); //400


            var rolesFromDb = await _roleManager.FindByIdAsync(id);

            if (rolesFromDb is null)
                return NotFound(); //404

            var roles = new RolesViewModel()
            {
                Id = rolesFromDb.Id,
                RoleName = rolesFromDb.Name
            };




            return View(roles);
        }

        [HttpGet]
        public async Task<IActionResult> Update(string? id)
        {
            return await Details(id, nameof(Update));
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // With Action Post (Request from web me only)
        public async Task<IActionResult> Update([FromRoute] string? id, RolesViewModel model)
        {
            if (id != model.Id) return BadRequest();
            if (ModelState.IsValid)
            {

                var rolesFromDb = await _roleManager.FindByIdAsync(id);
                if (rolesFromDb is null)
                    return NotFound(); //404

                rolesFromDb.Id = model.Id;
                rolesFromDb.Name = model.RoleName;

                await _roleManager.UpdateAsync(rolesFromDb);

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
        public async Task<IActionResult> Delete([FromRoute] string? id, RolesViewModel model)
        {
            if (id != model.Id) return BadRequest();
            if (ModelState.IsValid)
            {

                var rolesFromDb = await _roleManager.FindByIdAsync(id);
                if (rolesFromDb is null)
                    return NotFound(); //404



                await _roleManager.DeleteAsync(rolesFromDb);

                return RedirectToAction("Index");

            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> AddOrRemoveUsers(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role is null) return NotFound();
            ViewData["roleId"] = roleId;

            var usersInRole = new List<UsersInRoleViewModel>();
            var users = await _userManager.Users.ToListAsync();

            foreach (var user in users)
            {
                var userInRole = new UsersInRoleViewModel()
                {
                   UserId = user.Id,
                   UserName = user.UserName
                };
                if (await _userManager.IsInRoleAsync(user,role.Name))
                {
                    userInRole.IsSelected = true;
                }
                else
                {
                    userInRole.IsSelected = false;
                }
                usersInRole.Add(userInRole);
            }
            return View(usersInRole);
        }

        [HttpPost]

        public async Task<IActionResult> AddOrRemoveUsers(string roleId,List<UsersInRoleViewModel> users)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role is null) return NotFound();

            if (ModelState.IsValid)
            {
                foreach (var user in users)
                {
                    var appUser = await _userManager.FindByIdAsync(user.UserId);
                    if(appUser is not null)
                    {
                        if (user.IsSelected && ! await _userManager.IsInRoleAsync(appUser, role.Name))
                        {
                            await _userManager.AddToRoleAsync(appUser, role.Name);
                        }
                        else if (!user.IsSelected && await _userManager.IsInRoleAsync(appUser, role.Name))
                        {
                            await _userManager.RemoveFromRoleAsync(appUser, role.Name);
                        }
                    }
                    
                }

                return RedirectToAction(nameof(Update), new { id = roleId });

            }


            return View(users);
        }



    }
}
