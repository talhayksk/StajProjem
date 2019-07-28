using CMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMS.Controllers
{
    public class BaseController : Controller
    {
        protected CMSEntities db = new CMSEntities();
    }
}