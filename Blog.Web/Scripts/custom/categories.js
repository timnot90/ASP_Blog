function addCategorySuccess(data) {
    console.log(data);
    $("#categoryList").append(data);
}
function addCategoryFailure(data) {
    $("#add-category-partial-view").replaceWith(data);
}