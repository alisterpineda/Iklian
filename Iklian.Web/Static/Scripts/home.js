var urlInputElement = null;
var successTextElement = null;

$(document).ready(function () {
    urlInputElement = document.getElementById("url-text-input");
    successTextElement = document.getElementById("success-message");

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

function copyShortenedUrlToClipboard() {
    const copyButtonElement = $("#copy-button");
    copyButtonElement.text("Copied!");

    const tmpTextArea = document.createElement("textarea");
    document.body.appendChild(tmpTextArea);
    tmpTextArea.value = successTextElement.textContent;
    tmpTextArea.select();
    document.execCommand("copy");
    tmpTextArea.remove();
}

function displaySuccess(message) {
    const successAlert = $("#success-alert");
    const successMessage = $("#success-message");
    resetSuccessAlert();
    resetErrorAlert();

    successMessage.text(message);
    successAlert.show();
}

function displayError(message) {
    const errorMessage = $("#error-message");
    resetSuccessAlert();

    errorMessage.html(message);
    errorMessage.show();
}

function resetSuccessAlert() {
    $("#success-alert").hide();
    $("#copy-button").text("Copy");
}

function resetErrorAlert() {
    $("#error-message").hide();
}