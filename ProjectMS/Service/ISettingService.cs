using ProjectMS.Models.Setting;

namespace ProjectMS.Service
{
    public interface ISettingService
    {
        List<UserGroupsModel> GetAllUserGroups();
        UserGroupsModel GetUserGroupsById(int id);
        int AddUpdateUserGroups(UserGroupsModel groups);
        string DeleteUserGroups(int Id);
    }
}
