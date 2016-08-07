$(function () {
    $('#draggable').draggable();

    $('#droppable1, #droppable2, #droppable3, #droppable4').droppable({
        drop: function () {
            $(this).empty();
            var img = '<img src="http://minionomaniya.ru/wp-content/uploads/2016/01/%D0%9A%D0%B5%D0%B2%D0%B8%D0%BD.jpg" width="' + $(this).width() + '" />'
            $(this).html(img);
            $(this).height($(this).find('img').height());
        },
    });
});