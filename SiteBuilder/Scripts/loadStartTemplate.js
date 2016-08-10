$(document).ready(function () {
    $.ajax({
        type: 'POST',
        url: "/SiteBuilder/LoadTemplate",
        data: { nameTemplate: "template1" },
        success: function (data) {
            $("#layout").empty();
            $("#layout").html(data);
        }
    });
});