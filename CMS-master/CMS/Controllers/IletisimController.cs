using CMS.Helpers;
using CMS.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Configuration;
using System.Web.Helpers;
using System.Web.Mvc;

namespace CMS.Controllers
{
    public class IletisimController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(VMIletisim iletisim)
        {
            string dosya = "";
            if (iletisim.dosya != null)
                dosya = UserHelpers.FotografYukle(iletisim.dosya);

            WebMail.SmtpServer = ConfigurationManager.AppSettings["mailSmtp"];
            WebMail.SmtpPort = Convert.ToInt32(ConfigurationManager.AppSettings["mailPort"]);
            WebMail.UserName = ConfigurationManager.AppSettings["sunucuMail"];
            WebMail.Password = ConfigurationManager.AppSettings["sunucuMailSifre"];

            WebMail.EnableSsl = true;

            try
            {
                if (!string.IsNullOrEmpty(dosya))
                {
                    string file = Server.MapPath("~" + dosya);
                    WebMail.Send(
                        to: ConfigurationManager.AppSettings["adminMail"],
                        subject: iletisim.baslik,
                        body: iletisim.mesaj + "<br><br> TC: " + iletisim.tc + " <br><br>Ad Soyad: " + iletisim.adsoyad + " <br><br>Telefon Numarası: " + iletisim.telefon,
                        replyTo: ConfigurationManager.AppSettings["sunucuMail"],
                        isBodyHtml: true,
                        filesToAttach: new[] { file });
                }
                else
                {

                    WebMail.Send(
                        to: ConfigurationManager.AppSettings["adminMail"],
                        subject: iletisim.baslik,
                        body: iletisim.mesaj + "<br><br> TC: " + iletisim.tc + " <br><br>Ad Soyad: " + iletisim.adsoyad + " <br><br>Telefon Numarası: " + iletisim.telefon,
                        replyTo: ConfigurationManager.AppSettings["sunucuMail"],
                        isBodyHtml: true);
                }
                VMAlert alert = new VMAlert("success", Resources.Dil.basariliiletisim);

                VMIletisim vm = new VMIletisim()
                {
                    alert = alert
                };
                return View(vm);

            }
            catch (Exception ex)
            {
                VMAlert alert = new VMAlert("danger", ex.Message);

                VMIletisim vm = new VMIletisim()
                {
                    alert = alert
                };
                return View(vm);
            }

        }
    }
}