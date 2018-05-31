using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GIPHYPop.Models;
using Microsoft.AspNet.Identity;

namespace GIPHYPop.Controllers
{
    public class GifsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        
        // GET: Gifs
        public ActionResult SearchGiphy()
        {
            if(!User.Identity.IsAuthenticated)
            {
                return Redirect("/Home");
            }
            return View();
        }

        // GET: Gifs
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/Home");
            }
            Guid userid = Guid.Parse(User.Identity.GetUserId());
            var gifs = db.Gifs.Include(g => g.Category);
            gifs = gifs.Where(g => g.Category.UserId == userid);
            return View(gifs.ToList());
        }

        // POST: /Gifs/Add
        public ActionResult Add(string name, string url)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/Home");
            }
            Gif model = new Gif();
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name");
            model.Url = Request["imgurl"];
            model.Name = Request["imgname"];
            return View("Create", model);
        }

        // GET: Gifs/Create
        public ActionResult Create()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/Home");
            }
            Guid userid = Guid.Parse(User.Identity.GetUserId());
            ViewBag.CategoryId = new SelectList(db.Categories.Where(c => c.UserId == userid), "Id", "Name");
            Gif model = new Gif();
            model.Url = Request["imgurl"];
            model.Name = Request["imgname"];
            return View(model);
        }

        // POST: Gifs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Url,Name,CategoryId")] Gif gif)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/Home");
            }
            if (ModelState.IsValid)
            {
                db.Gifs.Add(gif);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", gif.CategoryId);
            return View(gif);
        }

        // GET: Gifs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gif gif = db.Gifs.Find(id);
            if (gif == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", gif.CategoryId);
            return View(gif);
        }

        // POST: Gifs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Url,Name,CategoryId")] Gif gif)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/Home");
            }
            if (ModelState.IsValid)
            {
                db.Entry(gif).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", gif.CategoryId);
            return View(gif);
        }

        // GET: Gifs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gif gif = db.Gifs.Find(id);
            if (gif == null)
            {
                return HttpNotFound();
            }
            return View(gif);
        }

        // POST: Gifs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/Home");
            }
            Gif gif = db.Gifs.Find(id);
            db.Gifs.Remove(gif);
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
