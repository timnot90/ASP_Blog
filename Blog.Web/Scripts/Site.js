"use strict";
var entryPagination;
$(document).ready(function () {
    $(".blog-navbar-nojs").hide();
    $(".blog-navbar-js").show();

    entryPagination = new BlogentryPagination();
    entryPagination.goToPage(0);

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
});

function BlogentryPagination() {
    $(".pagination-wrapper").show();
    var allEntries = $(".blogentry");
    var allPaginationItems = $(".pagination-item");
    var entriesPerPage = $("#blogentries-pagination").data("blogentries-per-page");
    var lastPageIndex = allPaginationItems.length - 1;
    var currentPageIndex = 0;
    var entryStartIndex = 0;
    var entryEndIndex = entriesPerPage - 1;
    var maxNumberOfPaginationItems = 5;

    this.goToPage = function (pageIndex) {
        if (allPaginationItems.length > 0) {
            entryStartIndex = (pageIndex) * entriesPerPage;
            entryEndIndex = (pageIndex + 1) * (entriesPerPage) - 1;
            currentPageIndex = pageIndex;
            allEntries.each(function(index) {
                if (index >= entryStartIndex && index <= entryEndIndex) {
                    $(this).css("display", "block");
                } else {
                    $(this).css("display", "none");
                }
            });

            allPaginationItems.each(function(index) {
                if (index == currentPageIndex) {
                    $(this).addClass("active");
                } else {
                    $(this).removeClass("active");
                }

                if (index >= currentPageIndex - parseInt(maxNumberOfPaginationItems / 2) && index <= currentPageIndex + parseInt(maxNumberOfPaginationItems / 2)
                    || currentPageIndex < parseInt(maxNumberOfPaginationItems / 2) && index < maxNumberOfPaginationItems
                    || currentPageIndex >= lastPageIndex - parseInt(maxNumberOfPaginationItems / 2) && index > lastPageIndex - maxNumberOfPaginationItems) {

                    $(this).css("display", "inline");
                } else {
                    $(this).css("display", "none");
                }
            });

            if (currentPageIndex == lastPageIndex) {
                $("#blogentries-pagination-next").addClass("disabled");
            } else {
                $("#blogentries-pagination-next").removeClass("disabled");
            }

            if (currentPageIndex == 0) {
                $("#blogentries-pagination-prev").addClass("disabled");
            } else {
                $("#blogentries-pagination-prev").removeClass("disabled");
            }
        }
    }

    this.goToNextPage = function () {
        console.log("goToNextPage");
        if (currentPageIndex != lastPageIndex) {
            this.goToPage(currentPageIndex + 1);
        } else {
            return false;
        }
    }

    this.goToPreviousPage = function () {
        console.log("goToPreviousPage");
        if (currentPageIndex != 0) {
            this.goToPage(currentPageIndex - 1);
        } else {
            return false;
        }
    }
}