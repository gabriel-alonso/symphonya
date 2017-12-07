using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Symphonya_RedeSocial.Models
{
    public class Show {
        public Int32 ID { get; set; }
        public Int32 AgendaID { get; set; }
        public Int32 UsuarioID { get; set; }
        public String Hora { get; set; }
        public String Data { get; set; }
        public String Titulo { get; set; }
        public String Descricao { get; set; }

        public Show() { }                

        public Show(Int32 ID)
        {
            SqlConnection Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["SymphonyaBCD"].ConnectionString);
            Conexao.Open();

            SqlCommand Comando = new SqlCommand();
            Comando.Connection = Conexao;
            Comando.CommandText = "SELECT * FROM Show WHERE ID=@ID;";
            Comando.Parameters.AddWithValue("@ID", ID);

            SqlDataReader Leitor = Comando.ExecuteReader();

            Leitor.Read();

            this.ID = (Int32)Leitor["ID"];
            this.AgendaID = (Int32)Leitor["AgendaID"];
            this.UsuarioID = (Int32)Leitor["UsuarioID"];
            this.Titulo = (String)Leitor["Titulo"];
            this.Hora = Leitor["Hora"].ToString();
            this.Data = Leitor["Data"].ToString();
            this.Descricao = (String)Leitor["Descricao"];

            Conexao.Close();
        }
        public Show(Int32 AgendaID, Int32 UsuarioID)
        {
            SqlConnection Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["SymphonyaBCD"].ConnectionString);
            Conexao.Open();

            //CRIACAO DO COMANDO SQL
            SqlCommand Comando = new SqlCommand();
            Comando.Connection = Conexao;
            Comando.CommandText = "SELECT * FROM Show WHERE AgendaID=@AgendaID AND UsuarioID=@UsuarioID;";
            Comando.Parameters.AddWithValue("@AgendaID", AgendaID);
            Comando.Parameters.AddWithValue("@UsuarioID", UsuarioID);

            SqlDataReader Leitor = Comando.ExecuteReader();

            //LEITURA DO RESULTADO DO COMANDO
            Leitor.Read();

            //COLETA DE DADOS -> USUARIO
            this.ID = (Int32)Leitor["ID"];
            this.AgendaID = (Int32)Leitor["AgendaID"];
            this.UsuarioID = (Int32)Leitor["UsuarioID"];


            Conexao.Close();
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

            List<Agenda> Seguidos = new List<Agenda>();
            while (Leitor.Read())
            {
                Agenda A = new Agenda();
                A.ID = (Int32)Leitor["ID"];
                A.Hora = (String)Leitor["Hora"];
                A.Data = (DateTime)Leitor["Data"];
                A.Titulo = (String)Leitor["Titulo"];
                A.Descricao = (String)Leitor["Descricao"];

                Seguidos.Add(A);
            }

            if (!Leitor.HasRows)
            {
                Conexao.Close();
                return null;
            }

            Conexao.Close();

            return Seguidos;
        }

        public static List<Show> Listar(Int32 IDUsuario)
        {
            SqlConnection Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["SymphonyaBCD"].ConnectionString);
            Conexao.Open();

            SqlCommand Comando = new SqlCommand();
            Comando.Connection = Conexao;
            Comando.CommandText = "SELECT Show.ID, Show.Hora, Show.Data, Show.Titulo, Show.Descricao, Show.UsuarioID FROM Show WHERE Show.UsuarioID = @ID;";
            Comando.Parameters.AddWithValue("@ID", IDUsuario);

            SqlDataReader Leitor = Comando.ExecuteReader();

            List<Show> Shows = new List<Show>();

            while (Leitor.Read())
            {
                Show S = new Show();
                S.ID = (Int32)Leitor["ID"];
                S.Hora = Leitor["Hora"].ToString();
                S.Data = Leitor["Data"].ToString();
                S.Titulo = (String)Leitor["Titulo"];
                S.Descricao = (String)Leitor["Descricao"];

                Shows.Add(S);
            }

            if (!Leitor.HasRows)
            {
                Conexao.Close();
                return null;
            }

            Conexao.Close();

            return Shows;
        }


        public Boolean NovoEvento(Int32 IDUsuario, Int32 IDAgenda)
        {

            SqlConnection Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["SymphonyaBCD"].ConnectionString);
            Conexao.Open();

            //CRIACAO DO COMANDO SQL
            SqlCommand Comando = new SqlCommand();
            Comando.Connection = Conexao;
            Comando.CommandText = "INSERT INTO Show (Hora, Data, Titulo, Descricao, UsuarioID, AgendaID)"
              + "VALUES (@Hora, @Data, @Titulo, @Descricao, @UsuarioID, @AgendaID);";

            DateTime datahora = DateTime.Now;
            //String Hora = datahora.Day + "/" + datahora.Month + "/" + datahora.Year;
            //String Data = datahora.Day + "/" + datahora.Month + "/" + datahora.Year;

            Comando.Parameters.AddWithValue("@Hora", this.Hora);
            Comando.Parameters.AddWithValue("@Data", this.Data);
            Comando.Parameters.AddWithValue("@Titulo", this.Titulo);
            Comando.Parameters.AddWithValue("@Descricao", this.Descricao);
            Comando.Parameters.AddWithValue("@UsuarioID", IDUsuario);
            Comando.Parameters.AddWithValue("@AgendaID", IDAgenda);

            Int32 Resultado = Comando.ExecuteNonQuery();

            Conexao.Close();

            return Resultado > 0 ? true : false;
        }

        public Boolean Excluir()
        {
            SqlConnection Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["SymphonyaBCD"].ConnectionString);
            Conexao.Open();

            SqlCommand Comando = new SqlCommand();
            Comando.Connection = Conexao;
            Comando.CommandText = "DELETE FROM Show WHERE ID = @ID";
            Comando.Parameters.AddWithValue("@ID", this.ID);

            Int32 Resultado = Comando.ExecuteNonQuery();

            Conexao.Close();

            return Resultado > 0 ? true : false;
        }
    }
}