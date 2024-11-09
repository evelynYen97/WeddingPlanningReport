using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using MimeKit;
using WeddingPlanningReport.Models;
using WeddingPlanningReport.Models.ViewModel;

namespace WeddingPlanningReport.Controllers
{
    public class ShopReviewsController : Controller
    {
        private readonly WeddingPlanningContext _context;
        private readonly IWebHostEnvironment _host;
        private readonly ViolationNoticeMailService _violationNoticeMailService;

        public ShopReviewsController(WeddingPlanningContext context, IWebHostEnvironment host,ViolationNoticeMailService vnms)
        {
            _context = context;
            _host = host;   
            _violationNoticeMailService = vnms;
        }

        // GET: ShopReviews
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.ShopReviews.ToListAsync());
        //}

        // GET: ShopReviews
        public async Task<IActionResult> Index()
        {
            var reviews = await _context.ShopReviews.ToListAsync();
            var reviewsViewModel = new List<ReviewsViewModel>();
            foreach (var review in reviews)
            {
                ReviewsViewModel rvm = new ReviewsViewModel
                {
                    ShopReviewId = review.ShopReviewId,
                    ShopName = _context.Shops?.Where(sn => sn.ShopId == review.ShopId).Select(s => s.ShopName).FirstOrDefault(),
                    MemberName = _context.Members.Where(m => m.MemberId == review.MemberId).Select(s => s.MemberName).FirstOrDefault(),
                    Rating = review.Rating,
                    Comment = review.Comment,
                    OrderYet = review.OrderYet,
                    CreatedTime = review.CreatedTime,
                    Status = review.Status,
                };
                reviewsViewModel.Add(rvm);
            }
            return View(reviewsViewModel);
        }

        // GET: ShopReviews/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shopReview = await _context.ShopReviews
                .FirstOrDefaultAsync(m => m.ShopReviewId == id);
            if (shopReview == null)
            {
                return NotFound();
            }

            return View(shopReview);
        }

        // GET: ShopReviews/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ShopReviews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ShopReviewId,ShopId,MemberId,Rating,Comment,OrderYet,CreatedTime,Status")] ShopReview shopReview)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shopReview);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(shopReview);
        }

        // GET: ShopReviews/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _context.ShopReviews.FindAsync(id);
            if (review == null)
            {
                return NotFound();
            }
              ReviewsViewModel rvm = new ReviewsViewModel
                {
                  ShopId=review.ShopId,
                  MemberId=review.MemberId,
                    ShopReviewId = review.ShopReviewId,
                    ShopName = _context.Shops?.Where(sn => sn.ShopId == review.ShopId).Select(s => s.ShopName).FirstOrDefault(),
                    MemberName = _context.Members.Where(m => m.MemberId == review.MemberId).Select(s => s.MemberName).FirstOrDefault(),
                    Rating = review.Rating,
                    Comment = review.Comment,
                    OrderYet = review.OrderYet,
                    CreatedTime = review.CreatedTime,
                    Status = review.Status,
                };
              
            return View(rvm);
        }

        // POST: ShopReviews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ShopReviewId,ShopId,MemberId,Rating,Comment,OrderYet,CreatedTime,Status")] ShopReview shopReview)
        {
            if (id != shopReview.ShopReviewId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {   string memberEmail = _context.Members.Where(m => m.MemberId == shopReview.MemberId).Select(m => m.Email).FirstOrDefault();
                    string memberName = _context.Members.Where(m => m.MemberId == shopReview.MemberId).Select(m => m.MemberName).FirstOrDefault();
                    string shopName = _context.Shops.Where(m => m.ShopId == shopReview.ShopId).Select(s=>s.ShopName).FirstOrDefault();
                    _context.Update(shopReview);
                    await _context.SaveChangesAsync();
                    if (shopReview.Status == true)
                    {
                        Task.Run(() => _violationNoticeMailService.SendEmailAsync(memberEmail, memberName, shopReview.Comment, shopReview.CreatedTime,shopName));
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShopReviewExists(shopReview.ShopReviewId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            var review = await _context.ShopReviews.FindAsync(id);
            ReviewsViewModel rvm = new ReviewsViewModel
            {
                ShopId = review.ShopId,
                MemberId = review.MemberId,
                ShopReviewId = review.ShopReviewId,
                ShopName = _context.Shops?.Where(sn => sn.ShopId == review.ShopId).Select(s => s.ShopName).FirstOrDefault(),
                MemberName = _context.Members.Where(m => m.MemberId == review.MemberId).Select(s => s.MemberName).FirstOrDefault(),
                Rating = review.Rating,
                Comment = review.Comment,
                OrderYet = review.OrderYet,
                CreatedTime = review.CreatedTime,
            };
            return View(rvm);
        }


        // 使用 MailKit 發送停權通知郵件
        //private async Task SendAccountSuspensionEmail(string memberEmail, string memberName)
        //{
        //    var emailMessage = new MimeMessage();
        //    emailMessage.From.Add(new MailboxAddress("Admin", "admin@example.com"));
        //    emailMessage.To.Add(new MailboxAddress(memberName, memberEmail));
        //    emailMessage.Subject = "帳號停權通知";

        //    var bodyBuilder = new BodyBuilder
        //    {
        //        TextBody = $"親愛的 {memberName}，\n\n您的帳號已被停權。如有任何疑問，請與我們的客服部門聯繫。\n\n謝謝！"
        //    };

        //    emailMessage.Body = bodyBuilder.ToMessageBody();

        //    using (var smtpClient = new SmtpClient())
        //    {
        //        // 連接到 SMTP 伺服器並發送郵件
        //        await smtpClient.ConnectAsync("smtp.example.com", 587, SecureSocketOptions.StartTls);
        //        await smtpClient.AuthenticateAsync("your-email@example.com", "your-email-password");
        //        await smtpClient.SendAsync(emailMessage);
        //        await smtpClient.DisconnectAsync(true);
        //    }
        //}



        // GET: ShopReviews/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reviews = await _context.ShopReviews
                .FirstOrDefaultAsync(m => m.ShopReviewId == id);
            if (reviews == null)
            {
                return NotFound();
            }
            ReviewsViewModel rvm = new ReviewsViewModel
            {
                ShopId = reviews.ShopId,
                MemberId = reviews.MemberId,
                ShopReviewId = reviews.ShopReviewId,
                ShopName = _context.Shops?.Where(sn => sn.ShopId == reviews.ShopId).Select(s => s.ShopName).FirstOrDefault(),
                MemberName = _context.Members.Where(m => m.MemberId == reviews.MemberId).Select(s => s.MemberName).FirstOrDefault(),
                Rating = reviews.Rating,
                Comment = reviews.Comment,
                OrderYet = reviews.OrderYet,
                CreatedTime = reviews.CreatedTime,
            };
            return View(rvm);
        }

        // POST: ShopReviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shopReview = await _context.ShopReviews.FindAsync(id);
            if (shopReview != null)
            {
                _context.ShopReviews.Remove(shopReview);
            }
            var reviewImgs = _context.ReviewImages.Where(rimg => rimg.ShopReviewId == id).ToArray();

            // 刪除相關圖片
            if (reviewImgs.Any()) {
                foreach (var image in reviewImgs)
                {
                    // 這裡可以選擇刪除檔案
                    string filePath = Path.Combine(_host.WebRootPath, "reviewImg", image.ImageName);
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath); // 刪除圖片檔案
                    }
                }
                _context.ReviewImages.RemoveRange(reviewImgs);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShopReviewExists(int id)
        {
            return _context.ShopReviews.Any(e => e.ShopReviewId == id);
        }
    }
}
