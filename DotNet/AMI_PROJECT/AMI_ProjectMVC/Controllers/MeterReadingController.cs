using Microsoft.AspNetCore.Mvc;

namespace AMI_ProjectMVC.Controllers
{
    public class MeterReadingController : Controller
    {
        public IActionResult MeterReadingData()
        {
            return View();
        }

        public IActionResult ConsumerMeterData()
        {
            return View();
        }
    }
}
