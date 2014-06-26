function handleLeaveCommentSuccess(data) {
    $("#comments").append(data);
    $("#leave-comment-header").val("");
    $("#leave-comment-body").val("");
}

function handleEditBlogentrySuccess(ajaxContext) {
    console.log("context: ");
    console.log(ajaxContext);
    window.location.href = "/Home/Blogentry?id=" + $("#blogentryId").val();
}