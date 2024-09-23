using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WeddingPlanningReport.Models;

namespace WeddingPlanningReport.Controllers
{
    public class HomeController : Controller
    {
        private readonly WeddingPlanningContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, WeddingPlanningContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.CurrentYear = DateTime.Now.Year;
            ViewBag.NumberV = _context.Venues.Count();
            ViewBag.NumberCar=_context.Cars.Count();
            ViewBag.NumberCake=_context.Cakes.Count();
            ViewBag.NumberDishes= _context.Dishes.Count();
            ViewBag.MemberNum=_context.Members.Count();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
