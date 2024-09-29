﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WeddingPlanningReport.Models;

namespace WeddingPlanningReport.Controllers
{
    public class SchedulesController : Controller
    {
        private readonly WeddingPlanningContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public SchedulesController(WeddingPlanningContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Schedules
        public async Task<IActionResult> Index()
        {
            return View(await _context.Schedules.ToListAsync());
        }

        public async Task<IActionResult> IndexNew()
        {
            return View(await _context.Schedules.ToListAsync());
        }

        // GET: Schedules/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedule = await _context.Schedules
                .FirstOrDefaultAsync(m => m.ScheduleId == id);
            if (schedule == null)
            {
                return NotFound();
            }

            return View(schedule);
        }

        // GET: Schedules/Create
        public IActionResult Create()
        {
            var CreateEvent = _context.Events.Select(m => new {
                m.EventId,
                DisplayName = m.EventId + " - " + m.EventName
            }).ToList();

            // 建立下拉選單
            ViewBag.eventID = new SelectList(CreateEvent, "EventId", "DisplayName");
            return View();
        }

        // POST: Schedules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ScheduleId,EventId,ScheduleTime,ScheduleStageName,ScheduleStageNotes,ScheduleStageImg1,IsDelete")] Schedule schedule, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string fileName = schedule.ScheduleStageImg1; // 保留现有的图片名
                if (file != null)
                {
                   
                    string newFileName = file.FileName;
                    string productPath = Path.Combine(wwwRootPath, @"scheduleImg");
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

                schedule.ScheduleStageImg1 = fileName;
                _context.Add(schedule);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(IndexNew));
            }
            return View(schedule);
        }

        // GET: Schedules/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedule = await _context.Schedules.FindAsync(id);
            if (schedule == null)
            {
                return NotFound();
            }
            var CreateEvent = _context.Events.Select(m => new {
                m.EventId,
                DisplayName = m.EventId + " - " + m.EventName
            }).ToList();

            var defaultEventId = _context.Schedules
                .Where(c => c.ScheduleId == id)
                .Select(c => c.EventId)
                .FirstOrDefault();

            // 建立下拉選單
            ViewBag.eventID = new SelectList(CreateEvent, "EventId", "DisplayName", defaultEventId);
            return View(schedule);
        }

        // POST: Schedules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ScheduleId,EventId,ScheduleTime,ScheduleStageName,ScheduleStageNotes,ScheduleStageImg1,IsDelete")] Schedule schedule, IFormFile? file)
        {
            if (id != schedule.ScheduleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string wwwRootPath = _webHostEnvironment.WebRootPath;
                    string fileName = schedule.ScheduleStageImg1; // 保留现有的图片名
                    if (file != null)
                    {
                        string newFileName = file.FileName;
                        string productPath = Path.Combine(wwwRootPath, @"scheduleImg");
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

                        string oldFilePath = Path.Combine(productPath, schedule.ScheduleStageImg1);
                        if (!string.IsNullOrEmpty(schedule.ScheduleStageImg1) && System.IO.File.Exists(oldFilePath))
                        {
                            System.IO.File.Delete(oldFilePath); // 删除旧文件
                        }

                        // 儲存圖片到指定路徑
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }
                        fileName = newFileName;
                    }

                    schedule.ScheduleStageImg1 = fileName;
                    _context.Update(schedule);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ScheduleExists(schedule.ScheduleId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(IndexNew));
            }
            return View(schedule);
        }

        // GET: Schedules/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedule = await _context.Schedules
                .FirstOrDefaultAsync(m => m.ScheduleId == id);
            if (schedule == null)
            {
                return NotFound();
            }
            var eventId = _context.Schedules
                .Where(c => c.ScheduleId == id)
                .Select(c => c.EventId)
                .FirstOrDefault();
            if (eventId != null)
            {
                var eventName = _context.Events
                    .Where(m => m.EventId== eventId) 
                    .Select(m => m.EventName)
                    .FirstOrDefault();

                ViewBag.eventName = eventName;
            }
            return View(schedule);
        }

        // POST: Schedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var schedule = await _context.Schedules.FindAsync(id);
            if (schedule != null)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string productPath = Path.Combine(wwwRootPath, @"scheduleImg");
                if (!string.IsNullOrEmpty(schedule.ScheduleStageImg1))
                {
                    string filePath = Path.Combine(productPath, schedule.ScheduleStageImg1);
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                }
                _context.Schedules.Remove(schedule);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(IndexNew));
        }

        private bool ScheduleExists(int id)
        {
            return _context.Schedules.Any(e => e.ScheduleId == id);
        }
    }
}
