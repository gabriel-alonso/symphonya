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
                //CRIA SESSÃO DO USUARIO
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

        public ActionResult VerUsuario()
        {
            //VERIFICA SE EXISTE ALGUM DADO NA SESSÃO USUARIO
            if (Session["Usuario"] != null)
            {
                //CRIA VIEWBAG CASO USUARIO ESTEJA LOGADO
                ViewBag.Logado = Session["Usuario"];
                //CRIA SESSÃO DO USUARIO
                Usuario User = (Usuario)Session["Usuario"];
                ViewBag.User = User;
                if (TempData["Usuario"] != null)
                {
                    ViewBag.Visualizacao = (Usuario)TempData["Usuario"];
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
        
        public ActionResult EditarPerfil()
        {
            if(Session["Usuario"] != null)
            {
                //CRIA VIEWBAG CASO USUARIO ESTEJA LOGADO
                ViewBag.Logado = Session["Usuario"];
                //CRIA SESSÃO DO USUARIO
                Usuario User = (Usuario)Session["Usuario"];
                ViewBag.User = User;

                if (Request.HttpMethod == "POST")
                {
                    String NovoNome = Request.Form["NovoNome"];
                    String NovoSobrenome = Request.Form["NovoSobrenome"];
                    String NovoEmail = Request.Form["NovoEmail"];
                    String NovaCidade = Request.Form["NovaCidade"];
                    String NovoEstado = Request.Form["NovoEstado"];
                    String NovoTelefone = Request.Form["NovoTelefone"];

                    Usuario EditarUsuario = new Usuario();

                    EditarUsuario = (Usuario)Session["Usuario"];
                    int ID = EditarUsuario.ID;

                    foreach (string fileName in Request.Files)
                    {
                        HttpPostedFileBase postedFile = Request.Files[fileName];
                        int contentLength = postedFile.ContentLength;
                        string contentType = postedFile.ContentType;
                        string nome = postedFile.FileName;

                        if (contentType.IndexOf("jpeg") > 0)
                        {
                            postedFile.SaveAs(HttpRuntime.AppDomainAppPath + "\\images\\img_users\\" + "imagemPerfil" + ID + ".jpg");
                            //postedFile.SaveAs(@"C:\Users\16128604\Source\Repos\lpw-2017-3infb-g4\Katiau\WebSite\images\img_users\" + "imagemPerfil" + ID + ".jpg");
                        }
                        // else
                        //  postedFile.SaveAs(@"C:\Users\16128604\Source\Repos\lpw-2017-3infb-g4\Katiau\WebSite\images\" + Request.Form["Desc"] + ".txt");

                    }

                    EditarUsuario.Imagem_Perfil = "imagemPerfil" + ID + ".jpg";

                    //if (NovoPerfil.NovaBio())
                    //{
                    //    ViewBag.Mensagem = "Perfil alterado com sucesso!";
                    //    ViewBag.BioU = Bio;
                    //    ViewBag.ImagemU = NovoPerfil.ImagemPerfil;
                    //    Response.Redirect("~/Perfil/Index", false);
                    //}
                    //else
                    //{
                    //    ViewBag.Mensagem = "Houve um erro ao alterar o Perfil. Verifique os dados e tente novamente.";
                    //}
                }
                return View();
            }
            Response.Redirect("~/Menu/Home", false);
            return View();

        }

        public ActionResult Pesquisar(String busca)
        {
            if(busca == null)
            {
                Response.Redirect("/Menu/Feed");
            }
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

                return RedirectToAction("VerUsuario", "Menu");
            }
            else
            {
                return RedirectToAction("Pesquisar", "Menu");
            }
        }

    }
}