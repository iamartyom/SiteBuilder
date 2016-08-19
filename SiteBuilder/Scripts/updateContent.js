function updateData(id) {
    var result = updateName(id);

    if (result) {
        var countBlocksTemplate = Number(document.getElementById('countBlocksTemplate').value) + 1;

        for (var x = 1; x < countBlocksTemplate; x++) {
            updateContent(id, x);
        }
    }

    history.go(-1);
}

function updateName(id) {
    if (document.getElementById("namePage").value != '') {
        $.ajax({
            type: 'POST',
            url: "/SiteBuilder/UpdatePageName",
            data: {
                id: id,
                name: document.getElementById("namePage").value,
            }
        });

        return true;
    }
    else {
        return false;
    }
}

function updateContent(id, position) {
    var contentTypeId = $('#droppable' + position).children(':first').attr('class');
    var data = null;

    switch (contentTypeId) {
        case '1':
            data = $('#droppable' + position + ' img').attr('src');
            break;
        case '2':
            data = $('#droppable' + position + ' iframe').attr('src');
            break;
        case '3':
            data = toMarkdown($('#droppable' + position).children(':first').next().html());
            break;
        default:
            break;
    }

    updateContentAjax(id, position, data, contentTypeId);
}

function updateContentAjax(id, position, data, contentTypeId) {
    $.ajax({
        type: 'POST',
        url: "/SiteBuilder/UpdateContent",
        data: {
            id: id,
            position: position,
            data: data,
            contentTypeId: contentTypeId,
        }
    });
}