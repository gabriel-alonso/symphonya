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

                //VIEWBAG RELACIONADAS AO RANKING DE USUARIOS
                ViewBag.Ranking = Usuario.Ranquear(true);

                //VIEWBAGS RELACIONADAS AOS ADMINISTRADORES DO SITE
                ViewBag.QntADM = Usuario.ContarADM();
                ViewBag.Administradores = Usuario.ListarADM();
            }
            else {
                Response.Redirect("/Menu/Feed/");
            }

            return View();
        }

        public ActionResult Bandas()
        {
            if (Session["Administrador"] != null)
            {
                //CRIA SESSÃO DO Administrador
                ViewBag.Logado = Session["Administrador"];
                Usuario User = (Usuario)Session["Administrador"];
                ViewBag.User = User;

                ViewBag.ContagemBandas = Models.Bandas.Contar();
                ViewBag.Bandas = Models.Bandas.Listar();

                //VIEWBAG RELACIONADAS AO RANKING DE USUARIOS
                ViewBag.Ranking = Usuario.Ranquear(true);

                //VIEWBAGS RELACIONADAS AOS ADMINISTRADORES DO SITE
                ViewBag.QntADM = Usuario.ContarADM();
                ViewBag.Administradores = Usuario.ListarADM();
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

        public void DesativarUsuarioADM(Int32 ID)
        {
            if(Usuario.Desativar(ID))
                Response.Redirect("~/Administracao/Usuarios");
            else
            {
                Response.Redirect("~/Administracao/Usuarios");
                ViewBag.Erro = "Erro ao desativar usuário!";

            }
                
        }

        public void AtivarUsuarioADM(Int32 ID)
        {
            if (Usuario.Ativar(ID))
                Response.Redirect("~/Administracao/Usuarios");
            else
            {
                Response.Redirect("~/Administracao/Usuarios");
                ViewBag.Erro = "Erro ao ativar usuário!";

            }

        }

        public ActionResult VerBandaADM(Int32 ID)
        {
                if (Session["Administrador"] != null)
                {
                    //CRIA SESSÃO DO Administrador
                    ViewBag.Logado = Session["Administrador"];
                    Usuario User = (Usuario)Session["Administrador"];
                    ViewBag.User = User;
            }
            else
            {
                Response.Redirect("~/Acesso/Login");
            }

                Bandas banda = new Bandas(ID);
                List<Integrantes> integrantes = Integrantes.ListarIntegrantes(banda.ID);
                ViewBag.Integrantes = integrantes;
                ViewBag.Banda = banda;

                return View();
        }
    }
}