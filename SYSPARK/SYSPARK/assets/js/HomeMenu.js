$(document).ready(counter, main());

var counter = 1;
function main() {
    $('#buttonNavToogleOptions').click(function () {
        //$('#navbarFromRight').toggle();
        if (counter === 1) {
            $('#navbar-right').animate({
                right: '0%'
            });
            counter = 0;
        } else {
            counter = 1;
            $('#navbar-right').animate({
                right: '-100%'
            });
        }
    });
};