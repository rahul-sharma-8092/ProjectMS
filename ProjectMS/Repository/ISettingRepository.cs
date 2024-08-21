using ProjectMS.Models.Setting;

namespace ProjectMS.Repository
{
    public interface ISettingRepository
    {
        List<UserGroupsModel> GetAllUserGroups();
        UserGroupsModel GetUserGroupsById(int id);
        int AddUpdateUserGroups(UserGroupsModel groups);
        string DeleteUserGroups(int Id);
    }
}
