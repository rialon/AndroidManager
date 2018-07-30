/// <reference path="jquery-3.0.0.js" />

$(function () {
    $("#noImageCheck").change(disbableImageUpload);

    function disbableImageUpload() {
        if ($(this).prop("checked")) {
            $("#avatarUpload").prop('disabled', true);
            $('#avatarImage').attr('src', null)
            if ($("#avatarUpload")[0].files && $("#avatarUpload")[0].files[0]) {
                $("#avatarUpload")[0].files[0] = null;
            }
        } else {
            $("#avatarUpload").prop('disabled', false);
        }
    }
})