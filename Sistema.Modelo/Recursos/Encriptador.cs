using System;
using System.Security.Cryptography;

namespace Sistema.Modelo.Recursos
{
    public class Encriptador
    {
        public static string Encriptar(string password)
        {
            // Generar una sal aleatoria
            byte[] salt = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }

            // Derivar una clave usando PBKDF2
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000))
            {
                byte[] hash = pbkdf2.GetBytes(20);

                // Combinar la sal y el hash en un solo arreglo de bytes
                byte[] hashBytes = new byte[36];
                Array.Copy(salt, 0, hashBytes, 0, 16);
                Array.Copy(hash, 0, hashBytes, 16, 20);

                // Convertir a Base64 para almacenamiento
                return Convert.ToBase64String(hashBytes);
            }
        }

        public static bool Verificar(string password, string storedHash)
        {
            // Extraer los bytes de la sal y el hash almacenado
            byte[] hashBytes = Convert.FromBase64String(storedHash);
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);

            // Derivar la clave con la sal original
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000))
            {
                byte[] hash = pbkdf2.GetBytes(20);

                // Comparar byte a byte los hashes
                for (int i = 0; i < 20; i++)
                {
                    if (hashBytes[i + 16] != hash[i])
                    {
                        return false;
                    }
                }

                return true;
            }
        }
    }
}
