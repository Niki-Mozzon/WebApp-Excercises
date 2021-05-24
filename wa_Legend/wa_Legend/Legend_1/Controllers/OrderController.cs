using Legend_1.Data;
using Legend_1.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Legend_1.Controllers
{
    public class OrderController : Controller
    {
        readonly private ApplicationDbContext _db;

        public OrderController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Order> orders = _db.Orders;
            return View(orders);
        }

        public IActionResult Upsert(int? id)
        {
            Order order = new();
            if (id == null)
            {
                //insert
                return View(order);
            }
            else
            {
                //update
                order = _db.Orders.Find(id);
                if (order == null)
                {
                    return NotFound();
                }
                return View(order);
            }
        }

        [HttpPost]
        public IActionResult Upsert(Order order)
        {
            if (ModelState.IsValid)
            {
                if (order.Id != 0)
                {
                    _db.Orders.Update(order);
                }
                else
                {
                    _db.Orders.Add(order);
                }
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(order);
        }


        public IActionResult Delete(int? id)
        {
            Order order = new();
            if (id == null || id == 0)
            {
                return NotFound();
            }
            order = _db.Orders.Find(id);
            return View(order);
        }

        public IActionResult DeletePost(Order order)
        {
            order = _db.Orders.Find(order.Id);
            if (order == null)
            {
                return NotFound();
            }
            _db.Orders.Remove(order);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
