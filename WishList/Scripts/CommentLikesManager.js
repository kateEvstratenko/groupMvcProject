(function () {
    'use strict';
    $.ajax({
        type: "POST",
        url: "/Comment/EnableLikes/",
        success: function (data) {
            if (data == 'True') {
                $('.myCustomCommentLikeButton').bind('click', function () {
                    UpdateLikes(this);
                });
            } else {
                $('.myCustomCommentLikeButton')._addClass("disabled");
            }
        }
    });
    var UpdateLikes = function (el) {
        var $el = $(el);
        $el.prop('disabled', true);
        var likeId = $el.attr('name');
        $.ajax({
            type: "POST",
            url: "/Comment/ChangeLikesCount/" + likeId.toString(),
            data: likeId,
            success: function (data) {
                $('#' + likeId.toString()).empty();
                $('#' + likeId.toString()).append(data);
                $el.prop('disabled', false);
            }
        });
    };
})();
