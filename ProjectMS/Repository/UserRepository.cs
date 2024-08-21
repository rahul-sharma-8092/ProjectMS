using ProjectMS.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ProjectMS.Repository
{
    public class UserRepository : IUserRepository
    {
        #region Constructor
        private readonly string connString = "";

        public UserRepository(IConfiguration _configuration)
        {
            connString = _configuration["ConnectionStrings:dbConnection"] ?? "";
        }
        #endregion

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
    }
}
