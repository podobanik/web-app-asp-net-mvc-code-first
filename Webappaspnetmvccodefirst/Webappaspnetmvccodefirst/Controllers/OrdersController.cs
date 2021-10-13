using Webappaspnetmvccodefirst.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Webappaspnetmvccodefirst.Controllers
{
    public class OrdersController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            GosuslugiContext db = new GosuslugiContext();
            var orders = db.Orders.ToList();
            return View(orders);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var order = new Order();
            return View(order);
        }

        [HttpPost]
        public ActionResult Create(Order model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var db = new GosuslugiContext();
            db.Orders.Add(model);
            db.SaveChanges();
            return RedirectPermanent("/Orders/Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var db = new GosuslugiContext();
            var order = db.Orders.FirstOrDefault(x => x.Id == id);

            if (order == null)
                return RedirectPermanent("/Orders/Index");

            db.Orders.Remove(order);
            db.SaveChanges();

            return RedirectPermanent("/Orders/Index");

        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            var db = new GosuslugiContext();
            var order = db.Orders.FirstOrDefault(x => x.Id == id);

            if (order == null)
                return RedirectPermanent("/Orders/Index");


            return View(order);

        }

        [HttpPost]
        public ActionResult Edit(Order model)
        {


            var db = new GosuslugiContext();
            var order = db.Orders.FirstOrDefault(x => x.Id == model.Id);

            if (order == null)
                ModelState.AddModelError("Id", "Запись не найдена");

            if (!ModelState.IsValid)
                return View(model);

            MappingOrder(model, order);

            db.Entry(order).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectPermanent("/Orders/Index");
        }
        private void MappingOrder(Order source, Order destination)
        {
            destination.Procedure = source.Procedure;
            destination.Datetime = source.Datetime;
            destination.Description = source.Description;
        }

    }
}