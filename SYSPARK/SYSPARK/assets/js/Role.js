//Desition wheater do or not the function getValue
function deleteRole() {
    $("#buttonDelete").click();
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

function clearingSomeControls() {
    $("#buttonClear").click(
        function clean() {
            $("#textboxRole").val('');
        });
};

function setValues(row) {
    if ($("#hiddenRoleId").val() === "") {
        $("#buttonInfoRoleTable").css('background-color', 'red');
        document.getElementById('buttonInfoRoleTable').value = 'Please, after any operation select one role!';

    } else {
        $("#textboxRole").val(row.cells[1].childNodes[0].nodeValue);
        $("#hiddenRoleId").val(row.cells[0].childNodes[0].nodeValue);
        $("#buttonClear").hide();
        $("#buttonAddRole").hide();
        $("#buttonUpdate").css('visibility', 'visible');
    }
}