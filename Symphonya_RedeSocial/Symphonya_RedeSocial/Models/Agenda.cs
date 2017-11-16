
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Symphonya_RedeSocial.Models
{
    public class Agenda
    {
        public Int32 ID { get; set; }
        public String Data { get; set; }

        public Agenda() { }

        public Agenda(Int32 ID)
        {
            SqlConnection Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["Symphonya"].ConnectionString);
            Conexao.Open();

            SqlCommand Comando = new SqlCommand();
            Comando.Connection = Conexao;
            Comando.CommandText = "SELECT * FROM Agenda WHERE ID=@ID;";
            Comando.Parameters.AddWithValue("@ID", ID);

            SqlDataReader Leitor = Comando.ExecuteReader();

            Leitor.Read();

            this.ID = (Int32)Leitor["ID"];
            this.Data = (String)Leitor["Data"];

            Conexao.Close();
        }
        public Boolean NovaAgenda()
        {

            SqlConnection Conexao = new SqlConnection("Server=ESN509VMSSQL;Database=Symphonya;User Id=Aluno;Password=Senai1234;");
            Conexao.Open();

            //CRIACAO DO COMANDO SQL
            SqlCommand Comando = new SqlCommand();
            Comando.Connection = Conexao;
            Comando.CommandText = "INSERT INTO Agenda (Data)"
              + "VALUES (@Data);";

            DateTime datahora = DateTime.Now;
            String Data = datahora.Day + "/" + datahora.Month + "/" + datahora.Year;

            Comando.Parameters.AddWithValue("@Data", this.Data); 

            Int32 Resultado = Comando.ExecuteNonQuery();

            Conexao.Close();

            return Resultado > 0 ? true : false;
        }


    }
}