(function () {
    'use strict';
    var wishListId = $('#currentWishList').attr('data-item');
    $.ajax({
        type: "POST",
        url: "/WishList/EnableVotes/" ,
        data: { id: wishListId },
        success: function (data) {
            if (data == 'True') {
                $('.myCustomVoteButton').bind('click', function () {
                    UpdateVotes(this);
                });
            } else {
                var cantVote = '<h2 style="color:red">You Can\'t Vote!</h2>';
                $('.myCustomVoteButton')._addClass("disabled").css('color','red');
                $('.alert').after(cantVote);
            }
        }
    });
    var UpdateVotes = function (el) {
        var $el = $(el);
        $el.prop('disabled', true);
        var voteId = $el.attr('name');
        $.ajax({
            type: "POST",
            url: "/WishList/ChangeVotesCount/" + voteId.toString(),
            data: voteId,
            success: function (data) {
                var newData = JSON.parse(data);
                $('#' + voteId.toString()).empty().append(newData[0]);
                $('#' + newData[1] + '-' + newData[2]).empty().append(newData[3]);
                $el.prop('disabled', false);
            }
        });
    };
})();
