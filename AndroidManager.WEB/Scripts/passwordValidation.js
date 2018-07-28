/// <reference path="jquery-3.0.0.js" />

$(function () {
    $("#password").change(validate);
    $("#confirmPassword").keyup(validate);

    function validate() {
        if ($("#password").val() != $("#confirmPassword").val()) {
            $("#confirmPassword")[0].setCustomValidity("Passwords don't match");
        } else {
            $("#confirmPassword")[0].setCustomValidity("");
        }
    }
})