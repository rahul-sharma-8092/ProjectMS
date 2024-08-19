function ddlActionChnage(dropdown) {
    let selectedAction = $(dropdown).val().toLowerCase();
    let groupId = $(dropdown).closest('tr').find('.group-id')[0].innerText.trim();

    groupId = encodeURIComponent(groupId);

    if (selectedAction === "edit") {
        window.location.href = "/Setting/AddEditUserGroups/?id=" + groupId;
    }
    else if (selectedAction === "delete") {
        window.location.href = "/Setting/DeleteGroups/?id=" + groupId;
    }
}