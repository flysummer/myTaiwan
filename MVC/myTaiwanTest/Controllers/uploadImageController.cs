using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Drawing;
using myTaiwanTest.Models;

namespace myTaiwanTest.Controllers {

    public class uploadImageController : Controller {
        private myTaiwanEntities db = new myTaiwanEntities();

        // GET: uploadImage
        public ActionResult ArctileIndex() {
            var userName = Session["userName"].ToString();
            Sign updateFace = db.Signs.FirstOrDefault(o => o.name == userName);
            return View("ArctileIndex", updateFace);
        }
        [HttpPost]
        public ActionResult uploadCover(HttpPostedFileBase imgOne) {
            var userName = Session["userName"].ToString();
            if (imgOne != null && imgOne.ContentLength > 0) {
                
                var fileName = userName+".png";
                var path = Path.Combine(Server.MapPath("~/coverImage"), fileName);
                imgOne.SaveAs(path);
                db.sp_setCoverUrl("/coverImage/"+fileName,Convert.ToInt32(Session["userID"]));
            }
            Sign updateCover = db.Signs.FirstOrDefault(o => o.name == userName);
            return View("ArctileIndex",updateCover);
        }
        [HttpPost]
        public ActionResult uploadfaceImage(HttpPostedFileBase imgTwo) {
            var userName = Session["userName"].ToString();
            if (imgTwo != null && imgTwo.ContentLength > 0) {
                var fileName = userName+ ".png";
                var path = Path.Combine(Server.MapPath("~/faceImage"), fileName);
                imgTwo.SaveAs(path);
                db.sp_setFaceUrl("/faceImage/" + fileName, Convert.ToInt32(Session["userID"]));
            }
            Sign updateFace = db.Signs.FirstOrDefault(o => o.name == userName);
            return View("ArctileIndex", updateFace);
        }



    }
}