$(document).ready(function () {
    $("#ForgotPasswordForm").on("submit", function (event) {
        var email = $("#Email").val();
        var emailPattern = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,6}$/;

        if (!emailPattern.test(email.toLowerCase())) {
            $("#spnEmail").html("Invalid Email Id.");
            $("#spnEmail").show();
            event.preventDefault(); // Prevent form submission
        } else {
            $("#spnEmail").html("");
            $("#spnEmail").hide();
        }
    });
});