var uri = "/api/Post/";

var editId;

function DeletePost(id) {
     $.ajax({
         url: uri + id,
         type: 'DELETE',
         success: function () {
             $('body').faLoadingStop();
             $("#post-" + id).remove();
         }
     });
 }

 function setUpForEdit() {
     $('#showTags').text("");
     $('#showCategories').text("");
     window.tagArray = [];
     window.categoryArray = [];
 }

 function EditPost(id) {
     $.getJSON('/api/Post/' + id)
         .done(function (data) {
             $('body').faLoadingStop();
             setUpForEdit();
             editId = data.Id;
             $(".modal-body #title").val(data.Title);
             if (data.Content != null) {
                 window.tinyMCE.get('content').setContent(data.Content);
             }
             $("#statuses").val(data.Status.Id);
             $("#publishDate").datepicker('setDate', moment(data.PublishDate).format("MM/DD/YYYY"));
             if (data.ExpireDate != null) {
                 $("#expireDate").datepicker('setDate', moment(data.ExpireDate).format("MM/DD/YYYY"));
             }
             $("#expireDate").datepicker('setDate', null);
             for (var s = 0; s < data.TagsOnPost.length; s++) {
                 $("#showTags").append("<span>" + data.TagsOnPost[s].Name +
                     "<span class='glyphicon glyphicon-remove-sign removeTag' aria-hidden='true'></span></span>");
             }
             for (var c = 0; c < data.CategoriesOnPost.length; c++) {
                 $("#showCategories").append("<span>" + data.CategoriesOnPost[c].Name +
                     "<span class='glyphicon glyphicon-remove-sign removeCat' aria-hidden='true'></span></span>");
             }
             $('#addPostModal').modal('show');
         });
 }

 function createRow(item) {
     var tr = $("<tr></tr>").attr("id", "post-" + item.Id)
         .append($("<td></td>")
             .append($("<div></div>").text(item.Title)
             )
         )
         .append($("<td></td>")
             .append($("<div></div>").html(item.Content)
             )
         )
         .append($("<td></td>")
             .append($("<button type='button' class='btn btn-success viewPostDetails'>View</button>")
                 .data("postid", item.Id)
             )
         )
         .append($("<td></td>")
             .append($("<button type='button' class='btn btn-danger deletePost'>Delete</button>")
                 .data("postid", item.Id)
             )
         )
         .append($("<td></td>")
             .append($("<button type='button' class='btn btn-info editPost'>Edit</button>")
                 .data("postid", item.Id)
             )
         );
     return tr;
 }

 //function wrap() {
 //    $(".wrapper").dotdotdot({
 //        ellipsis: '... ',
 //        wrap: 'word',
 //        fallbackToLetter: true,
 //        /*	jQuery-selector for the element to keep and put after the ellipsis. */
 //        after: "a.readmore",
 //        /*	Whether to update the ellipsis: true/'window' */
 //        watch: "window",
 //        height: 150,
 //        tolerance: 0,
 //        callback: function (isTruncated, orgContent) { },
 //        lastCharacter: {
 //            remove: [' ', ',', ';', '.', '!', '?'],
 //            noEllipsis: []
 //        }
 //    });
 //}

function createStaffRow(item) {
    var tr = $("<tr></tr>").attr("id", "post-" + item.Id)
         .append($("<td></td>").text(item.Title))
         .append($("<td></td>").html(item.Content));
    return tr;
}

function createNeedsRevisedRow(item) {
    var tr = $("<tr></tr>").attr("id", "post-" + item.Id)
        .append($("<td></td>").text(item.Title))
        .append($("<td></td>").html(item.Content))
        .append($("<td></td>")
            .append($("<button type='button' class='btn btn-info editPost'>Edit</button>")
                .data("postid", item.Id)
            )
        );
    return tr;
}

 function GetPending() {
     $("li").removeClass("clicked");
     $("#Pending").addClass("clicked");
     $('body').faLoading('fa-spinner');
     $.getJSON('/api/Posts/' + "PendingReview")
         .done(function (data) {
             $('body').faLoadingStop();
             $("#mainView tbody tr").remove();
             $.each(data, function (index, item) {
                 $(createRow(item)).appendTo($("#mainView tbody"));
             });
         });
 }

 function GetDrafts() {
     $("li").removeClass("clicked");
     $("#Drafts").addClass("clicked");
     $('body').faLoading();
     $.getJSON('/api/Posts/' + "Draft")
            .done(function (data) {
                $('body').faLoadingStop();
                $('#mainView tbody tr').remove();
                $.each(data, function (index, data) {
                    $(createRow(data)).appendTo($('#mainView tbody'));
                });
            });
}

 function GetApproved() {
     $("li").removeClass("clicked");
     $("#Approved").addClass("clicked");
     $('body').faLoading();
     $.getJSON('/api/Posts/' + "Approved")
            .done(function (data) {
                $('body').faLoadingStop();
                $('#mainView tbody tr').remove();
                $.each(data, function (index, data) {
                    $(createRow(data)).appendTo($('#mainView tbody'));
                });
             //wrap();
         });
 }

 function GetStaffApproved() {
     $("li").removeClass("clicked");
     $("#StaffApproved").addClass("clicked");
     $('body').faLoading();
     $.getJSON('/api/ApprovedPosts/' + "Approved")
         .done(function (data) {
             $('body').faLoadingStop();
             $('#mainView tbody tr').remove();
             $.each(data, function (index, data) {
                 $(createStaffRow(data)).appendTo($('#mainView tbody'));
             });
         });
 }

 function GetNeedsRevised() {
     $("li").removeClass("clicked");
     $("#NeedsRevised").addClass("clicked");
     $('body').faLoading();
     $.getJSON('/api/Posts/' + "NeedsRevised")
        .done(function (data) {
            $('body').faLoadingStop();
            $('#mainView tbody tr').remove();
            $.each(data, function (index, data) {
                $(createNeedsRevisedRow(data)).appendTo($('#mainView tbody'));
            });
        });
 }

 function SubmitEditPost() {
     var bPost = {};

     bPost.Id = editId;
     bPost.Title = $('#title').val();
     bPost.Content = window.tinymce.get('content').getContent();
     bPost.PublishDate = $('#publishDate').val();
     bPost.ExpireDate = $('#expireDate').val();
     if (window.userIsAdmin === 'True') {
         bPost.Status = $('#statuses option:selected').text();
     }
     else {
         bPost.Status = 'PendingReview';
     }
     GetAllTags();
     bPost.TagList = window.tagArray;
     GetAllCategories();
     bPost.CategoryList = window.categoryArray;

     $.ajax({
         url: '/api/Post/',
         type: 'PUT',
         data: bPost,
         success: function (response) {
             if (window.userIsAdmin === 'True') {
                 GetPending();
             }
             else {
                 GetNeedsRevised();
             }
             $('#addPostModal').modal('hide');
         }
     });
 }

 $(document).ready(function () {
     if (window.userIsAdmin === 'True') {
         GetPending();
     }
     else {
         GetNeedsRevised();
     }
    $(document).on("click", "#Pending", GetPending);
    $(document).on("click", "#Drafts", GetDrafts);
    $(document).on("click", "#Approved", GetApproved);
    $(document).on("click", "#StaffApproved", GetStaffApproved);
    $(document).on("click", "#NeedsRevised", GetNeedsRevised);

    $(document).on("click", ".editPost", function (e) {
        e.preventDefault();
        $('body').faLoading();
        window.tagArray = [];
        window.categoryArray = [];
        $("#savePost").hide();
        $("#saveEditPost").show();
        var btn = $(this);
        var postId = $(btn).data("postid");
        window.editDraftId = postId;
        $("#submitApproval").hide();
        $("#submitEditApproval").show();
        EditPost(postId);
    });
    $(document).on("click", ".deletePost", function (e) {
        e.preventDefault();
        var btn = $(this);
        var postId = $(btn).data("postid");
        $('body').faLoading();
        DeletePost(postId);
    });

     $(document).on("click", "#submitEditApproval", function(e) {
         e.preventDefault();
         SubmitEditPost();
     });

    $(document).on("click", "#saveEditPost", function (e) {
        e.preventDefault();
        SubmitEditPost();
       //This is where i deleted from
        
    });

    $(document).on("click", ".viewPostDetails", function (e) {
         e.preventDefault();
         var btn = $(this);
         var postId = $(btn).data("postid");
         window.location.href = "/Home/BlogPost/" + postId;
     });
 });




