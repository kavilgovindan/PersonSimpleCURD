﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

showModelPopup = (url, title) => {
    $.ajax({
        type: "GET",
        url: url,
        success: function (res) {
            $("#form-modal .modal-body").html(res);
            $("#form-modal .modal-title").html(title);
            $("#form-modal").modal('show');
        }
    })
}

FormAjaxPost = form => {
    try {
        $.ajax({
            type: "POST",
            url: form.action,
            data: new FormData(from),
            contentType: false,
            processData: false,
            success: function (res) {
                if (res.isvalid) {
                    $("#person-list").html(res.html);
                    $("#form-modal .modal-body").html('');
                    $("#form-modal .modal-title").html('');
                    $("#form-modal").modal('hide');
                }
                else{
                    $("#form-modal .modal-body").html(res.html);
                }
            },
            error: function (err) {
                console.log(err);
            }

        })
        // return false;
    }
    catch (e){
        console.log(e);
    }

    return false;
}