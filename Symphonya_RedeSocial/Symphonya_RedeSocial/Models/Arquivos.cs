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
        public static Boolean NovoArquivo(String tipo, String Nome, Int32 ID)

        {

            SqlConnection Conexao = new SqlConnection("Server=ESN509VMSSQL;Database=Symphonya;User Id=Aluno;Password=Senai1234;");
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
            SqlConnection Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["Symphonya"].ConnectionString);
            Conexao.Open();

            SqlCommand Comando = new SqlCommand();
            Comando.Connection = Conexao;
            if (limite)
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
    }

}
