using Symphonya_RedeSocial.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.HtmlControls;

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
                ViewBag.Instrumentos = Instrumentos.ListarEspecifico(User.ID);
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

        public ActionResult Agenda()
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
                ViewBag.Instrumentos = Instrumentos.ListarEspecifico(User.ID);
                ViewBag.User = User;

                if (Request.HttpMethod == "POST")
                {

                    String NovoNome = Request.Form["NovoNome"];
                    String NovoSobrenome = Request.Form["NovoSobrenome"];
                    String NovoEmail = Request.Form["NovoEmail"];
                    String NovaCidade = Request.Form["NovaCidade"];
                    String NovoEstado = Request.Form["NovoEstado"];
                    String NovoTelefone = Request.Form["NovoCelular"];

                    HttpPostedFileBase NovaImagemPerfil = Request.Files["NovaImagemPerfil"];
                    HttpPostedFileBase NovaImagemCapa = Request.Files["NovaImagemCapa"];

                    Usuario EditarUsuario = new Usuario();

                    EditarUsuario = (Usuario)Session["Usuario"];

                    //CASO O CAMPO DE NOME SEJA DIFERENTE DE NULO
                    if (NovoNome != "")
                    {
                        EditarUsuario.Nome = NovoNome;
                    }

                    //CASO O CAMPO DE SOBRENOME SEJA DIFERENTE DE NULO
                    if (NovoSobrenome != "")
                    {
                        EditarUsuario.Sobrenome = NovoSobrenome;
                    }

                    //CASO O CAMPO DE EMAIL SEJA DIFERENTE DE NULO
                    if (NovoEmail != "")
                    {
                        EditarUsuario.Email = NovoEmail;
                    }

                    //CASO O CAMPO DE CIDADE SEJA DIFERENTE DE NULO
                    if (NovaCidade != "")
                    {
                        EditarUsuario.Cidade = NovaCidade;
                    }

                    //CASO O CAMPO DE ESTADO SEJA DIFERENTE DE NULO
                    if (NovoEstado != "")
                    {
                        EditarUsuario.Estado = NovoEstado;
                    }

                    //CASO O CAMPO DE TELEFONE SEJA DIFERENTE DE NULO
                    if (NovoTelefone != "")
                    {
                        EditarUsuario.Telefone = NovoTelefone;
                    }

                    int ID = EditarUsuario.ID;

                    //CASO O CAMPO DE IMAGEM DE PERFIL SEJA DIFERENTE DE NULO
                    if (NovaImagemPerfil.FileName != "")
                    {
                        //PERCORRE OS FILES NO INPUT
                        foreach (string fileName in Request.Files)
                        {
                            //RECOLHE DADOS DO FILE QUE ESTA NO INPUT
                            HttpPostedFileBase postedFile = Request.Files[fileName];
                            int contentLength = postedFile.ContentLength;
                            string contentType = postedFile.ContentType;
                            string nome = postedFile.FileName;

                            //CRIA UM OBJETO IMAGEM PARA REDIMENSIONAMENTO
                            Imagem img = new Imagem();

                            //CASO O FILE POSSUA UMA EXTENSAO IGUAL A JPEG OU PNG OU JPG
                            if (contentType.IndexOf("jpeg") > 0 || contentType.IndexOf("png") > 0 || contentType.IndexOf("jpg") > 0)
                            {
                                //FORNECE AS DIMENSOES PARA O REDIMENSIONAMENTO
                                Bitmap arquivoConvertido = img.ResizeImage(postedFile.InputStream, 180, 180);

                                //CRIA O NOME DO ARQUIVO, ESTE QUE TRAS O ID DO USUARIO
                                string nomeArquivoUpload = "imagemPerfil" + ID + ".png";

                                //SALVA O ARQUIVO
                                arquivoConvertido.Save(HttpRuntime.AppDomainAppPath + "\\Imagens\\ImagensUsuario\\" + nomeArquivoUpload);
                                arquivoConvertido.Save(@"C:\Users\16128565\Source\Repos\Symphonya_RedeSocial\Symphonya_RedeSocial\Symphonya_RedeSocial\Imagens\ImagensUsuario" + nomeArquivoUpload);

                                //SETA A IMAGEM DE PERFIL DO USUARIO
                                EditarUsuario.Imagem_Perfil = nomeArquivoUpload;
                               }

                            }
                       }

                    //CASO O CAMPO DE IMAGEM DE CAPA SEJA DIFERENTE DE NULO
                    if(NovaImagemCapa.FileName != "")
                    {
                        //PERCORRE OS FILES NO INPUT
                        foreach (string fileName in Request.Files)
                        {
                            //RECOLHE DADOS DO FILE QUE ESTA NO INPUT
                            HttpPostedFileBase postedFile = Request.Files[fileName];
                            int contentLength = postedFile.ContentLength;
                            string contentType = postedFile.ContentType;
                            string nome = postedFile.FileName;

                            //CRIA UM OBJETO IMAGEM PARA REDIMENSIONAMENTO
                            Imagem img = new Imagem();

                            //CASO O FILE POSSUA UMA EXTENSAO IGUAL A JPEG OU PNG OU JPG
                            if (contentType.IndexOf("jpeg") > 0 || contentType.IndexOf("png") > 0 || contentType.IndexOf("jpg") > 0)
                            {
                                //FORNECE AS DIMENSOES PARA O REDIMENSIONAMENTO
                                Bitmap arquivoConvertido = img.ResizeImage(postedFile.InputStream, 1920, 1080);

                                //CRIA O NOME DO ARQUIVO, ESTE QUE TRAS O ID DO USUARIO
                                string nomeArquivoUpload = "imagemCapa" + ID + ".png";

                                //SALVA O ARQUIVO
                                arquivoConvertido.Save(HttpRuntime.AppDomainAppPath + "\\Imagens\\ImagensUsuario\\" + nomeArquivoUpload);
                                arquivoConvertido.Save(@"C:\Users\16128604\Source\Repos\Symphonya_RedeSocial\Symphonya_RedeSocial\Symphonya_RedeSocial\Imagens\ImagensUsuario" + nomeArquivoUpload);

                                //SETA A IMAGEM DE CAPA DO USUARIO
                                EditarUsuario.Imagem_Capa = nomeArquivoUpload;
                            }

                        }
                    }

                    if (EditarUsuario.Alterar())
                    {
                        ViewBag.Mensagem = "Perfil alterado com sucesso!";
                        Session["Usuario"] = EditarUsuario;
                        ViewBag.Usuario = (Usuario)Session["Usuario"];
                        Response.Redirect("~/Menu/Perfil", false);
                    }
                    else
                    {
                        ViewBag.Mensagem = "Houve um erro ao alterar o Perfil. Verifique os dados e tente novamente.";
                    }
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
        public ActionResult Email()
        {
            //VERIFICA SE EXISTE ALGUM DADO NA SESSÃO USUARIO
            if (Session["Usuario"] != null)
            {
                //CRIA VIEWBAG CASO USUARIO ESTEJA LOGADO
                ViewBag.Logado = Session["Usuario"];
                //CRIA SESSÃO DO USUARIO
                Usuario User = (Usuario)Session["Usuario"];
                ViewBag.Instrumentos = Instrumentos.ListarEspecifico(User.ID);
                ViewBag.User = User;



                if (Request.HttpMethod == "POST")
                {
           
                    /**Parte de enviar o email**/
                    MailMessage msg = new MailMessage();
                    SmtpClient smtp = new SmtpClient("smtp.office365.com");
                    msg.From = new MailAddress("contato12345123451@outlook.com");
                    msg.To.Add(Request.Form["email"].ToString());
                    msg.Subject = "Recuperação de Senha";
                    msg.Body = "sua nova senha é: ";

                    /**contato616516@outlook.com**/
                    smtp.Port = 587;
                    smtp.Credentials = new System.Net.NetworkCredential("contato12345123451@outlook.com", "senai1234");
                    smtp.EnableSsl = true;
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
        public ActionResult Arquivo()
        {
            //VERIFICA SE EXISTE ALGUM DADO NA SESSÃO USUARIO
            if (Session["Usuario"] != null)
            {
                //CRIA VIEWBAG CASO USUARIO ESTEJA LOGADO
                ViewBag.Logado = Session["Usuario"];
                //CRIA SESSÃO DO USUARIO
                Usuario User = (Usuario)Session["Usuario"];
                ViewBag.Instrumentos = Instrumentos.ListarEspecifico(User.ID);
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
        public ActionResult VerBandas()
        {
            //VERIFICA SE EXISTE ALGUM DADO NA SESSÃO USUARIO
            if (Session["Usuario"] != null)
            {
                //CRIA VIEWBAG CASO USUARIO ESTEJA LOGADO
                ViewBag.Logado = Session["Usuario"];
                //CRIA SESSÃO DO USUARIO
                Usuario User = (Usuario)Session["Usuario"];
                ViewBag.Instrumentos = Instrumentos.ListarEspecifico(User.ID);
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
    }
}