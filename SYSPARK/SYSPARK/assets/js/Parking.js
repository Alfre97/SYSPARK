$(document).ready();

var counter = 1;
function getValue(row) {
    var hiddenParkingValue = $("#hiddenParkingName");
    row.className = row.className == 'marcado' ? 'desmarcado' : 'marcado';
    if (counter === 1) {
        hiddenParkingValue.value = row.cells[1].childNodes[0].nodeValue;
        alert(hiddenParkingValue.value);
        counter = 0;
    } else {
        hiddenParkingValue.value = "";
        alert(hiddenParkingValue.value);
        counter = 1;
    }
}

function clear(){
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

function deleteParking() {
    $("#buttonDelete").click();
}