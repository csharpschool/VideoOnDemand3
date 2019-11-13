// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    $('.module-card-header').on('click', function () {
        $(this).siblings('.module-video, .module-downloads').toggle();// Toggles display: none
    });

    $('#comments li button.media-replies').on('click', function (e) {
        e.stopPropagation();
        let li = $(this).parent().parent().parent().parent().parent();
        li.children('ul').toggle();
        //$(this).children('ul').toggle();
        console.log(this);
        //console.log(.html());
    });

    $('#comments li button.media-reply').on('click', function (e) {
        e.stopPropagation();
        $(this).parent().parent().siblings('.media-input').toggle();
        //let id = $(this).attr('id');
        console.log('reply');
    });

    $('#comments li button.media-save').on('click', function (e) {
        e.stopPropagation();
        let button = $(this);
        let input = button.siblings('input');
        let text = input.val();
        let id = button.attr('id');

        console.log('save', text, id);

        button.parent().toggle();
        input.val("");
    });

});