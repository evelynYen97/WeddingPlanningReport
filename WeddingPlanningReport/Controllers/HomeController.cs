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
            // 獲取當前月份
            int currentMonth = DateTime.Now.Month;
            ViewBag.CurrentMonth = currentMonth;
            // 獲取前一個月和前兩個月的月份
            ViewBag.PreviousMonth = currentMonth == 1 ? 12 : currentMonth - 1; // 如果是1月，前一個月是12月
            ViewBag.TwoMonthsAgo = currentMonth <= 2 ? (currentMonth == 1 ? 11 : 12) : currentMonth - 2;

            //三個月内登入會員人數
            var threeMonthsAgo = DateTime.Now.AddMonths(-3);
            var activeMembersCount = _context.Members
                .Count(m => m.LastLoginTime >= threeMonthsAgo);
            ViewBag.ActiveMembersCount = activeMembersCount;
            ViewBag.CurrentYear = DateTime.Now.Year;

            //過去1個月注冊人數
            var firstDayOfTwoMonthAgo= new DateTime(DateTime.Now.Year, DateTime.Now.Month - 2, 1);
            var firstDayOfLastMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month - 1, 1);
            var firstDayOfCurrentMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var firstDayOfNextMonth = firstDayOfCurrentMonth.AddMonths(1);
            var lastTwoMonthRegistered= _context.Members.Count(m => m.RegistrationTime >= firstDayOfTwoMonthAgo && m.RegistrationTime <firstDayOfLastMonth  );
            var lastmonthRegistered = _context.Members.Count(m => m.RegistrationTime >= firstDayOfLastMonth && m.RegistrationTime < firstDayOfCurrentMonth);
            var thismonthRegistered= _context.Members.Count(m => m.RegistrationTime >= firstDayOfCurrentMonth && m.RegistrationTime < firstDayOfNextMonth);

            var odvm = new OrdersDetailsViewModel {
                VenuesTotal = _context.Venues.Count(),
                CakesTotal = _context.Cakes.Count(),
                CarsTotal = _context.Cars.Count(),
                DishesTotal = _context.Dishes.Count(),
                MemberNumber = _context.Members.Count(),
                preferC=_context.Members.Count(m => m.Preference == "中式風格"),
                preferW=_context.Members.Count(m => m.Preference == "西式風格"),
                lastTwoMonthMember= lastTwoMonthRegistered,
                lastMonthMember= lastmonthRegistered,
                thisMonthMember= thismonthRegistered,
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
