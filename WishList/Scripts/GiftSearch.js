(function () {
    'use strict';
    $('#myCustomSearchButton').on('click', function () {
        var val = $('#searchGiftInput')[0].value;
        if (val) {
            window.location.href = '/Gift/SearchResults/' + val;
        }
    });

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

            $('#giftSearchResult').show().empty();
        }, delay));
    });

    var UpdateGiftsList = function (namePart) {
        var spinnerImg = '<img class="spinner" src="/Content/images/spinner.gif"></img>';
        var $element = $('#giftSearchResult');
        $element.show().empty().append(spinnerImg);

        $.ajax({
            type: 'POST',
            url: '/Gift/GiftsSearch/' + namePart.toString(),
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
