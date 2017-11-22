using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Symphonya_RedeSocial.Models
{
    public class Integrantes
    {
        public Int32 ID { get; set; }
        public String Nome { get; set; }
        public String Sobrenome { get; set; }
        public String Email { get; set; }

        public Integrantes()
        {

        }

        public static List<Integrantes> ListarIntegrantes(Int32 ID)
        {
            SqlConnection Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["Symphonya"].ConnectionString);
            Conexao.Open();

            SqlCommand Comando = new SqlCommand();
            Comando.Connection = Conexao;
            Comando.CommandText = "SELECT * FROM Integrantes WHERE BandasID LIKE @ID;";
            Comando.Parameters.AddWithValue("@ID", ID);


            SqlDataReader Leitor = Comando.ExecuteReader();

            //LISTA COM ID DOS INTEGRANTES
            List<Integrantes> Integrantes = new List<Integrantes>();
            while (Leitor.Read())
            {
                Integrantes I = new Integrantes();
                I.ID = (Int32)Leitor["ID"];
                Integrantes.Add(I);
            }

            if (!Leitor.HasRows)
            {
                Conexao.Close();
                return null;
            }

            Conexao.Close();

            return Integrantes;
        }
    }
}