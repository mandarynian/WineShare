using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WineDocumentation.Infrastructure.Service;
using WineDocumentation.Infrastructure.DTO;
using WineDocumentation.Infrastructure.Commands.Users;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace WineDocumentation.Api.Controllers 
{
    [Route("[controller]")]
    public class UsersController : Controller 
    {
        
        private readonly IUserService _userService;
        public UsersController(IUserService userService, IHttpContextAccessor httpContextAccessor)
        {
            _userService = userService;
        }


        [Route("LogIn")]
        public IActionResult LogIn()
        {
            return View();
        }


        [Route("Login")]
        [HttpPost]
        // [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string email, string password)//public async Task<IActionResult> Login([FromBody]LogInUser request)
        {
            var user  = await _userService.LoginAsync(email,password);
            
            if (user != null)
            {
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(ClaimTypes.Role, user.Role)
                };
                var identyfi = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identyfi);
                var props = new AuthenticationProperties();
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, props).Wait();


                return RedirectToAction("all", "Wines");
            }

            return RedirectToAction("Login", "Users");
        }

        [Route("Logout")]
        public IActionResult Logout()
        {
            return View();
        }


        [Route("Logout")]
        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");

        }

        [HttpGet("{email}")]
        public async Task<UserDto> Get(string email)
        {
            var user = await _userService.GetAsync(email);
            return user;
        }

        //[HttpGet("all")]
        public async Task<IEnumerable<UserDto>> GetAll()
        {
            return await _userService.GetAllAsync();
        }

        [Route("Add")]
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }


        [Route("Update")]
        [HttpPost]
        public async Task<IActionResult> Update(UpdateUser update)
        {
            if (CheckAdmin())
            {
                await _userService.UpdateAsync(Guid.Parse(update.Id), update.Email, update.Username, update.Password, update.Role);
                return RedirectToAction("AdminPanel", "Users");
            }

            return RedirectToAction("Index", "Home");
        }

        [Route("AdminPanel")]
        public async Task<ActionResult> AdminPanel()
        {
            if (CheckAdmin())
            {
                var users = await _userService.GetAllAsync();
                ViewBag.Message = users;
                return View();
            }


            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Remove(string id)
        {
            await _userService.DeleteAsync(Guid.Parse(id));
            return RedirectToAction("AdminPanel", "Users");
        }

        [HttpPost("add")]
        public async Task<IActionResult> Post(CreateUser request)
        {
            if (!CheckUserIsLogIn())
            {
                await _userService.RegisterAsync(Guid.NewGuid(), request.Email, request.Username, request.Password, "User");
                return RedirectToAction("Login", "Users");
            }
            return RedirectToAction("Logout", "Users");
        }

        private bool CheckUserIsLogIn()
        {
            var user = (HttpContext.User.Identity as ClaimsIdentity);

            if (user.Name != null)
            {
                return true;
            }

            return false;
        }

        private bool CheckAdmin()
        {
            var user = (HttpContext.User.Identity as ClaimsIdentity);
            var claim = user.FindFirst(ClaimTypes.Role);
            var userRole = claim == null ? null : claim.Value;

            if (userRole != null && userRole == "Admin")
            {
                return true;
            }

            return false;
        }


    }
}