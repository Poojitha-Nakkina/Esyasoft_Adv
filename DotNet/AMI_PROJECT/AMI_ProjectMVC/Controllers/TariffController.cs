using Microsoft.AspNetCore.Mvc;

namespace AMI_ProjectMVC.Controllers
{
    public class TariffController : Controller
    {
        public IActionResult TariffData()
        {

            ViewBag.CanEdit = User.IsInRole("zoneAdmin") || User.IsInRole("superAdmin");
            return View();
        }

     
    }
}
