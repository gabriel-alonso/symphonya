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

        public Show(Int32 ID )
        {
            SqlConnection Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["Symphonya"].ConnectionString);
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
            this.Hora = (String)Leitor["Hora"];
            this.Data = (String)Leitor["Data"];
            this.Descricao = (String)Leitor["Descricao"];

            Conexao.Close();
        }
        public Show(Int32 AgendaID, Int32 UsuarioID)
        {
            SqlConnection Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["Symphonya"].ConnectionString);
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
            SqlConnection Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["Symphonya"].ConnectionString);
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
                A.Data = (String)Leitor["Data"];
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

        public Boolean NovoEvento(Int32 IDEvento)
        {

            SqlConnection Conexao = new SqlConnection("Server=ESN509VMSSQL;Database=Symphonya;User Id=Aluno;Password=Senai1234;");
            Conexao.Open();

            //CRIACAO DO COMANDO SQL
            SqlCommand Comando = new SqlCommand();
            Comando.Connection = Conexao;
            Comando.CommandText = "INSERT INTO Show (Hora ,Data,Titulo,Descricao, UsuarioID, AgendaID, IDEvento)"
              + "VALUES (@Hora,@Data,@Titulo,@Descricao,@UsuarioID,@AgendaID);";

            DateTime datahora = DateTime.Now;
            //String Hora = datahora.Day + "/" + datahora.Month + "/" + datahora.Year;
            //String Data = datahora.Day + "/" + datahora.Month + "/" + datahora.Year;

            Comando.Parameters.AddWithValue("@Hora", this.Hora);
            Comando.Parameters.AddWithValue("@Data", this.Data);
            Comando.Parameters.AddWithValue("@Titulo", this.Titulo);
            Comando.Parameters.AddWithValue("@Descricao", this.Descricao);
            Comando.Parameters.AddWithValue("@UsuarioID", UsuarioID);
            Comando.Parameters.AddWithValue("@AgendaID", AgendaID);

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