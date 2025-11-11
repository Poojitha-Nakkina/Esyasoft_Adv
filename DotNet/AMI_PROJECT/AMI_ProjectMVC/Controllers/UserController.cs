using Microsoft.AspNetCore.Mvc;

namespace AMI_ProjectMVC.Controllers
{
    public class UserController : Controller
    {
        public IActionResult UserData()
        {
            return View();
        }
    }
}
