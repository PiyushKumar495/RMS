using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using RailwayManagement.Models;

namespace RailwayManagement.Controllers
{
    public class AdminController : Controller
    {
        private readonly RailwayDBContext db = new RailwayDBContext(); 

        public ActionResult AdminDashboard()
        {
            var trains = db.Trains.ToList();
            return View(trains);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Trains train)
        {
            if (ModelState.IsValid)
            {
                db.Trains.Add(train);
                db.SaveChanges();
                return RedirectToAction("AdminDashboard");
            }
            return View(train);
        }

        public ActionResult Edit(int id)
        {
            var train = db.Trains.Find(id);
            if (train == null) return HttpNotFound();
            return View(train);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Trains train)
        {
            if (ModelState.IsValid)
            {
                db.Entry(train).State = (Microsoft.EntityFrameworkCore.EntityState)EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("AdminDashboard");
            }
            return View(train);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var train = db.Trains.Find(id);
            if (train != null)
            {
                db.Trains.Remove(train);
                db.SaveChanges();
            }
            return RedirectToAction("AdminDashboard");
        }


    }
}
