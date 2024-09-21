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
    public class ImgUsingsController : Controller
    {
        private readonly WeddingPlanningContext _context;

        public ImgUsingsController(WeddingPlanningContext context)
        {
            _context = context;
        }

        // GET: ImgUsings
        public async Task<IActionResult> Index(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // 根据传入的 id 过滤 ImgUsings 数据
            var imgUsings = await _context.ImgUsings
                .Where(iu => iu.EditingImgFileId == id) // 假设 ImgUsings 表中有一个外键字段来关联到 EditingImgFiles
                .ToListAsync();

            if (!imgUsings.Any())
            {
                return NotFound(); // 如果没有找到相关的记录，返回 NotFound()
            }
            return View(imgUsings); // 返回过滤后的数据
        }


        // GET: ImgUsings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var imgUsing = await _context.ImgUsings
                .FirstOrDefaultAsync(m => m.ImgUsingId == id);
            if (imgUsing == null)
            {
                return NotFound();
            }

            return View(imgUsing);
        }

        // GET: ImgUsings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ImgUsings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ImgUsingId,EditingImgFileId,MemberMaterialId,MaterialId,ImgX,ImgY,ImgW,ImgH,IsDelete")] ImgUsing imgUsing)
        {
            if (ModelState.IsValid)
            {
                _context.Add(imgUsing);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(imgUsing);
        }

        // GET: ImgUsings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var imgUsing = await _context.ImgUsings.FindAsync(id);
            if (imgUsing == null)
            {
                return NotFound();
            }
            return View(imgUsing);
        }

        // POST: ImgUsings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ImgUsingId,EditingImgFileId,MemberMaterialId,MaterialId,ImgX,ImgY,ImgW,ImgH,IsDelete")] ImgUsing imgUsing)
        {
            if (id != imgUsing.ImgUsingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(imgUsing);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ImgUsingExists(imgUsing.ImgUsingId))
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
            return View(imgUsing);
        }

        // GET: ImgUsings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var imgUsing = await _context.ImgUsings
                .FirstOrDefaultAsync(m => m.ImgUsingId == id);
            if (imgUsing == null)
            {
                return NotFound();
            }

            return View(imgUsing);
        }

        // POST: ImgUsings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var imgUsing = await _context.ImgUsings.FindAsync(id);
            if (imgUsing != null)
            {
                _context.ImgUsings.Remove(imgUsing);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ImgUsingExists(int id)
        {
            return _context.ImgUsings.Any(e => e.ImgUsingId == id);
        }
    }
}
