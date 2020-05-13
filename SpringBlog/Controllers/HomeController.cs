using SpringBlog.Models;
using SpringBlog.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using X.PagedList;

namespace SpringBlog.Controllers
{
    public class HomeController : BaseController
    {

        [Route("", Order =2, Name = "HomeDefault")]  //name ı  alttakini kullanma demek için yaptık. Layoutta . cid ve slug yokmuş gibi düşünüyor
        [Route("c/{cid}/{slug}", Order =1)]
        public ActionResult Index(string q,int? cid,string slug,int page=1)
        {
            var pageSize = 10;
            IQueryable<Post> posts = db.Posts;
            Category category = null;  //olurda cid e girmezse bunu atamış olucaz

            //search için
            if (q!=null)
            {
                posts = posts.Where(x => x.Category.CategoryName.Contains(q)
                || x.Title.Contains(q)
                || x.Content.Contains(q)
                );
            }

            if (cid != null && q==null)  //cid varsa bunu koy
            {
                category = db.Categories.Find(cid);
                if (category ==null)
                {
                    return HttpNotFound();
                }

                posts = posts.Where(x => x.CategoryId == cid);
            }
            var vm = new HomeIndexViewModel
            {

                //Posts = db.Posts.OrderByDescending(x => x.CreationTime).ToList()
                Posts = posts.OrderByDescending(x => x.CreationTime).ToPagedList(page, pageSize),
                Category=category,
                SearchTerm=q,
                CategoryId=cid
            };

            return View(vm);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            //ViewBag.Message = ConfigurationManager.AppSettings["ad"];

            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult CategoriesPartial()
        {
            var cats = db.Categories.OrderBy(x => x.CategoryName).ToList();
            return PartialView("_CategoriesPartial", cats);
        }
    }
}