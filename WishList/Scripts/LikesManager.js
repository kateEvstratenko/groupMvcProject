(function () {
    'use strict';
    $.ajax({
        type: "POST",
        url: "/Gift/EnableLikes/",
        success: function(data) {
            if (data == 'True') {
                $('.myCustomLikeButton').bind('click', function() { 
                    UpdateLikes(this);
                });
            } else {
                $('.myCustomLikeButton')._addClass("disabled");
            }
        }
    });
    var UpdateLikes = function(el) {
        var $el = $(el);
        $el.prop('disabled', true);
        var likeId = $el.attr('name');
        $.ajax({
            type: "POST",
            url: "/Gift/ChangeLikesCount/" + likeId.toString(),
            data: likeId,
            success: function(data) {
                $('#' + likeId.toString()).empty().append(data);
                $el.prop('disabled', false);
            }
        });
    };
})();
