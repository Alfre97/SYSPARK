$(document).ready();

function setValue(x, y) {
    var select = document.getElementById(x);
    var hiddenValue = document.getElementById(y);
    var position = select.selectedIndex;
    hiddenValue.value = select.options[position].value;
}