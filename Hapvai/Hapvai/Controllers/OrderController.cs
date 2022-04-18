using Hapvai.Data;
using Hapvai.Data.Models;
using Hapvai.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hapvai.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext context;
        const string SessionOrderId = "_OrderId";

        public OrderController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public ActionResult Index()
        {
            var currentOrderId = HttpContext.Session.GetInt32(SessionOrderId);
            if (currentOrderId == null) 
            {
                return View();
            }
            var orderFromDb = this.context.Orders.FirstOrDefault(o=> o.Id == currentOrderId);
            var products = this.context.Products.Where(p => p.OrderId == currentOrderId).ToList();

            var orderView = new OrderViewModel() 
            {
                OrderId = orderFromDb.Id,
                RestaurantId = orderFromDb.Id,
                Products = products
            };

            return View(orderView);
        }

        // GET: OrderController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: OrderController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrderController/Create
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

        // GET: OrderController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: OrderController/Edit/5
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

        // GET: OrderController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OrderController/Delete/5
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
