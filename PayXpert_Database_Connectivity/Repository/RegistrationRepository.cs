using PayXpert_Database_Connectivity.Utility;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayXpert_Database_Connectivity.Repository
{
    public class RegistrationRepository
    {
        public bool RegisterToSystem(string userName, string password, string email)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(DBUtil.GetConnectionString()))
                {
                    string query = @"INSERT INTO Users (userName, login_password, email, registration_date)
                                 VALUES (@UserName, @LoginPassword, @Email, CURRENT_TIMESTAMP);";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserName", userName);
                        command.Parameters.AddWithValue("@LoginPassword", password);
                        command.Parameters.AddWithValue("@Email", email);

                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();

                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to Register " + ex.Message);
                return false;
            }
        }
        public bool AuthenticateUser(string email, string password)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(DBUtil.GetConnectionString()))
                {
                    string query = @"SELECT COUNT(*) FROM Users WHERE email = @Email AND login_password = @Password;";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@Password", password);

                        connection.Open();
                        int count = (int)command.ExecuteScalar();

                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("User Not Found: " + ex.Message);
                return false;
            }
        }
    }

}
