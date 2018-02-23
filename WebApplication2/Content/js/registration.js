jQuery(document).ready(function () {
    jQuery("#register").click(function () {
        postData();
    })
});
function showAfterReg() {
    jQuery(document).ready(function () {
        jQuery('.showAfter').show();
        jQuery('.hide_alreadyLogIn_msg').hide();
    });
};
function alertMsgIfUserAlreadyExist() {
    jQuery(document).ready(function () {
        jQuery('.if_already_exist_alert').show();
    })
}
function fillValidCredentials() {
    jQuery(document).ready(function () {
        jQuery('.validCredentials').show();
    })
}
function reset() {
    jQuery(document).ready(function () {
        jQuery('#reg_form').find('input[type=text],input[type=password]').val("");
    });
}
function postData() {
    var userName = jQuery("#userName").val();
    var userEmail = jQuery("#userEmail").val();
    var userPassword = jQuery("#userPassword").val();
    var strData = {
        "userName":userName,
        "userEmail":userEmail,
        "userPassword":userPassword
    };
    strUrl = '/Register';
    jQuery.ajax({
        headers: { "Accept": "application/json" },
        type: 'POST',
        url: strUrl,
        data: JSON.stringify(strData),
        contentType: 'application/json',
        dataType: 'text',
        success: function (data) {
            showAfterReg();
            reset();
        },
        error: function (jqXHR, textStatus, errorThrown) {  
            if (jqXHR.status == 401) {
                alertMsgIfUserAlreadyExist();
            }
            if (jqXHR.status == 500) {
                fillValidCredentials();
            }
        }
    });
}
setInterval(function hideMe() {
    jQuery('.validCredentials').css({ 'display': 'none' });
    jQuery('.if_already_exist_alert').css({ 'display': 'none' });
}, 5000);
//function isValidPage() {
//    //Remove Existing error Message
//    jQuery(".uvFormFlashMessage").html("");
//    if (!jQuery("#reg_form").isValid(null, { errorMessagePosition: jQuery('.uvFormFlashMessage') }, true)) {
//        jQuery(".uvFormFlash-error").css("display", "block");
//        return false;
//    }
//    else {
//        jQuery(".uvFormFlash-error").css("display", "none");
//        return true;
//    }
//}