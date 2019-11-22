$(document).ready(function () {
    $('.module-card-header').on('click', function () {
        $(this).siblings('.module-video, .module-downloads').toggle();// Toggles display: none
    });

    $('#comments li').on('click', 'button.media-replies', function (e) {
        e.stopPropagation();
        let li = $(this).parent().parent().parent().parent().parent();
        li.children('ul').toggle();
    });

    $('#comments li').on('click', 'button.media-reply', function (e) {
        e.stopPropagation();
        $(this).parent().parent().siblings('.media-input').toggle();
    });

    // Executes after the form has been successfully submitted (data-ajax-success)
    topCommentFormSuccess = function (xhr) {
        document.getElementById("top-comment-form").reset();
    };
});