using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace CMS.Controllers
{
    public class DilController : Controller
    {
        public ActionResult Degistir(string dil)
        {
            if (dil != null)
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(dil);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(dil);
            }
            HttpCookie cookie = new HttpCookie("dil");
            cookie.Value = dil;
            Response.Cookies.Add(cookie);
            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}