jQuery(document).ready(function () {
    attachevent();
    jQuery(document).bind('keypress', function (e) {
        if (e.keyCode == 13) {
            jQuery("#login").trigger('click');
            return false;
        }
    });
});
function attachevent() {
    jQuery("#login").click(function () {
            mylogin();
    });
};
function showAfterReg() {
    jQuery(document).ready(function () {
        jQuery('.showAfter').show();
    });
};
function mylogin() {
    var userEmail = jQuery("#userEmail").val();
    var password = jQuery("#userPassword").val();
    
    if (jQuery("#userEmail").val() =="")
    {
        showAfterReg();
        jQuery("#userEmail").focus();
    }
    else if (jQuery("#userPassword").val() == "")
    {
        showAfterReg();
        jQuery("#userPassword").focus();
    }
    else
    {
        location.href = "/UserDetails";
    }



    //var strUrl = "";
    //if (jQuery("#userEmail").val() != "" && jQuery("#userPassword").val() != "") {
    //    strUrl = '/login?userEmail=' + userEmail + "&userPassword=" + password;
    //}
    ////else {
    ////    strUrl = '/login?userEmail=' + "&userPassword=";
    ////}
    //jQuery.ajax({
    //    headers: { "Accept": "application/json" },
    //    type: 'GET',
    //    url: strUrl,
    //    dataType: 'json',
    //    contentType: 'application/json',
    //    success: function (xhr) {
    //        location.href = "/dashboard";
    //    },
    //    error: function (result, status, xhr) {
    //        showAfterReg();
    //        //alert('Please enter valid login credential.');
    //    },
    //    beforeSend: function (xhr) {
    //    }

    //});

};


