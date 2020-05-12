$(function () {

    //https://getbootstrap.com/docs/4.4/components/tooltips/
    $('[data-toggle="tooltip"]').tooltip()
    bsCustomFileInput.init();

    $("#frmSearch").submit(function (event) {

        var q = $("#q").val().trim(); //textbox ın içine koymuyor değişkene koyuyor trimli halini.

        $("#q").val(q);

        if (!q) {
            event.preventDefault();
        }
    });


    
    
});