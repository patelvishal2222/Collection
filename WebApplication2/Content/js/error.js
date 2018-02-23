jQuery(document).ready(function () {
    attachEvents();
    configureMail();
    configureMessage();
    function getParamValuesByName(querystring) {
        if (querystring != "") {
            var qstring = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
            for (var i = 0; i < qstring.length; i++) {
                var urlparam = qstring[i].split('=');
                if (urlparam[0] == querystring) {
                    return decodeURIComponent(urlparam[1]);
                }
            }
        }
    }
    var searchData = getParamValuesByName('searchData');
    jQuery("#searchData").val(searchData);
    SearchErrorLog(1);
    jQuery(".new_recipient").click(function () {
        jQuery(".new_rec").append('<input type="text" class="form-control" style="margin-top:5px;">');
    });
    jQuery('#myModal').modal({
        keyboard: true,
        backdrop: "static",
        show: false,
    }).on('show', function () {
    });
    jQuery("body").on('click', '#accessData', function () {
        //jQuery('#errorDetails').html(jQuery('<table class="table table-bordered">' + '<tr>  <td> APPLICATION </td> ' + " <td  class=active>" + jQuery(this).data('id') + "</td></tr>" + '<tr>  <td>TIMESTAMP</td> ' + " <td>" + jQuery(this).data('app') + "</td></tr>" + '<tr>  <td>LEVEL</td> ' + " <td>" + jQuery(this).data('level') + "</td></tr>" + '<tr>  <td>SOURCE PAGE</td> ' + " <td>" + jQuery(this).data('page') + "</td></tr>" + '<tr>  <td>SOURCE FUNCTION</td> ' + " <td>" + jQuery(this).data('function') + "</td></tr>" + '<tr>  <td>MESSAGE</td> ' + " <td>" + jQuery(this).data('message') + "</td></tr>" + '<tr>  <td>TERMINAL ID</td> ' + " <td>" + jQuery(this).data('terminal') + "</td></tr>" + '<tr>  <td>STATUS</td> ' + " <td  class=success>" + jQuery(this).data('status') + "</td></tr>" + '</table>'));
        jQuery('#errorDetails').html(jQuery('<table class="table table-bordered">' + '<tr>  <td> APPLICATION </td> ' + " <td  class=active>" + jQuery(this).data('id') + "</td></tr>" + '<tr>  <td>TIMESTAMP</td> ' + " <td>" + jQuery(this).data('app') + "</td></tr>" + '<tr>  <td>LEVEL</td> ' + " <td>" + jQuery(this).data('level') + "</td></tr>" + '<tr>  <td>SOURCE FUNCTION</td> ' + " <td>" + jQuery(this).data('function') + "</td></tr>" + '<tr>  <td>MESSAGE</td> ' + " <td>" + jQuery(this).data('message') + "</td></tr>" + '<tr>  <td>STATUS</td> ' + " <td  class=success>" + jQuery(this).data('status') + "</td></tr>" + '</table>'));
        //jQuery('#ErrorDetailedMessage').html(jQuery('<div class="alert alert-success">' + jQuery(this).data('message') + '</div>'));
        //jQuery('#stackTrace').html(jQuery('<div class="alert alert-danger">' + jQuery(this).data('stacktrace') + '</div>'));
        jQuery('#ErrorDetailedMessageText').val(jQuery(this).data('message'));
        jQuery('#stackTraceText').val(jQuery(this).data('stacktrace'));
        jQuery('#myModal').modal('show');
    });
    jQuery(document).bind('keypress', function (e) {
        if (e.keyCode == 13) {
            jQuery("#Btnsearch").trigger('click');
            return false;
        }
    });
})
function attachEvents() {
    jQuery("#Btnsearch").click(function () {
        SearchErrorLog(1);
    });
    jQuery("#recordPerPage").change(function () {       
        SearchErrorLog(1);
    });
    jQuery("#jumpto").change(function () {
        SearchErrorLog(jQuery(this).val());
    });
    jQuery('.dismiss-modal').click(function () {
        reset();
    });
    jQuery('#BackToMessage').click(function () {
        configureMail();
    });
}
function SearchErrorLog(inPageNumber) {
    var strUrl = "";
    if (jQuery("#searchData").val() != "" && jQuery("#searchBy").val() != "") {
        strUrl = '/search/errorlogs?searchData=' + jQuery("#searchData").val() + "&searchBy=" + jQuery("#searchBy").val();
    }
    else {
        strUrl = '/search/errorlogs';
    }
    //alert(inPageNumber);
    var strdata = {
        "PageSize": jQuery("#recordPerPage").val(),
        "CurrentPage": inPageNumber,
        "Application": jQuery("#searchBy").val() == "1" ? jQuery("#searchData").val() : "",
        "SourcePage": jQuery("#searchBy").val() == "2" ? jQuery("#searchData").val() : "",
        "SourceFunction": jQuery("#searchBy").val() == "3" ? jQuery("#searchData").val() : ""
    };
    jQuery.ajax({
        headers: { "Accept": "application/json" },
        type: 'GET',
        url: strUrl,
        data: strdata,
        dataType: 'json',
        async:false,
        contentType: 'application/json',
        success: function (xhr) {
            var jsonResponseAccessHistory = {
                "AccessHistory": xhr.result
            };
            jQuery("#tblAccessHistory > tbody").html("");
            var templateAccessHistory = jQuery('#templateAccessHistory').html();
            Mustache.parse(templateAccessHistory);
            var renderedAccessHistory = Mustache.render(templateAccessHistory, jsonResponseAccessHistory);
            jQuery("[id$='tblAccessHistory'] > tbody").append(renderedAccessHistory);
            //Fill the page numbers            
            FillPageNumbers(xhr.totalPages);
            jQuery("#gotoPage").text(xhr.totalPages);
            jQuery("[id$='jumpto']").val(xhr.currentPage);
        },
        error: function (result, status, xhr) {
            alert('error');
        },
        beforeSend: function (xhr) {
        }
    });
}

/// <summary>
/// This function will fill the page number(s) list based on totalrows returned by AJAX call.
/// </summary>
function FillPageNumbers(totalPages) {

    var listItems = "";

    for (var intCounter = 1; intCounter <= totalPages; intCounter++) {
        listItems += "<option value='" + intCounter + "'>" + intCounter + "</option>";
    }

    jQuery("[id$='jumpto']").html(listItems);
}
function configureMail() {
    jQuery('.btn-mail').click(function () {
        jQuery('.modal-body0').hide();
        jQuery('.modal-body1').show();
    });
}
function configureMessage() {
    jQuery('.btn-message').click(function () {
        jQuery('.modal-body0').hide();
        jQuery('.modal-body2').show();
    });
}
function reset() {
    jQuery('#myModal').on('hidden.bs.modal', function (e) {
        //jQuery('tabbedView a:first').tab('show');
        jQuery('#tabbedView a[href="#errorDetails"]').tab('show')
        jQuery('.modal-body0').show();
        jQuery('.modal-body1').hide();
        jQuery('.modal-body2').hide();
    });
}