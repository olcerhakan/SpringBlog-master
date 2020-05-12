using SpringBlog.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SpringBlog.Areas.Admin.Controllers
{
    public class CommentsController : AdminBaseController
    {
        // GET: Admin/Comments
        public ActionResult Index()
        {
            return View(db.Comments.ToList());
        }

        [HttpPost]
        public ActionResult ChangeState(int id, bool isPublished)
        {
            var comment = db.Comments.Find(id);
            comment.State = isPublished ? CommentState.Approved : CommentState.Rejected;
            db.SaveChanges();
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var comment = db.Comments.Find(id);  //yorumu bul
            db.Comments.RemoveRange(comment.Children);      //çocuklarını kaldır
            db.Comments.Remove(comment);        //kendini kaldır
            db.SaveChanges();                   //kaydet

            return RedirectToAction("Index");
        }
    }
}