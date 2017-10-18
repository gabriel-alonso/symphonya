using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Symphonya_RedeSocial.Models
{
    public class Seguidores
    {
        public Int32 ID { get; set; }
        public String Nome { get; set; }
        public String Sobrenome { get; set; }
        public String Email { get; set; }

        public Seguidores()
        {

        }

        public static List<Seguidores> ListarSeguidores(Int32 ID)
        {
            SqlConnection Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["Symphonya"].ConnectionString);
            Conexao.Open();

            SqlCommand Comando = new SqlCommand();
            Comando.Connection = Conexao;
            Comando.CommandText = "SELECT Usuario.ID, Usuario.Nome, Usuario.Sobrenome, Usuario.Email FROM Usuario,Usuario_Has_Usuario WHERE IDUsuario LIKE @ID AND IDUsuario2 LIKE Usuario.ID;";
            Comando.Parameters.AddWithValue("@ID", ID);

            SqlDataReader Leitor = Comando.ExecuteReader();

            List<Seguidores> Seguidores = new List<Seguidores>();
            while (Leitor.Read())
            {
                Seguidores S = new Seguidores();
                S.ID = (Int32)Leitor["ID"];
                S.Nome = (String)Leitor["Nome"];
                S.Sobrenome = (String)Leitor["Sobrenome"];
                S.Email = (String)Leitor["Email"];

                Seguidores.Add(S);
            }

            if (!Leitor.HasRows)
            {
                Conexao.Close();
                return null;
            }

            Conexao.Close();

            return Seguidores;
        }
    }
}