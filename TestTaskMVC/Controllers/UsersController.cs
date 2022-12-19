using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestTaskMVC.Data.DTOs;
using TestTaskMVC.Data.Services;
using TestTaskMVC.Models;

namespace TestTaskMVC.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService service;

        public UsersController(IUserService service)
        {
            this.service = service;
        }

        [HttpGet]
        public IActionResult Register(string returnUrl) => View(new UserRegister() { ReturnUrl = returnUrl });

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(User user)
        {
            return View(await service.CreateAsync(user));
        }

        [HttpPut, Authorize(Roles = "Admin")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> UpdateAsync(long id, User user)
        {
            return View(await service.UpdateAsync(id, user));
        }

        [HttpDelete, Authorize(Roles = "Admin")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            await service.DeleteAsync(id);

            return View("Done");
        }


        [HttpGet, Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllAsync()
        {
            var users = await service.GetAllAsync();
            if (users.Length == 1)
                return View("Users not found!");

            return View(users);
        }

        public async Task<IActionResult> GetByIdAsync(long id)
        {
            return View(await service.GetById(id));
        }
    }
}
