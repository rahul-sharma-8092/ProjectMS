using Microsoft.Data.SqlClient;
using ProjectMS.Models;
using System.Data;

namespace ProjectMS.Repository
{
    public class AccountRepository : IAccountRepository
    {
        #region Constructor
        private readonly string connString = "";

        public AccountRepository(IConfiguration _configuration)
        {
            connString = _configuration["ConnectionStrings:dbConnection"] ?? "";
        }
        #endregion

        public bool CheckEmailExists(string email)
        {
            bool IsEmailExists = false;
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand("CheckEmailExists", conn);
            try
            {
                conn.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@email", email);
                SqlDataReader reader = cmd.ExecuteReader();
                IsEmailExists = reader.HasRows;
            }
            catch (Exception ex)
            {
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
            }
            return IsEmailExists;
        }

        public ForgotPasswordModel SaveForgotPassToken(ForgotPasswordModel model)
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand("SaveForgotPassToken", conn);

            try
            {
                conn.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Token", model.Token);
                cmd.Parameters.AddWithValue("@Email", model.Email);
                cmd.Parameters.AddWithValue("@IPAddress", model.IPAddress);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        model.Token = Convert.ToString(reader["Token"]);
                        model.FullName = Convert.ToString(reader["FullName"]);
                    }
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
            }
            return model;
        }

        public ResetPasswordModel GetForgotPassDetailByToken(string token)
        {
            ResetPasswordModel model = new ResetPasswordModel();
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand("GetForgotPassDetailByToken", conn);

            try
            {
                conn.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Token", token);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        model.Token = Convert.ToString(reader["Token"]);
                        model.Email = Convert.ToString(reader["Email"]);
                        model.FullName = Convert.ToString(reader["FullName"]);
                    }
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
            }
            return model;
        }

        public ResetPasswordModel SetResetPassword(ResetPasswordModel model)
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand("SetResetPassword", conn);

            try
            {
                conn.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Email", model.Email);
                cmd.Parameters.AddWithValue("@Password", model.Password);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        model.Email = Convert.ToString(reader["Email"]);
                        model.FullName = Convert.ToString(reader["FullName"]);
                    }
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
            }
            return model;
        }
    }
}
