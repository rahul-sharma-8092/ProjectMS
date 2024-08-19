namespace ProjectMS.Models.Setting
{
    public class UserGroupsModel
    {
        public int GroupId { get; set; }
        public string? GroupName { get; set; }
        public string? Description { get; set; }
        public bool IsDeleted { get; set; }
        public int NoOfUserAssociated { get; set; }
        public string? GId { get; set; }
        public List<GroupPermissionModel>? ModulePermissions { get; set; } = new List<GroupPermissionModel>();
    }

    public class GroupPermissionModel
    {
        public int GroupPermissionId { get; set; }
        public int GroupId { get; set; }
        public int ModuleId { get; set; }
        public string? ModuleName { get; set; }
        public int Permission { get; set; }
        public string? Description { get; set; }
        public bool IsDeleted { get; set; }
    }
}
