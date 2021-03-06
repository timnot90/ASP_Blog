﻿"use strict";
var entryPagination;

$(document).ready(function() {
    var paginationScript = new PaginationScript();
    paginationScript.initialize();
});

function PaginationScript() {

    this.initialize = function() {
        entryPagination = new BlogentryPagination();
        entryPagination.goToPage(0);
        $("#blogentries-pagination-prev").click(function() {
            entryPagination.goToPreviousPage();
        });
        $("#blogentries-pagination-next").click(function() {
            entryPagination.goToNextPage();
        });
        $(".pagination-item").each(function(index) {
            $(this).click(function() {
                entryPagination.goToPage(index);
            });
        });
    };

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

        this.goToPage = function(pageIndex) {
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
        };
        this.goToNextPage = function() {
            if (currentPageIndex != lastPageIndex) {
                this.goToPage(currentPageIndex + 1);
            }
        };
        this.goToPreviousPage = function() {
            if (currentPageIndex != 0) {
                this.goToPage(currentPageIndex - 1);
            }
        };
    }
}