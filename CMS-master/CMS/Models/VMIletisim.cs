using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS.Models
{
    public class VMIletisim
    {
        public string adsoyad { get; set; }
        public string tc { get; set; }
        public string baslik { get; set; }
        public string telefon { get; set; }
        public HttpPostedFileBase dosya { get; set; }
        public string mesaj { get; set; }

        public VMAlert alert { get; set; }
    }
}