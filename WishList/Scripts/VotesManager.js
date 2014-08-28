(function () {
    'use strict';
    $.ajax({
        type: 'POST',
        url: '/WishList/EnableVotes/'+$('WishListId').attr(name),
        success: function (data) {
            if (data == 'True') {
                $('.myCustomVoteButton').bind('click', UpdateVotes);
            } else {
                $('.myCustomVoteButton')._addClass("disabled");
            }
        }
    });
    var UpdateVotes = function () {
        var element = this;
        element.disabled = true;
        var voteId = element.attributes['name'].value;
        $.ajax({
            type: 'POST',
            url: '/WishList/Changee/' + voteId,
            data: voteId,
            success: function (data) {
                $('#' + voteId.toString()).empty();
                $('#' + voteId.toString()).append(data);
                element.disabled = false;
            }
        });
    };
})();
