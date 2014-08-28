(function() {
    'use strict';

    $('#datepicker').datepicker({
        minDate: new Date(1910, 0, 1),
        maxDate: new Date(2010, 0, 1),
        defaultDate:'-104y -8m -23d',
        firstDay: 1,
        yearRange: '1910:2010',
        changeYear: true,
        changeMonth: true,
        dateFormat: 'dd/mm/yy'
});
}());