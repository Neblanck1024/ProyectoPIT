//$(document).ready(function(){
//    $("#mostrar").on("click", function () {
//        $("#contacto").css("display", "block");
//    });

//    $("#mostrar").on("click", function () {
//        $("#contacto").css("display", "block");
//    });
//});
$("#mostrar").click(function () {
    $('#contacto').toggle(1000);
    $("#mostrar").css("display", "none");
});