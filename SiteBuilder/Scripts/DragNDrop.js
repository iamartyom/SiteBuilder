$(function () {
    $('#draggable').draggable();

    $('#droppable1, #droppable2, #droppable3, #droppable4').droppable({
        drop: function () {
            $(this).empty();
            var img = '<img src="http://www.sunhome.ru/UsersGallery/Cards/den_ulibki_kartinka.jpg" width="' + $(this).width() + '" />'
            $(this).html(img);
            $(this).height($(this).find('img').height());
        },
    });
});