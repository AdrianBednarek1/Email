using Email.Models;
using Email.Models.MailModels;
using Email.UserRepositoryService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Email.Controllers
{
    [Authorize]
    public class MenuController : Controller
    {
        private readonly UserService userService;
        private LoggedInModel loggedUser;
        private string userEmail => User?.FindFirstValue(ClaimTypes.Email) ?? "";
        public MenuController(UserService userService_)
        {
            userService = userService_;
        }
        public async Task<IActionResult> Index()
        {
            if (loggedUser == null) await Refresh();
            return View(loggedUser);
        }
        private async Task Refresh()
        {
            loggedUser = await userService.GetUserByEmail(userEmail);
        }
        [HttpGet]
        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }
        [HttpGet]
        public async Task<IActionResult> SendMail()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SendMail(MailModel? mailModel)
        {
            return View();
        }
    }
}
