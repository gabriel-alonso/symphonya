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
                //CRIA VIEWBAG CASO USUARIO ESTEJA LOGADO
                ViewBag.Logado = Session["Usuario"];

                if (TempData["Usuario"] == null)
                {
                    //CRIA SESSÃO DO USUARIO
                    Usuario User = (Usuario)Session["Usuario"];
                    ViewBag.User = User;
                }
                else
                {
                    ViewBag.User = TempData["Usuario"];
                }
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
                if(Bandas.Listar(busca) != null)
                {
                    List<Bandas> Bands = Bandas.Listar(busca);
                    ViewBag.Bandas = Bands;
                }
            }
            //CASO SESSAO SEJA NULA -> REDIRECIONAMENTO PARA PAGINA LOGIN
            else
            {
                Response.Redirect("/Acesso/Login");
            }

            return View();
        }

        public ActionResult MostrarUsuario(int id)
        {
            Usuario usuarios = new Usuario(id);
            if (usuarios != null)
            {
                TempData["Usuario"] = usuarios;

                return RedirectToAction("Perfil", "Menu");
            }
            else
            {
                return RedirectToAction("Pesuisar", "Menu");
            }
        }

        public ActionResult CadastroBanda()
        {

            if (Request.HttpMethod == "POST")
            {

                Bandas ba = new Bandas();

                ba.Nome = Request.Form["Nome"].ToString();
                ba.Descricao = Request.Form["Descricao"].ToString();              
                ba.NovaBanda();

                Response.Redirect("/Menu/CadastroBanda");

            }

            return View();
        }
    }
}