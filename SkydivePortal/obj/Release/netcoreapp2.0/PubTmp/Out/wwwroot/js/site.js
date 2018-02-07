// Write your JavaScript code.

$(document).ready(function () {
    $(".note-toggle").click(function () {
        $(this).parent().parent().next('.note-container').slideToggle(200);
    });

    $(".comment-toggle").click(function () {
        $(this).parent().next('.dz-user-post-comments-container').slideToggle(200);
    });

    $(".event-toggle").click(function () {
        $(this).parent().next('.dz-event-description').slideToggle(200);
    });



    $(".refresh-comment-button").click(function () {
        var element = $(this).next('.dz-user-post-just-comments');

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

    });

    $('.dz-add-user-post-comment-button').click(function () {
        var $form = $(this).parent().parent().parent();

        $.ajax({
            type: "POST",
            url: '/Dropzone/AddDzUserPostComment',
            data: $form.serialize(),
            error: function (xhr, status, error) {
            },
            success: function (response) {
                $form[0].reset();
            }
        });

        return false;

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
        })
    }, 1000);    




    
});

