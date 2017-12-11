using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace Symphonya_RedeSocial.Models
{
    public class Email
    {
        public bool EmailRecuperaracao(string email, string senha)
        {
            try
            {
                MailMessage mailMessage = new MailMessage();

                //Endereço que irá aparecer no e-mail do usuário
                mailMessage.From = new MailAddress("symphonyaadm@gmail.com");

                String destinatario = email;

                //@Html.ActionLink('Clique aqui para fazer o log in', 'Login', 'Account', routeValues: null, htmlAttributes: new { id = 'loginLink' })" + senha);
                String corpo = ("Sua nova senha é: " + senha+
                                "   Por motivos de segurança sua senha vem criptografada. Para descriptografa-la use um SHA1 decrypter: https://hashkiller.co.uk/sha1-decrypter.aspx");
                //destinatarios do e-mail, para incluir mais de um basta separar por ponto e virgula    
                mailMessage.To.Add(destinatario);
                //Com a passagem do dia e mês no Titulo do e-mail, todos os e-mails que fora recebidos no mesmo dia será agrupados em um único espaço no mailbox.
                mailMessage.Subject = "Symphonya - Recuperação de Senha";
                mailMessage.IsBodyHtml = true;

                //conteudo do corpo do e-mail
                mailMessage.Body = corpo.ToString();
                mailMessage.Priority = MailPriority.High;

                //smtp do e-mail que irá enviar
                // Do Outlook = smtp - mail.outlook.com
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");

                smtpClient.EnableSsl = true;
                smtpClient.Port = 587;

                //credenciais da conta que utilizará para enviar o e-mail
                smtpClient.Credentials = new NetworkCredential("symphonyaadm@gmail.com", "Symphonya1234!");

                smtpClient.Send(mailMessage);
                return true;

            }
            catch (Exception e)
            {
                String res = e.ToString();
                return false;
            }
        }
    }
}