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

        public ActionResult CadastroBanda()
        {
            if (Session["Usuario"] != null)
            {
                //CRIA VIEWBAG CASO USUARIO ESTEJA LOGADO
                ViewBag.Logado = Session["Usuario"];
                //CRIA SESSÃO DO USUARIO
                Usuario User = (Usuario)Session["Usuario"];
                ViewBag.User = User;

                //RETORNA OS USUARIOS, CASO HAJA RESULTADO
                if (Genero.Listar() != null)
                {
                    List<Genero> generos = Genero.Listar();
                    ViewBag.Generos = generos;
                }

                if (Request.HttpMethod == "POST")
                {
                    Bandas ba = new Bandas();
                    ba.Nome = Request.Form["Nome"].ToString();
                    ba.Descricao = Request.Form["Descricao"].ToString();
                    ba.NovaBanda();
                    Response.Redirect("/Menu/Feed");
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
                            postedFile.SaveAs(HttpRuntime.AppDomainAppPath + "\\Imagens\\ImagensUsuario\\" + "imagemPerfil" + ID + ".png");
                            postedFile.SaveAs(@"C:\Users\16128604\Source\Repos\Symphonya_RedeSocial\Symphonya_RedeSocial\Symphonya_RedeSocial\Imagens\ImagensUsuario" + "imagemPerfil" + ID + ".png");
                        }
                        else
                            ViewBag.Erro = "Erro ao enviar imagem, tente novamente!";

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
            Response.Redirect("~/Acesso/Login", false);
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

        public ActionResult Livestream()
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

        public ActionResult VerSeguidores()
        {
            //VERIFICA SE EXISTE ALGUM DADO NA SESSÃO USUARIO
            if (Session["Usuario"] != null)
            {
                //CRIA SESSÃO DO USUARIO
                ViewBag.Logado = Session["Usuario"];
                Usuario User = (Usuario)Session["Usuario"];
                ViewBag.User = User;

                if (Seguidores.ListarSeguidores(User.ID) != null)
                {
                    List<Seguidores> seguidores = Seguidores.ListarSeguidores(User.ID);
                    ViewBag.Seguidores = seguidores;
                }
                else
                {
                    ViewBag.Erro = "Não foram encontrados seguidores!";
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

        public ActionResult VerSeguidos()
        {
            //VERIFICA SE EXISTE ALGUM DADO NA SESSÃO USUARIO
            if (Session["Usuario"] != null)
            {
                //CRIA SESSÃO DO USUARIO
                ViewBag.Logado = Session["Usuario"];
                Usuario User = (Usuario)Session["Usuario"];
                ViewBag.User = User;

                if (Seguidores.ListarSeguidos(User.ID) != null)
                {
                    List<Seguidores> seguidos = Seguidores.ListarSeguidos(User.ID);
                    ViewBag.Seguidos = seguidos;
                }
                else
                {
                    ViewBag.Erro = "Não foram encontrados seguidores!";
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
        public ActionResult Unfollow(Int32 ID)
        {
            //VERIFICA SE EXISTE ALGUM DADO NA SESSÃO USUARIO
            if (Session["Usuario"] != null)
            {
                Seguidores.Unfollow(ID);
            }
            //CASO SESSAO SEJA NULA -> REDIRECIONAMENTO PARA PAGINA LOGIN
            else
            {
                Response.Redirect("/Acesso/Login");
            }

            return RedirectToAction("VerSeguidos", "Menu");
        }
    }
}