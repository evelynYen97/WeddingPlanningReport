using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeddingPlanningReport.Models;

namespace WeddingPlanningReport.Controllers
{
    [EnableCors("WeddingPlanningCors")]
    [Route("api/[controller]")]
    [ApiController]
    public class EventsAPIController : ControllerBase
    {
        private readonly WeddingPlanningContext _context;
        //private readonly IWebHostEnvironment _env;

        public EventsAPIController(WeddingPlanningContext context)
        {
            _context = context;
        }

        //api/EventsAPI/upload
        [HttpPost("upload")]
        public async Task<IActionResult> UploadImage(IFormFile image)
        {
            if (image == null || image.Length == 0)
                return BadRequest("沒有檔案上傳");

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "eventImg");
            var fileName = Path.GetFileName(image.FileName);
            var filePath = Path.Combine(uploadsFolder, fileName);

            // 確保上傳資料夾存在
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            // 將圖片儲存到伺服器上
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            var fileUrl = $"{Request.Scheme}://{Request.Host}/eventImg/{fileName}";

            return Ok(new { filePath = fileUrl });
        }

        private bool EventExists(int id)
        {
            return _context.Events.Any(e => e.EventId == id);
        }
    }
}
