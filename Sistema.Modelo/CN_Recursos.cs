using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

using System.Net.Mail;
using System.Net;
using System.IO;

namespace Sistema.Modelo
{
    public class CN_Recursos
    {

        public static string GenerarClave()
        {
            string clave = Guid.NewGuid().ToString("N").Substring(0, 6);
            return clave;
        }


        // Encriptacion de texto en SHA256
        public static String ConvertirSha256(string texto)
        {
            StringBuilder Sb = new StringBuilder();

            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(texto));

                foreach (byte b in result)
                    Sb.Append(b.ToString("x2"));
            }
            return Sb.ToString();
        }

        public static bool EnviarCorreo(string correo, string asunto, string mensaje)
        {
            bool resultado = false;

            try
            {
                MailMessage msg = new MailMessage();
                msg.To.Add(correo);
                msg.From = new MailAddress("liliethjimenez27@gmail.com");
                msg.Subject = asunto;
                msg.Body = mensaje;
                msg.IsBodyHtml = true;


                var smtp = new SmtpClient()
                {
                    Credentials = new NetworkCredential("liliethjimenez27@gmail.com", "123DaRlInG456"),
                    Host = "smtp.gmail.com",
                    Port= 587,
                    EnableSsl = true
                };

                smtp.Send(msg);
                resultado = true;

            }
            catch
            {

                resultado = false;

            }

            return resultado;
        }

    }
}
