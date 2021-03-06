﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DemoASPNET4.Models;

namespace DemoASPNET4.Controllers
{
    public class EmoFacesController : Controller
    {
        private DemoASPNET4Context db = new DemoASPNET4Context();

        // GET: EmoFaces
        public ActionResult Index()
        {
            var emoFaces = db.EmoFaces.Include(e => e.Picture);
            return View(emoFaces.ToList());
        }

        // GET: EmoFaces/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmoFace emoFace = db.EmoFaces.Find(id);
            if (emoFace == null)
            {
                return HttpNotFound();
            }
            return View(emoFace);
        }

        // GET: EmoFaces/Create
        public ActionResult Create()
        {
            ViewBag.EmoPictureId = new SelectList(db.EmoPictures, "Id", "Name");
            return View();
        }

        // POST: EmoFaces/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,EmoPictureId,X,Y,Width,Height")] EmoFace emoFace)
        {
            if (ModelState.IsValid)
            {
                db.EmoFaces.Add(emoFace);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmoPictureId = new SelectList(db.EmoPictures, "Id", "Name", emoFace.EmoPictureId);
            return View(emoFace);
        }

        // GET: EmoFaces/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmoFace emoFace = db.EmoFaces.Find(id);
            if (emoFace == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmoPictureId = new SelectList(db.EmoPictures, "Id", "Name", emoFace.EmoPictureId);
            return View(emoFace);
        }

        // POST: EmoFaces/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,EmoPictureId,X,Y,Width,Height")] EmoFace emoFace)
        {
            if (ModelState.IsValid)
            {
                db.Entry(emoFace).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmoPictureId = new SelectList(db.EmoPictures, "Id", "Name", emoFace.EmoPictureId);
            return View(emoFace);
        }

        // GET: EmoFaces/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmoFace emoFace = db.EmoFaces.Find(id);
            if (emoFace == null)
            {
                return HttpNotFound();
            }
            return View(emoFace);
        }

        // POST: EmoFaces/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EmoFace emoFace = db.EmoFaces.Find(id);
            db.EmoFaces.Remove(emoFace);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
