$(document).ready(function () {
    $.ajax({
        type: 'POST',
        url: "/SiteBuilder/LoadTemplate",
        data: { nameTemplate: document.getElementById('templateName').value },
        success: function (data) {
            $("#layout").empty();
            $("#layout").html(data);

            addContent();
        }
    });
});

function addContent() {
    var countBlocksTemplate = Number($('#countBlocksTemplate').attr('value')) + 1;

    for (var i = 1; i < countBlocksTemplate; i++) {
        $("#droppable" + i).empty();

        var contentType = document.getElementById('content_'+i).className;
        var data = document.getElementById('content_' + i).value;
        var templateContent = '';

        var widthBlock = $('#droppable' + i).width();

        switch (contentType) {
            case 'Image':
                templateContent = '<img src="' + data + '" width="' + widthBlock + '" />'
                break;
            case 'Video':
                templateContent = '<iframe width="' + widthBlock + '" height = "' + widthBlock / 4 * 3 + '" src="' + data + '" frameborder="0" class="2"></iframe>';
                break;
            case 'Markdown':
                templateContent = markDownToHtml(data);
                break;
            default:
                break;
        }

        $('#droppable' + i).html(templateContent);
        $('#droppable'+ i).height('100%');
    }
}

function markDownToHtml(data) {
    var converter = new showdown.Converter();
    return converter.makeHtml(data);
}