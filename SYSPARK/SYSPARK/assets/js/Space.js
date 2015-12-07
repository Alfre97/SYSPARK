//Desition wheater do or not the function getValue
var counter = 0;

function setValueCampus(x, y) {
    var select = document.getElementById(x);
    var hiddenValue = document.getElementById(y);
    var position = select.selectedIndex;
    hiddenValue.value = select.options[position].value;
}

function setValueParkingToView(x, y) {
    var select = document.getElementById(x);
    var hiddenValue = document.getElementById(y);
    var position = select.selectedIndex;
    hiddenValue.value = select.options[position].value;
}

function setValueCampusToView(x, y) {
    var select = document.getElementById(x);
    var hiddenValue = document.getElementById(y);
    var position = select.selectedIndex;
    hiddenValue.value = select.options[position].value;
}

function setValue(x, y) {
    var select = document.getElementById(x);
    var hiddenValue = document.getElementById(y);
    var position = select.selectedIndex;
    hiddenValue.value = select.options[position].value;
}

function deleteRole() {
    $("#buttonDelete").click();
}

function getValue(row) {
    if (row.className == 'marcado') {
        row.className = row.className == 'marcado' ? 'desmarcado' : 'marcado';
        $("#hiddenSpaceId").val('');
        $("#hiddenSpaceTypeId").val('');
    } else {
        row.className = row.className == 'marcado' ? 'desmarcado' : 'marcado';
        $("#hiddenSpaceId").val(row.cells[0].childNodes[0].nodeValue);
        $("#hiddenSpaceTypeId").val(row.cells[5].childNodes[0].nodeValue);
    }
}

function clearingSomeControls() {
    $("#textboxSpace").val('');
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
        $("#dateInitialDate").val(row.cells[2].childNodes[0].nodeValue);
        $("#dateFinalDate").val(row.cells[3].childNodes[0].nodeValue);
        var initialDate = document.getElementById("dateInitialDate");
        var finalDate = document.getElementById("dateFinalDate");

        initialDate.valueAsDate = (row.cells[2].childNodes[0].nodeValue).format('YYYY-MM-DD');
        finalDate.valueAsDate = (row.cells[3].childNodes[0].nodeValue).format('YYYY-MM-DD');


        switch (row.cells[4].childNodes[0].nodeValue) {
            case "True":
                var checkBoxOn = document.getElementById("checkBoxStatusOn");
                checkBoxOn.checked = true;
                var hidden = document.getElementById("hiddenStatusValue");
                hidden.value = "true";
                break;
            case "False":
                var checkBoxOff = document.getElementById("checkBoxStatusOff");
                checkBoxOff.checked = true;
                var hidden = document.getElementById("hiddenStatusValue");
                hidden.value = "false";
                break;
        }

        $("#hiddenLapseId").val(row.cells[0].childNodes[0].nodeValue);
        $("#buttonClear").css('visibility', 'hidden');
        $("#buttonAddLapse").css('visibility', 'hidden');
        $("#buttonUpdate").css('visibility', 'visible');
        $("#buttonCancelUpdate").css('visibility', 'visible');
    }
}

function checkedDesicion(x, y, z) {
    var checkBoxOn = document.getElementById(x);
    var checkBoxOff = document.getElementById(y);
    var hidden = document.getElementById(z);

    if (counter == 1) {
        if (checkBoxOff.checked == true) {
            checkBoxOn.checked = false;
            hidden.value = "false";
            counter = 0;
        }
    }
    else if (counter == 2) {
        if (checkBoxOn.checked == true) {
            checkBoxOff.checked = false;
            hidden.value = "true";
            counter = 0;
        }
    } else {
        if (checkBoxOn.checked == true) {
            checkBoxOff.checked = false;
            hidden.value = "true";
            counter = 1;
        }
        if (checkBoxOff.checked == true) {
            checkBoxOn.checked = false;
            hidden.value = "false";
            counter = 2;
        }
    }
}

function cancelUpdate() {
    $("#textboxLapse").val('');
    $("#dateInitialDate").val('');
    $("#dateFinalDate").val('');
    var checkBoxOn = document.getElementById("checkBoxStatusOn");
    checkBoxOn.checked = false;
    var checkBoxOff = document.getElementById("checkBoxStatusOff");
    checkBoxOff.checked = false;
    $("#hiddenLapseId").val('');
    $("#buttonClear").css('visibility', 'visible');
    $("#buttonAddLapse").css('visibility', 'visible');
    $("#buttonUpdate").css('visibility', 'hidden');
    $("#buttonCancelUpdate").css('visibility', 'hidden');
}
