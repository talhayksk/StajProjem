using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS.Areas.Admin.Models
{
    public class VMHome
    {
        public int KullaniciSayisi{ get; set; }
        public int MakaleSayisi{ get; set; }
        public int KategoriSayisi{ get; set; }
        public string KullaniciAdSoyad { get; set; }
    }
}