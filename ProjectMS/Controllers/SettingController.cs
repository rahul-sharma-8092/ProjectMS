using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectMS.Models.Setting;
using ProjectMS.Service;

namespace ProjectMS.Controllers
{
    [Authorize]
    public class SettingController : Controller
    {
        #region Constructor
        private readonly ISettingService settingService;
        private readonly IEncryptionService encryptionService;

        public SettingController(ISettingService _settingService, IEncryptionService _encryptionService)
        {
            settingService = _settingService;
            encryptionService = _encryptionService;
        }
        #endregion

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult UserGroups()
        {
            List<UserGroupsModel> listGroups = settingService.GetAllUserGroups();

            List<SelectListItem> action = new List<SelectListItem>();
            action.Add(new SelectListItem { Text = "Edit", Value = "Edit" });
            action.Add(new SelectListItem { Text = "Delete", Value = "Delete" });

            ViewBag.ddlAction = action;

            return View(listGroups);
        }

        [HttpGet]
        public IActionResult AddEditUserGroups(string? Id)
        {
            UserGroupsModel groupPermission = new UserGroupsModel();
            if (string.IsNullOrEmpty(Id))
            {
                ViewBag.PageMode = "Add";
                groupPermission = settingService.GetUserGroupsById(0);
            }
            else
            {   
                ViewBag.PageMode = "Edit";
                groupPermission = settingService.GetUserGroupsById(Convert.ToInt32(encryptionService.Decryption(Id)));
            }

            return View(groupPermission);
        }

        [HttpPost]
        public IActionResult AddEditUserGroups(UserGroupsModel groupPermission)
        {
            int GroupId = settingService.AddUpdateUserGroups(groupPermission);
            if (GroupId > 0 && groupPermission.GroupId == 0)
            {
                //User Added
                TempData["Success"] = "User Groups Added.";

                return RedirectToAction("UserGroups");
            }
            else if (GroupId > 0 && groupPermission.GroupId > 0)
            {
                //User Edited
                TempData["Success"] = "User Groups Edited.";

                return RedirectToAction("UserGroups");
            }
            TempData["Error"] = "Some thing went wrong.";
            return View(groupPermission);
        }

        public IActionResult DeleteGroups(string? Id)
        {
            Id = string.IsNullOrEmpty(Id) ? string.Empty : encryptionService.Decryption(Id);
            string GroupName = settingService.DeleteUserGroups(Convert.ToInt32(Id));
            if (!string.IsNullOrEmpty(GroupName))
            {
                //User-groups deleted.
                TempData["Success"] = "User Groups Deleted.";
            }
            return RedirectToAction("UserGroups");
        }
    }
}
