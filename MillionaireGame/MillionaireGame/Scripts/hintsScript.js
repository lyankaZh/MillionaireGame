$("#hint50crossed").hide();
$("#friendCallCrossed").hide();
$("#audienceAskCrossed").hide();




function fiftyHint() {
    $.ajax({
        url: window.redirectToFiftyUrl,

        contentType: 'application/html; charset=utf-8',

        dataType: 'html',

        data: { questionId: window.questionId },

        success: function (result) {
            $("#question").html(result);
            $("#hint50").hide();
            $("#hint50crossed").show();
            enableEvents();
        }
    });
}

function friendCallHint() {
    $.ajax({
        type: "GET",
        url: window.redirectToCallFriend,
        contentType: "application/html; charset=utf-8",
        data: { questionId: window.questionId },
        datatype: "html",
        success: function (result) {
            $('#emailModalContent').html(result);
            $('#emailModal').modal('show');
            //$("#friendCall").hide();
            //$("#friendCallCrossed").show();
        }
    });
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

//function changeHint50Image() {
    
//    //var image = $("#hint50");
//    //image.off("click", fiftyHint);
//    //image.attr("src", window.hint50CrossedImage);
//}

