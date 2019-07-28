using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CMS.Helpers;
using CMS.Models;

namespace CMS.Areas.Admin.Controllers
{
    public class MakaleController : BaseController
    {
        // GET: Admin/Makale
        public ActionResult Index()
        {
            var makale = db.Makale.Include(m => m.Kategori).Include(m => m.Kullanici);
            return View(makale.ToList());
        }

        // GET: Admin/Makale/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Makale makale = db.Makale.Include(m => m.Kullanici).Include(m => m.Kategori).FirstOrDefault(m => m.Mid == id);
            if (makale == null)
            {
                return HttpNotFound();
            }
            return View(makale);
        }

        // GET: Admin/Makale/Create
        public ActionResult Create()
        {
            //Select List Oluşturup Kategorileri Listelemek için
            ViewBag.KatId = new SelectList(db.Kategori, "KatId", "KategoriAdi");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Mid,Baslik,KisaIcerik,Icerik,Foto,EklenmeTarihi,GuncellemeTarihi,KatId,Kid")] Makale makale,HttpPostedFileBase dosya)
        {
            if (ModelState.IsValid)
            {
                if (dosya != null)
                    makale.Foto = UserHelpers.FotografYukle(dosya);

                var kul = db.Kullanici.FirstOrDefault(m => m.Kadi == User.Identity.Name);
                makale.Kid = kul.Kid;
                makale.EklenmeTarihi = DateTime.Now;
                db.Makale.Add(makale);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.KatId = new SelectList(db.Kategori, "KatId", "KategoriAdi", makale.KatId);
            return View(makale);
        }

        // GET: Admin/Makale/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Makale makale = db.Makale.Include(m => m.Kullanici).FirstOrDefault(m => m.Mid == id);
            if (makale == null)
            {
                return HttpNotFound();
            }
            if(makale.Kullanici.Kadi != User.Identity.Name)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.KatId = new SelectList(db.Kategori, "KatId", "KategoriAdi", makale.KatId);
            return View(makale);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Mid,Baslik,KisaIcerik,Icerik,Foto,EklenmeTarihi,GuncellemeTarihi,KatId,Kid")] Makale makale,HttpPostedFileBase dosya)
        {
            if (ModelState.IsValid)
            {
                var mak = db.Makale.Find(makale.Mid);
                if (dosya != null)
                {
                    UserHelpers.FotografSil(mak.Foto);
                    mak.Foto = UserHelpers.FotografYukle(dosya);
                }
                mak.GuncellemeTarihi = DateTime.Now;
                mak.Baslik = makale.Baslik;
                mak.KisaIcerik = makale.KisaIcerik;
                mak.Icerik = makale.Icerik;
                mak.KatId = makale.KatId;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KatId = new SelectList(db.Kategori, "KatId", "KategoriAdi", makale.KatId);
            return View(makale);
        }

        // POST: Admin/Makale/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Makale makale = db.Makale.Include(m => m.Kullanici).FirstOrDefault(m => m.Mid == id);
            if (makale.Kullanici.Kadi != User.Identity.Name)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserHelpers.FotografSil(makale.Foto);
            db.Makale.Remove(makale);
            db.SaveChanges();
            return Json(new { baslik = Resources.Dil.silindi, icerik = Resources.Dil.maksilindi });
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
