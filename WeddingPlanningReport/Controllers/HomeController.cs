using MailKit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WeddingPlanningReport.Models;
using WeddingPlanningReport.Models.ViewModel;

namespace WeddingPlanningReport.Controllers
{
    public class HomeController : Controller
    {
        private readonly WeddingPlanningContext _context;
        private readonly ILogger<HomeController> _logger;
        private readonly MailService _mailService;


        public HomeController(ILogger<HomeController> logger, WeddingPlanningContext context, MailService mailService)
        {
            _logger = logger;
            _context = context;
            _mailService = mailService;

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
            var lastTwoMonthRegistered= _context.Members.Count(m => m.RegistrationTime >= firstDayOfTwoMonthAgo && m.RegistrationTime <firstDayOfLastMonth);
            var lastmonthRegistered = _context.Members.Count(m => m.RegistrationTime >= firstDayOfLastMonth && m.RegistrationTime < firstDayOfCurrentMonth);
            var thismonthRegistered= _context.Members.Count(m => m.RegistrationTime >= firstDayOfCurrentMonth && m.RegistrationTime < firstDayOfNextMonth);

            //月訂單統計
            int venueMonthOrdersTotal = _context.Venues.Count(m => m.OrderTime >= firstDayOfCurrentMonth && m.OrderTime < firstDayOfNextMonth);
            int cakeMonthOrdersTotal = _context.CakeOrders.Count(m => m.OrderTime >= firstDayOfCurrentMonth && m.OrderTime < firstDayOfNextMonth);
            int dishesMonthOrdersTotal = _context.DishesOrders.Count(m => m.OrderTime >= firstDayOfCurrentMonth && m.OrderTime < firstDayOfNextMonth);
            int carMonthOrdersTotal = _context.CarRentals.Count(m => m.OrderTime >= firstDayOfCurrentMonth && m.OrderTime < firstDayOfNextMonth);

            //郵件數量
            ViewBag.UnrepliedCount = _context.Mail.Count(m => m.IsReplied == "未回覆");
            ViewBag.RepliedCount = _context.Mail.Count(m => m.IsReplied == "已回覆");


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
                VenueMonthOrdersTotal = venueMonthOrdersTotal,
                CakeMonthOrdersTotal = cakeMonthOrdersTotal,
                DishesMonthOrdersTotal= dishesMonthOrdersTotal,
                CarMonthOrdersTotal= carMonthOrdersTotal,
                OrdersTotal= venueMonthOrdersTotal+ cakeMonthOrdersTotal+ dishesMonthOrdersTotal+ carMonthOrdersTotal,
                MailViewModels = GetUnrepliedMails() // 加入未回覆郵件的數據
            };


            return View(odvm);
        }

        // 獲取未回覆的郵件列表
        private List<MailViewModel> GetUnrepliedMails()
        {
            return _context.Mail
                .Where(m => m.IsReplied == "未回覆") 
                .Select(m => new MailViewModel
                {
                    MailId = m.MailId,
                    MemberName = m.MemberName,
                    MemberEmail = m.MemberEmail,
                    MailDate = m.MailDate,
                    MailTitle = m.MailTitle,
                }).ToList();
        }

        // 獲取未回覆郵件的部分視圖
        public IActionResult GetUnrepliedMailsPartial()
        {
            var unrepliedMails = GetUnrepliedMails();
            return PartialView("_UnrepliedMailsPartial", unrepliedMails);
        }

        // 用來動態加載回覆表單的 GET 方法
        public async Task<IActionResult> LoadReplyForm(int mailId)
        {
            var mail = await _context.Mail
                .Where(m => m.MailId == mailId)
                .Select(m => new MailViewModel
                {
                    MailId = m.MailId,
                    MemberName = m.MemberName,
                    MailTitle = m.MailTitle,
                    MailContent = m.MailContent,
                    ReplyContent = m.ReplyContent,
                })
                .FirstOrDefaultAsync();

            if (mail == null)
            {
                return NotFound();
            }

            return PartialView("_ReplyPartial", mail);
        }

        // 獲取已回覆的郵件列表
        private List<MailViewModel> GetRepliedMails()
        {
            return _context.Mail
                .Where(m => m.IsReplied == "已回覆") 
                .Select(m => new MailViewModel
                {
                    MailId = m.MailId,
                    MemberName = m.MemberName,
                    MemberEmail = m.MemberEmail,
                    MailTitle = m.MailTitle,
                    ReplyDate = m.ReplyDate,
                }).ToList();
        }

        // 獲取已回覆郵件的部分視圖
        public IActionResult GetRepliedMailsPartial()
        {
            var repliedMails = GetRepliedMails();
            return PartialView("_RepliedMailsPartial", repliedMails); 
        }


        // 獲取郵件內容
        public async Task<IActionResult> LoadMailContent(int mailId)
        {          
            var mail = await _context.Mail
                .Where(m => m.MailId == mailId)
                .Select(m => new MailViewModel
                {
                    MailId = m.MailId,
                    MemberName = m.MemberName,
                    MemberEmail = m.MemberEmail,
                    MailTitle = m.MailTitle,
                    MailContent = m.MailContent,
                    MailDate = m.MailDate,
                    ReplyContent = m.ReplyContent,
                })
                .FirstOrDefaultAsync();

            if (mail == null)
            {
                return NotFound(); 
            }

            return PartialView("_MailContentPartial", mail); 
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reply(MailViewModel mvm)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                               .Select(e => e.ErrorMessage)
                                               .ToList();
                return Json(new { success = false, message = string.Join(", ", errors) });
            }

            var mail = await _context.Mail.FindAsync(mvm.MailId);
            if (mail == null)
            {
                return Json(new { success = false, message = "郵件不存在" });
            }

            try
            {
                mail.ReplyContent = mvm.ReplyContent;
                mail.ReplyDate = DateTime.Now;
                mail.IsReplied = "已回覆";

                await _context.SaveChangesAsync();

                // 使用 Task.Run 在背景中發送郵件
                Task.Run(() => _mailService.SendEmailAsync(mail.MemberEmail, mail.MailTitle, mail.MemberName, mail.MailContent, mvm.ReplyContent));

                // 計算未回覆和已回覆郵件數量
                int unrepliedCount = await _context.Mail.CountAsync(m => m.IsReplied == "未回覆");
                int repliedCount = await _context.Mail.CountAsync(m => m.IsReplied == "已回覆");

                return Json(new { success = true, unrepliedCount, repliedCount });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "郵件回覆過程中發生錯誤：" + ex.Message });
            }
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
