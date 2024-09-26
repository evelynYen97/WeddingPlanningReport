using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WeddingPlanningReport.Models;
using WeddingPlanningReport.Models.ViewModel;

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
            var odvm = new OrdersDetailsViewModel {
                VenueOrdersTotal = _context.Venues.Count(),
                CakeOrdersTotal = _context.CakeOrders.Count(),
                CarOrdersTotal = _context.CarRentals.Count(),
                DishesOrdersTotal = _context.Dishes.Count(),
                MemberNumber = _context.Members.Count(),
             };
            return View(odvm);
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
