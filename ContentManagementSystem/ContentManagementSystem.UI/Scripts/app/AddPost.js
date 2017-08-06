var url = '/api/Post/';

var tagArray = [];
var categoryArray = [];
var statuses = [];
var staffSubmitStatus;

function init() {
    $.getJSON('/api/Category', null, function () { })
       .done(function (data) {
           categoryArray = data;
           $.each(data, function (index, category) {
               $(createCategoryDropDown(category)).appendTo($('#selectCategory'));
           });
           $.getJSON('/api/PostStatus/', null, function () { })
               .done(function (data) {
               statuses = data;
               $.each(statuses, function (index, stat) {
                   $(createStatusDropDown(stat)).appendTo($('#statuses'));
               });
           });
       });
};

function createStatusDropDown(stat) {
    return '<option value=' + stat.Id + '>' + stat.Status + '</option>';
}

function createCategoryDropDown(data) {
    return '<option value=' + data + '>' + data.Name + '</option>';
}

function errorMessage(error) {
    return '<div class="alert alert-danger" role="alert" id="danger-alert">' +
        '<button type="button" class="close" data-dismiss="alert" aria-label="Close">' +
        '<span aria-hidden="true">&times;</span></button>' + error + '</div>';
}

function ClearModal() {
    tagArray = [];
    categoryArray = [];
    $("#title").val("");
    //window.tinymce.get('content').setContent("");
    $("#tags").val("");
    $("expireDate").datepicker('setDate', null);
    $("publishDate").datepicker('setDate', null);
    $('#showTags').text("");
    $('#showCategories').text("");
}

function GetAllTags() {
    $("#showTags span").each(function () {
        var tagText = $(this).text();
        tagArray.push(tagText);
    });
}

function GetAllCategories() {
    $("#showCategories span").each(function () {
        var catText = $(this).text();
        categoryArray.push(catText);
    });
}


function SubmitPost() {
    var bPost = {};

    bPost.Title = $('#title').val();
    bPost.Content = window.tinymce.get('content').getContent();
    bPost.PublishDate = $('#publishDate').val();
    bPost.ExpireDate = $('#expireDate').val();
    if (window.userIsAdmin === 'True') {
        bPost.Status = $('#statuses option:selected').text();
    }
    else {
        bPost.Status = staffSubmitStatus;
    }
    GetAllTags();
    bPost.TagList = tagArray;
    GetAllCategories();
    bPost.CategoryList = categoryArray;
    if ($("#addPostForm").valid()) {
        $.post(url, bPost)
        .done(function () {
           $('body').faLoadingStop();
            $('#addPostModal').modal('hide');
            if (window.userIsAdmin === 'True') {
                GetPending();
            }
            else {
                GetNeedsRevised();
            }
        })
        .fail(function (jqXhr, status, error) {
            $('body').faLoadingStop();
            $(errorMessage(error)).appendTo($('#errors'));
            $('#addPostModal').modal('hide');
        });
    }
    //this is a song that never ends, it goes on and on my friends.... 
}

function UpdateDraft() {
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
        bPost.Status = 'Draft';
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
                GetDrafts();
            }
            $('#addPostModal').modal('hide');
        }
    });
}

$(document).ready(function () {
    init();
    $(function () {
        $("#publishDate").datepicker();
        $("#expireDate").datepicker();
    });

    $('#tagButton').click(function () {
    //    $("#showTags").append("<span id='" + $('#tags').val() + "'>" + $('#tags').val() +
    //                 "<span class='glyphicon glyphicon-remove-sign removeTag' onClick='AddTag(" + $('#tags').val() + ");' aria-hidden='true'></span></span>");

        $("#showTags").append("<span>" + $('#tags').val() +
            "<span class='glyphicon glyphicon-remove-sign removeTag' aria-hidden='true'></span></span>")
            .data("tagName", $('#tags').val());

        //$('#showTags').append($('#tags').val() + ' ');
        //tagArray.push($('#tags').val());
    });
    $('#categoryButton').click(function () {
        $("#showCategories").append("<span>" + $('#selectCategory option:selected').text() +
                    "<span class='glyphicon glyphicon-remove-sign removeCat' aria-hidden='true'></span></span>")
                        .data("catName", $('#selectCategory option:selected').text());
                    
        //$('#showCategories').append($('#selectCategory option:selected').text() + ' ');
        //categoryArray.push($('#selectCategory option:selected').text());
    });

    $(document).on("click", ".removeTag", function (e) {
        e.preventDefault();
        var index = $.inArray($(this).parent().data("tagName"), tagArray);
        if (index >= 0) tagArray.splice(index, 1);
        $(this).parent().remove();
    });

    $(document).on("click", ".removeCat", function (e) {
        e.preventDefault();
        var index = $.inArray($(this).parent().data("catName"), categoryArray);
        if (index >= 0) categoryArray.splice(index, 1);
        $(this).parent().remove();
    });

    $(document).on("click", "#submitApproval", function(e) {
        e.preventDefault();
        staffSubmitStatus = "PendingReview";
        $('body').faLoading();
        SubmitPost();
    });

    $(document).on("click", "#saveDraft", function(e) {
        e.preventDefault();
        staffSubmitStatus = "Draft";
        $('body').faLoading();
        if (editDraftId == null) {
            SubmitPost();
        }
        else {
            UpdateDraft();
        }
        
    });

    $('#savePost').click(function () {
        $('body').faLoading();
        SubmitPost();
    });

    $('#btnAddPost').click(function () {
        ClearModal();
        $('#saveEditPost').hide();
        $("#submitEditApproval").hide();
        $("#submitApproval").show();
        $('#addPostModal').modal('show');
    });
});

