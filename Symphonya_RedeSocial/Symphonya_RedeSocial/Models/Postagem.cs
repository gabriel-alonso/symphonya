using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Symphonya_RedeSocial.Models
{
    public class Postagem
    {
        public Int32 ID { get; set; }
        public DateTime Data_Hora { get; set; }
        public String Texto { get; set; }
        public String Autor { get; set; }
        public Int32 AutorID { get; set; }

        public Postagem()
        {
        }

        public bool Postar()
        {
            SqlConnection Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["SymphonyaBCD"].ConnectionString);
            Conexao.Open();

            SqlCommand Comando = new SqlCommand();
            Comando.Connection = Conexao;
            Comando.CommandText = "INSERT INTO Postagem VALUES (@DataHora, @Texto, @AutorID)";
            Comando.Parameters.AddWithValue("@DataHora", this.Data_Hora);
            Comando.Parameters.AddWithValue("@Texto", this.Texto);
            Comando.Parameters.AddWithValue("@AutorID", this.AutorID);

           Int32 Resultado = Comando.ExecuteNonQuery();

            Conexao.Close();

            return Resultado > 0 ? true : false;
        }

        public static List<Postagem> ListarPostagem(Int32 ID)
        {
            SqlConnection Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["SymphonyaBCD"].ConnectionString);
            Conexao.Open();

            SqlCommand Comando = new SqlCommand();
            Comando.Connection = Conexao;
            Comando.CommandText = "SELECT Usuario.Nome, Usuario.Sobrenome, Postagem.ID, Postagem.Texto, Postagem.Data_Hora, Postagem.UsuarioID FROM Usuario,Postagem WHERE Postagem.UsuarioID = Usuario.ID AND Postagem.UsuarioID = @ID;";
            Comando.Parameters.AddWithValue("@ID", ID);


            SqlDataReader Leitor = Comando.ExecuteReader();

            //LISTA COM ID DOS INTEGRANTES
            List<Postagem> Postagens = new List<Postagem>();
            while (Leitor.Read())
            {
                Postagem P = new Postagem();
                P.ID = (Int32)Leitor["ID"];
                P.Texto = (String)Leitor["Texto"];
                P.Data_Hora = (DateTime)Leitor["Data_Hora"];
                P.Autor = (String)Leitor["Nome"] + " " + (String)Leitor["Sobrenome"];
                P.AutorID = (Int32)Leitor["UsuarioID"];
                Postagens.Add(P);
            }

            if (!Leitor.HasRows)
            {
                Conexao.Close();
                return null;
            }

            Conexao.Close();

            return Postagens;
        }
    }
}