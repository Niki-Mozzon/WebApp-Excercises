using Legend_2.Data;
using Legend_2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Legend_2.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _db;

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
                //Create
                return View(order);
            }
            //Update
            order = _db.Orders.Find(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Order order)
        {
            if (ModelState.IsValid)
            {
                if (order.Id == 0)
                {
                    _db.Orders.Add(order);
                }
                else
                {
                    Order temp = _db.Orders.AsNoTracking().FirstOrDefault(x => x.Id == order.Id);
                    if (temp == null)
                    {
                        return NotFound();
                    }
                    _db.Orders.Update(order);
                }
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(order);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Order order = _db.Orders.Find(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        public IActionResult DeletePost(Order order)
        {
            Order temp = _db.Orders.AsNoTracking().FirstOrDefault(x => x.Id == order.Id);
            if (temp==null)
            {
                return NotFound();
            }
            _db.Orders.Remove(order);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
