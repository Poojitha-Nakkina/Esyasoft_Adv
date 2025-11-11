using Microsoft.AspNetCore.Mvc;

namespace AMI_ProjectMVC.Controllers
{
    public class ConsumerController : Controller
    {
        public IActionResult ConsumerData()
        {
            return View();
        }

        public IActionResult Consumer()
        {
            return View();
        }
    }
}
