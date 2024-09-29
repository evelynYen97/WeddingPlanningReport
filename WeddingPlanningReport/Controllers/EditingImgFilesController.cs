using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WeddingPlanningReport.Models;

namespace WeddingPlanningReport.Controllers
{
    public class EditingImgFilesController : Controller
    {
        private readonly WeddingPlanningContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public EditingImgFilesController(WeddingPlanningContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: EditingImgFiles
        public async Task<IActionResult> Index()
        {
            return View(await _context.EditingImgFiles.ToListAsync());
        }

        // GET: EditingImgFiles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var editingImgFile = await _context.EditingImgFiles
                .FirstOrDefaultAsync(m => m.EditingImgFileId == id);
            if (editingImgFile == null)
            {
                return NotFound();
            }
            // 假设 `ImgUsingsController` 的 `Index` 方法接受一个 `id` 参数
            
            return RedirectToAction("IndexMore", "ImgUsings", new { id = editingImgFile.EditingImgFileId });
        }


        // GET: EditingImgFiles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EditingImgFiles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EditingImgFileId,MemberId,EditTime,Screenshot,ImgEditingName")] EditingImgFile editingImgFile)
        {
            if (ModelState.IsValid)
            {
                _context.Add(editingImgFile);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(editingImgFile);
        }

        // GET: EditingImgFiles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var editingImgFile = await _context.EditingImgFiles.FindAsync(id);
            if (editingImgFile == null)
            {
                return NotFound();
            }
            return View(editingImgFile);
        }

        // POST: EditingImgFiles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EditingImgFileId,MemberId,EditTime,Screenshot,ImgEditingName")] EditingImgFile editingImgFile)
        {
            if (id != editingImgFile.EditingImgFileId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(editingImgFile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EditingImgFileExists(editingImgFile.EditingImgFileId))
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
            return View(editingImgFile);
        }

        // GET: EditingImgFiles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var editingImgFile = await _context.EditingImgFiles
                .FirstOrDefaultAsync(m => m.EditingImgFileId == id);
            if (editingImgFile == null)
            {
                return NotFound();
            }

            return View(editingImgFile);
        }

        // POST: EditingImgFiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var editingImgFile = await _context.EditingImgFiles.FindAsync(id);
            if (editingImgFile != null)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string productPath = Path.Combine(wwwRootPath, @"圖片與圖層\圖片\會員提供圖");
                if (!string.IsNullOrEmpty(editingImgFile.ImgEditingName))
                {
                    string filePath = Path.Combine(productPath, editingImgFile.ImgEditingName);
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                }
                _context.EditingImgFiles.Remove(editingImgFile);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool EditingImgFileExists(int id)
        {
            return _context.EditingImgFiles.Any(e => e.EditingImgFileId == id);
        }
    }
}
