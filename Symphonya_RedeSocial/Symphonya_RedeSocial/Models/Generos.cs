using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Symphonya_RedeSocial.Models
{
    public class Genero
    {
        public Int32 ID { get; set; }
        public String Nome { get; set; }

        public Genero() { }

        public Boolean NovoGenero()
        {

            SqlConnection Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["SymphonyaBCD"].ConnectionString);
            Conexao.Open();

            //CRIACAO DO COMANDO SQL
            SqlCommand Comando = new SqlCommand();
            Comando.Connection = Conexao;
            Comando.CommandText = "INSERT INTO Usuario ( Nome )"
              + "VALUES ( @Nome );";
            Comando.Parameters.AddWithValue("@Nome", this.Nome);

            Int32 Resultado = Comando.ExecuteNonQuery();

            Conexao.Close();

            return Resultado > 0 ? true : false;
        }

        public static List<Genero> Listar()
        {
            SqlConnection Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["SymphonyaBCD"].ConnectionString);
            Conexao.Open();

            SqlCommand Comando = new SqlCommand();
            Comando.Connection = Conexao;
            Comando.CommandText = "SELECT * FROM Genero";

            SqlDataReader Leitor = Comando.ExecuteReader();

            List<Genero> genders = new List<Genero>();
            while (Leitor.Read())
            {
                Genero G = new Genero();
                G.ID = (Int32)Leitor["ID"];
                G.Nome = ((String)Leitor["Nome"]);

                genders.Add(G);
            }

            if (!Leitor.HasRows)
            {
                Conexao.Close();
                return null;
            }

            Conexao.Close();

            return genders;
        }


        public Boolean Apagar()
        {
            SqlConnection Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["SymphonyaBCD"].ConnectionString);
            Conexao.Open();

            SqlCommand Comando = new SqlCommand();
            Comando.Connection = Conexao;
            Comando.CommandText = "DELETE FROM Usuario WHERE ID = @ID;";
            Comando.Parameters.AddWithValue("@ID", this.ID);

            Int32 Resultado = Comando.ExecuteNonQuery();



            return Resultado > 0 ? true : false;
        }
        }
}