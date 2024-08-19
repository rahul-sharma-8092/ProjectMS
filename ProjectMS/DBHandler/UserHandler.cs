using Microsoft.Data.SqlClient;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using ProjectMS.Models;
using System.Data;

namespace ProjectMS.DBHandler
{
    public class UserHandler
    {
        private readonly string connString = "";

        public UserHandler(IConfiguration _configuration)
        {
            connString = _configuration["ConnectionStrings:dbConnection"] ?? "";
        }


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

        public Users GetUserDetailbyEmailID(string email)
        {
            Users users = new Users();
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand("GetUserDetailbyEmailID", conn);

            try
            {
                conn.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@email", email);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        users.UserID = Convert.ToInt64(reader["UserID"]);
                        users.FirstName = Convert.ToString(reader["FirstName"]);
                        users.LastName = Convert.ToString(reader["LastName"]);
                        users.EmailID = Convert.ToString(reader["Email"]);
                        users.Password = Convert.ToString(reader["Password"]);
                        users.PhoneNo = Convert.ToString(reader["Phone"]);
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
            return users;
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
