using Symphonya_RedeSocial.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Services;

namespace Symphonya_RedeSocial
{
    /// <summary>
    /// Summary description for SymphonyaCompanion
    /// </summary>
    [WebService(Namespace = "SymphonyaCompanion")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class SymphonyaCompanion : System.Web.Services.WebService
    {

        //WEBMETHOD PARA AUTENTICACAO NO APP
        [WebMethod]
        public Usuario Login(String email, String senha)
        {
            //RECEBE A SENHA DO USUARIO E CRIPTOGRAFA
            String SenhaEncriptada = FormsAuthentication.HashPasswordForStoringInConfigFile(senha, "SHA1");

            //VERIFICA SE INFORMACOES COLOCADAS PELO USUARIO BATEM COM INFORMACOES NO BANCO
            Usuario.Autenticar(email, SenhaEncriptada);

            //CRIA NOVO USUARIO COM INFORMACOES DO BANCO
            Usuario User = new Usuario(email,SenhaEncriptada);

            //RETORNA OS RESULTADOS
            return User;
        }

        //WEBMETHOD PARA RETORNAR QUEM SEGUE O USUARIO
        [WebMethod]
        public List<Seguidores> ListarSeguidores(Int32 ID)
        {
            //VARIAVEL DE LISTA DE SEGUIDORES
            List<Seguidores> seguidores = Seguidores.ListarSeguidores(ID);
            
            //RETORNA OS RESULTADOS
            return seguidores;
        }

        //WEBMETHOD PARA RETORNAR QUEM O USUARIO SEGUE
        [WebMethod]
        public List<Seguidores> ListarSeguidos(Int32 ID)
        {
            //VARIAVEL DE LISTA DE SEGUIDOS
            List<Seguidores> seguidos = Seguidores.ListarSeguidos(ID);

            //RETORNA OS RESULTADOS
            return seguidos;
        }
    }
}
