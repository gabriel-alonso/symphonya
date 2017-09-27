using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Symphonya_RedeSocial.Models
{
    public class Usuario
    {
        public Int32 ID { get; set; }
        public String Nome { get; set; }
        public String Sobrenome { get; set; }
        public int MesNascimento { get; set; }
        public Boolean Sexo { get; set; }
        public String Imagem_Perfil { get; set; }
        public String Email { get; set; }
        public String Senha { get; set; }
        public String Cidade { get; set; }
        public Int32 Estado { get; set; }
        public int Avaliacao { get; set; }
        public Boolean Modo { get; set; }
        public int DiaNascimento { get; set; }
        public int AnoNascimento { get; set; }

        public Usuario() { }

        public Usuario(Int32 ID)
        {
            SqlConnection Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["Symphonya"].ConnectionString);
            Conexao.Open();

            SqlCommand Comando = new SqlCommand();
            Comando.Connection = Conexao;
            Comando.CommandText = "SELECT * FROM Usuario WHERE ID=@ID;";
            Comando.Parameters.AddWithValue("@ID", ID);

            SqlDataReader Leitor = Comando.ExecuteReader();

            Leitor.Read();

            this.ID = (Int32)Leitor["ID"];
            this.Nome = (String)Leitor["Nome"];
            this.Sobrenome = (String)Leitor["Sobrenome"];
            this.MesNascimento = (Int32)Leitor["MesNascimento"];
            this.DiaNascimento = (Int32)Leitor["DiaNascimento"];
            this.AnoNascimento = (Int32)Leitor["AnoNascimento"];
            this.Sexo = (Boolean)Leitor["Sexo"];
            this.Imagem_Perfil = (String)Leitor["Imagem_Perfil"];
            this.Email = (String)Leitor["Email"];
            this.Senha = (String)Leitor["Senha"];
            this.Cidade = (String)Leitor["Cidade"];
            this.Estado = (Int32)Leitor["Estado"];
            this.Avaliacao = (Int32)Leitor["Avaliacao"];
            this.Modo = (Boolean)Leitor["Modo"];

            Conexao.Close();
        }
        public Usuario(String Email, String Senha)
        {
            SqlConnection Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["Symphonya"].ConnectionString);
            Conexao.Open();

            //CRIACAO DO COMANDO SQL
            SqlCommand Comando = new SqlCommand();
            Comando.Connection = Conexao;
            Comando.CommandText = "SELECT * FROM Usuario WHERE Email=@Email AND Senha=@Senha;";
            Comando.Parameters.AddWithValue("@Email", Email);
            Comando.Parameters.AddWithValue("@Senha", Senha);

            SqlDataReader Leitor = Comando.ExecuteReader();

            //LEITURA DO RESULTADO DO COMANDO
            Leitor.Read();

            //COLETA DE DADOS -> USUARIO
            this.ID = (Int32)Leitor["ID"];
            this.Nome = (String)Leitor["Nome"];
            this.Sobrenome = (String)Leitor["Sobrenome"];
            this.MesNascimento = (Int32)Leitor["MesNascimento"];
            this.DiaNascimento = (Int32)Leitor["DiaNascimento"];
            this.AnoNascimento = (Int32)Leitor["AnoNascimento"];
            this.Sexo = (Boolean)Leitor["Sexo"];
            this.Imagem_Perfil = (String)Leitor["Imagem_Perfil"];
            this.Email = (String)Leitor["Email"];
            this.Senha = (String)Leitor["Senha"];
            this.Cidade = (String)Leitor["Cidade"];
            this.Estado = (Int32)Leitor["Estado"];
            //this.Avaliacao = (Int32)Leitor["Avaliacao"];
            //this.Modo = (Boolean)Leitor["Modo"];


            Conexao.Close();
        }
        public Boolean NovoUser()
        {

            SqlConnection Conexao = new SqlConnection("Server=ESN509VMSSQL;Database=Symphonya;User Id=Aluno;Password=Senai1234;");
            Conexao.Open();

            //CRIACAO DO COMANDO SQL
            SqlCommand Comando = new SqlCommand();
            Comando.Connection = Conexao;
            Comando.CommandText = "INSERT INTO Usuario ( Nome , Sobrenome, DiaNascimento , MesNascimento , AnoNascimento , Email, Senha, Cidade, Estado, Imagem_Perfil, Sexo, Modo, Avaliacao)"
              + "VALUES ( @Nome , @Sobrenome, @DiaNascimento, @MesNascimento, @AnoNascimento, @Email, @Senha, @Cidade, @Estado, @Imagem_Perfil, @Sexo, @Modo, @Avaliacao);";
            Comando.Parameters.AddWithValue("@Nome", this.Nome);
            Comando.Parameters.AddWithValue("@Sobrenome", this.Sobrenome);
            Comando.Parameters.AddWithValue("@DiaNascimento", this.DiaNascimento);
            Comando.Parameters.AddWithValue("@MesNascimento", this.MesNascimento);
            Comando.Parameters.AddWithValue("@AnoNascimento", this.AnoNascimento);
            Comando.Parameters.AddWithValue("@Email", this.Email);
            Comando.Parameters.AddWithValue("@Senha", this.Senha);
            Comando.Parameters.AddWithValue("@Cidade", this.Cidade);
            Comando.Parameters.AddWithValue("@Estado", this.Estado);
            Comando.Parameters.AddWithValue("@Sexo", this.Sexo);
            Comando.Parameters.AddWithValue("@Modo", 0);
            Comando.Parameters.AddWithValue("@Avaliacao", 0);
            Comando.Parameters.AddWithValue("@Imagem_Perfil", this.Imagem_Perfil);

            Int32 Resultado = Comando.ExecuteNonQuery();

            Conexao.Close();

            return Resultado > 0 ? true : false;
        }
        public Boolean NovaBio()
        {
            SqlConnection Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["KatiauBD"].ConnectionString);
            Conexao.Open();


            SqlCommand Comando = new SqlCommand();
            Comando.Connection = Conexao;
            Comando.CommandText = "UPDATE Usuario SET BioU = @Bio , NickU = @Nick, ImagemU = @Imagem WHERE ID = @ID;";
            Comando.Parameters.AddWithValue("@ID", this.ID);
            //Comando.Parameters.AddWithValue("@Nick", this.Nick);
            //.Parameters.AddWithValue("@Bio", this.Bio);
            //.Parameters.AddWithValue("@Imagem", this.ImagemPerfil);

            Int32 Resultado = Comando.ExecuteNonQuery();

            Conexao.Close();

            return Resultado > 0 ? true : false;
        }

        public Boolean Novo()
        {
            SqlConnection Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["KatiauBD"].ConnectionString);
            Conexao.Open();

            SqlCommand Comando = new SqlCommand();
            Comando.Connection = Conexao;
            Comando.CommandText = "INSERT INTO Usuario (EmailU, NickU, NomeU, SobrenomeU, SenhaU, NascimentoU, BioU, ImagemU, Administrador)"
              + "VALUES (@Email, @NickU, @Nome, @Sobrenome, @Senha, @Nascimento, @Bio, @ImagemPerfil, @Adm);";
            Comando.Parameters.AddWithValue("@Email", this.Email);
           // Comando.Parameters.AddWithValue("@NickU", this.Nick);
            Comando.Parameters.AddWithValue("@Nome", this.Nome);
            Comando.Parameters.AddWithValue("@Sobrenome", this.Sobrenome);
            Comando.Parameters.AddWithValue("@Senha", this.Senha);
            Comando.Parameters.AddWithValue("@Nascimento", this.MesNascimento);
            Comando.Parameters.AddWithValue("@Bio", "Biografia");
            Comando.Parameters.AddWithValue("@ImagemPerfil", "tr4.jpg");
            Comando.Parameters.AddWithValue("@Adm", 0);



            Int32 Resultado = Comando.ExecuteNonQuery();

            Conexao.Close();

            return Resultado > 0 ? true : false;
        }

        public static List<Usuario> ListarU()
        {
            SqlConnection Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["KatiauBD"].ConnectionString);
            Conexao.Open();

            SqlCommand Comando = new SqlCommand();
            Comando.Connection = Conexao;
            Comando.CommandText = "SELECT * FROM Usuario;";

            SqlDataReader Leitor = Comando.ExecuteReader();

            List<Usuario> Users = new List<Usuario>();
            while (Leitor.Read())
            {
                Usuario U = new Usuario();
                U.ID = (Int32)Leitor["ID"];
                U.Nome = ((String)Leitor["NomeU"]);
              //  U.Nick = Leitor["NickU"].ToString();
                U.Sobrenome = (String)Leitor["SobrenomeU"];
                U.Email = ((String)Leitor["EmailU"]);
                U.Senha = (String)Leitor["SenhaU"];
              //  U.Nascimento = (String)Leitor["NascimentoU"];
             //   U.Bio = (String)Leitor["BioU"];
                //U.ImagemPerfil = (String)Leitor["ImagemU"];
               // U.Adm = (Boolean)Leitor["Administrador"];


                Users.Add(U);
            }

            Conexao.Close();

            return Users;
        }


        public Boolean Apagar()
        {
            SqlConnection Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["Symphonya"].ConnectionString);
            Conexao.Open();

            SqlCommand Comando = new SqlCommand();
            Comando.Connection = Conexao;
            Comando.CommandText = "DELETE FROM Usuario WHERE ID = @ID;";
            Comando.Parameters.AddWithValue("@ID", this.ID);

            Int32 Resultado = Comando.ExecuteNonQuery();



            return Resultado > 0 ? true : false;
        }

        public static Boolean Autenticar(String Email, String Senha)
        {
            SqlConnection Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["Symphonya"].ConnectionString);

            Conexao.Open();

            SqlCommand Comando = new SqlCommand();
            Comando.Connection = Conexao;
            Comando.CommandText = "SELECT ID FROM Usuario WHERE Email=@Email AND Senha=@Senha;";
            Comando.Parameters.AddWithValue("@Email", Email);
            Comando.Parameters.AddWithValue("@Senha", Senha);

            SqlDataReader Leitor = Comando.ExecuteReader();

            Leitor.Read();

            //VERIFICA SE O LEITOR TROUXE RESULTADOS
            if (!Leitor.HasRows)
            {
                //FECHA CONEXAO COM O BANCO
                Conexao.Close();
                return false;
            }

            //FECHA CONEXAO COM O BANCO
            Conexao.Close();
            return true;
        }
    }
}