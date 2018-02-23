////window.onload = function () {
////    document.getElementById('Btnsearch').click();
////}
//jQuery.noConflict();
//// load header menu 
//// bootstrap model code
//jQuery(document).ready(function () {
//    //chartSearch();
//    //loadMenu();
//    //getAppAtDash();
    
//    attachEvents();
//    function getParamValuesByName(querystring) {
//        var qstring = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
//        for (var i = 0; i < qstring.length; i++) {
//            var urlparam = qstring[i].split('=');
//            if (urlparam[0] == querystring) {
//                return urlparam[1];
//            }
//        }
//    }
//    var searchData = getParamValuesByName('searchData');
//    jQuery("#searchData").val(searchData);
//    SearchErrorLog();
//    //var userEmail = getParamValuesByName('userEmail');
//    //jQuery("#assignUserName").text(userEmail);
//    //jQuery(".btn-mail").click(function () {
//    //    jQuery("#changeContent").load('/view4');
//    //});
//    //jQuery(".btn-message").click(function () {
//    //    jQuery("#changeContent").load('/view3');
//    //});
//    jQuery(".new_recipient").click(function () {
//        jQuery(".new_rec").append('<input type="text" class="form-control" style="margin-top:5px;">');
//    });
//    jQuery('#myModal').modal({
//        keyboard: true,
//        backdrop: "static",
//        show: false,
//    }).on('show', function () {
//    });
//    jQuery("body").on('click', '#accessData', function () {
//        jQuery('#errorDetails').html(jQuery('<table class="table table-bordered">' + '<tr>  <td> APPLICATION </td> ' + " <td  class=active>" + jQuery(this).data('id') + "</td></tr>" + '<tr>  <td>TIMESTAMP</td> ' + " <td>" + jQuery(this).data('app') + "</td></tr>" + '<tr>  <td>LEVEL</td> ' + " <td>" + jQuery(this).data('level') + "</td></tr>" + '<tr>  <td>SOURCE PAGE</td> ' + " <td>" + jQuery(this).data('page') + "</td></tr>" + '<tr>  <td>SOURCE FUNCTION</td> ' + " <td>" + jQuery(this).data('function') + "</td></tr>" + '<tr>  <td>MESSAGE</td> ' + " <td>" + jQuery(this).data('message') + "</td></tr>" + '<tr>  <td>TERMINAL ID</td> ' + " <td>" + jQuery(this).data('terminal') + "</td></tr>" + '<tr>  <td>STATUS</td> ' + " <td  class=success>" + jQuery(this).data('status') + "</td></tr>" + '</table>'));
//        jQuery('#myModal').modal('show');
//    });
//});
////function loadMenu() {
////    jQuery(document).ready(function () {
////        jQuery("#menuLoad").load("/menu");
////    })
////}
//function attachEvents() {
//    jQuery("#Btnsearch").click(function () {
//        SearchErrorLog();
//    });

//    jQuery("#recordPerPage").change(function () {
//        SearchErrorLog();
//    });

//    jQuery("#jumpto").change(function () {
//        SearchErrorLog();
//    });
//    //jQuery("#login").click(function () {
//    //    mylogin();
//    //});
//}
////function to get the unique app to the dashboard dropdown menu
////function getAppAtDash() {
////    strUrl = '/getAppAtDash';
////    jQuery.ajax({
////        headers: { "Accept": "application/json" },
////        type: 'GET',
////        url: strUrl,
////        dataType: 'json',
////        contentType: 'application/json',
////        success: function (xhr) {
////            var jsonResponseAccessHistory = {
////                "AccessHistory": xhr.result
////            };
////            jQuery("#form-group > #distinctApps").html("");
////            var templateAccessHistory = jQuery('#templateAccessHistory').html();
////            Mustache.parse(templateAccessHistory);
////            var renderedAccessHistory = Mustache.render(templateAccessHistory, jsonResponseAccessHistory);
////            jQuery("[id$='form-group'] > #distinctApps").append(renderedAccessHistory);
////        },
////        error: function (result, status, xhr) {
////            alert('error');
////        },
////        beforeSend: function (xhr) {
////        }
////    })
////}

////function SearchErrorLog() {  
////    var strUrl = "";
////    if (jQuery("#searchData").val() != "" && jQuery("#searchBy").val() != "") {
////        strUrl = '/search/errorlogs?searchData=' + jQuery("#searchData").val() + "&searchBy=" + jQuery("#searchBy").val();
////    }
////    else {
////        strUrl = '/search/errorlogs';
////    }

////    var strdata = {
////        "PageSize": jQuery("#recordPerPage").val(),
////        "CurrentPage": jQuery("#jumpto").val()
////    };

////    jQuery.ajax({
////        headers: { "Accept": "application/json" },
////        type: 'GET',
////        url: strUrl,
////        data: strdata,
////        dataType: 'json',
////        contentType: 'application/json',
////        success: function (xhr) {
////            var jsonResponseAccessHistory = {
////                "AccessHistory": xhr.result
////            };
////            jQuery("#tblAccessHistory > tbody").html("");
////            var templateAccessHistory = jQuery('#templateAccessHistory').html();
////            Mustache.parse(templateAccessHistory);
////            var renderedAccessHistory = Mustache.render(templateAccessHistory, jsonResponseAccessHistory);
////            jQuery("[id$='tblAccessHistory'] > tbody").append(renderedAccessHistory);
////        },
////        error: function (result, status, xhr) {
////            alert('error');
////        },
////        beforeSend: function (xhr) {
////        }
////    });

////}


