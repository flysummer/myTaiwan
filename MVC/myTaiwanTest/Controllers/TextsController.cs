using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using myTaiwanTest.Models;

namespace myTaiwanTest.Controllers
{
    public class TextsController : Controller
    {
        private myTaiwanEntities db = new myTaiwanEntities();

        // GET: Texts
        public ActionResult Index()
        {
            var texts = db.Texts.Include(t => t.Sign).Include(t => t.County);
            return View(texts.ToList());
        }

        // GET: Texts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Text text = db.Texts.Find(id);
            if (text == null)
            {
                return HttpNotFound();
            }
            return View(text);
        }

        // GET: Texts/Create
        public ActionResult Create()
        {
            ViewBag.userID = new SelectList(db.Signs, "ID", "name");
            ViewBag.location = new SelectList(db.Counties, "countryID", "countryName");
            return View();
        }

        // POST: Texts/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "txtID,txtTitle,txtCreateTime,txtUpdateTime,userID,location,locationDescription")] Text text)
        {
            if (ModelState.IsValid)
            {
                db.Texts.Add(text);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.userID = new SelectList(db.Signs, "ID", "name", text.userID);
            ViewBag.location = new SelectList(db.Counties, "countryID", "countryName", text.location);
            return View(text);
        }

        // GET: Texts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Text text = db.Texts.Find(id);
            if (text == null)
            {
                return HttpNotFound();
            }
            ViewBag.userID = new SelectList(db.Signs, "ID", "name", text.userID);
            ViewBag.location = new SelectList(db.Counties, "countryID", "countryName", text.location);
            return View(text);
        }

        // POST: Texts/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "txtID,txtTitle,txtCreateTime,txtUpdateTime,userID,location,locationDescription")] Text text)
        {
            if (ModelState.IsValid)
            {
                db.Entry(text).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.userID = new SelectList(db.Signs, "ID", "name", text.userID);
            ViewBag.location = new SelectList(db.Counties, "countryID", "countryName", text.location);
            return View(text);
        }

        // GET: Texts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Text text = db.Texts.Find(id);
            if (text == null)
            {
                return HttpNotFound();
            }
            return View(text);
        }

        // POST: Texts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Text text = db.Texts.Find(id);
            db.Texts.Remove(text);
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
