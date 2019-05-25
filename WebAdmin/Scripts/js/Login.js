$(function () {
    if (window.parent.window != window) {
        window.top.location.href = "/Login/Index";
    }
    $("#loginDialog").dialog({
        title: "用户登录",
        closable: false,
        iconCls: 'icon-user_b',
        modal: true,
        width: 310,
        height: 220,
        buttons: [{
            id: "ui_login_btn",
            text: "登 录",
            handler: function () {
                $("#ui_login_btn").linkbutton("disable");
                if ($("#loginFrm").form("validate")) {
                    var param = {

                        UserName: $("#loginName").val(),
                        PassWord: $("#loginPwd").val(),
                        IsAutoLogin: $("input:checkbox:checked").val()
                    };
                    $.post("/Login/Index", param, function (data) {
                        if (data.Success) {
                            window.location.href = "/Home/Index";
                        }
                        else {
                            $("#ui_login_btn").linkbutton("enable");
                            $.show_alert("提示", data.Msg);
                        }
                    })
                }
                else {
                    $("#ui_login_btn").linkbutton("enable");
                }
            }
        }]
    });
    $("#loginName").focus();
});