﻿(function () {
    'use strict';
    
    $('#usersSearchInput').keypress(function (e) {
        var key = e.which;
        if (key == 13) {
        }
    });
    $('#usersSearchInput').on('input', function () {
        var $this = $(this);
        var delay = 1000;

        clearTimeout($this.data('timer'));
        $this.data('timer', setTimeout(function () {
            $this.removeData('timer');
            $('.hideMe').hide();
            UpdateUsersList($this[0].value);
        }, delay));
    }
);

    var UpdateUsersList = function (namePart) {
        $('#usersList').empty();
        $('#usersList').append('<img class="spinner" src="/Content/images/spinner.gif"></img>');
        
        $.ajax({
            type: "POST",
            url: "/User/UsersSearch/" + namePart.toString(),
            data: { namePart: namePart },
            success: function (data) {
                $('#usersList').empty();
                $('#usersList').append(data);
            }
        });
    };
})();
