using CMS.Areas.Admin.Models;
using CMS.Helpers;
using CMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CMS.Areas.Admin.Controllers
{
    public class ProfilController : BaseController
    {
        // GET: Admin/Profil
        public ActionResult Index()
        {
            return View(db.Kullanici.FirstOrDefault(m => m.Kadi == User.Identity.Name));
        }

        public ActionResult Ayarlar()
        {
            VMKullaniciVeAlert vm = new VMKullaniciVeAlert();
            vm.Kullanici = db.Kullanici.FirstOrDefault(m => m.Kadi == User.Identity.Name);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Ayarlar(Kullanici kullanici, HttpPostedFileBase dosya)
        {
            VMKullaniciVeAlert vm = new VMKullaniciVeAlert();
            var kul = db.Kullanici.FirstOrDefault(m => m.Kadi == User.Identity.Name);
            if (ModelState.IsValid)
            {

                if (dosya != null)
                {
                    UserHelpers.FotografSil(kul.Foto);
                    kul.Foto = UserHelpers.FotografYukle(dosya);
                }
                kul.AdSoyad = kullanici.AdSoyad;
                kul.Email = kullanici.Email;

                if (!string.IsNullOrEmpty(kullanici.Sifre))
                    kul.Sifre = kullanici.Sifre;

                vm.Alert = new VMAlert("success", Resources.Dil.basariliguncelleme);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                vm.Alert = new VMAlert("danger", Resources.Dil.formukontrolet);
            }
            return View(vm);
        }
    }
}