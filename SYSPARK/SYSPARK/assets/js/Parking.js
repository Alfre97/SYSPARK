//Desition wheater do or not the function getValue
var counter = 0;
var car = 0;
var moto = 0;
var handicap = 0;
var bus = 0;

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

function clearingSomeControls() {
    $("#textboxParkingName").val('');
    $("#textboxTotalSpace").val('');
    $("#textboxCarSpace").val('');
    $("#textboxMotorCycleSpace").val('');
    $("#textboxBusSpace").val('');
    $("#textboxHandicapSpace").val('');
}

function setValues(row) {
    if ($("#hiddenParkingId").val() === "") {
        $("#buttonInfoParkingTable").css('background-color', 'red');
        document.getElementById('buttonInfoParkingTable').value = 'Please, after any operation select one role!';

    } else {
        $("#textboxTotalSpace").attr("disabled", "disabled");
        $("#textboxCarSpace").attr("disabled", "disabled");
        $("#textboxMotorCycleSpace").attr("disabled", "disabled");
        $("#textboxBusSpace").attr("disabled", "disabled");
        $("#textboxHandicapSpace").attr("disabled", "disabled");

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

function setValue(x, y) {
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

function setColorAndValue(button) {
    if (counter == 0) {
        car = document.getElementById("textboxCarSpace").value;
        moto = document.getElementById("textboxMotorCycleSpace").value;
        handicap = document.getElementById("textboxHandicapSpace").value;
        bus = document.getElementById("textboxBusSpace").value;
        counter = 1;
    }

    var select = document.getElementById("selectType");
    var position = select.selectedIndex;
    var buttonError = document.getElementById("buttonErrors2");
    var hidden = document.getElementById(button.name);
    var spanCar = document.getElementById("spanCar");
    var spanMotorcycle = document.getElementById("spanMotorcycle");
    var spanHandicap = document.getElementById("spanHandicap");
    var spanBus = document.getElementById("spanBus");
    var buttonCreateParking = document.getElementById("buttonCreateParking");
    buttonCreateParking.style.visibility = "hidden";

    if (select.options[position].value === "1") {
        if (car > 0) {
            if (button.innerHTML == "Car") {
                car++;
                spanCar.innerHTML = car;
            }
            else if (button.innerHTML == "Motorcycle") {
                moto++;
                spanMotorcycle = moto;
            }
            else if (button.innerHTML == "Handicap") {
                handicap++;
                spanHandicap.innerHTML = handicap;
            }
            else if (button.innerHTML == "Bus") {
                bus++;
                spanBus.innerHTML = bus;
            }
            hidden.value = "Car";
            car = car - 1;
            button.innerHTML = "Car";
            button.style.background = '#4ed848';
            button.style.color = 'white';
            spanCar.innerHTML = car;
        } else {
            buttonError.innerHTML = "You can't add more car spaces.";
        }
    } else if (select.options[position].value === "2") {
        if (moto > 0) {
            if (button.innerHTML == "Car") {
                car++;
                spanCar.innerHTML = car;
            }
            else if (button.innerHTML == "Motorcycle") {
                moto++;
                spanMotorcycle = moto;
            }
            else if (button.innerHTML == "Handicap") {
                handicap++;
                spanHandicap.innerHTML = handicap;
            }
            else if (button.innerHTML == "Bus") {
                bus++;
                spanBus.innerHTML = bus;
            }
            hidden.value = "Motorcycle";
            moto = moto - 1;
            button.innerHTML = "Motorcycle";
            button.style.background = '#c5c872';
            button.style.color = 'white';
            spanMotorcycle.innerHTML = moto;
        } else {
            buttonError.innerHTML = "You can't add more motorcycle spaces.";
        }
    } else if (select.options[position].value === "3") {
        if (handicap > 0) {
            if (button.innerHTML == "Car") {
                car++;
                spanCar.innerHTML = car;
            }
            else if (button.innerHTML == "Motorcycle") {
                moto++;
                spanMotorcycle = moto;
            }
            else if (button.innerHTML == "Handicap") {
                handicap++;
                spanHandicap.innerHTML = handicap;
            }
            else if (button.innerHTML == "Bus") {
                bus++;
                spanBus.innerHTML = bus;
            }
            hidden.value = "Handicap";
            handicap = handicap - 1;
            button.innerHTML = "Handicap";
            button.style.background = 'blue';
            button.style.color = 'white';
            spanHandicap.innerHTML = handicap;
        } else {
            buttonError.innerHTML = "You can't add more handicap spaces.";
        }
    } else if (select.options[position].value === "4") {
        if (bus > 0) {
            if (button.innerHTML == "Car") {
                car++;
                spanCar.innerHTML = car;
            }
            else if (button.innerHTML == "Motorcycle") {
                moto++;
                spanMotorcycle = moto;
            }
            else if (button.innerHTML == "Handicap") {
                handicap++;
                spanHandicap.innerHTML = handicap;
            }
            else if (button.innerHTML == "Bus") {
                bus++;
                spanBus.innerHTML = bus;
            }
            hidden.value = "Bus";
            bus = bus - 1;
            button.innerHTML = "Bus";
            button.style.background = '#5e4c34';
            button.style.color = 'white';
            spanBus.innerHTML = bus;
        }
        else {
            buttonError.innerHTML = "You can't add more bus spaces.";
        }
    }
    else if (select.options[position].value === "5") {
        if (button.innerHTML == "Car") {
            car++;
            spanCar.innerHTML = car;
        }
        else if (button.innerHTML == "Motorcycle") {
            moto++;
            spanMotorcycle = moto;
        }
        else if (button.innerHTML == "Handicap") {
            handicap++;
            spanHandicap.innerHTML = handicap;
        }
        else if (button.innerHTML == "Bus") {
            bus++;
            spanBus.innerHTML = bus;
        }
        hidden.value = "Street";
        button.innerHTML = "Street";
        button.style.background = '#a5a5a5';
        button.style.color = 'white';
    }
    else if (select.options[position].value === "6") {
        if (button.innerHTML == "Car") {
            car++;
            spanCar.innerHTML = car;
        }
        else if (button.innerHTML == "Motorcycle") {
            moto++;
            spanMotorcycle = moto;
        }
        else if (button.innerHTML == "Handicap") {
            handicap++;
            spanHandicap.innerHTML = handicap;
        }
        else if (button.innerHTML == "Bus") {
            bus++;
            spanBus.innerHTML = bus;
        }
        hidden.value = "Clear";
        button.innerHTML = "Clear";
        button.style.background = 'white';
        button.style.color = 'black';
    }

    if (spanCar.innerHTML == "0" && spanMotorcycle.innerHTML == "0" && spanHandicap.innerHTML == "0" && spanBus.innerHTML == "0") {
        buttonCreateParking.style.visibility = "visible";
    }
}

function cancelGenerateMap() {
    counter = 0;
    $("#buttonGenerateMap").removeAttr("disabled");
    $("#textboxParkingName").removeAttr("disabled");
    $("#textHeight").removeAttr("disabled");
    $("#textWidth").removeAttr("disabled");
    $("#textboxTotalSpace").removeAttr("disabled");
    $("#textboxCarSpace").removeAttr("disabled");
    $("#textboxMotorCycleSpace").removeAttr("disabled");
    $("#textboxBusSpace").removeAttr("disabled");
    $("#textboxHandicapSpace").removeAttr("disabled");
}

