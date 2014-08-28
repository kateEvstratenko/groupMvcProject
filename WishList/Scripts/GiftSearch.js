(function () {
    'use strict';
    $('#searchGiftInput').keypress(function (e) {
        var key = e.which;
        if (key == 13) {
        }
    });
    $('#searchGiftInput').on('input', function () {
        var $this = $(this);
        var delay = 1000;

        clearTimeout($this.data('timer'));
        $this.data('timer', setTimeout(function () {
            $this.removeData('timer');

            UpdateGiftsList($this[0].value);
        }, delay));
    });

    $('#searchGiftInput').focusin(function () {
        var $this = $(this);
        clearTimeout($this.data('focusOut'));
    });

    $('#searchGiftInput').focusout(function() {
        var $this = $(this);
        var delay = 1000;

        clearTimeout($this.data('focusOut'));
        $this.data('focusOut', setTimeout(function () {
            $this.removeData('focusOut');

            $('#giftSearchResult').show();
            $('#giftSearchResult').empty();
        }, delay));
    });

    var UpdateGiftsList = function (namePart) {
        $('#giftSearchResult').show();
        $('#giftSearchResult').empty();
        $('#giftSearchResult').append('<img class="spinner" src="/Content/images/spinner.gif"></img>');

        $.ajax({
            type: "POST",
            url: "/Gift/GiftsSearch/" + namePart.toString(),
            data: { namePart: namePart },
            success: function (data) {
                $('#giftSearchResult').empty();
                if (namePart) {
                    $('#giftSearchResult').append(data);
                }
            }
        });
    };
})();
