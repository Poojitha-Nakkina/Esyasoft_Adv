using Microsoft.AspNetCore.Mvc;

namespace AMI_ProjectMVC.Controllers
{
    public class AuthController : Controller
    {

        public IActionResult Login()
        {
            return View();
        }
        public IActionResult RegisterConsumer()
        {
            return View();
        }
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpGet("Auth/ResetPassword")]
        public IActionResult ResetPassword(string token)
        {
            ViewBag.Token = token;
            return View();
        }
    }
}
