using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Data.SqlClient;

namespace FMS
{
    public class Utilities
    {
        public static string hashPassword(string password)
        {
            SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();

            byte[] password_bytes = Encoding.ASCII.GetBytes(password);
            byte[] encrypted_bytes = sha1.ComputeHash(password_bytes);
            return Convert.ToBase64String(encrypted_bytes);
        }

        public static void createAdmin(string password)
        {
            SqlConnection con = new SqlConnection(Properties.Resources.connectionString);
            SqlCommand command = con.CreateCommand();

            command.CommandText = "INSERT INTO [users] (user_username, user_password) VALUES (@username, @password)";
            command.Parameters.AddWithValue("@username", "admin");
            command.Parameters.AddWithValue("@password", hashPassword(password));

            con.Open();

            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {

            }

            con.Close();
        }
    }
}
