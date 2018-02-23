jQuery(document).ready(function () {
    ErrorLevelEnum();
    jQuery("#insertRec").click(function () {
        postdata();
    });
});
function showAfterReg() {
    jQuery(document).ready(function () {
        jQuery('.showAfter').show();
    });
};
function reset() {
    jQuery(document).ready(function () {
        jQuery('#test_form').find('input[type=text],input[type=password],textarea').val("");
    });
}
function postdata() {
    var app = jQuery("#Application").val();
    var Level = jQuery("#Level").val();
    var SourcePage = jQuery("#SourcePage").val();
    var SourceFunction = jQuery("#SourceFunction").val();
    var Message = jQuery("#Message").val();
    var StackTrace = jQuery("#StackTrace").val();
    var strdata = {
        "Application": app,
        "Level": Level,
        "SourcePage": SourcePage,
        "SourceFunction": SourceFunction,
        "Message": Message,
        "StackTrace": StackTrace
    };
    var strurl = '/errorlog';
    jQuery.ajax({
        headers: { "Accept": "application/json" },
        type: 'POST',
        url: strurl,
        data: JSON.stringify(strdata),
        contentType: 'application/json',
        dataType: 'text',
        success: function () {
            showAfterReg();
            reset();
        }
    });
}
function ErrorLevelEnum() {
    strUrl = '/ErrorLevelEnum';
    jQuery.ajax({
        headers: { "Accept": "application/json" },
        type: 'GET',
        url: strUrl,
        dataType: 'json',
        async: false,
        contentType: 'application/json',
        success: function (xhr) {
            var jsonResponseAccessHistory = {
                "AccessHistory": xhr.result
            };
            jQuery("#form-group > #Level").html("");
            var templateAccessHistory = jQuery('#templateAccessHistory').html();
            Mustache.parse(templateAccessHistory);
            var renderedAccessHistory = Mustache.render(templateAccessHistory, jsonResponseAccessHistory);
            jQuery("[id$='form-group'] > #Level").append(renderedAccessHistory);
        },
        error: function (result, status, xhr) {
            alert('error');
        },
        beforeSend: function (xhr) {
        }
    })
}