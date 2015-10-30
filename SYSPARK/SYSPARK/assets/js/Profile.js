$(document).ready(setValue(x, y));

function setValue(x, y) {
    var select = document.getElementById(x);
    var hiddenValue = document.getElementById(y);
    var position = select.selectedIndex;
    hiddenValue.value = select.options[position].value;

    if (hiddenValue.value > 1) {
        $("#textboxCode").css('visibility', 'visible');
        $("#hiddenConditionValue").val('1');
    }
    else {
        $("#textboxCode").css('visibility', 'hidden');
        $("#hiddenConditionValue").val('0');
    }
}