"use strict";
var userSettings;
$(document).ready(function () {
    userSettings = new UserSettingsScript();
    userSettings.initialize();
});

function UserSettingsScript() {

    this.initialize = function() {
        // Initialize all lock-switches and their click-events
        $(".lock-switch").each(function () {
            var state2 = $(this).data("is-locked");
            if (state2 == "False") {
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
        $.ajax("/Administration/Administration/ChangeRole?id=" + id + "&newRole=" + newRole + "&added=" + added);
    }
    function setUserLockedState(id, state) {
        console.log("set" + id + " to " + state);
        $.ajax("/Administration/Administration/SetUserLockedState?id=" + id + "&state=" + state);
    }
}