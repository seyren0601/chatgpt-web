function getCurrentTime() {
    var now = new Date();
    var hours = now.getHours() % 12 || 12; // Ensure 12-hour format
    var minutes = ('0' + now.getMinutes()).slice(-2); // Add leading zero if needed
    var ampm = now.getHours() >= 12 ? 'PM' : 'AM';
    return hours + ':' + minutes + ' ' + ampm;
}

function sendMessage() {
    var userInput = document.getElementById("userInput");
    var messages = document.getElementById("messages");

    var userMessage = userInput.value.trim();
    if (userMessage === "") return;

    // Create and append the user message element
    var userMessageElement = createMessageElement("sent", "User", userMessage);
    messages.appendChild(userMessageElement);

    // Clear the user input field after sending the message
    userInput.value = "";

    // Create the JSON payload
    var requestBody = {
        "ConversationID": 1,
        "message": {
            "role": "user",
            "content": userMessage
        }
    };

    // Make an AJAX request using the Fetch API
    fetch('https://localhost:44345/api/completion', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(requestBody),
    })
        .then(response => response.json())
        .then(data => handleBotResponse(data))
        .catch(handleError);
}

function handleBotResponse(data) {
    var choices = data.choices;
    if (choices && choices.length > 0) {
        var botMessageContent = choices[0].message.content;
        if (botMessageContent !== "") {
            // Create and append the bot message element
            var botMessageElement = createMessageElement("received", "Bot", botMessageContent);
            document.getElementById("messages").appendChild(botMessageElement);
        }
    }
}

function handleError(error) {
    // Handle errors
    console.error('Error:', error);
}

function createMessageElement(type, sender, message) {
    var messageElement = document.createElement("div");
    messageElement.classList.add("message", type);

    var messageContent = `
        <p>
            <span class="message-sender">${sender}:</span> ${message}
            <span class="message-time">${getCurrentTime()}</span>
        </p>`;

    messageElement.innerHTML = messageContent;
    return messageElement;
}

function initChatApp() {
    var searchInput = document.getElementById("userInput");
    var button = document.getElementById("Button");

    searchInput.addEventListener("keydown", function (event) {
        if (event.key === "Enter") {
            sendMessage();
            event.preventDefault();
        } else if (event.key === "Tab") {
            event.preventDefault();
            button.focus();
        }
    });

    button.addEventListener("click", function () {
        sendMessage();
        searchInput.focus();
    });
}

initChatApp();
