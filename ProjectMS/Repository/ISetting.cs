using ProjectMS.Models.Setting;

namespace ProjectMS.Repository
{
    public interface ISetting
    {
        public List<UserGroupsModel> GetAllUserGroups();
        public UserGroupsModel GetUserGroupsById(int id);
        public int AddUpdateUserGroups(UserGroupsModel groups);
        public string DeleteUserGroups(int Id);
    }
}
