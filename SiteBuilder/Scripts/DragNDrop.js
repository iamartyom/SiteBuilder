$(function () {
    $('#image, #video, #text').draggable({
        revert: function () {
            return true;
        },
    });

    $('[id^=droppable\]').droppable({
        drop: function (event, ui) {
            var id = ui.draggable.attr('id');

            if (id == "image") {
                uploadImage(this);
            }
            else if (id == "video") {
                askBox(this);
            }
            else if (id == "text") {
                markdownEditor(this);
            }
            else {
                alert('wtf');
            }
        },
    });
});

function getVideoId(url) {
    var regExp = /^.*(youtu.be\/|v\/|u\/\w\/|embed\/|watch\?v=|\&v=)([^#\&\?]*).*/;
    var match = url.match(regExp);
    if (match && match[2].length == 11)
    {
        return match[2];
    }
    else
    {
        Example.show("Invalid youtube link");
        return;
    }
}

function askBox(element)
{
    bootbox.prompt
        ({
        title: "Enter youtube link",
        callback: function (result)
        {
            if (result === null)
            {
                return;
            }
            else
            {
                var videoId = getVideoId(result);
                if (typeof videoId == 'undefined')
                {
                    return;
                }
                var code = '<iframe width="' + $(element).width() + '" height = "' + $(element).width() / 4 * 3 + '" src="//www.youtube.com/embed/' + videoId + '" frameborder="0" class="2"></iframe>';
                add(element, code);
            }
        }
    })
}

function markdownEditor(element) {
    $('#myModal').modal();
    $('#textButton').unbind().click(function () {
        if ($('#textareaForm').data('markdown').parseContent() != "") {
            var formData = '<input type="hidden" class="3"><div>' + $('#textareaForm').data('markdown').parseContent() + '</div>';
            add(element, formData);
        }
        else {
            Example.show('You didn`t input text');
        }
    });
}


function uploadImage(element) {
    cloudinary.openUploadWidget({ cloud_name: 'tematema', upload_preset: 'ciwchslt' },
      function (error, result) { console.log(error, result) });

    $(document).one('cloudinarywidgetsuccess', function (e, data) {
        var code = "<img src='" + data[0].secure_url + "' width='" + $(element).width() + "' class='1' />"
        add(element, code);
    });
}

function add(element, code) {
    $(element).empty();
    $(element).html(code);
    $(element).height("100%");
}

function submitForm(pageNumber) {
    if ($('#inputPageName').val().length != 0) {
        var countBlocksTemplate = Number(blocksTemplate()) + 1;

        var result = 0;

        for (var i = 1; i < countBlocksTemplate; i++) {
            result += checkEmpty('#droppable' + i);
        }

        if (result == (countBlocksTemplate - 1)) {
            savePage($('#inputSiteId').attr('value'), $('#inputPageName').val(), pageNumber);
        }
        else {
            alert('Please insert data in layout.');
        }
    }
    else {
        Example.show('Input page name please');
    }
}

function checkEmpty(element) {
    if ($(element).is(':empty')) {
        return 0;
    }
    else {
        return 1;
    }
}

function savePage(siteId, name, pageNumber) {
    var countBlocksTemplate = Number(blocksTemplate())+1;

    $.ajax({
        type: 'POST',
        url: "/SiteBuilder/SavePage",
        data: {
            SiteId: siteId,
            TemplateId: $('.template').attr('id'),
            PageNumber: pageNumber,
            Name: name,
        },
        success: function (data) {
            for (var i = 1; i < countBlocksTemplate; i++) {
                saveData(data, i, "#droppable" + i.toString());
            }

            $("#formPage").submit();
        },
        error: function (data) {
            alert('Error save page. Please refresh page.');
        }
    });
}

function saveData(id, position, tagId) {
    var contentTypeId = $(tagId).children(':first').attr('class');
    var data = null;

    if (contentTypeId == '3') {
        data = toMarkdown($(tagId).children(':first').next().html());
    }
    else 
    {
        data = $(tagId).children(':first').attr('src');
    };

    $.ajax({
        type: 'POST',
        url: "/SiteBuilder/SaveData",
        data: {
            PageId: id,
            Position: position,
            Data: data,
            ContentTypeId: contentTypeId,
        },
        success: function (data) {
            //
        },
        error: function (data) {
            //
        }
    });
}

function blocksTemplate() {
    var currentTemplate = $(".template").attr('id');
    var countBlocksTemplate = $("#Template" + currentTemplate).attr('value');

    return countBlocksTemplate;
}