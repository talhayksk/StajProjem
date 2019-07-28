using CMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMS.Areas.Admin.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        protected CMSEntities db = new CMSEntities();
    }
}