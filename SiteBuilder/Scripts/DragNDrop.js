$(function () {
    $('#draggable').draggable();

    $('#droppable1, #droppable2, #droppable3, #droppable4').droppable({
        drop: function () {
            alert($(this).attr('id'));
        },
    });
});