// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    $('.module-card-header').on('click', function () {
        $(this).siblings('.module-video, .module-downloads').toggle();// Toggles display: none
    });

    $('#comments li').on('click', function (e) {
        e.stopPropagation();
        $(this).children('ul').toggle();
        console.log(this);
    });

    $('#comments li button').on('click', function (e) {
        e.stopPropagation();
        $(this).parent().parent().siblings('div').toggle();
        //let id = $(this).attr('id');
    });

});