"use strict";
var entryPagination;
$(document).ready(function () {
    console.log("initializing entryPagination");
    entryPagination = new BlogentryPagination();
    entryPagination.goToPage(0);
});

function BlogentryPagination() {

    console.log("constructor");
    var allEntries = $(".blogentry");
    var allPaginationItems = $(".pagination-item");
    var entriesPerPage = $("#blogentries-pagination").data("blogentries-per-page");
    var lastPageIndex = allPaginationItems.length - 1;
    var currentPageIndex = 0;
    var entryStartIndex = 0;
    var entryEndIndex = entriesPerPage - 1;
    var maxNumberOfPaginationItems = 5;

    this.goToPage = function (pageIndex) {
        console.log("goToPage " + pageIndex);

        entryStartIndex = (pageIndex) * entriesPerPage;
        entryEndIndex = (pageIndex + 1) * (entriesPerPage) - 1;
        currentPageIndex = pageIndex;
        console.log("Start: " + entryStartIndex);
        console.log("End: " + entryEndIndex);
        allEntries.each(function (index) {
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