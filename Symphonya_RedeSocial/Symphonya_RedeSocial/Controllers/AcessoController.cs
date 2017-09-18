using Symphonya_RedeSocial.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Symphonya_RedeSocial.Controllers
{
    public class AcessoController : Controller
    {
        // GET: Acesso
        public ActionResult Login()
        {

                if (Request.HttpMethod == "POST")
                {
                    String Email = Request.Form["email"].ToString();
                    String Senha = Request.Form["senha"].ToString();

                    //CRIA CRIPTOGRAFIA DA SENHA DO USUARIO
                    String SenhaEncriptada = FormsAuthentication.HashPasswordForStoringInConfigFile(Senha, "SHA1");

                //AUTENTICA USUARIO
                if (Usuario.Autenticar(Email, SenhaEncriptada))
                {
                    Usuario Usuario = new Usuario(Email, SenhaEncriptada);
                    Session["Usuario"] = Usuario;
                    Response.Redirect("/Menu/Feed");
                }

                    //CASO INFORMACOES DIGITADAS SEJAM INVALIDAS
                    ViewBag.MsgErro = "Usuário e/ou Senha incorretos!";

                }
                
                return View();
            }

        public ActionResult Cadastro()
        {
            return View();
        }

        public void Sair()
        {
            Session.Abandon();
            Session.Clear();

            Response.Redirect("/Acesso/Login", false);
        }

    }
}