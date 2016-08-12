$(function () {

    Zepto('.funcs').dragswap({
        dropAnimation: false,
        dropComplete: function () {
            var sortArray = Zepto('.funcs').dragswap('toArray');
            $('#arrayResults').html('[' + sortArray.join(',') + ']');
            var sortJSON = Zepto('.funcs').dragswap('toJSON');
            $('#jsonResults').html(sortJSON);
            $.ajax({
                type: 'POST',
                url: "/SiteBuilder/SavePageNumber",
                data: {
                    SiteId: $('#inputSiteId').attr('value'),
                    PageValues: sortJSON
                },
                success: function (data) {
                    //
                },
                error: function (data) {
                    alert("error " + data);
                }
            });
        }
    });
});