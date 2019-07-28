using CMS.Areas.Admin.Models;
using CMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CMS.Areas.Admin.Controllers
{
    public class GuvenlikController : Controller
    {
        private CMSEntities db = new CMSEntities();
        public ActionResult Giris()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Giris(Kullanici kullanici)
        {
            var bak = db.Kullanici.FirstOrDefault(m => m.Kadi == kullanici.Kadi && m.Sifre == kullanici.Sifre);
            if (bak != null)
            {
                FormsAuthentication.SetAuthCookie(bak.Kadi, false);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                VMKullaniciVeAlert vm = new VMKullaniciVeAlert();
                vm.Alert = new VMAlert("danger", Resources.Dil.kuladisifrehatali);
                return View(vm);
            }
        }
        public ActionResult Kayit()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Kayit(Kullanici kullanici)
        {
            var bak = db.Kullanici.FirstOrDefault(m => m.Kadi == kullanici.Kadi);
            if (bak != null)
            {
                VMKullaniciVeAlert vm = new VMKullaniciVeAlert()
                {
                    Alert = new VMAlert("danger", Resources.Dil.gecersizkul)
                };
                return View(vm);
            }
            else
            {
                kullanici.Foto = "/Content/Uploads/noimg.png";
                kullanici.Yetki = "u";
                kullanici.UyelikTarihi = DateTime.Now;
                db.Kullanici.Add(kullanici);
                db.SaveChanges();

                FormsAuthentication.SetAuthCookie(kullanici.Kadi, false);
                return RedirectToAction("Index", "Home");

            }
        }

        public ActionResult Cikis()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Giris", "Guvenlik");
        }
    }
}