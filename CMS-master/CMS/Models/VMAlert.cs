using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS.Models
{
    public class VMAlert
    {
        public string Tip{ get; set; }
        public string Icerik { get; set; }

        public VMAlert(string tip, string ıcerik)
        {
            Tip = tip;
            Icerik = ıcerik;
        }
    }
}