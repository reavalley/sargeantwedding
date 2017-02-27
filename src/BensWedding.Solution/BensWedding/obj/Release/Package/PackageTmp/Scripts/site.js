$(document).ready(function (){
    $('.attendingDropdown').change(function () {
        hideShowMenuOptions($(this).val(), $(this).data("dayid"));
    });

    function hideShowMenuOptions(value, dayId) {
        if (value == dayId) {
            $('.menuOptions').show();
        }
        else {
            $('.menuOptions').hide();
        }
    }

    hideShowMenuOptions($('.attendingDropdown').val(), $('.attendingDropdown').data("dayid"));
});           