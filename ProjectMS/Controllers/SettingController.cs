using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectMS.Common;
using ProjectMS.Models.Setting;
using ProjectMS.Repository;

namespace ProjectMS.Controllers
{
    [Authorize]
    public class SettingController : Controller
    {
        private readonly ISetting setting;
        public SettingController(IConfiguration _configuration)
        {
            setting = new SettingRepository(_configuration);
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult UserGroups()
        {
            List<UserGroupsModel> listGroups = setting.GetAllUserGroups();

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
                groupPermission = setting.GetUserGroupsById(0);
            }
            else
            {   
                ViewBag.PageMode = "Edit";
                groupPermission = setting.GetUserGroupsById(Convert.ToInt32(Encryptions.Decryption(Id)));
            }

            return View(groupPermission);
        }

        [HttpPost]
        public IActionResult AddEditUserGroups(UserGroupsModel groupPermission)
        {
            int GroupId = setting.AddUpdateUserGroups(groupPermission);
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
            Id = string.IsNullOrEmpty(Id) ? string.Empty : Encryptions.Decryption(Id);
            string GroupName = setting.DeleteUserGroups(Convert.ToInt32(Id));
            if (!string.IsNullOrEmpty(GroupName))
            {
                //User-groups deleted.
                TempData["Success"] = "User Groups Deleted.";
            }
            return RedirectToAction("UserGroups");
        }
    }
}
