function handleLeaveCommentSuccess(data, status, xhr) {
    if (data.success) {
        $("#comments").append(data.data);
        $("#leave-comment-header").val("");
        $("#leave-comment-body").val("");
        $(".validation-summary").remove();
    } else {
        $("#leave-comment-wrapper").replaceWith(data.data);
    }
}

function handleEditBlogentrySuccess(ajaxContext) {
    console.log("context: ");
    console.log(ajaxContext);
    window.location.href = "/Home/Blogentry?id=" + $("#blogentryId").val();
}