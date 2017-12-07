using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Symphonya_RedeSocial.Models
{
    public class Arquivos
    {
        public Int32 ID { get; set; }
        public String Tipo { get; set; }
        public String Nome { get; set; }

        public Arquivos() { }

        public Boolean NovoArquivo(String tipo, String Nome, int ID)
        {

            SqlConnection Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["SymphonyaBCD"].ConnectionString);
            Conexao.Open();

            //CRIACAO DO COMANDO SQL
            SqlCommand Comando = new SqlCommand();
            Comando.Connection = Conexao;
            Comando.CommandText = "INSERT INTO Arquivos ( Tipo , Nome, Usuario_ID)"
              + "VALUES ( @Tipo , @Nome, @ID);";
            Comando.Parameters.AddWithValue("@Tipo", tipo);
            Comando.Parameters.AddWithValue("@Nome", Nome);
            Comando.Parameters.AddWithValue("@ID", ID);

            Int32 Resultado = Comando.ExecuteNonQuery();

            Conexao.Close();

            return Resultado > 0 ? true : false;
        }

        public static List<Arquivos> ListarArquivoUsuario(Int32 ID, bool limite)
        {
            SqlConnection Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["SymphonyaBCD"].ConnectionString);
            Conexao.Open();

            SqlCommand Comando = new SqlCommand();
            Comando.Connection = Conexao;
            if (limite)
                //Comando.CommandText = "	SELECT TOP 3 Usuario.Nome, Usuario.Sobrenome, Arquivos.Nome FROM Usuario,Arquivos WHERE Usuario_ID LIKE @ID;";
                Comando.CommandText = "	SELECT TOP 3 Nome FROM Arquivos WHERE Usuario_ID LIKE @ID;";
            else
                //Comando.CommandText = "	SELECT Usuario.Nome, Usuario.Sobrenome, Arquivos.Nome FROM Usuario,Arquivos WHERE Usuario_ID LIKE @ID;";
                Comando.CommandText = "	SELECT Nome FROM Arquivos WHERE Usuario_ID LIKE @ID;";

            Comando.Parameters.AddWithValue("@ID", ID);

            SqlDataReader Leitor = Comando.ExecuteReader();

            List<Arquivos> Arcs = new List<Arquivos>();
            while (Leitor.Read())
            {
                Arquivos A = new Arquivos();
                A.Nome = (String)Leitor["Nome"];
                Arcs.Add(A);
            }

            if (!Leitor.HasRows)
            {
                Conexao.Close();
                return null;
            }

            Conexao.Close();

            return Arcs;
        }
    }

}
