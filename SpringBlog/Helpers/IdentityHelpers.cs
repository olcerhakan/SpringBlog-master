using Microsoft.AspNet.Identity;
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
    }
}