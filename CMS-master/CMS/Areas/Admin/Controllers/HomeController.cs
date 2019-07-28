using CMS.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMS.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            VMHome vm = new VMHome()
            {
                KategoriSayisi = db.Kategori.Count(),
                MakaleSayisi = db.Makale.Count(),
                KullaniciSayisi = db.Kullanici.Count(),
                KullaniciAdSoyad = db.Kullanici.Select(m => new {m.Kadi,m.AdSoyad }).FirstOrDefault(m => m.Kadi == User.Identity.Name).AdSoyad
            };
            return View(vm);
        }

        public ActionResult KullaniciAlani()
        {
            VMKullaniciAlani vm = new VMKullaniciAlani();
            var kul = db.Kullanici.Select(m => new { m.Kadi,m.AdSoyad,m.Foto }).FirstOrDefault(m => m.Kadi == User.Identity.Name);
            vm.AdSoyad = kul.AdSoyad;
            vm.Fotograf = kul.Foto;
            return PartialView(vm);
        }
    }
}