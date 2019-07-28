using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS.Models
{
    public class VMArama
    {
        public string Aranacak { get; set; }
        public IEnumerable<Makale> Makaleler{ get; set; }

        public VMArama(string aranacak, List<Makale> makaleler)
        {
            Aranacak = aranacak;
            Makaleler = makaleler;
        }
    }
}