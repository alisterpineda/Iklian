function submitShortenUrl() {
    const success_message = $('#success-message');
    success_message.hide();
    const error_message = $('#error-message');
    error_message.hide();

    const url = document.getElementById('url-text-input').value;
    $.ajax({
        type: "POST",
        url: "/api/url/generate",
        data: JSON.stringify({ url: url }),
        dataType: "json",
        headers: {
            "Content-Type": "application/json",
            'Accept': 'application/json'
        },
        success: function(response) {
            success_message.html(response.alias);
            success_message.show();

        },
        error: function(error) {
            console.log("Error: " + error); //just use the err here
            error_message.html(error);
            error_message.show();
        }
    });
}