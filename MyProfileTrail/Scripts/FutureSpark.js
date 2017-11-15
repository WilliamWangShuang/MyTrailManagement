function register_Email_Tooltip() {
    $('#register_email_tooltip').show();
    $("#register_email_tooltip").delay(5000).hide();
}

$(document).ready(function () {
    
    var tooltips = $("[title]").tooltip({
        position: {
            my: "left top",
            at: "right+5 top-5",
            collision: "none"
        }
    });

});