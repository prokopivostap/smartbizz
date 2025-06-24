using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartBiz.Application.DTO;
using SmartBiz.Infrastructure;

namespace SmartBiz.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _context;

        public UserController(UserManager<User> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var users = _userManager.Users.ToList();

            var userDtos = new List<UserDto>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userDtos.Add(new UserDto
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    Role = roles.FirstOrDefault() ?? "—"
                });
            }

            return View(userDtos);
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(UserDto user)
        {
            if (!string.IsNullOrWhiteSpace(user.FullName) &&
                !string.IsNullOrWhiteSpace(user.Role) &&
                !string.IsNullOrWhiteSpace(user.Password))
            {
                var newUser = new User
                {
                    UserName = user.FullName,
                    FullName = user.FullName
                };

                var result = await _userManager.CreateAsync(newUser, user.Password);

                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "Не вдалося створити користувача");
                    return View(); 
                }

                var createdUser = await _context.Users.FirstOrDefaultAsync(t => t.UserName == newUser.UserName);
                var roleResult = await _userManager.AddToRoleAsync(createdUser, user.Role);

                if (!roleResult.Succeeded)
                {
                    ModelState.AddModelError("", "Не вдалося додати роль");
                }

                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Некоректні дані");
            return View(user);
        }


        [HttpPost]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(t => t.Id == id);

            await _userManager.DeleteAsync(user);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRoleAsync(int id, string newRole)
        {
            var user = await _context.Users.FirstOrDefaultAsync(t => t.Id == id);

            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                await _userManager.RemoveFromRoleAsync(user, role);
            }

            await _userManager.AddToRoleAsync(user, newRole);

            return RedirectToAction("Index");
        }
    }
}  