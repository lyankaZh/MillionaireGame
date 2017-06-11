//$("#1").click(function(e) {

//});

for (var i = 0; i < 4; i++) {
    $("#" + i).on("click", checkAnswer);
}

$("#t" + hightlightedTableIndex).css({ "background-color": "white", "color": "black" });

function checkAnswer(e) {
    var selectedButton = e.target;
    $(selectedButton).css('background-color', 'blue');
    setTimeout(function () {
            if (selectedButton.innerText == window.correctText) {
                $(selectedButton).css('background-color', 'greenyellow');
                var newButton = $('<button>Next question</button>');
                $(newButton).click(function () {
                    window.location.href = window.nextQuestionUrl;
                });
                $("body").append(newButton);
            } else {
                $('#' + window.correctButtonId).css("background-color", "greenyellow");
                $(selectedButton).css('background-color', 'red');
            }
            disableEvents();
        },
        1000);
}
function disableEvents() {
    for (var i = 0; i < 4; i++) {
        $("#" + i).off('click', checkAnswer);
    }
}

