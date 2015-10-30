$(document).ready(clear, show);

var counter = 0;
function getValue(row) {
    row.className = row.className == 'marcado' ? 'desmarcado' : 'marcado';
    if (counter === 0) {
        $("#hiddenParkingName").val(row.cells[0].childNodes[0].nodeValue);
        counter = 1;
    } else {
        $("#hiddenParkingName").val('');
        counter = 0;
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