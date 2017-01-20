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
    public class SignsController : Controller
    {
        private myTaiwanEntities db = new myTaiwanEntities();

        // GET: Signs
        public ActionResult Index()
        {
            return View(db.Signs.ToList());
        }

        // GET: Signs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sign sign = db.Signs.Find(id);
            if (sign == null)
            {
                return HttpNotFound();
            }
            return View(sign);
        }

        // GET: Signs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Signs/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,name,passwords,email,faceUrl,coverUrl")] Sign sign)
        {
            if (ModelState.IsValid)
            {
                db.Signs.Add(sign);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sign);
        }
        //註冊
        public ActionResult SignIn() {
            return View("Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignIn([Bind(Include = "name,passwords,email")] Sign sign) {
            if (ModelState.IsValid) {
                sign.coverUrl = "";
                sign.faceUrl = "";
                db.Signs.Add(sign);
                try
                {
                    db.SaveChanges();
                    var query = db.Signs.FirstOrDefault(o => o.name == sign.name &&
                                        o.passwords == sign.passwords);
                    if (query != null) {
                        Session["userName"] = query.name;
                        Session["userID"] = query.ID;
                        return View("DynamicIndex");
                    }
                    
                }
                catch(Exception ex)
                {
                    throw;
                }
            }
            return View("Login", sign);
        }


        public ActionResult LogIn()
        {
            return View("DynamicIndex");
        }
        //登入
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogIn([Bind(Include = "name,passwords")] Sign sign)
        {
            if (ModelState.IsValid)
            {
                //var query = from o in db.Signs
                //            where o.name == sign.name &&
                //                  o.passwords == sign.passwords
                //            select o;
                var query = db.Signs.FirstOrDefault(o => o.name == sign.name && 
                                        o.passwords == sign.passwords);
                try
                {
                    if (query != null)
                    {
                        Session["userName"] = query.name;
                        Session["userID"] = query.ID;
                        return View("DynamicIndex");
                    }
                        
                }
                catch(Exception ex)
                {
                    throw;
                }
                
            }
            return View("Login");
        }
        //登出
        public ActionResult LogOut()
        {
            //Session.Clear();
            return View("Login");
        }
        //好友列表
        public ActionResult myFriend()
        {
            return View();
        }
        //尋找好友(路人) //由searchtext取得資料
        [HttpPost]
        public ActionResult searchUser([Bind(Include = "name")] Sign sign)
        {
            //var query = from o in db.Signs
            //            where o.name == sign.name
            //            select new{
            //                name = o.name
            //            };
            
            var search = db.Signs.FirstOrDefault(o => o.name == sign.name);//好友(路人)資訊
            if(db.Friends.Count(o => o.friendID == search.ID) == 1)//有一筆資料(是好友)
                ViewData["isFriend"] = "true";
            else
                ViewData["isFriend"] = "false";//其他情況(非好友)

            return View("FriendPage", search);
        }
        //文章列表
        public ActionResult ArctileIndex()
        {
            return View();
        }
        //新增好友
        public ActionResult addFriend(int id)
        {
            Friend list = new Friend()
            {
                userID = Convert.ToInt32(Session["userID"]),
                friendID = id
            };
            db.Friends.Add(list);
            db.SaveChanges();
            ViewData["isFriend"] = "true";
            var search = db.Signs.FirstOrDefault(o => o.ID == id);
            return View("FriendPage", search);
        }
        //刪除好友
        public ActionResult delFriend(int id)
        {
            Friend list = db.Friends.FirstOrDefault(o => o.friendID == id);
            db.Friends.Remove(list);
            db.SaveChanges();
            ViewData["isFriend"] = "false";
            var search = db.Signs.FirstOrDefault(o => o.ID == id);
            return View("FriendPage", search);
        }

        public ActionResult addText()
        {
            return View();
        }




        //以下無用
        // GET: Signs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sign sign = db.Signs.Find(id);
            if (sign == null)
            {
                return HttpNotFound();
            }
            return View(sign);
        }

        // POST: Signs/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,name,passwords,email,faceUrl,coverUrl")] Sign sign)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sign).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sign);
        }

        // GET: Signs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sign sign = db.Signs.Find(id);
            if (sign == null)
            {
                return HttpNotFound();
            }
            return View(sign);
        }

        // POST: Signs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sign sign = db.Signs.Find(id);
            db.Signs.Remove(sign);
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
