using ProjectMS.Models.Setting;
using ProjectMS.Repository;

namespace ProjectMS.Service
{
    public class SettingService : ISettingService
    {
        #region Constructor
        private readonly ISettingRepository settingRepository;
        public SettingService(ISettingRepository _settingRepository)
        {
            settingRepository = _settingRepository;
        }
        #endregion

        public int AddUpdateUserGroups(UserGroupsModel groups)
        {
            return settingRepository.AddUpdateUserGroups(groups);
        }

        public string DeleteUserGroups(int Id)
        {
            return settingRepository.DeleteUserGroups(Id);
        }

        public List<UserGroupsModel> GetAllUserGroups()
        {
            return settingRepository.GetAllUserGroups();
        }

        public UserGroupsModel GetUserGroupsById(int id)
        {
            return settingRepository.GetUserGroupsById(id);
        }
    }
}
