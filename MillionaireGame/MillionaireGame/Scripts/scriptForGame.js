
    for (var i = 0; i < 4; i++) {
        var button = $("#" + i);
        if (button.text() != "") {
            $(button).on("click", checkAnswer);
        }
    }

    $("#img1").on("click", redirectToFiftyFifty);
    

$("#t" + hightlightedTableIndex).css({ "background-color": "white", "color": "black" });

function checkAnswer(e) {
    var selectedButton = e.target;
    $(selectedButton).css('background-color', 'blue');
    setTimeout(function () {
            if (selectedButton.innerText == window.correctText) {
                $(selectedButton).css('background-color', 'greenyellow');

                var newButton = $('<button>Next question</button>');
                $(newButton).click(function () {
                    //window.location.href = window.nextQuestionUrl;
                    post(window.nextQuestionUrl, { questionNumber: window.hightlightedTableIndex });
                });
                $("body").append(newButton);

            } else {
                $('#' + window.correctButtonId).css("background-color", "greenyellow");
                $(selectedButton).css('background-color', 'red');
            }
            disableEvents();
            $("#img1").off("click", redirectToFiftyFifty);
        },
        1000);
}
function disableEvents() {
    for (var i = 0; i < 4; i++) {
        $("#" + i).off('click', checkAnswer);
    }
}

function redirectToFiftyFifty() {
    window.location.href = window.redirectToFiftyUrl;
}

// Post to the provided URL with the specified parameters.
function post(path, parameters) {
    debugger;
    var form = $('<form></form>');

    form.attr("method", "post");
    form.attr("action", path);

    $.each(parameters, function (key, value) {
        var field = $('<input></input>');

        field.attr("type", "hidden");
        field.attr("name", key);
        field.attr("value", value);

        form.append(field);
    });

    // The form needs to be a part of the document in
    // order for us to be able to submit it.
    $(document.body).append(form);
    form.submit();
}


