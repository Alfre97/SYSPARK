//Desition wheater do or not the function getValue
function deleteRole() {
    $("#buttonDelete").click();
}

function getValue(row) {
    if (row.className == 'marcado') {
        row.className = row.className == 'marcado' ? 'desmarcado' : 'marcado';
        $("#hiddenCampusId").val('');
    } else {
        row.className = row.className == 'marcado' ? 'desmarcado' : 'marcado';
        $("#hiddenCampusId").val(row.cells[0].childNodes[0].nodeValue);
    }
}

function clearingSomeControls() {
    $("#textboxCampus").val('');
}

function setValues(row) {
    if ($("#hiddenCampusId").val() === "") {
        $("#buttonInfoLapseTable").css('background-color', 'red');
        document.getElementById('buttonInfoLapseTable').value = 'Please, after any operation select one campus!';

    } else {
        $("#textboxCampus").val(row.cells[1].childNodes[0].nodeValue);
        $("#hiddenCampusId").val(row.cells[0].childNodes[0].nodeValue);
        $("#buttonClear").css('visibility', 'hidden');
        $("#buttonAddRole").css('visibility', 'hidden');
        $("#buttonUpdate").css('visibility', 'visible');
    }
}