$(document).ready(setValue(x, y));

function setValue(x, y) {
    var select = document.getElementById(x);
    var hiddenValue = document.getElementById(y);
    var position = select.selectedIndex;
    hiddenValue.value = select.options[position].value;
    alert(hiddenValue.value);

    if (hiddenValue.value > 1) {
        $("#textboxCode").css('visibility', 'visible');
        $("#hiddenVisibleValue").val("1");
    }
    else {
        $("#textboxCode").css('visibility', 'hidden');
        $("#hiddenVisibleValue").val("0");
    }
}