$(document).ready(function () {
    $('#upRating, #downRating').click(function () {
        if (document.getElementById('upRating').className != 'btn btn-labeled btn-success disabled') {
            addRating(this);
        }
    })
})

function addRating(element) {
    var like = false;

    if (element.id == 'upRating') {
        like = true;
    }

    $.ajax({
        type: 'POST',
        url: "/SiteBuilder/AddRating",
        data: {
            SiteId: document.getElementById("idSite").value,
            UserId: document.getElementById("idUser").value,
            Like: like,
        },
        success: function (data) {
            showRating(like);
        },
        error: function (data) {
            //
        }
    });
}

function showRating(like) {
    document.getElementById('upRating').className = 'btn btn-labeled btn-success disabled';
    document.getElementById('downRating').className = 'btn btn-labeled btn-danger disabled';

    var rating = Number($('#likesCounter').children(':first').html());

    if (like) {
        rating += 1;
    }
    else {
        rating -= 1;
    }

    $('#likesCounter').children(':first').html(rating);
}