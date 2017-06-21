
$("#hint50").on("click", fiftyHint);
$("#hint50crossed").hide();

$("#friendCall").on("click", friendCallHint);
$("#friendCallCrossed").hide();

$("#audienceAsk").on("click", AskAudienceHint);
$("#audienceAskCrossed").hide();


function fiftyHint() {
    $.ajax({
        url: window.redirectToFiftyUrl,

        contentType: 'application/html; charset=utf-8',

        dataType: 'html',

        data: { questionId: window.questionId },

        success: function (result) {
            $("#question").html(result);
            changeHint50Image();
            enableEvents();
        }
    });
}

function friendCallHint() {
    
}

function AskAudienceHint() {
   $.ajax({
        type: "GET",
        url: window.redirectToAskAudience,
        contentType: "application/html; charset=utf-8",
        data: { questionId: window.questionId },
        datatype: "html",
        success: function (result) {  
            $('#audienceModalContent').html(result);
            $('#audienceModal').modal('show');
            $("#audienceAsk").hide();
            $("#audienceAskCrossed").show();
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

function enableEvents() {
    for (var i = 0; i < 4; i++) {
        var button = $("#" + i);
        if (button.text() != "") {
            $(button).on("click", checkAnswer);
        }
    }
}