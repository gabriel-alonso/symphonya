using System;
using System.Collections.Generic;
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
    }

}
