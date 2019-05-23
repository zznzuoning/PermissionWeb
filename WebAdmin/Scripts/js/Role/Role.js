$(function () {
    var oldSelectRoleId;
    $("#ui_role_dg").datagrid({
        url: "/Role/GetRoleList",
        striped: true,
        rownumbers: true,
        pagination: true,
        singleSelect: true,
        pageSize: 20,
        idFiled: "Id",
        sortName: "UpdateTime",
        sortOrder: "asc",
        pageList: [20, 40, 60, 80, 100],
        columns: [[
          {
              field: "Name", title: "角色名称", width: 100,
              formatter: function (value, row, index) {
                  return value.length > 8 ? '<span title="' + value + '">' + value + '</span>' : value;
              }
          },
          { field: "UpdateTime", title: "最后更新时间", width: 150, sortable: true, formatter: ChangeDateFormat },
          { field: "UpdateBy", title: "最后更新人", width: 130 },
          {
              field: "Description", title: "描述", width: 300,
              formatter: function (value, row, index) {
                  return value.length > 20 ? '<span title="' + value + '">' + value + '</span>' : value;
              }
          },
        ]],
        toolbar: [{
            text: "添加",
            iconCls: "icon-add",
            handler: AddRole
        },
        {
            text: "修改",
            iconCls: "icon-save",
            handler: UpdateRole
        },
        {
            text: "删除",
            iconCls: "icon-cut",
            handler: function () { }
        },
        {
            text: "角色授权",
            iconCls: "icon-key",
            handler: RoleAuthorize
        }],
        onSelect: function (rowIndex, rowData) {
            if (oldSelectRoleId == rowData.Id) {
                return;
            }
            oldSelectRoleId = rowData.Id;
            var $ui_role_layout = $("#ui_role_layout");
            var eastRoleUser = $ui_role_layout.layout("panel", "east");
            if (eastRoleUser.panel("options").collapsed) {
                $ui_role_layout.layout("expand", "east");
            }
            eastRoleUser.panel("setTitle", rowData.Name + "成员");
            if ($("#ui_role_user_dg").data("datagrid")) {
                $("#ui_role_user_dg").datagrid({
                    url: "/Role/GetUserListByRoleId?Id=" + rowData.Id
                });
            }
            else {
                $("#ui_role_user_dg").datagrid({
                    url: "/Role/GetUserListByRoleId?Id=" + rowData.Id,
                    striped: true,
                    rownumbers: true,
                    pagination: true,
                    singleSelect: true,
                    pageSize: 20,
                    idFiled: "Id",
                    sortName: "CreatTime",
                    sortOrder: "asc",
                    pageList: [20, 40, 60, 80, 100],
                    columns: [[
                        { field: 'Name', title: '登录名', width: 100 },
                        { field: 'UserName', title: '姓名', width: 80 },
                        {
                            field: 'IsAble', title: '启用', width: 60, align: 'center',
                            formatter: function (value, row, index) {
                                if (value != null) {
                                    return value ? '<img src="../Content/themes/icon/chk_checked.gif" alt="已启用" title="用户已启用" />' : '<img src="../Content/themes/icon/chk_unchecked.gif" alt="未启用" title="用户未启用" />';
                                }
                            }
                        },
                         {
                             field: 'IsEidtPass', title: '改密', width: 60, align: 'center',
                             formatter: function (value, row, index) {
                                 if (value != null) {
                                     return value ? '<img src="../Content/themes/icon/chk_checked.gif" alt="已改密" title="用户已改密" />' : '<img src="../Content/themes/icon/chk_unchecked.gif" alt="未改密" title="用户未改密" />';
                                 }
                             }
                         },
                        { field: 'CreatTime', title: '添加时间',formatter: ChangeDateFormat, sortable: true, width: 130 }
                    ]]
                });
            }
        }
    });
});
function AddRole() {
    $("<div/>").dialog({
        id: "ui_role_add_dialog",
        href: "/Role/Creat",
        title: "添加角色",
        height: 250,
        width: 400,
        modal: true,
        buttons: [{
            id: "ui_role_add_btn",
            text: "添 加",
            handler: function () {
                $("#RoleAddForm").form("submit", {
                    url: "/Role/Creat",
                    onSubmit: function (param) {
                        $("#ui_role_add_btn").linkbutton("disable");
                        if ($(this).form("validate")) {
                            return true;
                        }
                        else {
                            $("#ui_role_add_btn").linkbutton("enable");
                            return false;
                        }
                    },
                    success: function (data) {
                        var result = eval('(' + data + ')');
                        if (result.Success) {
                            $("#ui_role_add_dialog").dialog("destroy");
                            $.show_alert("提示", result.Msg);
                            $("#ui_role_dg").datagrid("reload").datagrid("clearSelections").datagrid("clearChecked");
                        } else {
                            $("#ui_role_add_btn").linkbutton("enable");
                            $.show_alert("提示",result.Msg)
                        }
                    }
                });
            }
        },
        {
            text: "取 消",
            handler: function () {
                $("#ui_role_add_dialog").dialog("destroy");
            }
        }],
        onLoad: function () {
            $("#txtRoleName").focus();
        },
        onClose: function () {
            $("#ui_role_dialog").dialog("destroy");
        }
    });
}
function UpdateRole()
{
    var rows = $("#ui_role_dg").datagrid("getChecked");
    if (rows.length < 1)
    {
        $.show_alert("提示", "请选择要修改的角色");
        return;
    }
    if (rows.length > 1)
    {
        $.show_alert("提示", "不支持批量修改角色");
        return;
    }
    $("<div/>").dialog({
        id: "ui_role_edit_dialog",
        title: "修改角色",
        href: "/Role/Update",
        height: 250,
        width: 400,
        modal: true,
        buttons: [{
            id: "ui_role_edit_btn",
            text: "修 改",
            handler: function () {
                $("#RoleEditForm").form("submit", {
                    url: "/Role/Update",
                    onSubmit: function (param) {
                        $("#ui_role_edit_btn").linkbutton("disable");
                        if ($(this).form("validate")) {
                            return true;
                        }
                        else {
                            $("#ui_role_edit_btn").linkbutton("enable");
                        }
                    },
                    success: function (data) {
                        var result = eval('(' + data + ')');
                        if (result.Success) {
                            $("#ui_role_edit_dialog").dialog("destroy");
                            $.show_alert("提示", result.Msg);
                            $("#ui_role_dg").datagrid("reload").datagrid("clearSelections").datagrid("clearChecked");
                        }
                        else {
                            $("#ui_role_edit_btn").linkbutton("enable");
                            $.show_alert("提示",result.Msg);
                        }
                    }
                });
            }
        },
        {
            text: "取 消",
            handler: function () {
                $("#ui_role_edit_dialog").dialog("destroy");

            }
        }],
        onLoad: function () {
            $("#hidid").val(rows[0].Id);
            $("#txtRoleName").val(rows[0].Name);
            $("#txtDescription").val(rows[0].Description);
        },
        onClose: function () {
            $("#ui_role_eidt_dialog").dialog("destroy");
        }
    });
}
function RoleAuthorize() {
    var rows = $("#ui_role_dg").datagrid("getChecked");
    if (rows.length < 1)
    {
        $.show_alert("提示", "请先勾选要授权的角色");
        return;
    }
    if (rows.length > 1) {
        $.show_alert("提示", "不支持批量角色授权");
        return;
    }
    $("<div/>").dialog({
        id: "ui_role_authorize_dialog",
        href: "/Role/RoleMenu",
        title:"角色授权:"+rows[0].Name,
        height: 500,
        width: 300,
        modal: true,
        buttons: [{
            id: "ui_role_authorize_btn",
            text: "授 权",
            handler: function () {
                $('#ui_role_authorize_btn').linkbutton('disable');
                var nodes = $('#treerolemenu').tree('getChecked');
                var arr = [];
                for (var i in nodes) {
                    if (nodes[i].attributes.buttonid != null) {
                        arr.push(nodes[i].attributes);
                    }
                }

                $.ajax({
                    url: "/Role/RoleMenu",
                    data: {
                        RoleId: rows[0].Id,
                        MenuButtonIds: arr
                    },
                    type:"Post",
                    dataType:"json",
                    success: function (data) {
                        if (data.Success) {
                            $("#ui_role_authorize_dialog").dialog("destroy");
                            $.show_alert("提示", data.Msg);
                            $("#ui_role_dg").datagrid("reload").datagrid("clearSelections").datagrid("clearChecked");
                        }
                        else {
                            $('#ui_role_authorize_btn').linkbutton('enable');
                            $.show_alert("提示", data.Msg);
                        }

                    }
                });
            }
        }],
        onLoad: function () {
            $('#treerolemenu').tree({
                url: "/Menu/GetAllRoleMenuButtonTree?Id=" + rows[0].Id,
                checkbox:true,
                loadFilter: function(data){
                        return data;
                },
                onBeforeExpand: function (node) {
                    if (node.children.length == 0) {
                        return false;
                    }
                    else {
                        return true;
                    }
                }
            });
        },
        onClose: function () {
            $("#ui_role_authorize_dialog").dialog("destroy");
        }
    });
}
function ui_role_searchdata() {
    $("#ui_role_dg").datagrid("load", {
        Name: $("#txtSearchRoleName").val()
    });
    $("#ui_role_dg").datagrid("clearSelections").datagrid("clearChecked")
}
function ui_role_cleardata() {
    $("#txtSearchRoleName").val("");
    $("#ui_role_dg").datagrid("load", {});
    $("#ui_role_dg").datagrid("clearSelections").datagrid("clearChecked")
}