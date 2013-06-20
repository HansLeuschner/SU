function sgFormatDate(dateToConvert) {
    var newDate = new Date(parseInt(dateToConvert.substr(6)));
    stDate = kendo.format("{0:d}", newDate);
    return stDate;
}
function sgDateYear(dateToConvert) {
    var newDate = new Date(parseInt(dateToConvert.substr(6)));
    stDate = kendo.format("{0:d}", newDate);
    return stDate.substr(-4);

}

var gCampaign = 0;

// initialize global selectors
function initGlobalSelectors() {
    $("#globalCampaign").kendoDropDownList({
        dataTextField: "Name",
        dataValueField: "CampaignId",
        optionLabel: "Select an active campaign...",
        dataSource: {
            transport: {
                read: {
                    dataType: "json",
                    url: "/campaign/campaignsListing",
                }
            }
        },
        change: function (e) {
            $.cookie('SU_GlobalCampaign', this.value(), { expires: 30, path: '/' });
            window.location.href = "/Home/Index";
        }
    });
}

// function to log an error to Server & locally if required
// This function should be used for ALL errors (i.e. replate all error alerts)
function logError(message) {
    try {
        $.ajax({
            type: 'POST',
            url: "/Error/LogJavascriptError",
            data: { message: message },
            global: false,
            error: function (jqXHR, textStatus, errorThrown) {
                LogErrorLocally("Could not log error to server (" + errorThrown + ") for message: " + message);
            }
        });
    }
    catch (ex) {
        LogErrorLocally("Could not log error to server (" + ex + ") for message: " + message);
    }
    // show error as notification (during development)
    LogErrorLocally(message);
}

// function to log grid errors
// This function should be used in all grid error event handlers
function logGridError(e) {
    var stErrMsg = "An error occurred";

    if (e && e.errors) {
        for (var prop in e.errors) {
            stErrMsg = e.errors[prop].errors;
        }
    }
    logError(stErrMsg);
}

// function to show any success message
function showSuccessAlert(message) {
    $.pnotify({
        type: 'success',
        text: message,
        opacity: 0.9
    });
}

// function to show any warning message
function showWarningAlert(message) {
    $.pnotify({
        type: 'info',
        text: message,
        opacity: 0.9
    });
}

// function to show confirm alert. (ok)
function showConfirm(message) {
    var bAnswer = _confirm(message);
    return (bAnswer);
}

// function to show any validation message
function showValidationAlert(message) {
    showModalAlert(message);
}

// function to show any message the user must see
var modal_overlay, modalBoxAlert;
function showModalAlert(message) {
    modalBoxAlert = $.pnotify({
        text: message,
        type: "info",
        styling: "jqueryui",
        icon: "ui-icon ui-icon-info",
        delay: 15000,
        history: false,
        stack: false,
        before_open: function (pnotify) {
            // Position this notice in the center of the screen.
            pnotify.css({
                "top": ($(window).height() / 2) - (pnotify.height() / 2),
                "left": ($(window).width() / 2) - (pnotify.width() / 2)
            });
            // Make a modal screen overlay.
            if (modal_overlay) modal_overlay.fadeIn("fast");
            else modal_overlay = $("<div />", {
                "class": "ui-widget-overlay",
                "css": {
                    "display": "none",
                    "position": "fixed",
                    "top": "0",
                    "bottom": "0",
                    "right": "0",
                    "left": "0"
                }
            }).appendTo("body").fadeIn("fast");
        },
        before_close: function () {
            modal_overlay.fadeOut("fast");
        }
    });
}

// function to show error
// Should not be called. use LogError
function showErrorAlert(message) {
    $.pnotify({
        type: 'error',
        text: message,
        opacity: 0.9
    });
}

// funtion to log error locally
// Should not be called directly
function LogErrorLocally(message) {
    // can't log error to server (or failed trying)
    // try logging to console
    console.log(message);

    // show as an error notification (sticky?)
    $.pnotify({
        type: 'error',
        text: message,
        opacity: 0.8
    });
}

// javascript error event handler
// should not be called directly
function logJavascriptError(msg, file_loc, line_no){
    var lMsg = "'" + msg + "' File: " + file_loc + " Line: " + line_no;
    logError(lMsg);
}

// global error handler for ajax issues
// Not to be called directly
// this error handler is set at bottom of this script
function logAjaxError(event, jqXHR, ajaxSettings, thrownError) {
    // parse params
    var lMsg;
    var ajaxUrl = ajaxSettings.url;
    var statuscode;
    if (jqXHR && jqXHR.status) {
        statuscode = jqXHR.status;
    }
    if (jqXHR != null && jqXHR.responseText != null && jqXHR.responseText != "") {
        lMsg = jQuery.parseJSON(jqXHR.responseText);
    }
    else {
        lMsg = "Ajax Error: " + thrownError + " on url: " + ajaxUrl + " with status code: " + statuscode;
    }
    logError(lMsg);
}

var _alert;
function consume_alert() {
    if (_alert) return;
    _alert = window.alert;
    window.alert = showWarningAlert;
}

var _confirm;
function consume_confirm() {
    if (_confirm) return;
    _confirm = window.confirm;
    window.confirm = showConfirm;
}

window.onerror = function( msg, file, line) {
    logJavascriptError( msg, file, line);
    return true;
};

$(function () {
    try {
        $("input[type=text]").first().focus();
        $(".defaultfocus").first().focus();

        // pnotify defaults
        $.pnotify.defaults.styling = "jqueryui";

        // change all alerts to pNotify
        consume_alert();
        consume_confirm();

        // set global ajax handler
        $(document).ajaxError(logAjaxError);
        $.ajaxSetup({
            cache: false
        });

        // initialize globals
        initGlobalSelectors();
        gCampaign = $.cookie('SU_GlobalCampaign');
        if (gCampaign && gCampaign != 0) {
            $("#globalCampaign").data("kendoDropDownList").value(gCampaign);
        }
    }
    catch (ex) {
        logJavascriptError(ex);
    }

});
