$(function () {
    $("#ui_button_dg").datagrid({
        url: "/Button/GetButtonList",
        striped: true,
        rownumbers: true,
        pagination: true,
        pageSize: 20,
        idFiled: "Id",
        sortName: "Sort",
        sortOrder: "asc",
        pageList: [20, 40, 60, 80, 100],
        columns: [[
             { field: "ck", checkbox: true },
            { field: "Name", title: "按钮名称", width: 100 },
            { field: "Code", title: "标识码", width: 100 },
            { field: "Icon", title: "图标", width: 130 },
            { field: "Sort", title: "排序", width: 100, sortable: true },
            { field: "UpdateDateTime", title: "最后更新时间", width: 150, formatter: ChangeDateFormat },
            { field: "UpdateBy", title: "最后更新人", width: 100 },
            { field: "Description", title: "描述", width: 200 }
        ]],
        onBeforeOpen: function () {

            setToolBar(this, "#ui_button_dg")
        }
    });
});
//添加
function AddButton() {
    $("<div/>").dialog({
        id: "ui_button_add_dialog",
        href: "/Button/Creat",
        title: "添加按钮",
        height: 300,
        width: 400,
        modal: true,
        buttons: [{
            id: "ui_button_add_btn",
            text: "添加",
            handler: function () {
                $("#ButtonAddForm").form("submit", {
                    url: "/Button/Creat",
                    onSubmit: function (param) {
                        param.Icon = $("#comboxIconTree").combotree("getText").toString()
                        if ($(this).form("validate")) {
                            $("#ui_button_add_btn").linkbutton("disable");
                            return true;
                        }
                        else {
                            $("#ui_button_add_btn").linkbutton("enable");
                            return false;
                        }
                    },
                    success: function (data) {
                        var result = eval('(' + data + ')');
                        if (result.Success) {
                            $("#ui_button_add_dialog").dialog("destroy");
                            $.show_alert("提示", result.Msg);
                            $("#ui_button_dg").datagrid("reload").datagrid("clearSelections").datagrid("clearChecked");
                        }
                        else {
                            $("#ui_button_add_btn").linkbutton("enable");
                            $.show_alert("提示", result.Msg);
                        }
                    }
                });
            }
        },
        {
            text: "取消",
            handler: function () {
                $("#ui_button_add_dialog").dialog("destroy");
                $("#ui_button_dg").datagrid("reload").datagrid("clearSelections").datagrid("clearChecked");
            }
        }
        ],
        onLoad: function () {
            $("#txtName").focus();
        },
        onClose: function () {
            $("#ui_button_add_dialog").dialog("destroy");
            $("#ui_button_dg").datagrid("reload").datagrid("clearSelections").datagrid("clearChecked");
        }
    });
}
//修改
function UpdateButton() {
    var rows = $("#ui_button_dg").datagrid("getChecked");
    if (rows.length < 1) {
        $.show_alert("提示", "请先勾选要修改的按钮");
        return;
    }
    if (rows.length > 1) {
        $.show_alert("提示", "不支持批量修改用户");
        $("#ui_button_dg").datagrid('clearSelections').datagrid('clearChecked');
        return;
    }
    $("<div/>").dialog({
        id: "ui_button_edit_dialog",
        href: "/Button/Update",
        title: "修改按钮",
        height: 300,
        width: 400,
        modal: true,
        buttons: [{
            id: "ui_button_edit_btn",
            text: "修改",
            handler: function () {
               
                $("#ButtonEditForm").form("submit", {
                    url: "/Button/Update",
                    onSubmit: function (param) {
                        param.Icon = $("#comboxIconTree").combotree("getText").toString();
                        if ($(this).form("validate")) {
                            $("#ui_button_edit_btn").linkbutton("disable");
                            return true;
                        }
                        else {
                            $("#ui_button_edit_btn").linkbutton("enable");
                            return false;
                        }
                    },
                    success: function (data) {
                        var result = eval('(' + data + ')');
                        if (result.Success) {
                            $("#ui_button_edit_dialog").dialog("destroy");
                            $.show_alert("提示", result.Msg);
                            $("#ui_button_dg").datagrid("reload").datagrid("clearSelections").datagrid("clearChecked");
                        }
                        else {
                            $("#ui_button_edit_btn").linkbutton("enable");
                            $.show_alert("提示", result.Msg);
                        }
                    }
                });
            }
        },
        {
            text: "取消",
            handler: function () {
                $("#ui_button_edit_dialog").dialog("destroy");
            }
        }
        ],
        onLoad: function () {
            $("#hidid").val(rows[0].Id);
            $("#txtName").val(rows[0].Name);
            $("#txtCode").val(rows[0].Code);
            $("#comboxIconTree").combotree("setValue", rows[0].Icon);
            $("#txtSort").numberspinner("setValue", rows[0].Sort);
            $("#txtDescription").val(rows[0].Description);
        },
        onClose: function () {
            $("#ui_button_edit_dialog").dialog("destroy");
        }
    });
}
//删除 支持批量删除
function DelButton() {
    var rows = $("#ui_button_dg").datagrid("getChecked");
    if (rows.length < 1)
    {
        $.show_alert("提示", "请先勾选要删除的按钮");
        return;
    }
    $.messager.confirm("提示", "确定删除选中行吗?", function (isChecked) {
        if (isChecked)
        {
            var ids = "";
            $.each(rows, function (i,row) {
                ids += row.Id + ",";
            })
            ids = ids.substring(0, ids.length - 1);
            $.ajax({
                url: "/Button/DelButtonByIDs",
                data: { ids},
                type: "POST",
                dataType: "json",
                success: function (data) {
                    if (data.Success) {
                        $.show_alert("提示", data.Msg);
                        $("#ui_button_dg").datagrid("reload").datagrid("clearSelections").datagrid("clearChecked");
                    }
                    else {
                        $.show_alert("提示",data.Msg);
                    }
                }
            });
        }
    });
}
function ui_button_searchdata()
{
    $("#ui_button_dg").datagrid("load", {
        Name: $("#txtUserId").val()
    });
    $("#ui_button_dg").datagrid('clearSelections').datagrid('clearChecked');
}
function ui_button_cleardata()
{
    $("#txtUserId").val("")
    $("#ui_button_dg").datagrid("load",{});
    $("#ui_button_dg").datagrid('clearSelections').datagrid('clearChecked');
}