function setValue(x, y) {
    var select = document.getElementById(x);
    var hiddenValue = document.getElementById(y);
    var position = select.selectedIndex;
    hiddenValue.value = select.options[position].value;
}

function deleteVehicle() {
    $("#buttonDelete").click();
}

function getValue(row) {
    if (row.className == 'marcado') {
        row.className = row.className == 'marcado' ? 'desmarcado' : 'marcado';
        $("#hiddenVehicleId").val('');
    } else {
        row.className = row.className == 'marcado' ? 'desmarcado' : 'marcado';
        $("#hiddenVehicleId").val(row.cells[0].childNodes[0].nodeValue);
    }
}

function clearingSomeControls() {
    $("#textboxLicense").val('');
}

function setValues(row) {
    if ($("#hiddenVehicleId").val() === "") {
        $("#buttonInfoVehicleTable").css('background-color', 'red');
        document.getElementById('buttonInfoVehicleTable').value = 'Please, after any operation select one vehicle!';

    } else {
        $("#textboxLicense").val(row.cells[1].childNodes[0].nodeValue);
        $("#hiddenVehicleId").val(row.cells[0].childNodes[0].nodeValue);
        $("#buttonClear").css('visibility', 'hidden');
        $("#buttonAddNewCar").css('visibility', 'hidden');
        $("#buttonUpdate").css('visibility', 'visible');
        $("#buttonCancelUpdate").css('visibility', 'visible');
    }
}

function cancelUpdate() {
    $("#textboxLicense").val('');
    $("#hiddenVehicleId").val('');
    $("#buttonClear").css('visibility', 'visible');
    $("#buttonAddNewCar").css('visibility', 'visible');
    $("#buttonUpdate").css('visibility', 'hidden');
    $("#buttonCancelUpdate").css('visibility', 'hidden');
}