//Desition wheater do or not the function getValue
function deleteParking() {
    $("#buttonDelete").click();
}

function getValue(row) {
    if (row.className == 'marcado') {
        row.className = row.className == 'marcado' ? 'desmarcado' : 'marcado';
        $("#hiddenParkingName").val('');
    } else {
        row.className = row.className == 'marcado' ? 'desmarcado' : 'marcado';
        $("#hiddenParkingName").val(row.cells[0].childNodes[0].nodeValue);
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

