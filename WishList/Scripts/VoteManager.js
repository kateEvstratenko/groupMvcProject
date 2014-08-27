(function () {
    'use strict';
    $.ajax({
        type: "POST",
        url: "/WishList/EnableVotes/",
        success: function (data) {
            if (data == 'True') {
                $('.myCustomVoteButton').bind('click', function () {
                    UpdateVotes(this);
                });
            } else {
                $('.myCustomVoteButton')._addClass("disabled");
            }
        }
    });
    var UpdateVotes = function (el) {
        var $el = $(el);
        $el.disabled = true;
        var voteId = $el.attr('name');
        $.ajax({
            type: "POST",
            url: "/WishList/ChangeVotesCount/" + voteId.toString(),
            data: voteId,
            success: function (data) {
                var newData = JSON.parse(data);
                $('#' + voteId.toString()).empty();
                $('#' + voteId.toString()).append(newData[0]);
                $('#' + newData[1] + '-' + newData[2]).empty();
                $('#' + newData[1] + '-' + newData[2]).append(newData[3]);
                $el.disabled = false;
            }
        });
    };
})();
