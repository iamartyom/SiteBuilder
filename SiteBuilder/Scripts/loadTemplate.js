function loadTemplate(e) {
    $.ajax({
        type: 'POST',
        url: "/SiteBuilder/LoadTemplate",
        data: { nameTemplate: e.id },
        success: function (data) {
            $("#layout").empty();
            $("#layout").html(data);
        }
    });
}