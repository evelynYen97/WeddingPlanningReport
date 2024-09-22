using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WeddingPlanningReport.Models;

namespace WeddingPlanningReport.Controllers
{
    public class CakesController : Controller
    {
        private readonly WeddingPlanningContext _context;

        public CakesController(WeddingPlanningContext context)
        {
            _context = context;
        }

        // GET: Cakes
        public async Task<IActionResult> Index()
        {
            return View();
        }

        //GET: Cakes/IndexJson
        public async Task<JsonResult> IndexJson()
        {
            Debug.WriteLine("XDDDDDDDD");
            return Json(_context.Cakes);          
        }

        //GET:Cakes/GetPicture(讀取圖片)
        //public async Task<FileResult> GetPicture(int id)
        //{ 
        //    Cake c = await _context.Cakes.FindAsync(id);
        //    byte[] ImageContent = c?.CakeImg;
        //    return File(ImageContent, "image/jpeg");
        //}


        // GET: Cakes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cake = await _context.Cakes
                .FirstOrDefaultAsync(m => m.CakeId == id);
            if (cake == null)
            {
                return NotFound();
            }

            return View(cake);
        }

        // GET: Cakes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cakes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Cake cake, string? CakeImgBase64)
        {
            if (ModelState.IsValid)
            {
                // 檢查是否有上傳圖片的 Base64 資料
                if (!string.IsNullOrEmpty(CakeImgBase64))
                {
                    // 將 Base64 字串轉換為二進制資料
                    var base64Data = CakeImgBase64.Split(',')[1];
                    var imageBytes = Convert.FromBase64String(base64Data);

                    // 儲存圖片檔案
                    var fileName = $"{Guid.NewGuid()}.jpg"; // 或者根據需要使用 PNG
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/cakesImg", fileName);
                    System.IO.File.WriteAllBytes(filePath, imageBytes);

                    Debug.WriteLine(Directory.GetCurrentDirectory());

                    // 保存圖片名稱到資料庫
                    cake.CakeImg = fileName;
                }
                else
                {
                    // 如果沒有上傳圖片，可以選擇不更新圖片
                    cake.CakeImg = "default.jpg"; // 可以根據需要指定預設圖片
                }

                // 儲存其他資料
                _context.Add(cake);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(cake);
        }

        // GET: Cakes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var cake = await _context.Cakes.FindAsync(id);
            if (cake == null)
            {
                return NotFound();
            }
            return View(cake);
        }

        // POST: Cakes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Cake cake, string? CakeImgBase64) 
        {            

           
            if (id != cake.CakeId)
            {
                return NotFound();
            }
            

            if (ModelState.IsValid)
            {
                
                try
                {
                    
                    if (!string.IsNullOrEmpty(CakeImgBase64))
                    {
                        // 將 Base64 字串轉換為二進制資料
                        var base64Data = CakeImgBase64.Split(',')[1];
                        var imageBytes = Convert.FromBase64String(base64Data);

                        // 儲存圖片檔案
                        var fileName = $"{Guid.NewGuid()}.jpg";
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/cakesImg", fileName);
                        System.IO.File.WriteAllBytes(filePath, imageBytes);

                        Debug.WriteLine(Directory.GetCurrentDirectory() + " 搜尋");

                        // 更新圖片名稱
                        cake.CakeImg = fileName;
                    }
                    else
                    {
                        // 如果沒有上傳新圖片，保留原有的圖片名稱
                        var existingCake = await _context.Cakes.AsNoTracking().FirstOrDefaultAsync(c => c.CakeId == id);
                        cake.CakeImg = existingCake?.CakeImg ?? "noimage.jpg"; // 或根據需求指定預設圖片
                    }

                    // 更新資料
                    _context.Update(cake);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CakeExists(cake.CakeId))
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

            return View(cake);
        }

        private bool CakeExists(int id)
        {
            return _context.Cakes.Any(e => e.CakeId == id);
        }

       

        // GET: Cakes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cake = await _context.Cakes
                .FirstOrDefaultAsync(m => m.CakeId == id);
            if (cake == null)
            {
                return NotFound();
            }

            return View(cake);
        }
        // POST: Cakes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cake = await _context.Cakes.FindAsync(id);
            if (cake == null)
            {
                // 如果沒有找到該項目，返回 404
                return NotFound();
            }

            // 刪除並儲存更改
            _context.Cakes.Remove(cake);
            await _context.SaveChangesAsync();

            // 重定向到 Index 頁面
            return RedirectToAction(nameof(Index));
        }
    }
}
