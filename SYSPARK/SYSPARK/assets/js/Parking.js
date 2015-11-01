$(document).ready();

var counter = 1;
function getValue(row) {
    var hiddenParkingValue = $("#hiddenParkingName");
    row.className = row.className == 'marcado' ? 'desmarcado' : 'marcado';
    alert("Hola0");
    alert(counter);
    if (counter === 1) {
        alert("Hola1");
        hiddenParkingValue.value = row.cells[1].childNodes[0].nodeValue;
        alert(hiddenParkingValue.value);
        counter = 0;
        alert(counter);
    } else {
        alert("Hola2");
        hiddenParkingValue.value = "";
        alert(hiddenParkingValue.value);
        counter = 1;
        alert(counter);
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