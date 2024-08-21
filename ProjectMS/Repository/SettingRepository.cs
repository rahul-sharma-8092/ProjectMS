using Microsoft.Data.SqlClient;
using ProjectMS.Models.Setting;
using ProjectMS.Service;
using System.Data;

namespace ProjectMS.Repository
{
    public class SettingRepository : ISettingRepository
    {
        #region Constructor
        private readonly string connString = "";
        private readonly IEncryptionService encryptionService;
        private readonly ICommonFuncService commonFuncService;

        public SettingRepository(IConfiguration _configuration, IEncryptionService _encryptionService, ICommonFuncService _commonFuncService)
        {
            connString = _configuration["ConnectionStrings:dbConnection"] ?? "";
            encryptionService = _encryptionService;
            commonFuncService = _commonFuncService;
        }
        #endregion

        public int AddUpdateUserGroups(UserGroupsModel model)
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand("AddUpdateUserGroups", conn);

            try
            {
                conn.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@GroupId", model.GroupId);
                cmd.Parameters.AddWithValue("@GroupName", model.GroupName);
                cmd.Parameters.AddWithValue("@GroupDescription", model.Description);

                DataTable table = commonFuncService.ConvertListToDataTable(model.ModulePermissions);
                cmd.Parameters.AddWithValue("@ModulePermission", table);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        string? message = Convert.ToString(reader["Message"]);
                        return Convert.ToInt32(reader["retVal"]);
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
            return 0;
        }

        public string DeleteUserGroups(int Id)
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand("DeleteUserGroups", conn);

            try
            {
                conn.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@GroupId", Id);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        return Convert.ToString(reader["Message"]);
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
            return "";
        }

        public List<UserGroupsModel> GetAllUserGroups()
        {
            List<UserGroupsModel> ListGroups = new List<UserGroupsModel>();
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand("GetAllUserGroups", conn);

            try
            {
                conn.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        UserGroupsModel groups = new UserGroupsModel();
                        groups.GroupId = Convert.ToInt32(reader["GroupId"]);
                        groups.GId = encryptionService.Encryption(Convert.ToString(reader["GroupId"] ?? "") ?? "");
                        groups.GroupName = Convert.ToString(reader["GroupName"]);
                        groups.Description = Convert.ToString(reader["Description"]);
                        groups.IsDeleted = Convert.ToBoolean(reader["IsDeleted"]);
                        groups.NoOfUserAssociated = Convert.ToInt32(reader["NoOfUserAssociated"]);

                        ListGroups.Add(groups);
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
            return ListGroups;
        }

        public UserGroupsModel GetUserGroupsById(int id)
        {
            UserGroupsModel groups = new UserGroupsModel();
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand("GetUserGroupsById", conn);

            try
            {
                conn.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@GroupId", id);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        GroupPermissionModel permission = new GroupPermissionModel();
                        groups.GroupId = Convert.ToInt32(reader["GroupId"]);
                        groups.GroupName = Convert.ToString(reader["GroupName"]);
                        groups.Description = Convert.ToString(reader["Description"]);
                        groups.IsDeleted = Convert.ToBoolean(reader["IsDeleted"]);
                        groups.NoOfUserAssociated = Convert.ToInt32(reader["NoOfUserAssociated"]);

                        permission.GroupPermissionId = Convert.ToInt32(reader["GroupPermissionId"]);
                        permission.ModuleId = Convert.ToInt32(reader["ModuleId"]);
                        permission.ModuleName = Convert.ToString(reader["ModuleName"]);
                        permission.Permission = Convert.ToInt32(reader["Permission"]);
                        permission.Description = Convert.ToString(reader["Description"]);

                        groups.ModulePermissions.Add(permission);
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
            return groups;
        }
    }
}
