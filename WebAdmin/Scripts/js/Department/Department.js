var oldSelectDepartmentId;

$(function () {
    $("#ui_department_tg").treegrid({
        url: "/User/GetDepartmentInfo",
        idField: "id",
        treeField: "text",
        columns: [[
             { field: 'text', title: '部门名称', width: 250 },
             { field: 'Sort', title: '排序', width: 50 },
             { field: 'UpdateBy', title: '最后更新人', width: 150 },
             { field: 'UpdateTime', title: '最后更新时间', width: 150, formatter: ChangeDateFormat }
        ]],
        onBeforeOpen: function () {
            setToolBar(this, "#ui_department_tg")
        },
        onClickRow: function (rows) {
            departmentIds = [];
            if (rows.children.length > 0) {
                recursionGetIds(rows)//递归获取部门id"
            }
            else {
                departmentIds.push(rows.id);
            }

            if (oldSelectDepartmentId == rows.id) {
                return;
            }
            oldSelectDepartmentId = rows.id;
            var $ui_department_layout = $("#ui_department_layout");
            var eastDepartmentUser = $ui_department_layout.layout("panel", "east");

            if (eastDepartmentUser.panel("options").collapsed) {   //判断是否展开
                $ui_department_layout.layout("expand", "east");
            }
            eastDepartmentUser.panel("setTitle", rows.text + "成员");
            if ($("#ui_department_user_dg").data("datagrid")) {
                $("#ui_department_user_dg").datagrid({       //请求数据
                    url: "/Department/GetUserByDepartmentId",
                    queryParams: { ids: $.makeArray(departmentIds) }
                });
            }
            else {
                $("#ui_department_user_dg").datagrid({       //初始化datagrid
                    url: "/Department/GetUserByDepartmentId",
                    striped: true, rownumbers: true, pagination: true, pageSize: 20, singleSelect: true,
                    idField: 'Id',
                    sortName: 'CreatTime',
                    sortOrder: 'asc',
                    pageList: [20, 40, 60, 80, 100],
                    queryParams: { ids: $.makeArray(departmentIds) },
                    columns: [[
                          { field: 'Name', title: '登录名', width: 100 },
                          { field: 'UserName', title: '姓名', width: 80 },
                          {
                              field: 'IsAble', title: '启用', width: 60, align: 'center',
                              formatter: function (value, row, index) {
                                  return value ? '<img src="../Content/themes/icon/chk_checked.gif" alt="已启用" title="用户已启用" />' : '<img src="../Content/themes/icon/chk_unchecked.gif" alt="未启用" title="用户未启用" />';
                              }
                          },
                          {
                              field: 'IsEidtPass', title: '改密', width: 60, align: 'center',
                              formatter: function (value, row, index) {
                                  return value ? '<img src="../Content/themes/icon/chk_checked.gif" alt="已改密" title="用户已改密" />' : '<img src="../Content/themes/icon/chk_unchecked.gif" alt="未改密" title="用户未改密" />';
                              }
                          },
                          { field: 'CreatTime', title: '添加时间', formatter: ChangeDateFormat, width: 130 }]]
                });
            }
        }
    });
});
//递归获取部门id
function recursionGetIds(rows) {

    if (rows.children != undefined) {
        $.each(rows.children, function (i, row) {
            if (row.children.length == 0) {
                departmentIds.push(row.id);
            }
            recursionGetIds(row);
        });
    }
}
function AddDepartment() {
    $("<div/>").dialog({
        id: "ui_departmenr_add_dialog",
        title: "添加部门",
        href: "/Department/Create",
        height: 250,
        width: 350,
        modal: true,
        buttons: [{
            id: "ui_department_add_btn",
            text: "添 加",
            handler: function () {
                $("#DepartmentAddForm").form("submit", {
                    url: "/Department/Create",
                    onSubmit: function (param) {
                        param.ParentId = $("#treeDepartmentParentId").combotree("getValues").toString();
                        $("#ui_department_add_btn").linkbutton("disable");
                        if ($(this).form("validate")) {
                            return true;
                        }
                        else {
                            $("#ui_department_add_btn").linkbutton("enable");
                            return false;
                        }
                    },
                    success: function (data) {
                        var result = eval('(' + data + ')');
                        if (result.Success)
                        {
                            $("#ui_departmenr_add_dialog").dialog("destroy");
                            $.show_alert("提示", result.Msg);
                            $("#ui_department_tg").treegrid("reload");
                        }
                        else {
                            $('#ui_department_add_btn').linkbutton('enable');
                            $.show_alert("提示", result.Msg);
                        }
                    }
                });
            }
        },
        {
            text: "取消",
            handler: function () {
                $("#ui_departmenr_add_dialog").dialog("destroy");
            }
        }],
        onLoad: function () {
            $("#txtDepartmentName").focus();
        },
        onClose: function () {
            $("#ui_departmenr_add_dialog").dialog("destroy");
        }
    });
}
function UpdateDepartment()
{
    var rowSelected = $("#ui_department_tg").treegrid("getSelected");
    if (!rowSelected) {
        $.show_alert("提示", "请先选择要修改的部门");
        return;
    }
    $("<div/>").dialog({
        id: "ui_department_edit_dialog",
        href: "/Department/Update",
        title: "修改部门",
        height: 200,
        width: 300,
        modal: true,
        buttons: [{
            id: "ui_department_edit_btn",
            text: '修 改',
            handler: function () {
                $("#DepartmentEditForm").form("submit", {
                    url: "/Department/Update",
                    onSubmit: function (param) {
                        param.Name = $("#txtDepartmentName").val();
                        param.Sort = $('#txtSort').val();
                        param.Id = $("#hidid").val();

                        if ($(this).form('validate')) {
                            $('#ui_department_add_btn').linkbutton('disable');
                            return true;
                        }
                        else {
                            $('#ui_department_edit_btn').linkbutton('enable');
                            return false;
                        }
                    },
                    success: function (data) {
                        
                        var result = eval('(' + data + ')');
                        if (result.Success) {
                            $("#ui_department_edit_dialog").dialog('destroy');
                            $.show_alert("提示", result.Msg);
                            $("#ui_department_tg").treegrid("reload");
                        } else {
                            $('#ui_department_edit_btn').linkbutton('enable');
                            $.show_alert("提示", result.Msg);
                        }
                    }
                });
            }
        }, {
            text: '取 消',
            handler: function () {
                $("#ui_department_edit_dialog").dialog('destroy');
            }
        }],
        onLoad: function () {
            $("#hidid").val(rowSelected.id);
            $("#txtDepartmentName").val(rowSelected.text);
            $('#txtSort').numberspinner('setValue', rowSelected.Sort);
        },
        onClose: function () {
            $("#ui_department_edit_dialog").dialog('destroy');
        }
    });
}