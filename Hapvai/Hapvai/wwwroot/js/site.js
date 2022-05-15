// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

showInPopup = (url, title) => {
    console.log(url)
    $.ajax({
        type: 'GET',
        url: url,
        success: function (res) {
            $('#addProductModal .modal-body').html(res);
            $('#addProductModal .modal-title').html(title);
            $('#addProductModal').modal('show');
        }
    })

}

jQueryAjaxPost = form => {
    console.log('post action')
    try {
        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {
                if (res.isValid) {
                    console.log(res);
                    $('#view-all').html(res.html)
                    $('#addProductModal .modal-body').html('');
                    $('#addProductModal .modal-title').html('');
                    $('#addProductModal').modal('hide');
                }
                else
                    $('#form-modal .modal-body').html(res.html);
            },
            error: function (err) {
                console.log(err)
            }
        })
        //to prevent default form submit event
        return false;
    } catch (ex) {
        console.log(ex)
    }
}