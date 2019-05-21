$(function () {
    $("#ui_menu_dg").datagrid({
        url: "/Menu/GetMenuList",
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
            { field: "Name", title: "菜单名称", width: 100 },
             { field: "ParentName", title: "父菜单", width: 100 },
            { field: "Code", title: "标识码", width: 100 },
            { field: "LinkAddress", title: "链接地址", width: 100 },
            { field: "Icon", title: "图标", width: 130 },
            { field: "Sort", title: "排序", width: 100, sortable: true },
            { field: "UpdateTime", title: "最后更新时间", width: 150, formatter: ChangeDateFormat },
            { field: "UpdateBy", title: "最后更新人", width: 100 }
        ]],
        toolbar: [{
            text: "添加",
            iconCls: "icon-add",
            handler: AddMenu
        },
        {
            text: "修改",
            iconCls: "icon-save",
            handler: UpdateMenu
        },
        {
            text: "删除",
            iconCls: "icon-cut",
            handler: DelMenu
        }]
    });
});
//添加
AddMenu = function () {
    $("<div/>").dialog({
        id: "ui_menu_add_dialog",
        title: "添加菜单",
        href: "/Menu/Creat",
        height: 300,
        width: 400,
        modal: true,
        buttons: [{
            id: "ui_menu_add_btn",
            text: "添加",
            handler: function () {
                $("#MenuAddForm").form("submit", {
                    url: "/Menu/Creat",
                    onSubmit: function (param) {
                        param.ParentId = $("#comboxParentIdTree").combotree("getValues").toString()
                        param.Icon = $("#comboxIconTree").combotree("getText").toString()
                        if ($(this).form("validate")) {
                            $("#ui_menu_add_btn").linkbutton("disable");
                            return true;
                        }
                        else {
                            $("#ui_menu_add_btn").linkbutton("enable");
                            return false;
                        }
                    },
                    success: function (data) {
                        var result = eval('(' + data + ')');
                        if (result.Success) {
                            $("#ui_menu_add_dialog").dialog("destroy");
                            $.show_alert("提示", result.Msg);
                            $("#ui_menu_dg").datagrid("reload").datagrid("clearSelections").datagrid("clearChecked")
                        }
                        else {
                            $("#ui_menu_add_btn").linkbutton("enable");
                            $.show_alert("提示", result.Msg);
                        }
                    }
                });
            }
        }, {
            text: "取消",
            handler: function () {
                $("#ui_menu_add_dialog").dialog("destroy");
            }
        }],
        onLoad: function () {

            $("#txtName").focus();
        },
        onClose: function () {
            $("#ui_menu_add_dialog").dialog("destroy");
        }
    });
}
//修改
UpdateMenu = function () {

    var rows = $("#ui_menu_dg").datagrid("getChecked");
    if (rows.length < 1) {
        $.show_alert("提示", "请选择要修改的菜单");
        return;
    }
    if (rows.length > 1) {
        $.show_alert("提示", "不支持批量修改菜单");
        return;
    }
    $("<div/>").dialog({
        id: "ui_menu_edit_dialog",
        title: "修改菜单",
        href: "/Menu/Update",
        height: 300,
        width: 400,
        modal: true,
        buttons: [{
            id: "ui_menu_edit_btn",
            text: "修改",
            handler: function () {
                $("#MenuEditForm").form("submit", {
                    url: "/Menu/Update",
                    onSubmit: function (param) {
                        param.ParentId = $("#comboxParentIdTree").combotree("getValues").toString()
                        param.Icon = $("#comboxIconTree").combotree("getText").toString()
                        if ($(this).form("validate")) {
                            $("#ui_menu_edit_btn").linkbutton("disable");
                            return true;
                        }
                        else {
                            $("#ui_menu_edit_btn").linkbutton("enable");
                            return false;
                        }
                    },
                    success: function (data) {
                        var result = eval('(' + data + ')');
                        if (result.Success) {
                            $("#ui_menu_edit_dialog").dialog("destroy");
                            $.show_alert("提示", result.Msg);
                            $("#ui_menu_dg").datagrid("reload").datagrid("clearSelections").datagrid("clearChecked");
                        }
                        else {
                            $("#ui_menu_edit_btn").linkbutton("enable");
                            $.show_alert("提示", result.Msg);
                        }
                    }
                });
            }
        },
         {
             text: "取消",
             handler: function () {
                 $("#ui_menu_edit_dialog").dialog("destroy");
             }
         }],
        onLoad: function () {
            $("#hid").val(rows[0].Id);
            $("#txtName").val(rows[0].Name);
            if (rows[0].ParentId != null)
            {
                $("#comboxParentIdTree").combotree("setValue", rows[0].ParentId);
            }
          
            $("#txtCode").val(rows[0].Code);
            $("#txtLinkAddress").val(rows[0].LinkAddress);
            $("#comboxIconTree").combotree("setValue", rows[0].Icon);
            $("#txtSort").numberspinner("setValue",rows[0].Sort);
        },
        onClose: function () {
            $("#ui_menu_edit_dialog").dialog("destroy");
        }
    });
}
//删除(支持批量删除)
DelMenu = function () {
    var rows = $("#ui_menu_dg").datagrid("getChecked");
    if (rows.length < 1)
    {
        $.show_alert("请选择要删除的菜单");
        return;
    }
    $.messager.confirm("提示", "确定删除选中行吗", function (isChecked) {
        if (isChecked)
        {
            var ids = "";
            $.each(rows, function (i, row) {
                ids += row.Id + ",";
            });
            ids = ids.substring(0, ids.length - 1);
            $.ajax({
                url: "/Menu/Del",
                data: { ids},
                type: "Post",
                dataType: "json",
                success: function (data) {
                    if (data.Success) {
                        $.show_alert("提示", data.Msg);
                        $("#ui_menu_dg").datagrid("reload").datagrid("clearSelections").datagrid("clearChecked");
                    }
                    else {
                        $.show_alert("提示", data.Msg);
                    }
                }
            });
        }
    });
}
ui_menu_searchdata = function () {

    $("#ui_menu_dg").datagrid("load", {
        Name: $("#txtMenuName").val()
    });
    $("#ui_menu_dg").datagrid("clearSelections").datagrid("clearChecked")
}

ui_menu_cleardata = function () {
    $("#txtMenuName").val("")
    $("#ui_menu_dg").datagrid("load", {});
    $("#ui_menu_dg").datagrid("clearSelections").datagrid("clearChecked")
}