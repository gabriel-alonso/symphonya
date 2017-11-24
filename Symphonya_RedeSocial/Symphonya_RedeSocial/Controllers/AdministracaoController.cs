using Symphonya_RedeSocial.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Symphonya_RedeSocial.Controllers
{
    public class AdministracaoController : Controller
    {
        // GET: Administracao
        public ActionResult Usuarios()
        {
            if (Session["Administrador"] != null)
            {
                    //CRIA SESSÃO DO Administrador
                    ViewBag.Logado = Session["Administrador"];
                    Usuario User = (Usuario)Session["Administrador"];
                    ViewBag.User = User;

                ViewBag.ContagemUsuarios = Usuario.Contar();
                ViewBag.Usuarios = Usuario.Listar();
            }
            else {
                Response.Redirect("/Menu/Feed/");
            }

            return View();
        }

        public ActionResult VerUsuarioADM(Int32 ID)
        {
            Usuario user = new Usuario(ID);
            TempData["Usuario"] = user;

            Response.Redirect("~/Menu/VerUsuario");
            return View();
        }

        public void ExcluirUsuarioADM(Int32 ID)
        {
            if(Usuario.Apagar(ID))
                Response.Redirect("~/Administracao/Usuarios");
            else
            {
                Response.Redirect("~/Administracao/Usuarios");
                ViewBag.Erro = "Erro ao excluir usuário!";

            }
                
        }
    }
}