using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Symphonya_RedeSocial.Models
{

    public class Bandas
    {
        public Int32 ID { get; set; }
        public String Nome { get; set; }
        public String Descricao { get; set; }
        public String Lider { get; set; }

        public Bandas() { }

        public Bandas(Int32 ID)
        {
            SqlConnection Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["Symphonya"].ConnectionString);
            Conexao.Open();

            SqlCommand Comando = new SqlCommand();
            Comando.Connection = Conexao;
            Comando.CommandText = "SELECT * FROM Bandas WHERE ID=@ID;";
            Comando.Parameters.AddWithValue("@ID", ID);

            SqlDataReader Leitor = Comando.ExecuteReader();

            Leitor.Read();

            this.ID = (Int32)Leitor["ID"];
            this.Nome = (String)Leitor["Nome"];
            this.Descricao = (String)Leitor["Descricao"];

            Conexao.Close();
        }

        public static List<Bandas> ListarBandas(Int32 ID)
        {
            SqlConnection Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["Symphonya"].ConnectionString);
            Conexao.Open();

            SqlCommand Comando = new SqlCommand();
            Comando.Connection = Conexao;
            Comando.CommandText = "SELECT Bandas.ID, Bandas.Nome, Bandas.Descricao, Bandas.Lider FROM Bandas,Usuario_Has_Bandas WHERE BandasID LIKE @ID AND UsuarioID LIKE Bandas.ID;";
            Comando.Parameters.AddWithValue("@ID", ID);

            SqlDataReader Leitor = Comando.ExecuteReader();

            List<Bandas> Bandas = new List<Bandas>();
            while (Leitor.Read())
            {
                Bandas B = new Bandas();
                B.ID = (Int32)Leitor["ID"];
                B.Nome = ((String)Leitor["Nome"]);
                B.Descricao = (String)Leitor["Descricao"];
                B.Lider = (String)Leitor["Lider"];

                Bandas.Add(B);
            }

            if (!Leitor.HasRows)
            {
                Conexao.Close();
                return null;
            }

            Conexao.Close();

            return Bandas;
        }
        public Boolean NovaBanda(Int32 IDU)
        {

            SqlConnection Conexao = new SqlConnection("Server=ESN509VMSSQL;Database=Symphonya;User Id=Aluno;Password=Senai1234;");
            Conexao.Open();

            //CRIACAO DO COMANDO SQL
            SqlCommand Comando = new SqlCommand();
            Comando.Connection = Conexao;
            Comando.CommandText = "INSERT INTO Bandas ( Nome , Descricao, Lider, IDU)"
              + "VALUES ( @Nome , @Descricao, @Lider);";
            Comando.Parameters.AddWithValue("@Nome", this.Nome);
            Comando.Parameters.AddWithValue("@Descricao", this.Descricao);
            Comando.Parameters.AddWithValue("@Lider", this.Lider);

            Int32 Resultado = Comando.ExecuteNonQuery();

            Conexao.Close();

            return Resultado > 0 ? true : false;
        }
        public static Boolean excludeBand(Int32 ID)
        {
            SqlConnection Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["Symphonya"].ConnectionString);
            Conexao.Open();

            SqlCommand Comando = new SqlCommand();
            Comando.Connection = Conexao;
            Comando.CommandText = "DELETE FROM Usuario_Has_Bandas WHERE BandasID = @ID;";
            Comando.Parameters.AddWithValue("@ID", ID);

            Int32 Resultado = Comando.ExecuteNonQuery();

            Conexao.Close();
            return Resultado > 0 ? true : false;

        }

        internal void NovaBanda()
        {
            throw new NotImplementedException();
        }
    }
}