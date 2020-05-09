$(document).ready(function () {
    $("#url-text-input").keyup(function (event) {
        if (event.keyCode === 13) {  // `enter` key
            submitShortenUrl();
        }
    });
});

function submitShortenUrl() {
    const url = document.getElementById("url-text-input").value;

    $.ajax({
        type: "POST",
        url: "/api/generateAlias",
        data: JSON.stringify({ url: url }),
        dataType: "json",
        headers: {
            "Content-Type": "application/json",
            'Accept': "application/json"
        },
        success: function (response) {
            const generatedLink = window.location.origin + "/" + response.alias;
            displaySuccess(generatedLink);

        },
        error: function(error) {
            if (error.status === 400) {
                displayError("Please enter a valid URL.");
            } else {
                displayError("An unknown error occured.");
            }
        }
    });
}

function displaySuccess(message) {
    const successMessage = $("#success-message");
    const errorMessage = $("#error-message");
    errorMessage.hide();

    successMessage.html(message);
    successMessage.show();
}


function displayError(message) {
    const errorMessage = $("#error-message");
    const successMessage = $("#success-message");
    successMessage.hide();

    errorMessage.html(message);
    errorMessage.show();
}