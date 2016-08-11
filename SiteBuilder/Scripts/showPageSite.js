$(document).ready(function () {
    $.ajax({
        type: 'POST',
        url: "/SiteBuilder/LoadTemplate",
        data: { nameTemplate: document.getElementById('templateName').value },
        success: function (data) {
            $("#layout").empty();
            $("#layout").html(data);
        }
    });
});