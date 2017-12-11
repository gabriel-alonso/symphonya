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
        
        //METODO DE LOGIN
        public ActionResult Login()
        {

            if(TempData["Sucesso"] != null)
            {
                ViewBag.MsgSucCadastro = TempData["Sucesso"];
            }

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

                    if (Usuario.Modo == true)
                    {
                        ViewBag.MsgErro = "Sua conta foi desativada!";
                        return View();
                    }
                    else
                    {
                        //VERIFICA SE O USUARIO É UM ADM
                        if (Usuario.Nivel != 0)
                        {
                            Session["Administrador"] = Usuario;
                        }
                        else
                        {
                            Session["Usuario"] = Usuario;
                        }

                        Response.Redirect("~/Menu/Feed");
                    }
                }
                else
                {
                    //CASO INFORMACOES DIGITADAS SEJAM INVALIDAS
                    ViewBag.MsgErro = "Usuário e/ou Senha incorretos!";
                }
                }
                
                return View();
            }

        //METODO DE CADASTRO
        public ActionResult Cadastro()
        {
            if (Request.HttpMethod == "POST")
            {

                //VERIFICA SE JA EXISTE UM EMAIL CADASTRADO
                if (Usuario.VerificarEmail(Request.Form["Email"].ToString()))
                {
                    ViewBag.MsgErro = "E-Mail já cadastrado!";
                }

                else
                {
                    Usuario Us = new Usuario();

                    Us.Nome = Request.Form["Nome"].ToString();
                    Us.Sobrenome = Request.Form["Sobrenome"].ToString();
                    Us.MesNascimento = Convert.ToInt32(Request.Form["MesNascimento"]);
                    Us.DiaNascimento = Convert.ToInt32(Request.Form["DiaNascimento"]);
                    Us.AnoNascimento = Convert.ToInt32(Request.Form["AnoNascimento"]);
                    Us.Email = Request.Form["Email"].ToString();
                    Us.Senha = FormsAuthentication.HashPasswordForStoringInConfigFile(Request.Form["Senha"], "SHA1");
                    Us.Cidade = Request.Form["Cidade"].ToString();
                    Us.Estado = Request.Form["Estado"].ToString();
                    //Us.Estado = Convert.ToInt32(Request.Form["Estado"]);
                    Us.Sexo = Convert.ToBoolean(Request.Form["Sexo"]);
                    //Us.Imagem_Perfil = Convert.ToString(Request.Form["Imagem_Perfil"]);
                    //Us.Imagem_Capa = Convert.ToString(Request.Form["Imagem_Capa"]);
                    Us.Telefone = Request.Form["Telefone"].ToString();
                    Us.NovoUser();

                    Response.Redirect("/Acesso/Login");
                    TempData["Sucesso"] = "Conta criada com sucesso!";
                }

            }

            return View();
        }

        //RECUPERACAO DE SENHA
        public ActionResult RecuperarSenha()
        {
            ViewBag.Recuperar = Session["Email"];
            return View();
        }

        public void Recuperar()
        {
            if (Request.HttpMethod == "POST")
            {
                Email E = new Email();
                Usuario U = new Usuario();

                String Email = Request.Form["Email"];

                if (U.Verificar(Email))
                {
                    E.EmailRecuperaracao(Email, U.Senha);
                    Session["Email"] = "E-mail enviado com sucesso!";
                    Response.Redirect("~/Acesso/Login");
                }
                else
                {
                    Session["Email"] = "E-mail não cadastrado!";
                    Response.Redirect("~/Acesso/RecuperarSenha");
                }

            }
        }

        //METODO PARA FAZER LOGOFF
        public void Sair()
        {
            Session.Abandon();
            Session.Clear();

            Response.Redirect("/Acesso/Login");
        }

    }
}