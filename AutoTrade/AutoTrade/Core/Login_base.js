/* SVN.committedRevision=799527 */
function inputFocus() { $(".login_field input").focus(function () { $(this).addClass("cur") }); $(".login_field input").blur(function () { $(this).removeClass("cur") }); $(".login_field input").keydown(function () { $(".login_field span").hide() }) } function focus_un(b) {
    if (b == 1) { $("#un").focus(); $("#un").live("click keydown", function () { if ($("#un").val() == "E-mail/手机号码/用户名") { $("#un").val(""); $("#un").focus() } }); $("#un").mouseover(function () { $("#un").focus() }); $("#un").mouseout(function () { if (!$("#un").val()) { $("#un").val("E-mail/手机号码/用户名") } }) } else {
        if (b == 2) {
            $("#un").focus(); $("#un").live("click keydown", function () { if ($("#un").val() == "E-mail/手机号码/用户名") { $("#un").val(""); $("#un").focus() } }); $("#un").mouseover(function () { $("#un").focus() }); $("#un").mouseout(function () {
                if (!$("#un").val()) {
                    $("un").val("E-mail/手机号码/用户名")
                }
            })
        }
    }
} function accountValueInit(d) { var c = getCookie("ac"); if (c) { d.val(decodeURIComponent(c)) } else { if (!d.val()) { d.val("E-mail/手机号码/用户名") } } return d.val() } function accountBlurIsShowValidCode(l, g, j, i) {
    var k = l.val(); var h = true; l.blur(function () {
        l.removeClass("cur"); if (showValidCode != 1 && l.val() && l.val() != "E-mail/手机号码/用户名" && (l.val() != k || h)) {
            k = l.val(); h = false; var b = { "credentials.username": l.val() }; var a = URLPrefix.passport + "/publicPassport/showValidate.do";
            jQuery.post(a, b, function (c) {

                if (c) {
                    if (c.ShowValidCode == 1) {
                        passport_refresh_valid_code(); g.show(); $("#vcd").attr("tabindex", "3"); $("#vcd").val(""); $("#autoLoginDiv").hide(); $("#isAutoLogin").attr("value", "0")
                    } else { if (!g.is(":hidden")) { $("#vcd_div").hide(); $("#autoLoginDiv").show(); $("#autoLoginDiv").attr("tabindex", "3") } } 
                } 
            })
        } 
    })
} function init_valid_cod(c) {
    if (document.all) {
        c.click()
    } else { var d = document.createEvent("MouseEvents"); d.initEvent("click", true, true); c.click() }
} function loginCoopSwitch(c) { var d; if ((currSiteId == 1 && siteType == 2)) { d = 2 } else { if (currSiteId == 2) { d = 3 } } $(".login_coop_switch li").click(function () { $(this).addClass("cur").siblings().removeClass("cur"); $(c).hide(); $(c).eq($(this).index()).show(); if (stringTrim(this.innerHTML) == "支付宝") { openAlipay(d) } else { if (stringTrim(this.innerHTML) == "新浪微博") { openSina(d) } else { if (stringTrim(this.innerHTML) == "腾讯QQ") { openTencent(d) } } } }) } function loginRememb(b) { $(b).hover(function () { $(this).addClass("login_safe_hover"); $(this).next().show() }, function () { $(this).removeClass("login_safe_hover"); $(this).next().hide() }) } function isShowAuto() {
    if (autoLoginFlag == 1) {
        loginRememb("#loginRememb"); jQuery("#autoLoginDiv").show(); jQuery("#autoLoginCheck").click(function () {
            var b = jQuery("#isAutoLogin").val();
            if (jQuery("#autoLoginCheck").attr("checked")) { jQuery("#isAutoLogin").attr("value", "1") } else { jQuery("#isAutoLogin").attr("value", "0") }
        })
    }
} function isShowValidCode() { if (showValidCode == 1) { passport_refresh_valid_code(); $("#vcd_div").show(); $("#vcd").attr("tabindex", "3"); $("#vcd").val(""); $("#autoLoginDiv").hide(); $("#isAutoLogin").attr("value", "0") } if (jQuery("#vcd")) { $("#vcd").blur(function () { $("#vcd").removeClass("cur"); if ($("#vcd").val() == "") { $("#vcd_desc").text("不能为空"); $("#vcd_desc").show() } }) } } function getCookie(h) { var g = document.cookie.split(";"); for (var e = 0; e < g.length; e++) { var f = g[e].split("="); if (f[0].replace(/(^\s*)|(\s*$)/g, "") == h) { return f[1] } } return "" } function passport_refresh_valid_code() {
    var c = $("#valid_code_pic"); var d = "/passport/valid_code.do"; if (valid_code_service_flag == 1) {
        d = URLPrefix.validCodeShowUrl
    } if (c) { c.attr("src", d + "?t=" + Math.random()) }
} function forgetAccount(b) { if (b == 1) { $("#accountDesc").text("您可以用邮箱、绑定手机登录"); $("#accountDesc").show(); $("#un").focus() } else { if (b == 2) { $("#accountDesc").html("您可以用邮箱、绑定手机登录"); $("#accountDesc").show(); $("#un").focus() } } } function doEnter() { $("#pwd,#vcd,#login_button").keydown(function (b) { b.stopPropagation(); if (b.keyCode == 13) { if (jQuery.browser.msie && jQuery.browser.version == "6.0") { jQuery("#login_button").trigger("click") } else { jQuery("#login_button").focus().click() } } }) } function checkAccount_beforeLogin() {
    var b = $("#un").val(); if (b == "" || b == "E-mail/手机号码/用户名") { $("#accountDesc").text("登录账号不能为空"); $("#accountDesc").show(); $("#un").focus(); return false } else {
        if (stringLen(b) > 100) {
            $("#accountDesc").text("账号长度不能超过100位"); $("#accountDesc").show(); $("#un").focus(); return false
        } else { if (b.toLowerCase().indexOf("<script") > -1 || b.toLowerCase().indexOf("<\/script") > -1) { $("#accountDesc").html("账号中包含非法字符"); $("#accountDesc").show(); $("#un").focus(); return false } }
    } return true
} function checkValidCode_beforeLogin() { if ($("#vcd_div").is(":hidden")) { return true } if ($("#vcd").val() == "") { $("#vcd_desc").text("不能为空"); $("#vcd_desc").show(); $("#vcd").focus(); return false } return true } function checkAccount_beforeUnionYhdLogin() {
    var b = $("#un_yhd").val(); if (b == "" || b == "E-mail/手机号码/用户名") { $("#accountDesc_yhd").text("登录账号不能为空"); $("#accountDesc_yhd").show(); $("#un_yhd").focus(); return false } else {
        if (stringLen(b) > 100) { $("#accountDesc_yhd").text("账号长度不能超过100位"); $("#accountDesc_yhd").show(); $("#un_yhd").focus(); return false } else {
            if (b.toLowerCase().indexOf("<script") > -1 || b.toLowerCase().indexOf("<\/script") > -1) {
                $("#accountDesc_yhd").html("账号中包含非法字符");
                $("#accountDesc_yhd").show(); $("#un_yhd").focus(); return false
            }
        }
    } return true
} function checkPwd_beforeLogin() { var d = /\s+/; var c = $("#pwd").val(); if ($("#pwd").val() == "") { $("#pwd_desc").text("密码不能为空"); $("#pwd_desc").show(); $("#pwd").focus(); return false } else { if (d.test(c)) { $("#pwd_desc").text("密码不能有空格"); $("#pwd_desc").show(); $("#pwd").focus(); return false } } return true } function checkPwd_beforeUnionYhdLogin() { var d = /\s+/; var c = $("#pwd_yhd").val(); if ($("#pwd_yhd").val() == "") { $("#pwd_desc_yhd").text("密码不能为空"); $("#pwd_desc_yhd").show(); $("#pwd_yhd").focus(); return false } else { if (d.test(c)) { $("#pwd_desc_yhd").text("密码不能有空格"); $("#pwd_desc_yhd").show(); $("#pwd_yhd").focus(); return false } } return true } function stringTrim(b) { return b.replace(/(^\s*)|(\s*$)/g, "") } function stringLen(c) {
    c = stringTrim(c); var d = 0;
    if (c) { d = c.replace(/[^\x00-\xff]/g, "***").length } return d
} function double_submit() {
    $(".login_field span").hide(); if (!checkAccount_beforeLogin()) { return false } if (!checkPwd_beforeLogin()) { return false } if (!checkValidCode_beforeLogin()) { return false } var d = { "credentials.username": $("#un").val(), "credentials.password": $("#pwd").val(), validCode: $("#vcd").val(), loginSource: $("#login_source").val(), returnUrl: returnUrl, isAutoLogin: jQuery("#isAutoLogin").val() }; var c = URLPrefix.passport + "/publicPassport/login.do";
     jQuery.post(c, d, function (a) {
        if (a) {
            if (a.errorCode != 0) {
                if (a.ShowValidCode == 1) { $("#autoLoginDiv").hide(); $("#isAutoLogin").attr("value", "0"); passport_refresh_valid_code(); $("#vcd_div").show(); $("#vcd").attr("tabindex", "3") } if (a.errorCode == 1) {
                    $("#pwd_desc").text("用户名、密码不正确"); $("#pwd_desc").show(); $("#pwd").focus()
                } else {
                    if (a.errorCode == 2) { $("#vcd_desc").text("验证码不正确"); $("#vcd_desc").show(); $("#vcd").focus() } else {
                        if (a.errorCode == 3) { $("#accountDesc").text("用户名、密码不正确"); $("#accountDesc").show(); $("#un").focus() } else {
                            if (a.errorCode == 4) { $("#accountDesc").text("请通过支付宝联合登录1号店"); $("#accountDesc").show() } else {
                                if (a.errorCode == 5) { if ($("#login_source").val() == 1) { window.location.href = "/passport/goToPage.do" } else { if ($("#login_source").val() == 2) { var b = encodeURIComponent(URLPrefix.passport + "/passport/goToPage.do"); window.location = resetIframeUrl + "?result=" + LOGIN_RESULT.FAIL + "&exceptionUrl=" + b } } return } else {
                                    if (a.errorCode == 6) {
                                        if ($("#login_source").val() == 1) { showAgreeNewContract(a.userSiteType, a.random, a.returnUrl, null, $("#isAutoLogin").val()) } else {
                                            showAgreeNewContract(a.userSiteType, a.random, a.returnUrl, resetIframeUrl, $("#isAutoLogin").val())
                                        } return
                                    } else {
                                        if (a.errorCode == 7) { if ($("#login_source").val() == 1) { showAuth(a.userSiteType, a.random, a.returnUrl, null, $("#isAutoLogin").val()) } else { showAuth(a.userSiteType, a.random, a.returnUrl, resetIframeUrl, $("#isAutoLogin").val()) } return } else {
                                            if (a.errorCode == 11) { $("#pwd_desc").html("密码错还可试2次"); $("#pwd_desc").show(); $("#pwd").focus(); return } else {
                                                if (a.errorCode == 12) { $("#pwd_desc").html("密码错还可试1次"); $("#pwd_desc").show(); $("#pwd").focus(); return } else {
                                                    if (a.errorCode == 13) { $("#pwd_desc").html("您的账户异常，预计1个工作日内处理完毕"); $("#pwd_desc").show(); return } else {
                                                        if (a.errorCode == 14) { $("#vcd_desc").text("登录异常，请输入验证码"); $("#vcd_desc").show(); $("#vcd").focus(); return } else {
                                                            if (a.errorCode == -9) {
                                                                if ($("#login_source").val() == 1) { window.location.href = "/passport/toSafeNoticFrom.do" } else {
                                                                    if ($("#login_source").val() == 2) {
                                                                        var b = encodeURIComponent(URLPrefix.passport + "/passport/toSafeNoticFrom.do");
                                                                        window.location = resetIframeUrl + "?result=" + LOGIN_RESULT.FAIL + "&exceptionUrl=" + b
                                                                    }
                                                                } return
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            } else { if (a.errorCode == 0) { var f = ""; if ($("#login_source").val() == 1) { f = a.returnUrl } else { if ($("#login_source").val() == 2) { f = resetIframeUrl + "?result=" + LOGIN_RESULT.SUCCESS } } if (currSiteId == 1) { loginSyncCookie(a.isAgreeAuth, f) } else { window.location = f } } }
        }
    })
} function double_unionYhd_submit() {
    $(".login_field span").hide(); if (!checkAccount_beforeUnionYhdLogin()) { return false } if (!checkPwd_beforeUnionYhdLogin()) { return false } var d = { "credentials.username": $("#un_yhd").val(), "credentials.password": $("#pwd_yhd").val(), validCode: $("#vcd_yhd").val(), loginSource: $("#login_source").val(), returnUrl: returnUrl, isAutoLogin: jQuery("#isAutoLogin_yhd").val() }; var c = URLPrefix.passport + "/publicPassport/login_special.do"; jQuery.post(c, d, function (a) {
        if (a) {
            if (a.errorCode != 0) {
                if (a.ShowValidCode == 1) {
                    init_valid_cod($("#rvcd_yhd"));
                    $("#vcd_div_yhd").show()
                } if (a.errorCode == 1) { $("#pwd_desc_yhd").text("用户名、密码不正确"); $("#pwd_desc_yhd").show(); $("#pwd_yhd").focus() } else {
                    if (a.errorCode == 2) { $("#vcd_desc_yhd").text("验证码不正确"); $("#vcd_desc_yhd").show(); $("#vcd").focus() } else {
                        if (a.errorCode == 3) { $("#accountDesc_yhd").text("用户名、密码不正确"); $("#accountDesc_yhd").show(); $("#un_yhd").focus() } else {
                            if (a.errorCode == 4) { $("#accountDesc").text("请通过支付宝联合登录1号店"); $("#accountDesc").show() } else {
                                if (a.errorCode == 5) { if ($("#login_source").val() == 1) { window.location.href = "/passport/goToPage.do" } if ($("#login_source").val() == 2) { var b = encodeURIComponent(URLPrefix.passport + "/passport/goToPage.do"); window.location = resetIframeUrl + "?result=" + LOGIN_RESULT.FAIL + "&exceptionUrl=" + b } if ($("#login_source").val() == 3) { } return } else {
                                    if (a.errorCode == 6) {
                                        if ($("#login_source").val() == 1) {
                                            showAgreeNewContract(a.userSiteType, a.random, a.returnUrl)
                                        } else { showAgreeNewContract(a.userSiteType, a.random, a.returnUrl, resetIframeUrl) } return
                                    } else {
                                        if (a.errorCode == 7) { if ($("#login_source").val() == 1) { showAuth(a.userSiteType, a.random, a.returnUrl) } else { showAuth(a.userSiteType, a.random, a.returnUrl, resetIframeUrl) } return } else {
                                            if (a.errorCode == 11) { $("#pwd_desc_yhd").html("密码错还可试2次"); $("#pwd_desc_yhd").show(); $("#pwd_yhd").focus(); return } else {
                                                if (a.errorCode == 12) { $("#pwd_desc_yhd").html("密码错还可试1次"); $("#pwd_desc_yhd").show(); $("#pwd_yhd").focus(); return } else {
                                                    if (a.errorCode == -9) {
                                                        if ($("#login_source").val() == 1) { window.location.href = "/passport/toSafeNoticFrom.do" } else { if ($("#login_source").val() == 2) { var b = encodeURIComponent(URLPrefix.passport + "/passport/toSafeNoticFrom.do"); window.location = resetIframeUrl + "?result=" + LOGIN_RESULT.FAIL + "&exceptionUrl=" + b } } return
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            } else { if (a.errorCode == 0) { var f; if ($("#login_source").val() == 1) { f = a.returnUrl } else { if ($("#login_source").val() == 2) { f = resetIframeUrl + "?result=" + LOGIN_RESULT.SUCCESS } } if (currSiteId == 1) { loginSyncCookie(a.isAgreeAuth, f) } else { window.location = f } } }
        }
    })
} function jumpOut(c) { var d = encodeURIComponent(URLPrefix.passport + "/passport/login_input.do"); redirectUrl = resetIframeUrl + "?result=1&exceptionUrl=" + d; window.location = redirectUrl } function resetLocation(f, g) {
    var h = g; if ($("#login_source").val() == LOGIN_SOURCE.FRAME) { if (g && g.lastIndexOf("/cart/cart.do?action=view") < 0) { var e = encodeURIComponent(g); h = resetIframeUrl + "?result=" + f + "&exceptionUrl=" + e } else { h = resetIframeUrl + "?result=" + f } } if (currSiteId == 1 && f == LOGIN_RESULT.SUCCESS) {
        isAgreeAuth(function (a) {
            var b = 0; if (a && a.isAgreeAuth == 1) { b = 1 } loginSyncCookie(b, h)
        })
    } else { window.location = h }
} function register() { var c = /^[a-zA-Z0-9][.\w]+@[a-zA-Z0-9]+(\.[a-zA-Z0-9]+)+$/; var d = /^[a-zA-Z]\w{3,15}$/; if (d.test($("#un").val())) { $("#r_name").val($("#un").val()) } else { if (c.test($("#un").val())) { $("#r_email").val($("#un").val()) } } $("#registerform").submit() } function regYihaodianUser(d) {
    if (d == DOMAIN_TYPE.MALL) { if (!returnUrl) { returnUrl = URLPrefix.mall } var e = URLPrefix.passportother + "/legal/showAuth.do?returnUrl=" + encodeURIComponent(returnUrl) + "&currentDomain=0&mustLogout=1"; var f = yhdPassportUrl + "/passport/register_input.do?fromSiteType=" + DOMAIN_TYPE.MALL + "&returnUrl=" + encodeURIComponent(e); window.open(f) } if (d == DOMAIN_TYPE.YW_111) {
        if (!returnUrl) { returnUrl = ywPassportUrl + "/passport/login_input.do" } var f = yhdPassportUrl + "/passport/register_input.do?fromSiteType=" + DOMAIN_TYPE.YW_111 + "&returnUrl=" + encodeURIComponent(returnUrl);
        window.open(f)
    }
} function openSina(b) { if (b == DOMAIN_TYPE.MALL) { window.open(mallPassportUrl + "/sina/login.do") } if (b == DOMAIN_TYPE.YW_111) { window.open(ywPassportUrl + "/sina/login.do") } $("#snpic").html("<img src='" + $("#imgpath").val() + "/login_coop_weibo.png'/>") } function openAlipay(b) { if (b == DOMAIN_TYPE.MALL) { window.open(mallPassportUrl + "/alipay/login.do") } if (b == DOMAIN_TYPE.YW_111) { window.open(ywPassportUrl + "/alipay/login.do") } $("#alpic").html("<img src='" + $("#imgpath").val() + "/login_coop_alipay.png'/>") } function openTencent(b) { if (b == DOMAIN_TYPE.MALL) { window.open(mallPassportUrl + "/qq/login.do") } if (b == DOMAIN_TYPE.YW_111) { window.open(ywPassportUrl + "/qq/login.do") } $("#qqpic").html("<img src='" + $("#imgpath").val() + "/login_coop_qq.png'/>") } function showAgreeNewContract(q, p, l, j, m) {
    var o = ""; if (q && q == siteType) {
        o = URLPrefix.passport;
        var k = encodeURIComponent(l); var r = o + "/legal/showNewContract.do?random=" + p + "&returnUrl=" + k + "&isAutoLogin=" + m; if (j) { r = j + "?result=" + LOGIN_RESULT.FAIL + "&exceptionUrl=" + encodeURIComponent(r) } window.location = r
    } else { o = URLPrefix.passportother; var n = 0; if (siteType == 2) { n = 1 } var k = encodeURIComponent(l); var r = o + "/legal/showAuth.do?random=" + p + "&returnUrl=" + k + "&showContract=" + n + "&isAutoLogin=" + m; if (j) { r = j + "?result=" + LOGIN_RESULT.SUCCESS + "&exceptionUrl=" + encodeURIComponent(r) } window.location = r }
} function showAuth(q, p, m, j, n) {
    var o = URLPrefix.passport; var l = 1; if (q && q != siteType) { o = URLPrefix.passportother; l = 0 } var k = encodeURIComponent(m); var r = o + "/legal/showAuth.do?random=" + p + "&currentDomain=" + l + "&isAutoLogin=" + n + "&returnUrl=" + k; if (j) { r = j + "?result=" + LOGIN_RESULT.SUCCESS + "&exceptionUrl=" + encodeURIComponent(r) } window.location = r
} function pageInit() { if (jQuery("#un")) { accountValueInit(jQuery("#un")); accountBlurIsShowValidCode($("#un"), $("#vcd_div"), $("#valid_code_pic"), $("#rvcd")) } if (jQuery("#un_yhd")) { accountValueInit(jQuery("#un_yhd")); accountBlurIsShowValidCode($("#un_yhd"), $("#vcd_div_yhd"), $("#valid_code_pic_yhd"), $("#rvcd_yhd")) } if ((currSiteId == 1 && siteType == 2) || currSiteId == 2) { loginCoopSwitch(".login_form") } inputFocus(); isShowAuto(); isShowValidCode(); doEnter(); focus_un(currSiteId) };