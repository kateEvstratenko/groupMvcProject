var UpdateLikes = function () {
    var likeId = this.attributes["name"].value;
    $.ajax({
        type: "POST",
        url: "/Gift/ChangeLikesCount/" + likeId.toString(),
        data: likeId,
        success: function (data) {
            $('#' + likeId.toString()).empty();
            $('#' + likeId.toString()).append(data);
        }
    });
};
var SetLikesEvents = function () {
    $('.myCustomLikeButton').bind('click', UpdateLikes);
}();