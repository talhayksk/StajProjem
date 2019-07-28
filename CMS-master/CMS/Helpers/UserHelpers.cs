using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace CMS.Helpers
{
    public static class UserHelpers
    {
        public static string FotografYukle(HttpPostedFileBase file)
        {
            var resimAdi = "";
            if (file != null)
            {
                //Dosyaya uniq bir isim vermek için
                Guid guid = Guid.NewGuid();
                string str = guid.ToString();

                //Eğerki dosya dolu olarak gelmişse
                if (file.ContentLength > 0)
                {
                    //Dosya uzantısını aldık
                    var uzanti = Path.GetExtension(file.FileName).ToLower();
                    //Uzantı resim uzantıları ile uyuşuyorsa
                    if (uzanti == ".jpg" || uzanti == ".png" || uzanti == ".jpeg")
                    {
                        resimAdi = str + uzanti;
                        var path = Path.Combine(HttpContext.Current.Server.MapPath("~/Content/Uploads"), resimAdi);
                        file.SaveAs(path);
                    }
                    else
                    {
                        //Diğer Koşullarda ön tanımlı icon
                        resimAdi = "noimg.png";
                    }
                }
            }
            else
            {
                resimAdi = "noimg.png";
            }

            return "/Content/Uploads/" + resimAdi;
        }

        public static void FotografSil(string resimyol)
        {
            string resyol = HttpContext.Current.Request.MapPath("~" + resimyol);
            //Resim varsa ve usericon değilse sil
            if (System.IO.File.Exists(resyol) && !resyol.Equals("/Content/Uploads/noimg.png"))
            {
                System.IO.File.Delete(resyol);
            }
        }

        public static bool KullaniciAdiKontrolu(string username)
        {
            //A dan Z ye kadar ve 0 ila 9 rakamları dışında karakter icermeyen kullanıcı adı kuralı
            string kural = "^[a-zA-Z0-9]+$";
            Regex rgx = new Regex(kural);
            return rgx.IsMatch(username); //Kurala uyuyorsa true uymuyorsa false dönderdik
        }
    }
}