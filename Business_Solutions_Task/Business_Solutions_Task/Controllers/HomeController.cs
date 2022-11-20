using Business_Solutions_Task.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business_Solutions_Task.Controllers
{
    public class HomeController : Controller
    {
        ModelContext db;
        public HomeController(ModelContext context)
        {
            db = context;
        }
        [HttpGet]
        public IActionResult MainPage()
        {
            return View();
        }
        [HttpGet]
        public IActionResult ViewingPage()
        {
            ViewData["PrividerId"] = new SelectList(db.Providers, "Id", "Name");
            return View();
        }
        [HttpGet]
        public IActionResult CreatePage()
        {
            ViewData["PrividerId"] = new SelectList(db.Providers, "Id", "Name");
            ViewData["OrderId"] = new SelectList(db.Orders, "Id", "Namber", "Date");
            return View();
        }


        [HttpGet]
        public JsonResult GetPage()
        {

                var orders = from c in db.OrderItems
                             select new
                             {
                                 c.Id,
                                 c.Name,
                                 c.Quantity,
                                 c.Unit,
                                 c.OrderId,
                                 ordN = c.Order.Namber,
                                 ordD = c.Order.Date.ToString("dd.MM.yyyy"),
                                 provId = c.Order.Provider.Id,
                                 provName = c.Order.Provider.Name
                             };
                return Json(orders);
            
        }
        [HttpPost]
        public IActionResult GetPage(DateTime? date1, DateTime? date2, int? prividerId)
        {

            if (prividerId != null & (date1 != null & date2 != null))
            {
                var orders = from c in db.OrderItems
                             where c.Order.Provider.Id == prividerId & (c.Order.Date > date1 & c.Order.Date < date2)
                             select new
                             {
                                 c.Id,
                                 c.Name,
                                 c.Quantity,
                                 c.Unit,
                                 c.OrderId,
                                 ordN = c.Order.Namber,
                                 ordD = c.Order.Date.ToString("dd.MM.yyyy"),
                                 provId = c.Order.Provider.Id,
                                 provName = c.Order.Provider.Name
                             };
                return Json(orders);
            }
            else if (date1 != null & date2 != null)
            {
                var orders = from c in db.OrderItems
                             where c.Order.Date > date1 & c.Order.Date < date2
                             select new
                             {
                                 c.Id,
                                 c.Name,
                                 c.Quantity,
                                 c.Unit,
                                 c.OrderId,
                                 ordN = c.Order.Namber,
                                 ordD = c.Order.Date.ToString("dd.MM.yyyy"),
                                 provId = c.Order.Provider.Id,
                                 provName = c.Order.Provider.Name
                             };
                return Json(orders);
            }
            else if ( prividerId != null)
            {
                var orders = from c in db.OrderItems
                             where c.Order.Provider.Id == prividerId
                             select new
                             {
                                 c.Id,
                                 c.Name,
                                 c.Quantity,
                                 c.Unit,
                                 c.OrderId,
                                 ordN = c.Order.Namber,
                                 ordD = c.Order.Date.ToString("dd.MM.yyyy"),
                                 provId = c.Order.Provider.Id,
                                 provName = c.Order.Provider.Name
                             };
                return Json(orders);
            }
            return NoContent();
        }
        [HttpPost]
        public IActionResult UpdatePage(int orderItemId, string name, decimal quantity, string unit, int orderId, string namber, DateTime date)
        {
            var orderitems = db.OrderItems.FirstOrDefault(q => q.Id == orderItemId);
            orderitems.Name = name;
            orderitems.Quantity = quantity;
            orderitems.Unit = unit;
            db.Update(orderitems);
            var orders = db.Orders.FirstOrDefault(q => q.Id == orderId);
            orders.Namber = namber;
            orders.Date = date;
            db.Update(orders);
            db.SaveChanges();
            return NoContent();

        }
        [HttpPost]
        public IActionResult RemoveOrderItem(int orderItemId)
        {
            var orderItem = db.OrderItems.FirstOrDefault(q => q.Id == orderItemId);

            if (orderItem != null)
            {
                db.Remove(orderItem);

                db.SaveChanges();
            }

            return NoContent();

        }
        [HttpPost]
        public IActionResult AddOrder(string number, DateTime date, int prividerId)
        {

            if (number != null && prividerId != 0)
            {
                Order neworder = new Order
                {

                    Namber = number,
                    Date = date,
                    ProviderId = prividerId
                };

                db.Add(neworder);

                db.SaveChanges();

                var OrderData = from c in db.Orders
                               where c.Id == neworder.Id
                               select new
                               {
                                   c.Id,
                                   c.Namber,
                                   c.Date,
                                   c.ProviderId
                               };

                return Json(OrderData);
            }

            return NoContent();
        }
        [HttpPost]
        public IActionResult AddOrderItem(string name, decimal quantity, string unit, int orderId)
        {

            if (name != null && orderId != 0)
            {
                OrderItem neworderitem = new OrderItem
                {

                    Name = name,
                    Quantity = quantity,
                    Unit = unit,
                    OrderId = orderId
                };

                db.Add(neworderitem);

                db.SaveChanges();

                var OrderItemData = from c in db.OrderItems
                                    where c.Id == neworderitem.Id
                                select new
                                {
                                    c.Id,
                                    c.Name,
                                    c.Quantity,
                                    c.Unit,
                                    c.OrderId
                                };

                return Json(OrderItemData);
            }

            return NoContent();
        }
    }
}
