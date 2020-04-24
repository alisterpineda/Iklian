function submitShortenUrl() {
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
            console.log("Finished. Alias: " + response.alias); //just use the resp here
        },
        error: function(error) {
            console.log("Error: " + error); //just use the err here
        }
    });
}