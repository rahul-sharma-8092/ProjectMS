using ProjectMS.DBHandler;
using ProjectMS.Models.Setting;

namespace ProjectMS.Repository
{
    public class SettingRepository : ISetting
    {
        private readonly IConfiguration configuration;
        private readonly SettingHandler settingHandler;
        public SettingRepository(IConfiguration _configuration)
        {
            configuration = _configuration;
            settingHandler = new SettingHandler(configuration);
        }

        int ISetting.AddUpdateUserGroups(UserGroupsModel groups)
        {
            return settingHandler.AddUpdateUserGroups(groups);
        }

        string ISetting.DeleteUserGroups(int Id)
        {
            return settingHandler.DeleteUserGroups(Id);
        }

        List<UserGroupsModel> ISetting.GetAllUserGroups()
        {
            List<UserGroupsModel> listGroups = settingHandler.GetAllUserGroups();

            return listGroups;
        }

        UserGroupsModel ISetting.GetUserGroupsById(int id)
        {
           return settingHandler.GetUserGroupsById(id);
        }
    }
}
