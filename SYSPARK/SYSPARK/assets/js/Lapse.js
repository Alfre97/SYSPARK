//Desition wheater do or not the function getValue
var counter = 0;

function deleteRole() {
    $("#buttonDelete").click();
}

function getValue(row) {
    if (row.className == 'marcado') {
        row.className = row.className == 'marcado' ? 'desmarcado' : 'marcado';
        $("#hiddenLapseId").val('');
    } else {
        row.className = row.className == 'marcado' ? 'desmarcado' : 'marcado';
        $("#hiddenLapseId").val(row.cells[0].childNodes[0].nodeValue);
    }
}

function clearingSomeControls() {
    $("#textboxLapse").val('');
    $("#dateInitialDate").val('');
    $("#dateFinalDate").val('');
    $("#radioStatusOn").val('');
    $("#radioStatusOff").val('');
}

function setValues(row) {
    if ($("#hiddenLapseId").val() === "") {
        $("#buttonInfoLapseTable").css('background-color', 'red');
        document.getElementById('buttonInfoLapseTable').value = 'Please, after any operation select one lapse!';

    } else {
        $("#textboxLapse").val(row.cells[1].childNodes[0].nodeValue);
        $("#dateInitialDate").val(row.cells[1].childNodes[0].nodeValue);
        $("#dateFinalDate").val(row.cells[1].childNodes[0].nodeValue);
        $("#radioStatusOn").val(row.cells[1].childNodes[0].nodeValue);
        $("#radioStatusOff").val(row.cells[1].childNodes[0].nodeValue);

        $("#hiddenLapseId").val(row.cells[0].childNodes[0].nodeValue);
        $("#buttonClear").css('visibility', 'hidden');
        $("#buttonAddRole").css('visibility', 'hidden');
        $("#buttonUpdate").css('visibility', 'visible');
    }
}

function checkedDesicion(x, y, z) {
    var radioOn = document.getElementById(x);
    var radioOff = document.getElementById(y);
    var hidden = document.getElementById(z);

    if (counter == 1) {
        if (radioOff.checked == true) {
            radioOn.checked = false;
            hidden.value = "false";
            counter = 0;
        }
    }
    else if (counter == 2) {
        if (radioOn.checked == true) {
            radioOff.checked = false;
            hidden.value = "true";
            counter = 0;
        }
    } else {
        if (radioOn.checked == true) {
            radioOff.checked = false;
            hidden.value = "true";
            counter = 1;
        }
        if (radioOff.checked == true) {
            radioOn.checked = false;
            hidden.value = "false";
            counter = 2;
        }
    }
}
