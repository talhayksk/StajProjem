using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CMS.Areas.Admin.Models;
using CMS.Helpers;
using CMS.Models;

namespace CMS.Areas.Admin.Controllers
{
    [Authorize(Roles = "a")]
    public class KullaniciController : BaseController
    {

        // GET: Admin/Kullanici
        public ActionResult Index()
        {
            return View(db.Kullanici.ToList());
        }


        // GET: Admin/Kullanici/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Kid,Kadi,Sifre,AdSoyad,Email,UyelikTarihi,Foto,Yetki")] Kullanici kullanici,  HttpPostedFileBase dosya)
        {
            if (ModelState.IsValid)
            {
                var bak = db.Kullanici.FirstOrDefault(m => m.Kadi == kullanici.Kadi);
                if (!UserHelpers.KullaniciAdiKontrolu(kullanici.Kadi) || bak != null)
                {
                    VMKullaniciVeAlert kullaniciVeAlert = new VMKullaniciVeAlert()
                    {
                        Alert = new VMAlert("danger", Resources.Dil.gecersizkul)
                    };
                    return View(kullaniciVeAlert);
                }
                else if (string.IsNullOrEmpty(kullanici.Sifre))
                {
                    VMKullaniciVeAlert kullaniciVeAlert = new VMKullaniciVeAlert()
                    {
                        Alert = new VMAlert("danger", Resources.Dil.sifregir)
                    };
                    return View(kullaniciVeAlert);
                }
                string foto = UserHelpers.FotografYukle(dosya);
                kullanici.Foto = foto;
                kullanici.UyelikTarihi = DateTime.Now;
                db.Kullanici.Add(kullanici);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                VMKullaniciVeAlert kullaniciVeAlert2 = new VMKullaniciVeAlert()
                {
                    Alert = new VMAlert("danger", Resources.Dil.formukontrolet)
                };
                return View(kullaniciVeAlert2);
            }

        }

        // GET: Admin/Kullanici/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VMKullaniciVeAlert vm = new VMKullaniciVeAlert()
            {
                Kullanici = db.Kullanici.Find(id)
            };
            
            if (vm.Kullanici == null)
            {
                return HttpNotFound();
            }
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Kid,Kadi,Sifre,AdSoyad,Email,UyelikTarihi,Foto,Yetki")] Kullanici kullanici, HttpPostedFileBase dosya)
        {
            VMKullaniciVeAlert vm = new VMKullaniciVeAlert();
            if (ModelState.IsValid)
            {
                var kul = db.Kullanici.Find(kullanici.Kid);
                if (kul == null)
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                if (dosya != null)
                {
                    UserHelpers.FotografSil(kul.Foto);
                    kul.Foto = UserHelpers.FotografYukle(dosya);
                }
                kul.AdSoyad = kullanici.AdSoyad;
                kul.Email = kullanici.Email;
                kul.Yetki = kullanici.Yetki;

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

        // POST: Admin/Kullanici/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Kullanici kullanici = db.Kullanici.Find(id);
            var foto = kullanici.Foto;
            db.Kullanici.Remove(kullanici);
            db.SaveChanges();
            UserHelpers.FotografSil(foto);
            return Json(new {baslik=Resources.Dil.silindi,icerik=Resources.Dil.kulsilindi});
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
