(function () {
    'use strict';
    
    $(document).ready(function () {
        var data = $('.myCustomCommentsDiv').attr('name');
        var expr = /\w+/g;
        var myParams = data.match(expr);
        
        $.ajax({
            type: "POST",
            url: "/Comment/DisplayComments/",
            data: { id: +myParams[1], kind: myParams[0] },
            success: function (data) {
                $('.myCustomCommentsDiv').prepend(data);
            }
        });
    });
})();
