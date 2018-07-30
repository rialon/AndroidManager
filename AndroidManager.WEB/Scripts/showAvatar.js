/// <reference path="jquery-3.0.0.js" />

$(function () {
    $("#avatarUpload").change(showAvatar);

    function showAvatar() {
        if (this.files && this.files[0]) {
            var reader = new FileReader();
            reader.onload = function (e) {
                $('#avatarImage')
                    .attr('src', e.target.result)
            };
            reader.readAsDataURL(this.files[0]);
        }
    }
})