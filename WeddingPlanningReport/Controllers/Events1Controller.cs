using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Humanizer.Localisation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WeddingPlanningReport.Models;

namespace WeddingPlanningReport.Controllers
{
    public class Events1Controller : Controller
    {
        private readonly WeddingPlanningContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public Events1Controller(WeddingPlanningContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Events1
        public async Task<IActionResult> Index()
        {
            return View(await _context.Events.ToListAsync());
        }

        // GET: Events1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Events
                .FirstOrDefaultAsync(m => m.EventId == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // GET: Events1/Create
        public IActionResult Create()
        {
            var CaseName = _context.WeddingPlans.Select(m => new {
                m.CaseId,
                DisplayName = m.CaseId + " - " + m.WeddingName
            }).ToList();

            // 建立下拉選單
            ViewBag.CaseNameID = new SelectList(CaseName, "CaseId", "DisplayName");
            return View();
        }

        // POST: Events1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EventId,CaseId,EventName,EventTime,EventLocation,EventNotes,EventLocationImg,EventVenueImg1,EventVenueImg2,IsDelete")] Event @event)
        {
            if (ModelState.IsValid)
            {
                _context.Add(@event);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(@event);
        }

        // GET: Events1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Events.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }
            var caseId = _context.Events
                .Where(c => c.EventId == id)
                .Select(c => c.CaseId)
                .FirstOrDefault();
            if (caseId != null)
            {
                var caseName = _context.WeddingPlans
                    .Where(m => m.CaseId == caseId) // 确保使用正确的字段名
                    .Select(m => m.WeddingName) // 假设姓名字段为 Name
                    .FirstOrDefault();

                ViewBag.caseIDName = caseId +"-"+caseName;
            }
            return View(@event);
        }

        // POST: Events1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EventId,CaseId,EventName,EventTime,EventLocation,EventNotes,EventLocationImg,EventVenueImg1,EventVenueImg2,IsDelete")] Event @event, IFormFile? file, IFormFile? file1, IFormFile? file2)
        {
            if (id != @event.EventId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //圖片變更start
                    string wwwRootPath = _webHostEnvironment.WebRootPath;
                    string fileName = @event.EventLocationImg; // 保留现有的图片名
                    if (file != null)
                    {
                        //string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);為圖片生成一個唯一的檔名 (Guid.NewGuid())
                        string newFileName = file.FileName;
                        string productPath = Path.Combine(wwwRootPath, @"eventImg");
                        // 防止檔名衝突，如果檔案已存在，可以加後綴或處理邏輯
                        string filePath = Path.Combine(productPath, newFileName);
                        if (System.IO.File.Exists(filePath))
                        {
                            // 檔案已存在，這裡可以根據需求修改，例如在檔名後加上時間戳
                            string fileExtension = Path.GetExtension(newFileName);
                            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(newFileName);
                            newFileName = $"{fileNameWithoutExtension}{DateTime.Now:yyyyMMddHHmmss}{fileExtension}";
                            filePath = Path.Combine(productPath, newFileName);
                        }
                        // 儲存圖片到指定路徑
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }
                        fileName = newFileName;
                    }

                    @event.EventLocationImg = fileName;
                    //圖片變更end
                    //圖片變更start
                    string fileName1 = @event.EventVenueImg1; 
                    if (file1 != null)
                    {
                        string newFileName = file1.FileName;
                        string productPath = Path.Combine(wwwRootPath, @"eventImg");
                        // 防止檔名衝突，如果檔案已存在，可以加後綴或處理邏輯
                        string filePath = Path.Combine(productPath, newFileName);
                        if (System.IO.File.Exists(filePath))
                        {
                            string fileExtension = Path.GetExtension(newFileName);
                            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(newFileName);
                            newFileName = $"{fileNameWithoutExtension}{DateTime.Now:yyyyMMddHHmmss}{fileExtension}";
                            filePath = Path.Combine(productPath, newFileName);
                        }
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await file1.CopyToAsync(fileStream);
                        }
                        fileName1 = newFileName;
                    }

                    @event.EventVenueImg1 = fileName1;
                    //圖片變更end
                    //圖片變更start
                    string fileName2 = @event.EventVenueImg2; // 保留现有的图片名
                    if (file2 != null)
                    {
                        string newFileName = file2.FileName;
                        string productPath = Path.Combine(wwwRootPath, @"eventImg");
                        // 防止檔名衝突，如果檔案已存在，可以加後綴或處理邏輯
                        string filePath = Path.Combine(productPath, newFileName);
                        if (System.IO.File.Exists(filePath))
                        {
                            // 檔案已存在，這裡可以根據需求修改，例如在檔名後加上時間戳
                            string fileExtension = Path.GetExtension(newFileName);
                            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(newFileName);
                            newFileName = $"{fileNameWithoutExtension}{DateTime.Now:yyyyMMddHHmmss}{fileExtension}";
                            filePath = Path.Combine(productPath, newFileName);
                        }
                        // 儲存圖片到指定路徑
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await file2.CopyToAsync(fileStream);
                        }
                        fileName2 = newFileName;
                    }

                    @event.EventVenueImg2 = fileName2;
                    _context.Update(@event);
                    await _context.SaveChangesAsync();
                    //圖片變更end

                }
                catch (DbUpdateConcurrencyException)
                {//在資料庫更新過程中，可能會遇到並發更新的問題這段代碼捕獲異常，如果 Venue 不存在，則返回 NotFound()，否則重新拋出異常
                    if (!EventExists(@event.EventId))
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
            return View(@event);
        }

        // GET: Events1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Events
                .FirstOrDefaultAsync(m => m.EventId == id);
            if (@event == null)
            {
                return NotFound();
            }

            var caseId = _context.Events
                .Where(c => c.EventId == id)
                .Select(c => c.CaseId)
                .FirstOrDefault();
            if (caseId != null)
            {
                var caseName = _context.WeddingPlans
                    .Where(m => m.CaseId == caseId)
                    .Select(m => m.WeddingName)
                    .FirstOrDefault();

                ViewBag.caseName = caseName;
            }
            return View(@event);
        }

        // POST: Events1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @event = await _context.Events.FindAsync(id);
            if (@event != null)
            {
                _context.Events.Remove(@event);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventExists(int id)
        {
            return _context.Events.Any(e => e.EventId == id);
        }
    }
}
