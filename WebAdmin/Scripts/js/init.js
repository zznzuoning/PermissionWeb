$(function () {
    BindRightAccordion()
    CheckIsChangePwd();
});
function BindRightAccordion() {
    $("#RightAccordion").accordion({ //初始化accordion
        fillSpace: true,
        fit: true,
        border: false,
        animate: false
    });
    $('#tabs').tabs({
        tools: [{
            text: "刷新",
            iconCls: 'icon-arrow_refresh',
            handler: refreshTab
        }, {
            text: "全部关闭",
            iconCls: 'icon-delete3',
            handler: closeTab
        }]
    });
    $.getJSON("/Home/GetTreeByEasyui", { Id: null },
        function (data) {
            $.each(data, function (i, e) {
                var id = e.id;
                $('#RightAccordion').accordion('add', {
                    title: e.text,
                    content: "<ul id='tree" + id + "' ></ul>",
                    selected: true,
                    iconCls: e.iconCls
                });
                $.parser.parse();
                $.getJSON("/Home/GetTreeByEasyui", { Id: id }, function (data) {
                    $("#tree" + id).tree({
                        data: data,
                        onBeforeExpand: function (node, param) {
                            $("#tree" + id).tree('options').url = "/Home/GetTreeByEasyui?Id=" + node.id;
                        },
                        onClick: function (node) {
                            if (node.state == 'closed') {
                                $(this).tree('expand', node.target);
                            }
                            else if (node.state == 'open') {
                                $(this).tree('collapse', node.target);
                                addTab(node.text, node.attributes, node.iconCls)
                            }
                        }
                    });
                })
            })
        });
}
function addTab(subtitle, url, icon) {
    var strHtml = '<iframe id="frmWorkArea" width="99.5%" height="99%" style="padding:1px;" frameborder="0" scrolling="yes" src="' + url + '"></iframe>';
    if (!$("#tabs").tabs('exists', subtitle)) {
        $("#tabs").tabs('add', {
            title: subtitle,
            content: strHtml,
            iconCls: icon,
            closable: true,
            loadingMessage: '正在加载中......'
        });
    }
    else {
        $("#tabs").tabs('select', subtitle)
    }
}
//刷新指定选项卡
function refreshTabs(title) {
    if ($('#tabs').tabs('exists', title)) {
        $('#tabs').tabs('getTab', title).panel('refresh');
    }
}
//刷新选项卡
function refreshTab() {
    var index = $('#tabs').tabs('getTabIndex', $('#tabs').tabs('getSelected'));
    if (index != -1) {
        var tab = $('#tabs').tabs('getTab', index);
        $('#tabs').tabs('update', {
            tab: tab,
            options: {
                selected: true
            }
        });
    }
}

//关闭选项卡
function closeTab() {
    $('.tabs-inner span').each(function (i, n) {
        var t = $(n).text();
        if (t != '') {
            if (t != "我的主页") {
                $('#tabs').tabs('close', t);
            }
        }
    });
}
function CheckIsChangePwd() {
    $.ajax({
        url: "/Home/CheckIsChangePwd?r=" + Math.random(),
        type: "post",
        dataType: "json",
        success: function (result) {
            if (result.Success) {
                if (!result.Data)
                    changePwd("first");
            }
            else {
                window.location.href = "/Login/Index";
            }
        }
    });
}

function changePwd(changetype) {
    var titles = "修改密码";
    var outText = "取 消";
    var isAble = false;
    if (changetype == "first") {
        titles = "首次登陆必须重置密码";
        outText = "退 出";
        isAble = true;
    }

    $("<div/>").dialog({
        id: "ui_user_userchangepwd_dialog",
        href: "/User/ChangePwd",
        title: titles,
        height: 220,
        width: 320,
        modal: true,
        closable: false,
        buttons: [
            {
            id: "ui_user_userchangepwd_edit",
            text: '修 改',
            handler: function () {
                $("#ui_user_userchangepwd_form").form("submit", {
                    url: "/User/UpdatePwd",
                    onSubmit: function (param) {
                        $("#ui_user_userchangepwd_edit").linkbutton("disable");
                        if ($(this).form("validate")) {
                            var postData = {
                                UserPwd: $("#txtoriginalpwd").val(),
                                NewPwd: $("#txtnewpwd").val(),
                                ConfirmPwd: $("#txtconfirmpwd").val()
                            };
                            if (postData.UserPwd == postData.NewPwd) {
                                $.show_alert("提示", "新密码不能与原密码相同");
                                return false;
                            }
                            param.newPwd = postData.NewPwd;
                            return true;
                        }
                        else {
                            $("#ui_user_userchangepwd_edit").linkbutton("enable");
                            return false;
                        }
                    },
                    success: function (data) {
                        var result = eval('(' + data + ')');
                        if (result.Success) {
                            $("#ui_user_userchangepwd_dialog").dialog('destroy');
                            $.show_alert("提示", result.Msg);
                            UserLoginOut();
                        }
                        else {
                            $('#ui_user_userchangepwd_edit').linkbutton('enable');
                            $.show_alert("提示", result.Msg);
                        }
                    }
                });
            }
            },
        {
            text: outText,
            disabled: isAble,
            handler: function () {
                if (changetype == "first") {
                    UserLoginOut();
                }
                else {
                    $("#ui_user_userchangepwd_dialog").dialog('destroy');
                }
            }
        }],
        onLoad: function () {
            $("#txtoriginalpwd").focus();
            if (changetype == "first") {
                $.show_alert("提示", "首次登陆必须重置密码");
            }
        }
    });
}
//退出系统
function loginOut() {
    $.messager.confirm('提示！', '确定退出系统？', function (r) {
        if (r) {
            var para = { "action": "logout" };
            $.ajax({
                url: "/Login/LogOut",
                type: "post",
                data: para,
                dataType: "json",
                success: function (result) {
                    if (result.Success) {
                        window.location.href = "/Login/Index";
                    }
                    else {
                        $.show_alert("提示", result.msg);
                    }
                }
            })
        }
    })
};

//退出系统
function UserLoginOut() {
    var para = { "action": "logout" };
    $.ajax({
        url: "/Login/LogOut",
        type: "post",
        data: para,
        dataType: "json",
        success: function (result) {
            if (result.Success) {
                window.location.href = "/Login/Index";
            }
            else {
                $.show_alert("提示", result.msg);
            }
        }
    })
}