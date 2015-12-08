function setValue(x, y) {
    var select = document.getElementById(x);
    var hiddenValue = document.getElementById(y);
    var position = select.selectedIndex;
    hiddenValue.value = select.options[position].value;
}

function getValue(row) {
    if (row.className == 'marcado') {
        row.className = row.className == 'marcado' ? 'desmarcado' : 'marcado';
        $("#hiddenRoleId").val('');
    } else {
        row.className = row.className == 'marcado' ? 'desmarcado' : 'marcado';
        $("#hiddenRoleId").val(row.cells[0].childNodes[0].nodeValue);
    }
}

function setValues(row) {
    if ($("#hiddenRoleId").val() === "") {
        $("#buttonInfoRoleTable").css('background-color', 'red');
        document.getElementById('buttonInfoRoleTable').value = 'Please, after any operation select one role!';

    } else {
        $("#textboxRole").val(row.cells[1].childNodes[0].nodeValue);
        $("#hiddenRoleId").val(row.cells[0].childNodes[0].nodeValue);
        $("#buttonClear").css('visibility', 'hidden');
        $("#buttonAddRole").css('visibility', 'hidden');
        $("#buttonUpdate").css('visibility', 'visible');
        $("#buttonCancelUpdate").css('visibility', 'visible');
    }
}
