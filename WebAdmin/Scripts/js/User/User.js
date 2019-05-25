$(function () {

    $("#ui_user_dg").datagrid({
        url: "/User/GetAllUserInfo",
        striped: true, rownumbers: true, pagination: true, pageSize: 20,
        idField: 'Id',
        sortName: 'UpdateTime',
        sortOrder: 'desc',
        pageList: [20, 40, 60, 80, 100],
        frozenColumns: [[
                         { field: 'ck', checkbox: true },
                         { width: 100, title: '登录名', field: 'AccountName' },
                         { width: 100, title: '用户名称', field: 'UserName' },
                         {
                             field: 'Role', title: '角色', width: 200,
                             formatter: function (value, row, index) {
                                 if (value != null) {
                                     return value.length > 12 ? '<span title="' + value + '">' + value + '</span>' : value;
                                 }
                             }
                         },
                        {
                            field: 'Department', title: '部门', width: 150,
                            formatter: function (value, row, index) {
                                if (value != null) {
                                    return value.length > 12 ? '<span title="' + value + '">' + value + '</span>' : value;
                                }
                            }
                        }
        ]],
        columns: [[
                   { field: 'MobilePhone', title: '联系人手机', width: 100 },
                   { field: 'Email', title: '邮箱', width: 150 },
                   {
                       field: 'IsAble', title: '启用', width: 40, align: 'center',
                       formatter: function (value, row, index) {
                           return value ? '<img src="../../Content/themes/icon/chk_checked.gif" alt="已启用" title="用户已启用" />' : '<img src="../../Content/themes/icon/chk_unchecked.gif" alt="未启用" title="用户未启用" />';
                       }
                   },
                    {
                        field: 'IsEdit', title: '改密', width: 40, align: 'center',
                        formatter: function (value, row, index) {
                            return value ? '<img src="../../Content/themes/icon/chk_checked.gif" alt="已改密" title="用户已改密" />' : '<img src="../../Content/themes/icon/chk_unchecked.gif" alt="未改密" title="用户未改密" />';
                        }
                    },
                   { field: 'UpdateTime', title: '最后更新时间', formatter: ChangeDateFormat, sortable: true, width: 130 },
                   { field: 'UpdateBy', title: '最后更新人', width: 130 }
        ]],
        toolbar: [{
            text: '添加',
            iconCls: 'icon-add',
            handler: AddUser
        }, {
            text: '删除',
            iconCls: 'icon-cut',
            handler: function () { }
        }, '-', {
            text: '修改',
            iconCls: 'icon-save',
            handler: UpdateUser
        },
        {
            text: '角色设置',
            iconCls: 'icon-user_key',
            handler: SetUserRole
        }, '-', {
            text: '部门设置',
            iconCls: 'icon-group',
            handler: SetUserDepartment
        }]
    });

});
//添加用户
function AddUser() {
    $("<div/>").dialog({
        id: "ui_user_add_dialog",
        href: "/User/CreatUser",
        title: "添加用户",
        height: 370,
        width: 610,
        modal: true,
        buttons: [{
            id: "ui_user_add_btn",
            text: "添加",
            handler: function () {
                $("#UserAddForm").form("submit", {
                    url: "/User/AddUser",
                    onSubmit: function (param) {
                        param.IsAble = $("#selUserIsableAdd").get(0).checked;
                        param.IsChangePwd = $("#selIfChangepwdAdd").get(0).checked;
                        if ($(this).form("validate")) {
                            $("#ui_user_add_btn").linkbutton('disable');
                            return true;
                        }
                        else {
                            $('#ui_user_add_btn').linkbutton('enable');   //恢复按钮
                            return false;
                        }
                    },
                    success: function (data) {
                        var result = eval('(' + data + ')');
                        if (result.Success) {
                            $("#ui_user_add_dialog").dialog('destroy');
                            $.show_alert("提示", result.Msg);
                            $("#ui_user_dg").datagrid("reload").datagrid('clearSelections').datagrid('clearChecked');
                        }
                        else {
                            $('#ui_user_add_btn').linkbutton('enable');
                            $.show_alert("提示", result.Msg);
                        }
                    }
                })
            }
        },
        {
            text: "取消",
            handler: function () {
                $("#ui_user_add_dialog").dialog('destroy');
            }
        }],
        onLoad: function () {
            $("#txtUserIDAdd").focus();
        },
        onClose: function () {
            $("#ui_user_add_dialog").dialog('destroy');  //销毁dialog对象
        }
    });
}

//修改用户
function UpdateUser() {
    var rows = $("#ui_user_dg").datagrid("getChecked");
    if (rows.length < 1) {
        $.show_alert("提示", "请先勾选要修改的用户");
        return;
    }
    if (rows.length > 1) {
        $.show_alert("提示", "不支持批量修改用户");
        $("#ui_user_dg").datagrid('clearSelections').datagrid('clearChecked');
        return;
    }
    $("<div/>").dialog({
        id: "ui_user_edit_dialog",
        title: "修改用户",
        href: "/User/UpdateUser",
        height: 370,
        width: 610,
        modal: true,
        buttons: [{
            id: "ui_user_edit_btn",
            text: "修 改",
            handler: function () {
                $("#UserUpdateForm").form("submit", {
                    url: "/User/UpdateUser",
                    onSubmit: function (param) {
                        param.IsAble = $("#selUserIsableEdit").get(0).checked;
                        param.IsChangePwd = $("#selIsChangepwdEdit").get(0).checked;
                        if ($(this).form("validate")) {
                            $("#ui_user_edit_btn").linkbutton("disable");
                            return true;
                        }
                        else {
                            $('#ui_user_edit_btn').linkbutton('enable');
                            return false;
                        }
                    },
                    success: function (data) {
                        var result = eval('(' + data + ')');
                        if (result.Success) {
                            $("#ui_user_edit_dialog").dialog("destroy");
                            $.show_alert("提示", result.Msg);
                            $("#ui_user_dg").datagrid("reload").datagrid('clearSelections').datagrid('clearChecked');
                        }
                        else {
                            $('#ui_user_edit_btn').linkbutton('enable');
                            $.show_alert("提示", result.Msg);
                        }
                    }
                });
            }
        }, {
            text: "取 消",
            handler: function () {
                $("#ui_user_edit_dialog").dialog("destroy");
            }
        }],
        onLoad: function () {
            var id = rows[0].Id;
            $.ajax({
                url: "/User/GetUserById",
                data: {
                    id
                    },
                        type: "Get",
                        dataType: "json",
                        success: function (data) {
                            if (data.Success) {
                                $("#hidid").val(data.Data.Id);
                                $("#txtUserIDEdit").val(data.Data.AccountName);
                                $("#txtAccountNameEdit").val(data.Data.AccountName);

                                $("#txtUserNameEdit").val(data.Data.RealName);
                                if (data.Data.IsAble) {
                                    $("#selUserIsableEdit").attr("checked", "checked");
                                }
                                if (data.Data.IsChangePwd) {
                                    $("#selIsChangepwdEdit").attr("checked", "checked");
                                }
                                $("#txtUserDescriptionEdit").val(data.Data.Description);
                                $("#txtMobilePhone").val(data.Data.MobilePhone);
                                $("#txtEmail").val(data.Data.Email);
                            } else {
                                $.show_alert("提示", data.Msg);
                            }
                        }
            });
        },
        onClose: function () {
            $("#ui_user_edit_dialog").dialog("destroy");
        }
    });
}

//角色设置(可批量设置)
function SetUserRole() {
    var rows = $("#ui_user_dg").datagrid("getChecked");
    if (rows.length < 1) {
        $.show_alert("提示", "请先勾选要设置的用户");
        return;
    }
    $("<div/>").dialog({
        id: "ui_user_setrole_dialog",
        href: "/User/UserRole",
        title: rows.length == 1 ? "设置角色" : "批量设置角色：" + rows.length + "个用户",
        height: 220,
        width: 380,
        modal: true,
        buttons: [{
            id: "ui_user_setrole_btn",
            text: "确定",
            handler: function () {
                $("#SetRoleForm").form("submit", {
                    url: "/User/UserRole",
                    onSubmit: function (param) {
                        $("#ui_user_setrole_btn").linkbutton("disable");
                        param.RoleIds = $("#comboxrole").combobox("getValues");
                    },
                    success: function (data) {
                        var result = eval('(' + data + ')');
                        if (result.Success) {
                            $("#ui_user_setrole_dialog").dialog("destroy");
                            $.show_alert("提示", result.Msg);
                            $("#ui_user_dg").datagrid("reload").datagrid("clearSelections").datagrid('clearChecked');
                        }
                        else {
                            $("#ui_user_setrole_btn").linkbutton("enable");
                            $.show_alert("提示", result.Msg);
                        }
                    }
                });
            }
        }],
        onLoad: function () {
            if (rows.length == 1) {
                $("#comboxrole").combobox("setValues", stringToList(rows[0].RoleId));
                $("#hiduserid").val(rows[0].Id);
                $("#txtusernameR").val(rows[0].UserName);
            }
            else {
                var userIds = "", userNames = "";
                for (var i = 0; i < rows.length; i++) {
                    userIds += rows[i].Id + ",";
                    userNames += rows[i].UserName + ','
                }
                $("#hiduserid").val(userIds.substring(0, userIds.length - 1));
                $("#txtusernameR").val(userNames.substring(0, userNames.length - 1));
            }
        },
        onClose: function () {
            $("#ui_user_setrole_dialog").dialog("destroy");
        }
    });
}
//设置部门(可批量设置)
function SetUserDepartment() {
    var rows = $("#ui_user_dg").datagrid("getChecked");
    if (rows.length < 1) {
        $.show_alert("提示", "请先勾选要设置的用户");
        return;
    }
    $("<div/>").dialog({
        id: "ui_user_setdepartment_dialog",
        href: "/User/UserDepartment",
        title: rows.length == 1 ? "设置部门" : "批量设置部门：" + rows.length + "个用户",
        height: 220,
        width: 380,
        modal: true,
        buttons: [{
            id: "ui_user_setdepartment_btn",
            text: "确定",
            handler: function () {
                $("#SetUserDeptForm").form("submit", {
                    url: "/User/UserDepartment",
                    onSubmit: function (param) {
                        $("#ui_user_setdepartment_btn").linkbutton("disable");
                        param.DepartmentIds = $("#treeDepartmentParentId").combotree("getValues").toString();
                    },
                    success: function (data) {
                        var result = eval('(' + data + ')');
                        if (result.Success) {
                            $("#ui_user_setdepartment_dialog").dialog("destroy");
                            $.show_alert("提示", result.Msg);
                            $("#ui_user_dg").datagrid("reload").datagrid("clearSelections").datagrid('clearChecked');
                        }
                        else {
                            $("#ui_user_setdepartment_btn").linkbutton("enable");
                            $.show_alert("提示", result.Msg);
                        }
                    }
                });
            }
        }],
        onLoad: function () {
            if (rows.length == 1) {
                $("#hidUserIdDept").val(rows[0].Id);
                $("#txtUserNameDept").val(rows[0].UserName);
                $("#treeDepartmentParentId").combotree("setValues", stringToList(rows[0].DepartmentId));
            }
            else {
                var userIds = "";
                var userNames = "";
                for (var i = 0; i < rows.length; i++)
                {
                    userIds += rows[i].Id + ",";
                    userNames += rows[i].UserName + ",";
                }
                $("#hidUserIdDept").val(userIds.substring(0,userIds.length-1));
                $("#txtUserNameDept").val(userNames.substring(0, userNames.length - 1));
            }
        },
        onClose: function () {
            $("#ui_user_setdepartment_dialog").dialog("destroy");
        }
    });
}
function ui_user_searchdata() {
    $("#ui_user_dg").datagrid('load', {
        LogName: $('#txtUserId').val(),
        UserName: $('#txtUserName').val(),
        IsUse: $('#selUserIsable').val(),
        IsEditPass: $('#selIfChangepwd').val(),
    });
    $("#ui_user_dg").datagrid('clearSelections').datagrid('clearChecked');
}

function ui_user_cleardata() {
    $('#ui_user_search input').val('');
    $('#ui_user_search select').val('select');
    $("#ui_user_dg").datagrid('load', {});

    $("#ui_user_dg").datagrid('clearSelections').datagrid('clearChecked');
}