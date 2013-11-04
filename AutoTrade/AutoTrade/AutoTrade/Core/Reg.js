
注册-个人用户
注册-个人用户
	
GET jquery-1.2.6.pack.js
	
304 Not Modified
	
reg.jd.com
	
0 B
	
58.83.158.53:443
	
 
128ms
	
GET jd.lib.js
	
304 Not Modified
	
reg.jd.com
	
9.8 KB
	
58.83.158.53:443
	
 
171ms
	
GET jdThickBox.js
	
304 Not Modified
	
reg.jd.com
	
2.0 KB
	
58.83.158.53:443
	
 
179ms
	
GET jdValidate.js?t=20130814
	
304 Not Modified
	
reg.jd.com
	
0 B
	
58.83.158.53:443
	
 
175ms
	
GET jdValidate.emReg.js?t=20130425
	
304 Not Modified
	
reg.jd.com
	
1.1 KB
	
58.83.158.53:443
	
 
212ms
参数头信息响应缓存Cookies

$.extend(
    validateFunction, {
        regValidate: function () {
            if ($("#mobileCodeDiv").attr("class") == 'item') {
                $("#regName").jdValidate(validatePrompt.regName, validateFunction.regName, true);
                $("#pwd").jdValidate(validatePrompt.pwd, validateFunction.pwd, true);
                $("#pwdRepeat").jdValidate(validatePrompt.pwdRepeat, validateFunction.pwdRepeat, true);
                $("#mobileCode").jdValidate(validatePrompt.mobileCode, validateFunction.mobileCode, true);
                return validateFunction.FORM_submit(["#regName", "#pwd", "#pwdRepeat", "#mobileCode"]);
            } else {
                $("#regName").jdValidate(validatePrompt.regName, validateFunction.regName, true);
                $("#pwd").jdValidate(validatePrompt.pwd, validateFunction.pwd, true);
                $("#pwdRepeat").jdValidate(validatePrompt.pwdRepeat, validateFunction.pwdRepeat, true);
                return validateFunction.FORM_submit(["#regName", "#pwd", "#pwdRepeat"]);
            }
        }
    });
var isSubmit = false;
$("#pwd").bind("keyup",function () {
    validateFunction.pwdstrength();
}).jdValidate(validatePrompt.pwd, validateFunction.pwd)
$("#pwdRepeat").jdValidate(validatePrompt.pwdRepeat, validateFunction.pwdRepeat);
$("#regName").jdValidate(validatePrompt.regName, validateFunction.regName);
$("#mobileCode").jdValidate(validatePrompt.mobileCode, validateFunction.mobileCode);

function checkReadMe() {
    if ($("#readme").attr("checked") == true) {
        $("#protocol_error").removeClass().addClass("error hide");
        return true;
    } else {
        $("#protocol_error").removeClass().addClass("error");
        return false;
    }
}

function agreeonProtocol() {
    if ($("#readme").attr("checked") == true) {
        $("#protocol_error").removeClass().addClass("error hide");
        return true;
    }
}

function protocolReg() {
    $("#closeBox").click();
    reg();
}

function reg() {
    if (isSubmit) {
        return;
    }
    var agreeProtocol = checkReadMe();
    var regNameok = validateRegName();
    isSubmit = true;

    var passed = validateFunction.regValidate() && regNameok && agreeProtocol;
    //pageTracker._trackEvent('Button', 'Regist', 'Normal');
    if (passed) {
        $("#registsubmit").attr({ "disabled": "disabled" }).removeClass().addClass("btn-img btn-regist wait-btn");
        $.ajax({
            type: "POST",
            url: "../reg/regService?r=" + Math.random() + "&" + location.search.substring(1),
            contentType: "application/x-www-form-urlencoded; charset=utf-8",
            data: $("#personRegForm").serialize(),
            success: function (result) {
                if (result) {
                    var obj = eval(result);
                    if (obj.info) {
                        alert(obj.info);
                        verc();
                        $("#registsubmit").removeAttr("disabled").removeClass().addClass("btn-img btn-regist");
                        isSubmit = false;
                        return;
                    }
                    if (obj.noAuth) {
                        verc();
                        window.location = obj.noAuth;
                        return;
                    }
                    if (obj.success == true) {
                        window.location = obj.dispatchUrl;
                    }
                }
            }
        });
    } else {
        $("#registsubmit").removeAttr("disabled").removeClass().addClass("btn-img btn-regist");
        isSubmit = false;
    }
}

function popupReg() {
    var agreeProtocol = checkReadMe();
    var passed = validateRegName() && validateFunction.regValidate() && agreeProtocol;
    if (passed) {
        $("#popupRegButton").attr({ "disabled": "disabled" }).removeClass().addClass("btn-img btn-regist wait-btn");
        $.ajax({
            type: "POST",
            url: "../reg/regService?r=" + Math.random(),
            contentType: "application/x-www-form-urlencoded; charset=utf-8",
            data: $("#popupPersonRegForm").serialize(),
            success: function (result) {
                if (result) {
                    var obj = eval(result);
                    if (obj.info) {
                        alert(obj.info);
                        verc();
                        $("#popupRegButton").removeAttr("disabled").removeClass().addClass("btn-img btn-regist");
                        return;
                    }
                    if (obj.noAuth) {
                        verc();
                        window.parent.location = obj.noAuth;
                        return;
                    }
                    if (obj.success == true) {
                        window.parent.jdModelCallCenter.init(true);
                        return;
                    }
                }
            }
        });
    } else {
        $("#popupRegButton").removeAttr("disabled").removeClass().addClass("btn-img btn-regist");
    }
}

function popupContinueReg() {
    $("#protocolContent").removeClass().addClass("regist-bor hide");
    $("#popupPersonRegForm").show();

    popupReg();
}

function showProtocol() {
    $("#popupPersonRegForm").hide();
    $("#protocolContent").removeClass().addClass("regist-bor");

}
//$("#authcode").bind('keyup', function (event) {
//    if (event.keyCode == 13) {
//        $("#registsubmit").click();
//    }
//});

	
GET wl.js
	
304 Not Modified
	
cscssl.jd.com
	
0 B
	
58.83.206.162:443
	
 
341ms
 	
6 个请求
			
12.9 KB
	
(12.9 KB 来自缓存)
898ms (onload: 1.5s)
