function handleLeaveCommentSuccess(data) {
    if (data.success) {
        $("#comments").append(data.data);
        $("#leave-comment-header").val("");
        $("#leave-comment-body").val("");
        $(".validation-summary").remove();
    } else {
        $("#leave-comment-wrapper").replaceWith(data.data);
    }
}

function handleEditBlogentrySuccess() {
    window.location.href = "/Home/Blogentry?id=" + $("#blogentryId").val();
}