using Microsoft.AspNetCore.Mvc;

namespace AMI_ProjectMVC.Controllers
{
    public class BillReadingController : Controller
    {
        public IActionResult BillReadingData()
        {
            return View();
        }

      

        public IActionResult BillReadingConsumer()
        {
            return View();
        }
    }
}
