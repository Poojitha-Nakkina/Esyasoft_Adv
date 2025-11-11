using Microsoft.AspNetCore.Mvc;

namespace AMI_ProjectMVC.Controllers
{
    public class UploadMeterReadingController : Controller
    {
        public IActionResult Upload()
        {
            return View();
        }
    }
}
