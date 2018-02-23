jQuery(document).ready(function () {
    getAppAtDash();
    getAlert();
    jQuery("#distinctApps").trigger("change");
})
function getAlert() {
    jQuery("#distinctApps").change(function () {
        applicationname = jQuery("#distinctApps option:selected").val();
        var selected = jQuery("#distinctApps option:eq(0)").val();
        if (applicationname == selected) {
            jQuery(".commomsearch").attr("href", "error?" + "searchData=" + "");
            jQuery(".commonWidget").attr("href", "error?" + "searchData=" + "");
        }
        else {
            jQuery(".commomsearch").attr("href", "error?" + "searchData=" + applicationname);
            jQuery(".commonWidget").attr("href", "error?" + "searchData=" + applicationname);
        }
        totalError(applicationname);
        TodayAppWiseError(applicationname);
        getErrorPerHour(applicationname);
    });
};
function totalError(applicationname) {
    strUrl = '/getAllErrorCount/' + applicationname;
    jQuery.ajax({
        headers: { "Accept": "application/json" },
        type: 'GET',
        url: strUrl,
        dataType: 'json',
        contentType: 'application/json',
        success: function (xhr, data) {
            jQuery('#totalError').html("&nbsp;" + xhr.result);
        },
        error: function (result, status, xhr) {
            alert('error');
        },
        beforeSend: function (xhr) {
        }
    })
}
function TodayAppWiseError(applicationname) {
    strUrl = '/getTodayErrorCount/' + applicationname;
    jQuery.ajax({
        headers: { "Accept": "application/json" },
        type: 'GET',
        url: strUrl,
        dataType: 'json',
        contentType: 'application/json',
        success: function (xhr, data) {
            jQuery('#todayError').html("&nbsp;" + xhr.result);
        },
        error: function (result, status, xhr) {
            alert('error');
        },
        beforeSend: function (xhr) {
        }
    })
}
function getErrorPerHour(applicationname) {
    strUrl = '/getErrorPerHour/' + applicationname;
    jQuery.ajax({
        headers: { "Accept": "application/json" },
        type: 'GET',
        url: strUrl,
        dataType: 'json',
        contentType: 'application/json',
        success: function (xhr, data) {
            if (jQuery(xhr.result).length > 0) {
                jQuery('#errorPerHour').html("&nbsp;" + xhr.result);
            } else {
                jQuery('#errorPerHour').html("&nbsp; 0");
            }
            
        },
        error: function (result, status, xhr) {
            alert('error');
        },
        beforeSend: function (xhr) {
        }
    })
}
function getAppAtDash() {
    strUrl = '/getAppAtDash';
    jQuery.ajax({
        headers: { "Accept": "application/json" },
        type: 'GET',
        url: strUrl,
        dataType: 'json',
        async:false,
        contentType: 'application/json',
        success: function (xhr) {
            var jsonResponseAccessHistory = {
                "AccessHistory": xhr.result
            };
            jQuery("#form-group > #distinctApps").html("");
            var templateAccessHistory = jQuery('#templateAccessHistory').html();
            Mustache.parse(templateAccessHistory);
            var renderedAccessHistory = Mustache.render(templateAccessHistory, jsonResponseAccessHistory);
            jQuery("[id$='form-group'] > #distinctApps").append(renderedAccessHistory);
        },
        error: function (result, status, xhr) {
            alert('error');
        },
        beforeSend: function (xhr) {
        }
    })
}
setInterval(function hideMe() {
    jQuery('.welcomemsg').css({ 'display': 'none' });
}, 10000);