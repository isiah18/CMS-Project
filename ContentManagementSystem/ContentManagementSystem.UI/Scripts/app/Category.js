
function ClearCatModal() {
    $("catName").val("");
}

function SubmitCategory() {
    var c = {}
    c.Name = $("#catName").val();

    $.post('/api/Category/', c)
        .done(function () {
            $('body').faLoadingStop();
            $('#categoryModal').modal('hide');
            window.location.href = "/Home/Index/";
        })
        .fail(function (jqXhr, status, error) {
            $('body').faLoadingStop();
            $(errorMessage(error)).appendTo($('#errors'));
            $('#categoryModal').modal('hide');
        });
}

$(document).ready(function() {
    $(document).on("click", "#btnAddCategory", function(e) {
        e.preventDefault();
        ClearCatModal();
        $('#categoryModal').modal('show');
    });

    $(document).on("click", "#saveCategory", function(e) {
        e.preventDefault();
        $('body').faLoading();
        SubmitCategory();
    });
})