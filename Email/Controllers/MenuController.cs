using Email.MailRepositoryService;
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
        private readonly MailService mailService;
        private string userEmail => User?.FindFirstValue(ClaimTypes.Email) ?? "";
        public MenuController(UserService userService_, MailService mailService_)
        {
            userService = userService_;
            mailService = mailService_;
        }
        public async Task<IActionResult> Index()
        {
            GetMailsModel getMails = new GetMailsModel(userEmail,"",Entity.EmailCategories.Primary,Entity.EmailTypes.Received);
            IQueryable<ListMailModel> listMailModel = await mailService.GetMails(getMails);
            return View(listMailModel);
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
        [HttpGet]
        public async Task<IActionResult> OpenMail(int mailId)
        {
            MailModel mailModel = await mailService.GetMailById(mailId);
            bool mailIsSeen = mailModel.Seen;
            if(mailIsSeen) return View(mailModel);
            UpdateMail updateMail = new UpdateMail(mailModel);
            await mailService.UpdateMail(updateMail);
            return View(mailModel);
        }
        [HttpPost]
		public async Task<IActionResult> Delete(int mailId)
		{
            await mailService.Delete(mailId);
			return Redirect("/Menu/Index");
		}
		[HttpPost]
        public async Task<IActionResult> SendMail(SendMailModel? mailModel)
        {
            if (!ModelState.IsValid) return View();
            bool mailIsSent =  await userService.SendEmail(mailModel);
            ViewData["EmailValidation"] = mailIsSent ? null : "Incorrect email input!";
            if(!mailIsSent) return View();
            return Redirect("/Menu/Index");
        }
    }
}
