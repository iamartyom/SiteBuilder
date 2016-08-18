$(function () {
                $('.userSite').mouseover(function () {
                    loadEditPanel(this);
                });
            });

            function loadEditPanel(element) {
                $.ajax({
                    type: 'POST',
                    url: "/User/LoadEditPanel/",
                    data: {
                        nameSite: $(element).children(':first').attr('id'),
                        user : $(element).children(':first').attr('class')
                    },
                    success: function (data) {
                        $('.editPanel').empty();
                        $(element).after(data);
                    }
                });
            }