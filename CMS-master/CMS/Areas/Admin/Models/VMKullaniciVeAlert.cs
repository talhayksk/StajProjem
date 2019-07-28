using CMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS.Areas.Admin.Models
{
    public class VMKullaniciVeAlert
    {
        public Kullanici Kullanici { get; set; }
        public VMAlert Alert { get; set; }

    }
}