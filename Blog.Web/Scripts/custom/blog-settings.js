"use strict";
var blogSettings;
$(document).ready(function () {
    blogSettings = new BlogSettingsScript();
    blogSettings.initialize();
});

function BlogSettingsScript()
{
    if ($("#cb-smtp-are-credentials-mandatory").is(':checked')) {
        $("#smtp-user-credentials").show();
    } else {
        $("#smtp-user-credentials").hide();
    }
    this.initialize = function() {
        $("#cb-smtp-are-credentials-mandatory").click(function () {
            if ($(this).is(':checked')) {
                $("#smtp-user-credentials").show();
            } else {
                $("#smtp-user-credentials").hide();
            }
        });
    }
}
