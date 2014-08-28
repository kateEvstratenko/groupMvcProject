'use strict';
    function closeModalWindow(data) {
        if (data.success) {
            $('#newWishList').modal('hide');
            $.ajax({
                url: "/WishList/ViewWishListPartial/",
                data: { id: data.newWishListId },
                success: function (data) {
                    $('#wishListsTable').append(data);
                }
            });
        }
    }
