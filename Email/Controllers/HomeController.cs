using Email.Models;
using Email.UserRepositoryService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace Email.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserService userService;
        public HomeController(ILogger<HomeController> logger, UserService userService_)
        {
            _logger = logger;
            userService= userService_;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            if (User.Identity.IsAuthenticated) return Redirect("/Menu/Index");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            if (!ModelState.IsValid) return View();
            ClaimsPrincipal cookies = await userService.Login(loginModel);
            ViewData["InputValidation"] = cookies is null ? "Wrong Password or Email" : null;
            if (cookies is null) return View();
            await HttpContext.SignInAsync(cookies);
            return Redirect("/Menu/Index");
        }
        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }
		[HttpPost]
        public async Task<IActionResult> Register(RegisterModel accountModel)
        {
            if (!ModelState.IsValid) return View();
            bool correctInput = await userService.Create(accountModel);
            ViewData["emailAddress"] = correctInput ? null : "Email address is incorrect or already exists!.";
            if (!correctInput) return View();
            return RedirectToAction("Login");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}