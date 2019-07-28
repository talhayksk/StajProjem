using CMS.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMS.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Slider()
        {
            return PartialView(db.Makale.Where(m => m.Foto != null).Take(3).OrderByDescending(m => m.Mid).ToList());
        }
        public ActionResult SonPaylasilan()
        {
            return PartialView(db.Makale.Take(3).OrderByDescending(m => m.Mid).ToList());
        }
        public ActionResult Kategoriler()
        {
            return PartialView(db.Kategori.Include(m => m.Makale).Take(3).OrderByDescending(m => m.KatId).ToList());
        }
    }
}