using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WeddingPlanningReport.Controllers
{
    public class WebsiteMaintenanceController : Controller
    {
        // GET: WebsiteMaintenanceController
        public ActionResult Index()
        {
            return View();
        }

        // GET: WebsiteMaintenanceController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: WebsiteMaintenanceController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WebsiteMaintenanceController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: WebsiteMaintenanceController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: WebsiteMaintenanceController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: WebsiteMaintenanceController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: WebsiteMaintenanceController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
