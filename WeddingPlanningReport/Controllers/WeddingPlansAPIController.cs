using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeddingPlanningReport.Models;
using WeddingPlanningReport.Models.ViewModel;

namespace WeddingPlanningReport.Controllers
{
    [EnableCors("WeddingPlanningCors")]
    [Route("api/[controller]")]
    [ApiController]
    public class WeddingPlansAPIController : ControllerBase
    {
        private readonly WeddingPlanningContext _context;

        public WeddingPlansAPIController(WeddingPlanningContext context)
        {
            _context = context;
        }

        // GET: api/WeddingPlansAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WeddingPlan>>> GetWeddingPlans()
        {
            return await _context.WeddingPlans.ToListAsync();
        }

        // GET: api/WeddingPlansAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WeddingPlan>> GetWeddingPlan(int id)
        {
            var weddingPlan = await _context.WeddingPlans.FindAsync(id);

            if (weddingPlan == null)
            {
                return NotFound();
            }

            return weddingPlan;
        }

        // PUT: api/WeddingPlansAPI/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWeddingPlan(int id, WeddingPlan weddingPlan)
        {
            if (id != weddingPlan.CaseId)
            {
                return BadRequest();
            }

            _context.Entry(weddingPlan).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WeddingPlanExists(id))
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

        // POST: api/WeddingPlansAPI
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<WeddingPlan>> PostWeddingPlan(WeddingPlan weddingPlan)
        {
            _context.WeddingPlans.Add(weddingPlan);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWeddingPlan", new { id = weddingPlan.CaseId }, weddingPlan);
        }

        // DELETE: api/WeddingPlansAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWeddingPlan(int id)
        {
            var weddingPlan = await _context.WeddingPlans.FindAsync(id);
            if (weddingPlan == null)
            {
                return NotFound();
            }

            _context.WeddingPlans.Remove(weddingPlan);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/WeddingPlansAPI/VenueImgData/5
        [HttpGet("VenueImgData/{id}")]
        public async Task<VenueImgDataDTO> GetVenueImgData(int id)
        {
            var weddingPlan = await _context.WeddingPlans.Where(plan => plan.MemberId == id).FirstOrDefaultAsync();
            var venue = await _context.Venues.Where(plan => plan.MemberId == id).FirstOrDefaultAsync();
            //var editing = await _context.EditingImgFiles.Where(plan => plan.MemberId == id).FirstOrDefaultAsync();

            if (weddingPlan == null)
            {
                weddingPlan = await _context.WeddingPlans.Where(plan => plan.MemberId == 1).FirstOrDefaultAsync();
            }
            if (venue == null)
            {
                venue = await _context.Venues.Where(plan => plan.MemberId == 1).FirstOrDefaultAsync();
            }
            //if (editing == null || editing.Screenshot == null)
            //{
            //    editing = await _context.EditingImgFiles.Where(plan => plan.MemberId == 1).FirstOrDefaultAsync();
            //}

            // 設定圖片路徑，使用傳入的 imgName
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Ven1", venue.VenueImg);

            if (!System.IO.File.Exists(filePath))
            {
                filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Ven1", "venue1.jpg");
            }

            // 讀取圖片並轉換為 Base64
            var venue1Bytes = await System.IO.File.ReadAllBytesAsync(filePath);
            var base64venue1Img = Convert.ToBase64String(venue1Bytes);

            // 設定圖片路徑，使用傳入的 imgName
            var filePath2 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Ven1", venue.VenueImg2);

            if (!System.IO.File.Exists(filePath))
            {
                filePath2 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Ven1", "venue2.jpg");
            }

            // 讀取圖片並轉換為 Base64
            var venue2Bytes = await System.IO.File.ReadAllBytesAsync(filePath2);
            var base64venue2Img = Convert.ToBase64String(venue2Bytes);


            VenueImgDataDTO venueImgDataDTO = new VenueImgDataDTO
            {
                venueImgName1 = base64venue1Img,
                venueImgName2 = base64venue2Img
                //editingImgName = base64Image,
            };

            return venueImgDataDTO;
        }

        private bool WeddingPlanExists(int id)
        {
            return _context.WeddingPlans.Any(e => e.CaseId == id);
        }
    }
}
