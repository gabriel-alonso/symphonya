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
            if (Request.HttpMethod == "POST")
            {

                Usuario Us = new Usuario();

               
                Us.Nome = Request.Form["Nome"].ToString();
                Us.Sobrenome = Request.Form["Sobrenome"].ToString();
                Us.MesNascimento = Convert.ToInt32(Request.Form["MesNascimento"]);
                Us.DiaNascimento = Convert.ToInt32(Request.Form["DiaNascimento"]);
                Us.AnoNascimento = Convert.ToInt32(Request.Form["AnoNascimento"]);
                Us.Email = Request.Form["Email"].ToString();
                Us.Senha = Request.Form["Senha"].ToString();
                Us.Cidade = Request.Form["Cidade"].ToString();
                Us.Estado = Convert.ToInt32(Request.Form["Estado"]);
                //Us.Sexo = Convert.ToBoolean(Request.Form["Sexo"]);
                Us.NovoUser();

            }

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