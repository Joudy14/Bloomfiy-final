using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Bloomfiy_final.Models;

namespace Bloomfiy_final.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminOrderController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // ============= INDEX =============
        public ActionResult Index()
        {
            var orders = db.Orders
                           .Include(o => o.Items)
                           .OrderByDescending(o => o.Date)
                           .ToList();

            return View("~/Views/Admin/AdminOrder/Index.cshtml", orders);
        }

        // ============= DETAILS =============
        public ActionResult Details(int id)
        {
            var order = db.Orders
                          .Include(o => o.Items)
                          .FirstOrDefault(o => o.OrderId == id);

            if (order == null)
                return HttpNotFound();

            return View("~/Views/Admin/AdminOrder/Details.cshtml", order);
        }

        // ============= UPDATE STATUS (POST) =============
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateStatus(int id, string status)
        {
            var order = db.Orders.Find(id);
            if (order == null)
                return HttpNotFound();

            order.Status = status;
            db.SaveChanges();

            TempData["SuccessMessage"] = "Order status updated successfully.";
            return RedirectToAction("Index");
        }
    }
}
