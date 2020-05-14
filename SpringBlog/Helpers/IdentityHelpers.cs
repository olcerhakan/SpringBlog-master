using Microsoft.AspNet.Identity;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity.EntityFramework;
using SpringBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

namespace SpringBlog.Helpers
{
    public static class IdentityHelpers
    {
        //burda claims den cekiyoruz db ye gerek kalmıyor.
        public static string DisplayName(this IIdentity identity)
        {
            var claims = ((ClaimsIdentity)identity).Claims;

            string displayName = claims.FirstOrDefault(x => x.Type == "DisplayName").Value;

            return displayName;
        }

        //burda veri tabanından cekiyoruz. claims den cekince login logout gerekicek
        //şunu sağlayacak. Index.cshtml de @User.Identity.ProfilePhoto deyince fotosu gelecek.
        public static string ProfilePhoto(this IIdentity identity)
        {
            //using blogunun dısına cıkarken dispose metotu otomatik cagırılıyor. db den.
            using (var db = new ApplicationDbContext())
            {
                //GetUserId Claims den cekiyor. cookie nin icinden alıyor direk.
                string userId = identity.GetUserId();
                var user = db.Users.Find(userId);
                return user.ProfilePhoto;
            }

        }

        public static void SeedRolesAndUsers()
        {
            var context = new ApplicationDbContext();
            // https://stackoverflow.com/questions/19280527/mvc-5-seed-users-and-roles
            if (!context.Roles.Any(r => r.Name == "admin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "admin" };

                manager.Create(role);
            }

            if (!context.Users.Any(u => u.UserName == "hakanolcer7@gmail.com"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser
                {
                    UserName = "hakanolcer7@gmail.com",
                    Email = "hakanolcer7@gmail.com",
                    DisplayName = "Hakan Ö.",
                    EmailConfirmed = true
                };

                manager.Create(user, "Password1.");
                manager.AddToRole(user.Id, "admin");

                #region Seed Categories and Posts
                if (!context.Categories.Any())
                {
                    context.Categories.Add(new Category
                    {
                        CategoryName = "Sample Category 1",
                        Slug = "sample-category-1",
                        Posts = new List<Post>
                        {
                            new Post
                            {
                                Title = "Sample Post 1",
                                AuthorId = user.Id,
                                Content = "<p>Tincidunt integer eu augue augue nunc elit dolor, luctus placerat scelerisque euismod, iaculis eu lacus nunc mi elit, vehicula ut laoreet ac, aliquam sit amet justo nunc tempor, metus vel.</p>",
                                Slug = "sample-post-1",
                                CreationTime = DateTime.Now,
                                ModificationTime = DateTime.Now
                            },
                            new Post
                            {
                                Title = "Sample Post 2",
                                AuthorId = user.Id,
                                Content = "<p>Tincidunt integer eu augue augue nunc elit dolor, luctus placerat scelerisque euismod, iaculis eu lacus nunc mi elit, vehicula ut laoreet ac, aliquam sit amet justo nunc tempor, metus vel.</p>",
                                Slug = "sample-post-2",
                                CreationTime = DateTime.Now,
                                ModificationTime = DateTime.Now
                            }
                        }
                    });
                }
                #endregion

            }

            context.SaveChanges();
            context.Dispose();
        }
    }
}