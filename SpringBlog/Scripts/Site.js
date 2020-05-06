$(function () {

    $("#frmSearch").submit(function (event) {

        var q = $("#q").val().trim(); //textbox ın içine koymuyor değişkene koyuyor trimli halini.

        $("#q").val(q);

        if (!q) {
            event.preventDefault();
        }
    });
    
});