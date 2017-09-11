using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Symphonya_RedeSocial.Controllers
{
    public class AcessoController : Controller
    {
        // GET: Acesso
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Cadastro()
        {
            return View();
        }
    }
}