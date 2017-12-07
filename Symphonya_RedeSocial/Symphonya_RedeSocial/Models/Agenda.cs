
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
        public DateTime Data { get; set; }
        public String Hora { get; set; }
        public String Titulo { get; set; }
        public String Descricao { get; set; }
        public Int32 AgendaID { get; set; }
        public Int32 UsuarioID { get; set; }

        public Agenda() { }

        public Agenda(Int32 ID)
        {
            SqlConnection Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["SymphonyaBCD"].ConnectionString);
            Conexao.Open();

            SqlCommand Comando = new SqlCommand();
            Comando.Connection = Conexao;
            Comando.CommandText = "SELECT * FROM Agenda WHERE UsuarioID=@ID;";
            Comando.Parameters.AddWithValue("@ID", ID);

            SqlDataReader Leitor = Comando.ExecuteReader();

            Leitor.Read();

            this.ID = (Int32)Leitor["ID"];
            this.Data = (DateTime)Leitor["Data"];

            Conexao.Close();
        }

        public static Boolean Mostrar(Int32 IDUsuario)
        {
            SqlConnection Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["SymphonyaBCD"].ConnectionString);
            Conexao.Open();

            SqlCommand Comando = new SqlCommand();
            Comando.Connection = Conexao;
            Comando.CommandText = "SELECT * FROM Agenda WHERE UsuarioID=@ID;";
            Comando.Parameters.AddWithValue("@ID", IDUsuario);

            SqlDataReader Leitor = Comando.ExecuteReader();

            Leitor.Read();

            if (!Leitor.HasRows)
            {
                return false;
            }

            Agenda agenda = new Agenda();
            agenda.ID = (Int32)Leitor["ID"];
            agenda.Data = (DateTime)Leitor["Data"];

            Conexao.Close();

            return true;
        }

        public static List<Agenda> ListarAgenda(Int32 ID)
        {
            SqlConnection Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["SymphonyaBCD"].ConnectionString);
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
                A.Data = (DateTime)Leitor["Data"];
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
        public Boolean NovoEvento(Int32 IDU)
        {

            SqlConnection Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["SymphonyaBCD"].ConnectionString);
            Conexao.Open();

            //CRIACAO DO COMANDO SQL
            SqlCommand Comando = new SqlCommand();
            Comando.Connection = Conexao;
            Comando.CommandText = "INSERT INTO Show (Hora ,Data,Titulo,Descricao, UsuarioID, AgendaID, IDU)"
              + "VALUES (@Hora,@Data,@Titulo,@Descricao,@UsuarioID,@AgendaID);";

            DateTime datahora = DateTime.Now;
            //String Hora = datahora.Day + "/" + datahora.Month + "/" + datahora.Year;
            //String Data = datahora.Day + "/" + datahora.Month + "/" + datahora.Year;

            Comando.Parameters.AddWithValue("@Hora", this.Hora);
            Comando.Parameters.AddWithValue("@Data", this.Data);
            Comando.Parameters.AddWithValue("@Titulo", this.Titulo);
            Comando.Parameters.AddWithValue("@Descricao", this.Descricao);
            //Comando.Parameters.AddWithValue("@UsuarioID", UsuarioID);
            //Comando.Parameters.AddWithValue("@AgendaID", AgendaID);

            Int32 Resultado = Comando.ExecuteNonQuery();

            Conexao.Close();

            return Resultado > 0 ? true : false;
        }

        public Boolean NovaAgenda(Int32 IDUsuario)
        {
            SqlConnection Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["SymphonyaBCD"].ConnectionString);
            Conexao.Open();

            //CRIACAO DO COMANDO SQL
            SqlCommand Comando = new SqlCommand();
            Comando.Connection = Conexao;
            Comando.CommandText = "INSERT INTO Agenda VALUES (@Data, @UsuarioID);";
            Comando.Parameters.AddWithValue("@Data", DateTime.Now.Date);
            Comando.Parameters.AddWithValue("@UsuarioID", IDUsuario);

            Int32 Resultado = Comando.ExecuteNonQuery();

            Conexao.Close();

            return Resultado > 0 ? true : false;
        }

        internal void NovoEvento()
        {
            throw new NotImplementedException();
        }
    }
}