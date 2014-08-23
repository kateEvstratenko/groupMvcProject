(function () {
    'use strict';
    $('button.btn').each(function () {
        $(this).on('click', function () {
            callController($(this).attr('id'));
        });
    });

    function callController(userId) {
        $.ajax({
            url: '../Admin/SwitchRole',
            dataType: 'html',
            traditional: true,
            type: 'GET',
            data: { userId: userId, roleId: $('.' + userId).val() }
        });
    }
}());