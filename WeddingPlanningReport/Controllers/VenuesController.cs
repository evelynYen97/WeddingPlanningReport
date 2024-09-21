using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WeddingPlanningReport.Models;

namespace WeddingPlanningReport.Controllers
{
    public class VenuesController : Controller
    {
        private readonly WeddingPlanningContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;//0919新增

        public VenuesController(WeddingPlanningContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Venues
        public async Task<IActionResult> Index()
        {
            return View(await _context.Venues.ToListAsync());
        }


        // GET: Venues/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venue = await _context.Venues
                .FirstOrDefaultAsync(m => m.VenueId == id);
            if (venue == null)
            {
                return NotFound();
            }

            return View(venue);
        }

        // GET: Venues/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Venues/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VenueId,ShopId,MemberId,VenueFunction,VenueStyle,InOurDoor,RoadApplication,VenueName,TableCapacity,GuestCapacity,VenueRentalPrice,VenueImg,VenueInfo,AvailableTime,VenueImg2,IsDelete")] Venue venue,IFormFile? file, IFormFile? file4)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string fileName = "noimage.jpg"; // 保留现有的图片名
                string fileName2 = "noimage.jpg"; // 保留第二张图片名
                if (file != null)
                {
                    //string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);為圖片生成一個唯一的檔名 (Guid.NewGuid())
                    string newFileName = file.FileName;
                    string productPath = Path.Combine(wwwRootPath, @"Ven1");

                    // 防止檔名衝突，如果檔案已存在，可以加後綴或處理邏輯
                    string filePath = Path.Combine(productPath, newFileName);
                    if (System.IO.File.Exists(filePath))
                    {
                        // 檔案已存在，這裡可以根據需求修改，例如在檔名後加上時間戳
                        string fileExtension = Path.GetExtension(newFileName);
                        string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(newFileName);
                        newFileName = $"{fileNameWithoutExtension}_{DateTime.Now:yyyyMMddHHmmss}{fileExtension}";
                        filePath = Path.Combine(productPath, newFileName);
                    }
                    // 儲存圖片到指定路徑
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    fileName = newFileName;
                }
                venue.VenueImg = fileName;

                if (file4 != null)
                {
                    string newFileName2 = file4.FileName;
                    string productPath2 = Path.Combine(wwwRootPath, @"Ven1");

                    // 防止檔名衝突
                    string filePath2 = Path.Combine(productPath2, newFileName2);
                    if (System.IO.File.Exists(filePath2))
                    {
                        // 檔案已存在，添加时间戳
                        string fileExtension2 = Path.GetExtension(newFileName2);
                        string fileNameWithoutExtension2 = Path.GetFileNameWithoutExtension(newFileName2);
                        newFileName2 = $"{fileNameWithoutExtension2}_{DateTime.Now:yyyyMMddHHmmss}{fileExtension2}";
                        filePath2 = Path.Combine(productPath2, newFileName2);
                    }
                    // 儲存第二张图片
                    using (var fileStream2 = new FileStream(filePath2, FileMode.Create))
                    {
                        await file4.CopyToAsync(fileStream2);
                    }
                    fileName2 = newFileName2;
                }
                venue.VenueImg2 = fileName2;
                _context.Add(venue);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(venue);
        }

        // GET: Venues/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venue = await _context.Venues.FindAsync(id);//藉由id抓出venue物件
            if (venue == null)
            {
                return NotFound();
            }
            return View(venue);
        }

        // POST: Venues/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int id, [Bind("VenueId,ShopId,MemberId,VenueFunction,VenueStyle,InOurDoor,RoadApplication,VenueName,TableCapacity,GuestCapacity,VenueRentalPrice,VenueImg,VenueInfo,AvailableTime,VenueImg2,IsDelete")] Venue venue, IFormFile? file, IFormFile? file4)
        {
            if (id != venue.VenueId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)//這會檢查表單提交的資料是否合法，確保所有資料符合模型的驗證規則
            {
                try
                {
                    string wwwRootPath = _webHostEnvironment.WebRootPath;
                    string fileName = venue.VenueImg; // 保留现有的图片名
                    string fileName2 = venue.VenueImg2; // 保留第二张图片名
                    if (file != null)
                    {
                        //string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);為圖片生成一個唯一的檔名 (Guid.NewGuid())
                        string newFileName = file.FileName;
                        string productPath = Path.Combine(wwwRootPath, @"Ven1");

                        // 防止檔名衝突，如果檔案已存在，可以加後綴或處理邏輯
                        string filePath = Path.Combine(productPath, newFileName);
                        if (System.IO.File.Exists(filePath))
                        {
                            // 檔案已存在，這裡可以根據需求修改，例如在檔名後加上時間戳
                            string fileExtension = Path.GetExtension(newFileName);
                            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(newFileName);
                            newFileName = $"{fileNameWithoutExtension}_{DateTime.Now:yyyyMMddHHmmss}{fileExtension}";
                            filePath = Path.Combine(productPath, newFileName);
                        }
                        // 儲存圖片到指定路徑
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }
                        fileName = newFileName;
                    }
                    venue.VenueImg = fileName;

                    if (file4 != null)
                    {
                        string newFileName2 = file4.FileName;
                        string productPath2 = Path.Combine(wwwRootPath, @"Ven1");

                        // 防止檔名衝突
                        string filePath2 = Path.Combine(productPath2, newFileName2);
                        if (System.IO.File.Exists(filePath2))
                        {
                            // 檔案已存在，添加时间戳
                            string fileExtension2 = Path.GetExtension(newFileName2);
                            string fileNameWithoutExtension2 = Path.GetFileNameWithoutExtension(newFileName2);
                            newFileName2 = $"{fileNameWithoutExtension2}_{DateTime.Now:yyyyMMddHHmmss}{fileExtension2}";
                            filePath2 = Path.Combine(productPath2, newFileName2);
                        }
                        // 儲存第二张图片
                        using (var fileStream2 = new FileStream(filePath2, FileMode.Create))
                        {
                            await file4.CopyToAsync(fileStream2);
                        }
                        fileName2 = newFileName2;
                    }
                    venue.VenueImg2 = fileName2;
                    _context.Update(venue);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {//在資料庫更新過程中，可能會遇到並發更新的問題這段代碼捕獲異常，如果 Venue 不存在，則返回 NotFound()，否則重新拋出異常
                    if (!VenueExists(venue.VenueId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));//更新成功後，使用導向到 Index 頁面
            }
            return View(venue);
        }

        // GET: Venues/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venue = await _context.Venues
                .FirstOrDefaultAsync(m => m.VenueId == id);
            if (venue == null)
            {
                return NotFound();
            }

            return View(venue);
        }

        // POST: Venues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var venue = await _context.Venues.FindAsync(id);
            if (venue != null)
            {
                _context.Venues.Remove(venue);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VenueExists(int id)
        {
            return _context.Venues.Any(e => e.VenueId == id);
        }
    }
}
