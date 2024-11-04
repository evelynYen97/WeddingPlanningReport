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
    public class ShopReviewsAPIController : ControllerBase
    {
        private readonly WeddingPlanningContext _context;
        private readonly IWebHostEnvironment _host;


        public ShopReviewsAPIController(WeddingPlanningContext context, IWebHostEnvironment host)
        {
            _context = context;
            _host = host;
        }

        // GET: api/ShopReviewsAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShopReview>>> GetShopReviews()
        {
            return await _context.ShopReviews.ToListAsync();
        }

        // GET: api/ShopReviewsAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ShopReview>> GetShopReview(int id)
        {
            var shopReview = await _context.ShopReviews.FindAsync(id);

            if (shopReview == null)
            {
                return NotFound();
            }

            return shopReview;
        }

        // PUT: api/ShopReviewsAPI/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShopReview(int id, ShopReview shopReview)
        {
            if (id != shopReview.ShopReviewId)
            {
                return BadRequest();
            }

            _context.Entry(shopReview).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShopReviewExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ShopReviewsAPI
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ShopReview>> PostShopReview(ShopReview shopReview)
        {
            _context.ShopReviews.Add(shopReview);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetShopReview", new { id = shopReview.ShopReviewId }, shopReview);
        }

        // DELETE: api/ShopReviewsAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShopReview(int id)
        {
            var shopReview = await _context.ShopReviews.FindAsync(id);
            if (shopReview == null)
            {
                return NotFound();
            }

            _context.ShopReviews.Remove(shopReview);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        //POST: api/ShopReviewsAPI/ShopReviewsAndImgs
        [HttpPost("ShopReviewsAndImgs")]
        public async Task<ActionResult<ShopReview>> PostShopReviewAndImgs([FromForm] ShopReview shopReview, [FromForm] List<IFormFile> files)
        {
            await _context.ShopReviews.AddAsync(shopReview);
            await _context.SaveChangesAsync();

            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    // 加時間戳記
                    var timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
                    var fileExtension = Path.GetExtension(file.FileName);
                    var newFileName = $"{Path.GetFileNameWithoutExtension(file.FileName)}_{timestamp}{fileExtension}";
                    string filePath = Path.Combine(_host.WebRootPath, "reviewImg", newFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }

                    byte[] imgByte;
                    using (var memoryStream = new MemoryStream())
                    {
                        await file.CopyToAsync(memoryStream);   //memoryStream是在記憶體的一種儲存方式
                        imgByte = memoryStream.ToArray(); //把圖片轉成二進位
                    }
                    var reviewImg = new ReviewImage
                    {
                        ShopReviewId = shopReview.ShopReviewId,
                        ImageUrl = imgByte,
                        ImageName = newFileName
                    };
                    await _context.ReviewImages.AddAsync(reviewImg);
                }
            }
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetShopReview", new { id = shopReview.ShopReviewId }, shopReview);
        }

        //DELETE : api/ShopReviewsAPI/DeleteShopReviewAndImg/5
        [HttpDelete("DeleteShopReviewAndImg/{id}")]
        public async Task<string> DeleteShopReviewAndImg(int id)
        {
            // 找到指定的評論
            var shopReview = await _context.ShopReviews.FindAsync(id);

            if (shopReview == null)
            {
                return "查無此評論"; // 如果找不到，返回 404
            }

            var reviewImgs = _context.ReviewImages.Where(rimg => rimg.ShopReviewId == id).ToArray();

            // 刪除相關圖片
            foreach (var image in reviewImgs)
            {
                // 這裡可以選擇刪除檔案
                string filePath = Path.Combine(_host.WebRootPath, "reviewImg", image.ImageName);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath); // 刪除圖片檔案
                }
            }

            // 刪除評論和圖片
            _context.ReviewImages.RemoveRange(reviewImgs); // 刪除相關圖片
            _context.ShopReviews.Remove(shopReview); // 刪除評論
            await _context.SaveChangesAsync(); // 提交變更

            return "刪除成功"; // 返回204 No Content
        }

        private bool ShopReviewExists(int id)
        {
            return _context.ShopReviews.Any(e => e.ShopReviewId == id);
        }
    }
}
