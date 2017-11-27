using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Symphonya_RedeSocial.Models
{
    public class Instrumentos
    {
        public Int32 ID { get; set; }
        public String Nome { get; set; }
        public String Classificacao { get; set; }
        public String Icone { get; set; }
        public Int32 Maestria { get; set; }

        public Instrumentos()
        {

        }

        public Instrumentos(Int32 IDInstrumento, Int32 IDUsuario)
        {
            SqlConnection Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["Symphonya"].ConnectionString);
            Conexao.Open();

            SqlCommand Comando = new SqlCommand();
            Comando.Connection = Conexao;
            Comando.CommandText = "SELECT Instrumento.Nome, Instrumento.Icone, Nivel FROM Instrumento, Usuario_Has_Instrumentos WHERE Instrumento.ID = @IDInstrumento AND UsuarioID = @IDUsuario;";
            Comando.Parameters.AddWithValue("@IDInstrumento", IDInstrumento);
            Comando.Parameters.AddWithValue("@IDUsuario", IDUsuario);

            SqlDataReader Leitor = Comando.ExecuteReader();

            Leitor.Read();

            this.ID = (Int32)Leitor["ID"];
            this.Nome = (String)Leitor["Nome"];
            this.Classificacao = (String)Leitor["Classificacao"];
            this.Icone = (String)Leitor["Icone"];

            Conexao.Close();
        }

        public Instrumentos(String Nome)
        {
            SqlConnection Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["Symphonya"].ConnectionString);
            Conexao.Open();

            SqlCommand Comando = new SqlCommand();
            Comando.Connection = Conexao;
            Comando.CommandText = "SELECT * FROM Instrumentos WHERE Nome=@Nome;";
            Comando.Parameters.AddWithValue("@Nome", Nome);

            SqlDataReader Leitor = Comando.ExecuteReader();

            Leitor.Read();

            this.ID = (Int32)Leitor["ID"];
            this.Nome = (String)Leitor["Nome"];
            this.Classificacao = (String)Leitor["Classificacao"];
            this.Icone = (String)Leitor["Icone"];

            Conexao.Close();
        }

        public static List<Instrumentos> Listar()
        {
            SqlConnection Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["Symphonya"].ConnectionString);
            Conexao.Open();

            SqlCommand Comando = new SqlCommand();
            Comando.Connection = Conexao;
            Comando.CommandText = "SELECT * FROM Instrumento";

            SqlDataReader Leitor = Comando.ExecuteReader();

            List<Instrumentos> Instrumentos = new List<Instrumentos>();
            while (Leitor.Read())
            {
                Instrumentos Instrumento = new Instrumentos();
                Instrumento.ID = (Int32)Leitor["ID"];
                Instrumento.Nome = (String)Leitor["Nome"];
                Instrumento.Classificacao = (String)Leitor["Classificacao"];
                Instrumento.Icone = (String)Leitor["Icone"];

                Instrumentos.Add(Instrumento);
            }

            if (!Leitor.HasRows)
            {
                Conexao.Close();
                return null;
            }

            Conexao.Close();

            return Instrumentos;
        }

        public static List<Instrumentos> ListarEspecifico(Int32 ID, bool limite)
        {
            SqlConnection Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["Symphonya"].ConnectionString);
            Conexao.Open();

            SqlCommand Comando = new SqlCommand();
            Comando.Connection = Conexao;
            if(limite)
                Comando.CommandText = "	SELECT TOP 3 ID,Instrumento.Nome, Instrumento.Icone, Nivel FROM Instrumento, Usuario_Has_Instrumentos WHERE Instrumento.ID = InstrumentoID AND UsuarioID = @ID;";
            else
                Comando.CommandText = "	SELECT ID,Instrumento.Nome, Instrumento.Icone, Nivel FROM Instrumento, Usuario_Has_Instrumentos WHERE Instrumento.ID = InstrumentoID AND UsuarioID = @ID;";

            Comando.Parameters.AddWithValue("@ID", ID);

            SqlDataReader Leitor = Comando.ExecuteReader();

            List<Instrumentos> Instrumentos = new List<Instrumentos>();
            while (Leitor.Read())
            {
                Instrumentos Instrumento = new Instrumentos();
                Instrumento.ID = (Int32)Leitor["ID"];
                Instrumento.Nome = (String)Leitor["Nome"];
                Instrumento.Icone = (String)Leitor["Icone"];
                int maestria = (Int32)Leitor["Nivel"];
                switch (maestria)
                {
                    case 1: Instrumento.Maestria = 25; break;
                    case 2: Instrumento.Maestria = 50; break;
                    case 3: Instrumento.Maestria = 75; break;
                    case 4: Instrumento.Maestria = 100; break;
                    default: break;
                }

                Instrumentos.Add(Instrumento);
            }

            if (!Leitor.HasRows)
            {
                Conexao.Close();
                return null;
            }

            Conexao.Close();

            return Instrumentos;
        }

        public Boolean Alterar(Int32 IDUsuario)
        {
            SqlConnection Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["Symphonya"].ConnectionString);
            Conexao.Open();

            SqlCommand Comando = new SqlCommand();
            Comando.Connection = Conexao;
            Comando.CommandText = "UPDATE Usuario_Has_Instrumentos SET ID = @ID, Nome = @Nome, Icone = @Icone, Classificacao = @Classificacao, Nivel = @Nivel WHERE IDInstrumento = @ID AND IDUsuario = @IDUsuario;";
            Comando.Parameters.AddWithValue("@ID", this.ID);
            Comando.Parameters.AddWithValue("@Nome", this.Nome);
            Comando.Parameters.AddWithValue("@Icone", this.Icone);
            Comando.Parameters.AddWithValue("@Classificacao", this.Classificacao);
            Comando.Parameters.AddWithValue("@Nivel", this.Maestria);
            Comando.Parameters.AddWithValue("@IDUsuario", IDUsuario);

            Int32 Resultado = Comando.ExecuteNonQuery();

            Conexao.Close();

            return Resultado > 0 ? true : false;
        }
    }
}