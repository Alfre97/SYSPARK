//Desition wheater do or not the function getValue
function deleteRole() {
    $("#buttonDelete").click();
}

function getValue(row) {
    if (row.className == 'marcado') {
        row.className = row.className == 'marcado' ? 'desmarcado' : 'marcado';
        $("#hiddenVehicleTypeId").val('');
    } else {
        row.className = row.className == 'marcado' ? 'desmarcado' : 'marcado';
        $("#hiddenVehicleTypeId").val(row.cells[0].childNodes[0].nodeValue);
    }
}

function clearingSomeControls() {
    $("#textboxVehicleType").val('');
}

function setValues(row) {
    if ($("#hiddenVehicleTypeId").val() === "") {
        $("#buttonInfoVehicleTypeTable").css('background-color', 'red');
        document.getElementById('buttonInfoVehicleTypeTable').value = 'Please, after any operation select one vehicle type!';

    } else {
        $("#textboxVehicleType").val(row.cells[1].childNodes[0].nodeValue);
        $("#hiddenVehicleTypeId").val(row.cells[0].childNodes[0].nodeValue);
        $("#buttonClear").css('visibility', 'hidden');
        $("#buttonAddVehicleType").css('visibility', 'hidden');
        $("#buttonUpdate").css('visibility', 'visible');
        $("#buttonCancelUpdate").css('visibility', 'visible');
    }
}

function cancelUpdate() {
    $("#textboxVehicleType").val('');
    $("#hiddenVehicleTypeId").val('');
    $("#buttonClear").css('visibility', 'visible');
    $("#buttonAddVehicleType").css('visibility', 'visible');
    $("#buttonUpdate").css('visibility', 'hidden');
    $("#buttonCancelUpdate").css('visibility', 'hidden');
}