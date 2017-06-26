//$("#takeMoney").off("click", AskAudienceHint);

function checkAnswer(e) {
    var selectedButton = e.target;
    var selectedButtonId = $(selectedButton).attr("id");
    disableEvents();
    $(selectedButton).css('background-color', 'orange');
    setTimeout(function() {
            $.ajax({
                url: window.redirectToCheckAnswerUrl,

                data: { questionId: window.questionId, selectedOption: selectedButtonId },

                success: function (result) {
                    if (selectedButtonId == result) {
                        $(selectedButton).css('background-color', 'greenyellow');
                        if (window.questionNumber === 15) {
                            setTimeout(function () { takeMoney(); }, 500);
                        } else {
                            $("#next_question").show();
                        }
                    } else {
                        $('#' + result).css("background-color", "greenyellow");
                        $(selectedButton).css('background-color', 'red');
                        setTimeout(function () {
                            window.location.href = window.redirectToFailure;
                        }, 500);
                    }
                }
            });
        },
        1000);
  
    //var selectedItem = e.target.tagName;
    //var selectedButton;
    //if (selectedItem == "SPAN") {
    //    selectedButton = e.target.parentElement;
    //} else {
    //    selectedButton = e.target;
    //}

    //var id = $(selectedButton).attr("id");
    //$(selectedButton).css('background-color', 'orange');
    //disableEvents();
    //setTimeout(function () {
    //    if (($("#option" + id)).text() == window.correctText) {

    //        $(selectedButton).css('background-color', 'greenyellow');

    //        if (window.questionNumber === 3) {
    //            setTimeout(function() {takeMoney();}, 500);
    //        } else {
    //            $("#next_question").show();
    //            //var newButton = $('<button>Next question</button>');
    //            //$(newButton).click(function () {
    //            //    
    //            //    post(window.nextQuestionUrl, { questionNumber: window.hightlightedTableIndex });
    //            //});
    //            //$("body").append(newButton);     
    //        }

    //    } else {
    //        $('#' + window.correctButtonId).css("background-color", "greenyellow");
    //        $(selectedButton).css('background-color', 'red');

    //        setTimeout(function () {
    //                window.location.href = window.redirectToFailure;},500);
    //    }
    //},
    //    1000);
}

function enableEvents() {
    for (var i = 1; i <= 4; i++) {
        var button = $("#" + i);
        if (button.text() != "") {
            $(button).on("click", checkAnswer);
        }
    }
    $("#hint50").on("click", fiftyHint);
    $("#friendCall").on("click", friendCallHint);
    $("#audienceAsk").on("click", AskAudienceHint);
}

function disableEvents() {
    for (var i = 1; i <= 4; i++) {
        $("#" + i).off("click");
    }

    $("#hint50").off("click", fiftyHint);
    $("#friendCall").off("click", friendCallHint);
    $("#audienceAsk").off("click", AskAudienceHint);

}

function redirectToFiftyFifty() {
    window.location.href = window.redirectToFiftyUrl;
}

function takeMoney() {
    window.location.href = window.redirectToVictory;
}
