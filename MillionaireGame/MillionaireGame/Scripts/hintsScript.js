
$("#hint50").on("click", fiftyHint);
$("#hint50crossed").hide();

function fiftyHint() {
    $.ajax({
        url: window.redirectToFiftyUrl,

        success: function () {
            
            changeHint50Image();
        }
    });
}

function changeHint50Image() {
    $("#hint50").hide();
    $("#hint50crossed").show();
    //var image = $("#hint50");
    //image.off("click", fiftyHint);
    //image.attr("src", window.hint50CrossedImage);
}