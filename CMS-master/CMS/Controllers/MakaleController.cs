using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using CMS.Models;

namespace CMS.Controllers
{
    public class MakaleController : BaseController
    {
        // GET: Makale
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Oku(int id)
        {
            var makale = db.Makale.Include(m => m.Kullanici).Include(m => m.Kategori).FirstOrDefault(m => m.Mid == id);
            if (makale == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            return View(makale);
        }
        public ActionResult Kategori(int id)
        {
            var kat = db.Kategori.Find(id);
            if (kat == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            return View(db.Makale.Where(m => m.KatId == kat.KatId).ToList());
        }
        public ActionResult Kullanici(int id)
        {
            var kul = db.Kullanici.Find(id);
            if (kul == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            return View(db.Makale.Where(m => m.Kid == kul.Kid).ToList());
        }

        [HttpPost]
        public ActionResult Ara(string aranacak)
        {
            return View(new VMArama(aranacak, db.Makale.Where(m => m.Baslik.Contains(aranacak)).ToList()));
        }


        public ActionResult AnaSayfaMakale()
        {
            return PartialView(db.Makale.OrderByDescending(m => m.Mid).ToList());
        }
    }
}