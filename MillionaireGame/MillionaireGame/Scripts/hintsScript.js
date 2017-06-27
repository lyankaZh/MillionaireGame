
function fiftyHint() {
    $.ajax({
        url: window.redirectToFiftyUrl,

        contentType: 'application/html; charset=utf-8',

        dataType: 'html',

        data: { questionId: window.questionId },

        success: function (result) {
            $("#question").html(result);
            var hint50 = $("#hint50");
            hint50.attr("src", "../Content/Images/HintsImages/hint50Crossed.png");
            hint50.off("click", fiftyHint);
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
            var audienceAsk = $("#audienceAsk");
            audienceAsk.attr("src", "../Content/Images/HintsImages/hintAskCrossed.png");
            audienceAsk.off("click", AskAudienceHint);
           
        }
    });
}

function disableEmailHint() {
    var friendCall = $("#friendCall");
    friendCall.attr("src", "../Content/Images/HintsImages/hintPhoneCrossed.png");
    friendCall.off("click", friendCallHint);

}