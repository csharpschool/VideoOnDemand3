// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    $('.module-card-header').on('click', function () {
        $(this).siblings('.module-video, .module-downloads').toggle();// Toggles display: none
    });

    $('#comments li').on('click', 'button.media-replies', function (e) {
        console.log('clicked');
        e.stopPropagation();
        let li = $(this).parent().parent().parent().parent().parent();
        li.children('ul').toggle();
    });

    $('#comments li').on('click', 'button.media-reply', function (e) {
        console.log('clicked');
        e.stopPropagation();
        $(this).parent().parent().siblings('.media-input').toggle();
    });

   /* $('#comments li button.media-save').on('click', function (e) {
        e.stopPropagation();
        let button = $(this);
        let inputTitle = $('input.media-comment-input-title');
        let inputBody = $('input.media-comment-input-body');
        let title = inputTitle.val();
        let body = inputBody.val();
        let parentId = button.attr('id');
        let antiforegry = $('[name="__RequestVerificationToken"]').val();
        console.log(title, body, parentId, antiforegry);

        // MAKE AJAX CALL TO BACKEND ACTION 
        //$.ajax("api/comments/", { method: "post" })
        //    .then(function () {
        //    });

        //$.post("/api/comments/", { parentId, title, body, __RequestVerificationToken: antiforegry })
        //    .done(function (data) {
        //        alert("Data Loaded: " + data);
        //    });

        //$.ajax({
        //    type: 'post',
        //    url: '/api/comments/',
        //    data: { parentId, title, body, __RequestVerificationToken: antiforegry },
        //    contentType: 'application/json'
        //}).then(function (data) {
        //    alert("Data Loaded: " + data);
        //});

        //$.post("/api/comments/", { parentId, title, body, __RequestVerificationToken: antiforegry },
        //    function (data) {
        //        alert("Data Loaded: " + data);
        //    }).fail(function (xhr, status, error) { alert("Error: " + error); });



        //$.ajax("/api/comments/", { method: "get" })
        //    .then(function (response) {
        //        console.dir(response);
        //});

        $.ajax({
            method: "POST",
            url: "/api/comments/",
            //data: { parentId: parentId, title: title, body: body },
            data: {
                "parentId": 7,
                "title": "Title 1",
                "body": "Body 1" },
            //headers:
            //{
            //    "RequestVerificationToken": antiforegry
            //},
            contentType: 'application/json; charset=utf-8',
            dataType: 'json'
        })
            .done(function (msg) {
                alert("Data Saved: " + msg);
            });

        button.parent().parent().parent().toggle();
        inputBody.val("");
        inputTitle.val("");
    });*/

});