using Microsoft.AspNetCore.Mvc;

namespace WeddingPlanningReport.Controllers
{
    public class WebsiteMaintain : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
