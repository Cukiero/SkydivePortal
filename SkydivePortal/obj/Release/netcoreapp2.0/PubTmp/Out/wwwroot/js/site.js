// Write your JavaScript code.

$(document).ready(function () {

    function refreshComments(element) {
        var id = element.data('postid');
        $.ajax({
            url: '/Dropzone/GetPostComments',
            type: 'GET',
            data: { postid: id },
            dataType: 'html',
            success: function (data) {
                element.html(data);
            },
            error: function (xhr, status, error) {
                $(".dz-user-post-just-comments").html(xhr.responseText);
            }
        });
    }

    

    $(".note-toggle").click(function () {
        $(this).parent().parent().next('.note-container').slideToggle(200);
    });

    $(".comment-toggle").click(function () {
        $(this).parent().next('.dz-user-post-comments-container').slideToggle(200);
    });

    $(".event-toggle").click(function () {
        $(this).parent().next('.dz-event-description').slideToggle(200);
    });

    $("body").on("click", ".dz-comment-remove-btn" ,function () {
        var id = $(this).data('commentid');
        var element = $(this).parent().parent();
        $.ajax({
            url: '/Dropzone/RemoveDzUserPostComment',
            type: 'GET',
            data: { id: id },
            dataType: 'html',
            success: function () {
                refreshComments(element);
            },
            error: function (xhr, status, error) {
            }
        });
    });
    

    $('.dz-add-user-post-comment-button').click(function () {
        var $form = $(this).parent().parent().parent();
        var element = $(this).parent().parent().parent().prev();
        $.ajax({
            type: "POST",
            url: '/Dropzone/AddDzUserPostComment',
            data: $form.serialize(),
            error: function (xhr, status, error) {
            },
            success: function (response) {
                $form[0].reset();
                refreshComments(element);
            }
        });

        return false;

    });
    $(".dz-add-user-post-comment-input").keydown(function (e) {
        e = e || event;
        if (e.keyCode === 13 && e.shiftKey) {
            e.preventDefault();
            var $form = $(this).parent().parent().parent();
            var element = $(this).parent().parent().parent().prev();
            $.ajax({
                type: "POST",
                url: '/Dropzone/AddDzUserPostComment',
                data: $form.serialize(),
                error: function (xhr, status, error) {
                },
                success: function (response) {
                    $form[0].reset();
                    refreshComments(element);
                }
            });
        }
    });


    autosize($('textarea'));



    $(".form-jump-input").change(function () {
        $(this).css('background-color', 'rgba(252, 255, 76, 0.7)');
    });
    $(".form-jump-input-note").change(function () {
        $(this).css('background-color', 'rgba(252, 255, 76, 0.7)');
    });


    setInterval(function () {
        $(".dz-user-post-just-comments").each(function () {
            var element = $(this);
            if (element.parent().parent().css('display') != 'none') {
                refreshComments(element);
            }
        });

    }, 2000);   

 
    



    
});

