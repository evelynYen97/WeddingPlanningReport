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
            return View( _context.Cakes.ToList());
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
        public async Task<IActionResult> Create([Bind("CakeId,ShopId,CakeStyles,CakeName,CakeDescription,CakePrice,CakeStatus,CakeStock,CakeAnnotation,AllergenInfo,CakeContent")] Cake cake, IFormFile ImageFile)
            //加入圖片開始
        {
            if (ImageFile != null)
            {
                // 獲取檔名
                var fileName = Path.GetFileName(ImageFile.FileName);              

                // 定義儲存路徑
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/cakesImg", fileName);

                // 將圖片檔案儲存到指定位置
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(stream);
                }

                // 將檔名儲存到Cake模型中
                cake.CakeImg = fileName;
            }
            //加入圖片結束
            if (ModelState.IsValid)
            {
                _context.Add(cake);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cake);
        }

        // GET: Cakes/Edit/5
        public async Task<IActionResult> Edit(int? id )
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
        public async Task<IActionResult> Edit(int id, [Bind("CakeId,ShopId,CakeStyles,CakeName,CakeDescription,CakePrice,CakeStatus,CakeStock,CakeAnnotation,AllergenInfo,CakeContent")] Cake cake, IFormFile ImageFile)
        {
            if (id != cake.CakeId)
            {
                return NotFound();
            }

            if (ImageFile != null)
            {
                // 獲取檔名
                var fileName = Path.GetFileName(ImageFile.FileName);

                // 定義儲存路徑
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/cakesImg", fileName);

                // 將圖片檔案儲存到指定位置
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(stream);
                }

                // 將檔名儲存到Cake模型中
                cake.CakeImg = fileName;
            }

            if (!ModelState.IsValid)
            {
                // 如果模型狀態無效，重新顯示編輯頁面
                return View(cake);
            }

            try
            {
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
            if (cake != null)
            {
                _context.Cakes.Remove(cake);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CakeExists(int id)
        {
            return _context.Cakes.Any(e => e.CakeId == id);
        }
    }
}
