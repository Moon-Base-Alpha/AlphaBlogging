// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//------------------ Tag function -------------------------
$('#addKey').click(function (e) {
    addKey();
});


//Add key function
function addKey() {
    let key = $('#keysInput').val()
    if (key != '') {
        let tag = document.createElement('span')
        $(tag).append(key)
        $(tag).attr('onclick', `$(this).remove();removeKey("${key}");`)
        $('.addedKeysHolder').append(tag)
        $('#keysInput').val('')
        resetKey()
    }
}

//According to my goals, this function was equal to the resetKey(). But here I put it in a separate block, so you can easily customize it if you want
function removeKey() {
    resetKey()
}

//Reset all keys
function resetKey() {
    $('#lkey').val('')
    for (let i = 1; i <= $('.addedKeysHolder span').length; i++) {
        let theKey = $(`.addedKeysHolder span:nth-child(${i})`).text()
        let prevVal = $('#lkey').val() + ','
        $('#lkey').val(prevVal + theKey)
    }
    $('#lkey').val($('#lkey').val());
    $('#keysInput').focus();
}

//------------------ End Tag function -------------------------