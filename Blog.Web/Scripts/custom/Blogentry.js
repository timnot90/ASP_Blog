function handleLeaveCommentSuccess(data) {
    $("#comments").append(data);
}

function handleEditBlogentrySuccess(ajaxContext) {
    console.log("context: ");
    console.log(ajaxContext);
    window.location.href = "/Home/Blogentry?id=" + $("#blogentryId").val();
}