using Symphonya_RedeSocial.Models;
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
            //VERIFICA SE EXISTE ALGUM DADO NA SESSÃO USUARIO
            if (Session["Usuario"] != null)
            {
                //CRIA SESSÃO DO USUARIO
                ViewBag.Logado = Session["Usuario"];
                Usuario User = (Usuario)Session["Usuario"];
                ViewBag.User = User;

                //METODO PARA BUSCA DE USUARIOS, MUSICAS, BANDAS
                    if (Request.HttpMethod == "POST")
                    {
                    String busca = Request.Form["busca"].ToString();
                    Response.Redirect("/Menu/Pesquisar/" + busca);
                    }
                }
            //CASO SESSAO SEJA NULA -> REDIRECIONAMENTO PARA PAGINA LOGIN
            else
            {
                Response.Redirect("/Acesso/Login");
            }

            return View();
        }

        public ActionResult Perfil()
        {
            //VERIFICA SE EXISTE ALGUM DADO NA SESSÃO USUARIO
            if (Session["Usuario"] != null)
            {
                //CRIA SESSÃO DO USUARIO
                ViewBag.Logado = Session["Usuario"];
                Usuario User = (Usuario)Session["Usuario"];
                ViewBag.User = User;

                //METODO PARA BUSCA DE USUARIOS, MUSICAS, BANDAS
                if (Request.HttpMethod == "POST")
                {
                    String busca = Request.Form["busca"].ToString();
                    Response.Redirect("/Menu/Pesquisar/" + busca);
                }

            }
            //CASO SESSAO SEJA NULA -> REDIRECIONAMENTO PARA PAGINA LOGIN
            else
            {
                Response.Redirect("/Acesso/Login");
            }
            return View();
        }

        public ActionResult Pesquisar(String busca)
        {
            //VERIFICA SE EXISTE ALGUM DADO NA SESSÃO USUARIO
            if (Session["Usuario"] != null)
            {
                //CRIA SESSÃO DO USUARIO
                ViewBag.Logado = Session["Usuario"];
                Usuario User = (Usuario)Session["Usuario"];
                ViewBag.User = User;

                //RETORNA OS USUARIOS, CASO HAJA RESULTADO
                if (Usuario.Listar(busca) != null)
                {
                    List<Usuario> Usuarios = Usuario.Listar(busca);
                    ViewBag.Usuarios = Usuarios;
                }
            }
            //CASO SESSAO SEJA NULA -> REDIRECIONAMENTO PARA PAGINA LOGIN
            else
            {
                Response.Redirect("/Acesso/Login");
            }

            return View();
        }
    }
}