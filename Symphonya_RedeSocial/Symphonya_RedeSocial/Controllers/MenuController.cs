using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Symphonya_RedeSocial.Controllers
{
    public class MenuController : Controller
    {
        // GET: Menu
        public ActionResult Feed()
        {
            return View();
        }

        public ActionResult Perfil()
        {
            return View();
        }
    }
}