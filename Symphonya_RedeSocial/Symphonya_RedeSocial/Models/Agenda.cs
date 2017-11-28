
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
        public String Hora { get; set; }
        public String Titulo { get; set; }
        public String Descricao { get; set; }
        public Int32 AgendaID { get; set; }
        public Int32 UsuarioID { get; set; }

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

        public static List<Agenda> ListarAgenda(Int32 ID)
        {
            SqlConnection Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["Symphonya"].ConnectionString);
            Conexao.Open();

            SqlCommand Comando = new SqlCommand();
            Comando.Connection = Conexao;
            Comando.CommandText = "SELECT Show.ID, Show.Hora, Show.Data, Show.Titulo, Show.Descricao, Show.UsuarioID, Show.AgendaID FROM Show,Agenda WHERE IDUsuario LIKE @ID AND IDUsuario2 LIKE Usuario.ID;";
            Comando.Parameters.AddWithValue("@ID", ID);
            

            SqlDataReader Leitor = Comando.ExecuteReader();

            List<Agenda> Agendas = new List<Agenda>();
            while (Leitor.Read())
            {
                Agenda A = new Agenda();
                A.ID = (Int32)Leitor["ID"];
                A.Hora = (String)Leitor["Hora"];
                A.Data = (String)Leitor["Data"];
                A.Titulo = (String)Leitor["Titulo"];
                A.Descricao = (String)Leitor["Descricao"];

                Agendas.Add(A);
            }

            if (!Leitor.HasRows)
            {
                Conexao.Close();
                return null;
            }

            Conexao.Close();

            return Agendas;
        }

    }
}