function ClearStaticPageModal() {
    $("#spTitle").val("");
    window.tinymce.get('spMCEcontent').setContent("");
    $('#spIsLive').prop('checked', false);
}

function createStaticPageRow(item) {
    var tr = $("<tr></tr>").attr("id", "staticPage-" + item.Id)
         .append($("<td></td>").text(item.Title))
         .append($("<td></td>").html(item.Content))
         .append($("<td></td>")
             .append($("<button type='button' class='btn btn-success viewStaticPage'>View</button>")
                 .data("staticPageId", item.Id)
             )
         )
         .append($("<td></td>")
             .append($("<button type='button' class='btn btn-danger deleteStaticPage'>Delete</button>")
                 .data("staticPageId", item.Id)
             )
         )
         .append($("<td></td>")
             .append($("<button type='button' class='btn btn-info editStaticPage'>Edit</button>")
                 .data("staticPageId", item.Id)
             )
         );
    return tr;
}

function SubmitStaticPage() {
    var sp = {};

    sp.Title = $('#spTitle').val();
    sp.Content = window.tinymce.get('spMCEcontent').getContent();
    if ($('#spIsLive').is(":checked")) {
        sp.IsLive = true;
    } else {
        sp.IsLive = false;
    }
    $.post(staticPageAPI, sp)
        .done(function () {
            $('body').faLoadingStop();
            $('#staticPageModal').modal('hide');
            if (window.typeOfUser === "Admin") {
                GetPending();
            }
            else {
                GetDrafts();
            }
        })
        .fail(function (jqXhr, status, error) {
            $(errorMessage(error)).appendTo($('#errors'));
            $('#staticPageModal').modal('hide');
        });
}

function DeleteStaticPage(id) {
    $.ajax({
        url: staticPageAPI + id,
        type: 'DELETE',
        success: function () {
            $('body').faLoadingStop();
            $("#staticPage-" + id).remove();
        }
    });
}

function EditStaticPage(id) {
    $.getJSON(staticPageAPI + id)
        .done(function (data) {
            //$('body').faLoadingStop();
            //setUpForEdit();
            editId = data.Id;
            $(".modal-body #spTitle").val(data.Title);
            if (data.Content != null) {
                window.tinyMCE.get('spMCEcontent').setContent(data.Content);
            }
            if (data.IsLive == true) {
                $('#spIsLive').prop('checked', true);
            } else {
                $('#spIsLive').prop('checked', false);
            }
            

            $('#staticPageModal').modal('show');
        });
}

function SubmitEditStaticPage() {
    var statPage = {};

    statPage.Id = editId;
    statPage.Title = $('#spTitle').val();
    statPage.Content = window.tinymce.get('spMCEcontent').getContent();
    if ($('#spIsLive').is(":checked")) {
        statPage.IsLive = 'true';
    } else {
        statPage.IsLive = 'false';
    }

    $.ajax({
        url: staticPageAPI,
        type: 'PUT',
        data: statPage,
        success: function (response) {
            GetStaticPages();
            $('#staticPageModal').modal('hide');
        }
    });
}

function GetStaticPages() {
    $("li").removeClass("clicked");
    $("#StaticPages").addClass("clicked");
    $('body').faLoading();
    $.getJSON(staticPageAPI)
           .done(function (data) {
               $('body').faLoadingStop();
               $('#mainView tbody tr').remove();
               $.each(data, function (index, data) {
                   $(createStaticPageRow(data)).appendTo($('#mainView tbody'));
               });
           });
}

$(document).ready(function () {

    $(document).on("click", "#StaticPages", GetStaticPages);

    $('#saveStaticPage').click(function () {
        $('body').faLoading();
        SubmitStaticPage();
    });

    $(document).on("click", "#saveEditStaticPage", function (e) {
        e.preventDefault();
        SubmitEditStaticPage();
    });



    $(document).on("click", ".deleteStaticPage", function (e) {
        e.preventDefault();
        var btn = $(this);
        var staticPageId = $(btn).data("staticPageId");
        $('body').faLoading();
        DeleteStaticPage(staticPageId);
    });

    $('#btnAddStaticPage').click(function () {
        ClearStaticPageModal();
        $('#saveEditStaticPage').hide();
        $('#staticPageModal').modal('show');
    });

    $(document).on("click", ".editStaticPage", function (e) {
        e.preventDefault();
        $("#saveStaticPage").hide();
        $("#saveEditStaticPage").show();
        var btn = $(this);
        var staticPageId = $(btn).data("staticPageId");
        EditStaticPage(staticPageId);
    });

    $(document).on("click", ".viewStaticPage", function (e) {
        e.preventDefault();
        var btn = $(this);
        var staticPageId = $(btn).data("staticPageId");
        window.location.href = "/Page/Index/" + staticPageId;
    });
});