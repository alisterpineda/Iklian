﻿function submitShortenUrl() {
    const url = document.getElementById("url-text-input").value;

    $.ajax({
        type: "POST",
        url: "/api/url/generate",
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
            console.log(`Error: ${error}`); //just use the err here
            displayError("Error");
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