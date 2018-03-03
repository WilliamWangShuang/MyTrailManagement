//<<Client side validations>>

// validate first name
function registor_validate_first_name() {
    var inputFName = $("input[id$='txtFirstName']") ;
    if ($(inputFName) === null || $(inputFName).val().length == 0) {
        $("#spanClientSideValidateFirstName").show();
    } else {
        $("#spanClientSideValidateFirstName").hide();
    }
}

// validate last name
function registor_validate_last_name() {
    var inputLName = $("input[id$='txtLastName']");
    if ($(inputLName) === null || $(inputLName).val().length == 0) {
        $("#spanClientSideValidateLastName").show();
    } else {
        $("#spanClientSideValidateLastName").hide();
    }
}

//validate Email
function registor_validate_email() {
    var inputEmail = $("input[id$='txtEmail']");
    if ($(inputEmail) === null || $(inputEmail).length == 0) {
        $("#spanClientSideValidateEmail").show();
    } else {
        if ($(inputEmail).val().indexOf('@') <= -1)
            $("#spanClientSideValidateEmail").show();
        else
            $("#spanClientSideValidateEmail").hide();
    }
}

//validate password
function registor_validate_password() {
    var inputPwd = $("input[id$='txtPwd']");
    if ($(inputPwd) === null || $(inputPwd).val().length == 0) {
        $("#spanClientSideValidatePwd").show();
        $("#spanClientSideValidateConfirmPwd").hide();
    } else {
        if ($(inputPwd).val().length < 8 
            || ($(inputPwd).val().indexOf('@') <= -1 && $(inputPwd).val().indexOf('/') <= -1 && $(inputPwd).val().indexOf('*') <= -1 && $(inputPwd).val().indexOf('&') <= -1 && $(inputPwd).val().indexOf('%') <= -1 && $(inputPwd).val().indexOf('$') <= -1 && $(inputPwd).val().indexOf('#') <= -1)
            || $(inputPwd).val().match(/\d+/g) === null)
            $("#spanClientSideValidatePwd").show();
        else
            $("#spanClientSideValidatePwd").hide();
    }
}

// validate confirm password
function registor_validate_confrim_password() {
    var inputConfirmPwd = $("input[id$='txtConirmPwd']");
    if ($("input[id$='txtPwd']") === null || $("input[id$='txtPwd']").val().length == 0) {
        $("#spanClientSideValidateConfirmPwd").hide();
        return;
    }

    if ($(inputConfirmPwd) === null || $(inputConfirmPwd).length == 0) {
        $("#spanClientSideValidateConfirmPwd").show();
    } else {
        if ($(inputConfirmPwd).val() !== $("input[id$='txtPwd']").val())
            $("#spanClientSideValidateConfirmPwd").show();
        else
            $("#spanClientSideValidateConfirmPwd").hide();
    }
}

// validate empty password when login
function login_empty_password() {
    var pwd = $("input[id$='txtPwd']");
    if ($(pwd).val() === null || $(pwd).val().length == 0)
        $("#spanClientSideValidatePwd").show();
    else
        $("#spanClientSideValidatePwd").hide();
}

// validate login form on client side to ensure no empty value for password and email(account), and email in correct format
function login_check_form() {
    registor_validate_email();
    login_empty_password();

    if ($("#spanClientSideValidateEmail").is(":hidden") &&
        $("#spanClientSideValidatePwd").is(":hidden")) {
        $("#btnLogin").click();
    }
}

function confirm_login() {
    var email = $("input[id$='txtEmail']").val();
    var password = $("input[id$='txtPwd']").val();

    var jsonObject = {
        "Email": email,
        "Password": password
    };

    $.ajax({
        url: "/Customers/Login",
        type: "POST",
        data: JSON.stringify(jsonObject),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            isFail = data.match("^Fail");
            if (!isFail) {
                $(".fakeloader").fakeLoader({

                    // Time in milliseconds for fakeLoader disappear
                    timeToHide: 9000,

                    // 'spinner1', 'spinner2', 'spinner3', 'spinner4', 'spinner5', 'spinner6', 'spinner7' 
                    spinner: "spinner4",
                    bgColor: "#929292"

                });
                window.location.href = data;
            }
            else {
                $("#btnShowInvalidAccMsg").click();
            }
        }
    });
}

function create_user_check_form() {
    registor_validate_first_name();
    registor_validate_last_name();
    registor_validate_email();
    registor_validate_password();
    registor_validate_confrim_password();

    if($("#spanClientSideValidateFirstName").is(":hidden") && 
       $("#spanClientSideValidateLastName").is(":hidden") && 
       $("#spanClientSideValidateEmail").is(":hidden") && 
       $("#spanClientSideValidatePwd").is(":hidden") && 
       $("#spanClientSideValidateConfirmPwd").is(":hidden")) {
            $("#btnCreateUser").click();
    }
}

//<<end client side validations>>

function fblogin() {
    FB.login(function (response) {
        //...
    }, { scope: 'read_stream,publish_stream,offline_access' });
}


$(document).ready(function () {
    var tooltips = $("[title]").tooltip({
        position: {
            my: "left top",
            at: "right+5 top-5",
            collision: "none"
        }
    });  
});