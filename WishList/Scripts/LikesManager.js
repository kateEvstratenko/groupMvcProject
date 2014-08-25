(function () {
    'use strict';
    $.ajax({
        type: "POST",
        url: "/Gift/EnableLikes/",
        success: function(data) {
            if (data == 'True') {
                $('.myCustomLikeButton').bind('click', UpdateLikes);
            } else {
                $('.myCustomLikeButton')._addClass("disabled");
            }
        }
    });
    var UpdateLikes = function() {
        var element = this;
        element.disabled = true;
        var likeId = element.attributes["name"].value;
        $.ajax({
            type: "POST",
            url: "/Gift/ChangeLikesCount/" + likeId.toString(),
            data: likeId,
            success: function(data) {
                $('#' + likeId.toString()).empty();
                $('#' + likeId.toString()).append(data);
                element.disabled = false;
            }
        });
    };
})();
