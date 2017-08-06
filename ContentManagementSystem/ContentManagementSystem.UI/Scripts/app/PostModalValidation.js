$(document).ready(function() {
    $("#addPostForm").validate({
        rules : {
            title : {
                required: true
            },
            publishDate: {
                required: true,
                date: true
            }
        }, invalidHandler: function (event, validator) {
            // 'this' refers to the form
            var errors = validator.numberOfInvalids();
            //$('body').faLoadingStop();
            alert(errors + "Evil teapots");
        }
       
    });
})