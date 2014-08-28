'use strict';
    function closeModalWindow(data) {
        if (data.success) {
            $('#newWishList').modal('hide');
            location.reload();
        }
    }
