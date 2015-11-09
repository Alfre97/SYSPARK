&(document).ready(counter, getValue(row), clear(), deleteRole());

var counter = 1;
function getValue(row) {
    var hiddenParkingValue = $("#hiddenRoleName");
    row.className = row.className == 'marcado' ? 'desmarcado' : 'marcado';
    if (counter === 1) {
        hiddenParkingValue.value = row.cells[0].childNodes[0].nodeValue;
        alert(hiddenParkingValue.value);
        counter = 0;
    } else {
        hiddenParkingValue.value = "";
        alert(hiddenParkingValue.value);
        counter = 1;
    }
}

function clearingSomeControls() {
    document.getElementById("buttonClear").click(
        function clean() {
            document.getElementById("textboxRole").value = "";
        });
};

function deleteRole() {
    $("#buttonDelete").click();
}