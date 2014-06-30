"use strict";
var blogSettings;
$(document).ready(function () {
    blogSettings = new UserSettingsScript();
    blogSettings.initialize();
});

function UserSettingsScript() {

    this.initialize = function() {
        // Initialize all lock-switches and their click-events
        $(".lock-switch").each(function () {
            var state = $(this).data("is-locked");
            if (state == "False") {
                $(this).bootstrapSwitch("state", false, false);
            } else {
                $(this).bootstrapSwitch("state", true, true);
            }
            $(this).on('switchChange.bootstrapSwitch', function (event, state) {
                setUserLockedState($(this).data("user-id"), state);
            });
        });

        // Initialize role-checkbox click-events
        $(".roleCheckbox").each(function () {
            $(this).click(function () {
                if ($(this).is(':checked')) {
                    roleChanged($(this).data("user-id"), $(this).data("role"), true);
                } else {
                    roleChanged($(this).data("user-id"), $(this).data("role"), false);
                }
            });
        });
    }

    function roleChanged(id, newRole, added) {
        $.ajax("/Administration/Home/ChangeRole?id=" + id + "&newRole=" + newRole + "&added=" + added);
    }
    function setUserLockedState(id, state) {
        $.ajax("/Administration/Home/SetUserLockedState?id=" + id + "&state=" + state);
    }
}