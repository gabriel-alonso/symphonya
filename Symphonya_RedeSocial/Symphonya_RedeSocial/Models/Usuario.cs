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
        public String Imagem_Capa { get; set; }
        public String Email { get; set; }
        public String Senha { get; set; }
        public String Cidade { get; set; }
        public String Estado { get; set; }
        public int Avaliacao { get; set; }
        public Boolean Modo { get; set; }
        public int DiaNascimento { get; set; }
        public int AnoNascimento { get; set; }
        public String Telefone { get; set; }
        public string Biografia { get; set; }
        public Int32 Nivel { get; set; }
        public String Youtube { get; set; }
        public String Facebook { get; set; }
        public String Twitch { get; set; }

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
            this.Imagem_Capa = (String)Leitor["Imagem_Capa"];
            this.Email = (String)Leitor["Email"];
            this.Senha = (String)Leitor["Senha"];
            this.Cidade = (String)Leitor["Cidade"];
            this.Estado = (String)Leitor["Estado"];
            this.Avaliacao = (Int32)Leitor["Avaliacao"];
            this.Modo = (Boolean)Leitor["Modo"];
            this.Telefone = (String)Leitor["Telefone"];
            this.Biografia = (String)Leitor["Biografia"];
            this.Nivel = (Int32)Leitor["Nivel"];
            if (Leitor["Youtube"] != null)
            {
                this.Youtube = (String)Leitor["Youtube"];
            }
            if (Leitor["Facebook"] != null)
            {
                this.Facebook = (String)Leitor["Facebook"];
            }
            if (Leitor["Twitch"] != null)
            {
                this.Twitch = (String)Leitor["Twitch"];
            }

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
            this.Imagem_Capa = (String)Leitor["Imagem_Capa"];
            this.Email = (String)Leitor["Email"];
            this.Senha = (String)Leitor["Senha"];
            this.Cidade = (String)Leitor["Cidade"];
            this.Estado = (String)Leitor["Estado"];
            this.Avaliacao = (Int32)Leitor["Avaliacao"];
            this.Modo = (Boolean)Leitor["Modo"];
            this.Telefone = (String)Leitor["Telefone"];
            this.Biografia = (String)Leitor["Biografia"];
            this.Nivel = (Int32)Leitor["Nivel"];

            if(Leitor["Youtube"] != null)
            {
                this.Youtube = Leitor["Youtube"].ToString();
            }
            if (Leitor["Facebook"] != null)
            {
                this.Facebook = (String)Leitor["Facebook"].ToString();
            }
            if (Leitor["Twitch"] != null)
            {
                this.Twitch = (String)Leitor["Twitch"].ToString();
            }

            Conexao.Close();
        }
        public Boolean NovoUser()
        {
            SqlConnection Conexao = new SqlConnection("Server=ESN509VMSSQL;Database=Symphonya;User Id=Aluno;Password=Senai1234;");
            Conexao.Open();

            //CRIACAO DO COMANDO SQL
            SqlCommand Comando = new SqlCommand();
            Comando.Connection = Conexao;
            Comando.CommandText = "INSERT INTO Usuario ( Nome , Sobrenome, DiaNascimento , MesNascimento , AnoNascimento , Email, Senha, Cidade, Estado, Imagem_Perfil, Imagem_Capa, Sexo, Modo, Avaliacao, Telefone, Biografia, Nivel)"
              + "VALUES ( @Nome , @Sobrenome, @DiaNascimento, @MesNascimento, @AnoNascimento, @Email, @Senha, @Cidade, @Estado, @Imagem_Perfil, @Imagem_Capa, @Sexo, @Modo, @Avaliacao, @Telefone, @Biografia, @Nivel);";
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
            Comando.Parameters.AddWithValue("@Imagem_Perfil", "imagemPadrao.png");
            Comando.Parameters.AddWithValue("@Imagem_Capa", "fundoPerfil.jpg");
            Comando.Parameters.AddWithValue("@Telefone", this.Telefone);
            Comando.Parameters.AddWithValue("@Biografia", "Conte-nos algo sobre você!");
            Comando.Parameters.AddWithValue("@Nivel", 0);

            Int32 Resultado = Comando.ExecuteNonQuery();

            Conexao.Close();

            return Resultado > 0 ? true : false;
        }
        public Boolean Alterar()
        {
            SqlConnection Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["Symphonya"].ConnectionString);
            Conexao.Open();

            SqlCommand Comando = new SqlCommand();
            Comando.Connection = Conexao;
            Comando.CommandText = "UPDATE Usuario SET Biografia = @Biografia, Nome = @Nome, Sobrenome = @Sobrenome, Cidade = @Cidade, Estado = @Estado, Telefone = @Telefone, Imagem_Capa = @ImagemCapa, Imagem_Perfil = @ImagemPerfil, Youtube = @Youtube, Facebook = @Facebook, Twitch = @Twitch WHERE ID = @ID;";
            Comando.Parameters.AddWithValue("@ID", this.ID);
            Comando.Parameters.AddWithValue("@Nome", this.Nome);
            Comando.Parameters.AddWithValue("@Sobrenome", this.Sobrenome);
            Comando.Parameters.AddWithValue("@Cidade", this.Cidade);
            Comando.Parameters.AddWithValue("@Estado", this.Estado);
            Comando.Parameters.AddWithValue("@Telefone", this.Telefone);
            Comando.Parameters.AddWithValue("@ImagemPerfil", this.Imagem_Perfil);
            Comando.Parameters.AddWithValue("@ImagemCapa", this.Imagem_Capa);
            Comando.Parameters.AddWithValue("@Biografia", this.Biografia);
            Comando.Parameters.AddWithValue("@Youtube", this.Youtube);
            Comando.Parameters.AddWithValue("@Facebook", this.Facebook);
            Comando.Parameters.AddWithValue("@Twitch", this.Twitch);

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

        public static Int32 Contar()
        {
            SqlConnection Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["Symphonya"].ConnectionString);
            Conexao.Open();

            SqlCommand Comando = new SqlCommand();
            Comando.Connection = Conexao;
            Comando.CommandText = "SELECT COUNT (*) FROM Usuario;";

            SqlDataReader Leitor = Comando.ExecuteReader();
            Leitor.Read();

            Int32 Contador = (Int32)Leitor[""];

            if (!Leitor.HasRows)
            {
                Conexao.Close();
                return 0;
            }

            Conexao.Close();

            return Contador;
        }

        public static List<Usuario> Listar()
        {
            SqlConnection Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["Symphonya"].ConnectionString);
            Conexao.Open();

            SqlCommand Comando = new SqlCommand();
            Comando.Connection = Conexao;
            Comando.CommandText = "SELECT * FROM Usuario ORDER BY Nome;";

            SqlDataReader Leitor = Comando.ExecuteReader();

            List<Usuario> Users = new List<Usuario>();
            while (Leitor.Read())
            {
                Usuario U = new Usuario();
                U.ID = (Int32)Leitor["ID"];
                U.Nome = ((String)Leitor["Nome"]);
                U.Sobrenome = (String)Leitor["Sobrenome"];
                U.MesNascimento = (Int32)Leitor["MesNascimento"];
                U.DiaNascimento = (Int32)Leitor["DiaNascimento"];
                U.AnoNascimento = (Int32)Leitor["AnoNascimento"];
                U.Sexo = (Boolean)Leitor["Sexo"];
                U.Imagem_Perfil = (String)Leitor["Imagem_Perfil"];
                U.Imagem_Capa = (String)Leitor["Imagem_Capa"];
                U.Email = (String)Leitor["Email"];
                U.Senha = (String)Leitor["Senha"];
                U.Cidade = (String)Leitor["Cidade"];
                U.Estado = (String)Leitor["Estado"];
                U.Avaliacao = (Int32)Leitor["Avaliacao"];
                U.Modo = (Boolean)Leitor["Modo"];
                U.Telefone = (String)Leitor["Telefone"];
                U.Biografia = (String)Leitor["Biografia"];
                U.Nivel = (Int32)Leitor["Nivel"];
                U.Youtube = (String)Leitor["Youtube"];
                U.Facebook = (String)Leitor["Facebook"];
                U.Twitch = (String)Leitor["Twitch"];

                Users.Add(U);
            }

            if (!Leitor.HasRows)
            {
                Conexao.Close();
                return null;
            }

            Conexao.Close();

            return Users;
        }

        public static List<Usuario> Listar(String pesquisa)
        {
            SqlConnection Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["Symphonya"].ConnectionString);
            Conexao.Open();

            SqlCommand Comando = new SqlCommand();
            Comando.Connection = Conexao;
            Comando.CommandText = "SELECT * FROM Usuario WHERE Nome LIKE '"+@pesquisa+"%'";

            SqlDataReader Leitor = Comando.ExecuteReader();

            List<Usuario> Users = new List<Usuario>();
            while (Leitor.Read())
            {
                Usuario U = new Usuario();
                U.ID = (Int32)Leitor["ID"];
                U.Nome = ((String)Leitor["Nome"]);
                U.Sobrenome = (String)Leitor["Sobrenome"];
                U.MesNascimento = (Int32)Leitor["MesNascimento"];
                U.DiaNascimento = (Int32)Leitor["DiaNascimento"];
                U.AnoNascimento = (Int32)Leitor["AnoNascimento"];
                U.Sexo = (Boolean)Leitor["Sexo"];
                U.Imagem_Perfil = (String)Leitor["Imagem_Perfil"];
                U.Imagem_Capa = (String)Leitor["Imagem_Capa"];
                U.Email = (String)Leitor["Email"];
                U.Senha = (String)Leitor["Senha"];
                U.Cidade = (String)Leitor["Cidade"];
                U.Estado = (String)Leitor["Estado"];
                U.Avaliacao = (Int32)Leitor["Avaliacao"];
                U.Modo = (Boolean)Leitor["Modo"];
                U.Telefone = (String)Leitor["Telefone"];
                U.Biografia = (String)Leitor["Biografia"];
                U.Nivel = (Int32)Leitor["Nivel"];
                U.Youtube = (String)Leitor["Youtube"];
                U.Facebook = (String)Leitor["Facebook"];
                U.Twitch = (String)Leitor["Twitch"];

                Users.Add(U);
            }

            if (!Leitor.HasRows)
            {
                Conexao.Close();
                return null;
            }

            Conexao.Close();
                
            return Users;
        }


        public static Boolean Desativar(Int32 ID)
        {
            SqlConnection Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["Symphonya"].ConnectionString);
            Conexao.Open();

            SqlCommand Comando = new SqlCommand();
            Comando.Connection = Conexao;
            Comando.CommandText = "UPDATE Usuario SET Modo = 1 WHERE Usuario.ID = @ID";
            Comando.Parameters.AddWithValue("@ID", ID);

            Int32 Resultado = Comando.ExecuteNonQuery();

            return Resultado > 0 ? true : false;
        }

        public static Boolean Ativar(Int32 ID)
        {
            SqlConnection Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["Symphonya"].ConnectionString);
            Conexao.Open();

            SqlCommand Comando = new SqlCommand();
            Comando.Connection = Conexao;
            Comando.CommandText = "UPDATE Usuario SET Modo = 0 WHERE Usuario.ID = @ID";
            Comando.Parameters.AddWithValue("@ID", ID);

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