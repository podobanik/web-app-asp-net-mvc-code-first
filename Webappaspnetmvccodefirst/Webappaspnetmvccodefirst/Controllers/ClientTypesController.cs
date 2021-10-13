using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webappaspnetmvccodefirst.Models;

namespace Webappaspnetmvccodefirst.Controllers
{
    public class ClientTypesController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            var db = new GosuslugiContext();
            var clienttypes = db.ClientTypes.ToList();

            return View(clienttypes);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var clienttypes = new ClientType();
            return View(clienttypes);
        }

        [HttpPost]
        public ActionResult Create(ClientType model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var db = new GosuslugiContext();

            db.ClientTypes.Add(model);
            db.SaveChanges();

            return RedirectPermanent("/ClientTypes/Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var db = new GosuslugiContext();
            var clienttype = db.ClientTypes.FirstOrDefault(x => x.Id == id);
            if (clienttype == null)
                return RedirectPermanent("/ClientTypes/Index");

            db.ClientTypes.Remove(clienttype);
            db.SaveChanges();

            return RedirectPermanent("/ClientTypes/Index");
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            var db = new GosuslugiContext();
            var clienttype = db.ClientTypes.FirstOrDefault(x => x.Id == id);
            if (clienttype == null)
                return RedirectPermanent("/ClientTypes/Index");

            return View(clienttype);
        }

        [HttpPost]
        public ActionResult Edit(ClientType model)
        {
            var db = new GosuslugiContext();
            var clienttype = db.ClientTypes.FirstOrDefault(x => x.Id == model.Id);
            if (clienttype == null)
                ModelState.AddModelError("Id", "Тип не найден");

            if (!ModelState.IsValid)
                return View(model);

            MappingGenre(model, clienttype);

            db.Entry(clienttype).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectPermanent("/ClientTypes/Index");
        }

        private void MappingGenre(ClientType sourse, ClientType destination)
        {
            destination.Name = sourse.Name;
        }
    }
}