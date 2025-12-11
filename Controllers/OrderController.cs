using System.Linq;
using System.Web.Mvc;
using Bloomfiy_final.Models;
using Microsoft.AspNet.Identity;

public class OrdersController : Controller
{
    private ApplicationDbContext db = new ApplicationDbContext();

    [Authorize]
    public ActionResult MyOrders()
    {
        string userId = User.Identity.GetUserId();

        var orders = db.Orders
                       .Where(o => o.UserId == userId)
                       .OrderByDescending(o => o.Date)
                       .ToList();

        return View(orders);
    }

    [Authorize]
    public ActionResult Details(int id)
    {
        var order = db.Orders
                       .Include("Items")
                       .FirstOrDefault(o => o.OrderId == id);

        if (order == null)
            return HttpNotFound();

        // safety: user must own the order
        if (order.UserId != User.Identity.GetUserId())
            return new HttpUnauthorizedResult();

        return View(order);
    }
}
