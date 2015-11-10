//Desition wheater do or not the function getValue
function deleteParking() {
    $("#buttonDelete").click();
}

function getValue(row) {
    if (row.className == 'marcado') {
        row.className = row.className == 'marcado' ? 'desmarcado' : 'marcado';
        $("#hiddenParkingId").val('');
    } else {
        row.className = row.className == 'marcado' ? 'desmarcado' : 'marcado';
        $("#hiddenParkingId").val(row.cells[0].childNodes[0].nodeValue);
    }
}

function clearingSomeControls(){
$("#buttonClear").click(
    function clean() {
        $("#textboxParkingName").val('');
        $("#textboxTotalSpace").val('');
        $("#textboxCarSpace").val('');
        $("#textboxMotorCycleSpace").val('');
        $("#textboxBusSpace").val('');
        $("#textboxHandicapSpace").val('');
    });
};

function setValues(row) {
    if ($("#hiddenParkingId").val() === "") {
        $("#buttonInfoParkingTable").css('background-color', 'red');
        document.getElementById('buttonInfoParkingTable').value = 'Please, after any operation select one role!';

    } else {
        $("#hiddenParkingId").val(row.cells[0].childNodes[0].nodeValue);
        $("#textboxParkingName").val(row.cells[1].childNodes[0].nodeValue);
        $("#textboxTotalSpace").val(row.cells[2].childNodes[0].nodeValue);
        $("#textboxCarSpace").val(row.cells[3].childNodes[0].nodeValue);
        $("#textboxMotorCycleSpace").val(row.cells[4].childNodes[0].nodeValue);
        $("#textboxBusSpace").val(row.cells[5].childNodes[0].nodeValue);
        $("#textboxHandicapSpace").val(row.cells[6].childNodes[0].nodeValue);

        $("#buttonClear").hide();
        $("#buttonAddParking").hide();
        $("#buttonUpdate").css('visibility', 'visible');
    }
}