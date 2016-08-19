$(document).ready(function () {
    $.ajax({
        type: 'POST',
        url: "/SiteBuilder/ShowComments",
        data: {
            id: document.getElementById("idPage").value,
        },
        success: function (data) {
            $("#comments").empty();
            $("#comments").html(data);

            initializeComments();
        },
        error: function (data) {
            //
        }
    });
})

function initializeComments() {
    $('#comments-container').comments({
        enableReplying: false,
        enableEditing: false,
        enableUpvoting: false,

        profilePictureURL: 'https://viima-app.s3.amazonaws.com/media/user_profiles/user-icon.png',
        roundProfilePictures: true,
        textareaRows: 1,
        getComments: function (success, error) {
            setTimeout(function () {
                success(commentsArray);
            }, 500);
        },
        postComment: function (data, success, error) {
            saveComment(data);

            setTimeout(function () {
                success(data);
            }, 500);
        },
        putComment: function (data, success, error) {
            setTimeout(function () {
                success(data);
            }, 500);
        },
        deleteComment: function (data, success, error) {
            setTimeout(function () {
                success();
            }, 500);
        },
    });
}

function saveComment(data) {
    $.ajax({
        type: 'POST',
        url: "/SiteBuilder/SaveComment",
        data: {
            PageId: document.getElementById("idPage").value,
            UserId: document.getElementById('idUser').value,
            Content: data.content,
            Date: data.created,
        }
    });
}