function getCurrentTime() {
    var now = new Date();
    var hours = now.getHours() % 12 || 12;
    var minutes = ('0' + now.getMinutes()).slice(-2);
    var ampm = now.getHours() >= 12 ? 'PM' : 'AM';
    return hours + ':' + minutes + ' ' + ampm;
}

function sendMessage() {
    var userInput = document.getElementById("searchInput");
    var messages = document.getElementById("messages");

    var userMessage = userInput.value.trim();
    if (userMessage === "") return;

    // Create the user message element
    var userMessageElement = createSendMessage("sent", "User", userMessage);
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
        .then(data => {
            var choices = data.choices;
            if (choices && choices.length > 0) {
                var botMessageContent = choices[0].message.content;
                if (botMessageContent !== "") {
                    // Create the bot message element
                    var botMessageElement = createMessageElement("received", "Bot", botMessageContent);
                    messages.appendChild(botMessageElement);
                }
            }
        })
        .catch(error => {
            // Handle errors
            console.error('Error:', error);
        });
    }

function createSendMessage(type, sender, message) {
    var messageElement = document.createElement("div");
    messageElement.classList.add("message", type);
    var messageContent = `
        <p>
            <span class="message-sender"><i class="fa-regular fa-user"></i>${sender}:</span> ${message}
            <span class="message-time">${getCurrentTime()}</span>
        </p>`;
    messageElement.innerHTML = messageContent;
    return messageElement;
}
function createMessageElement(type, sender, message) {
    var messageElement = document.createElement("div");
    messageElement.classList.add("message", type);
    var messageContent = `
        <p>
            <span class="message-sender"><i class="fa-solid fa-robot"></i>${sender}:</span> ${message}
            <span class="message-time">${getCurrentTime()}</span>
        </p>`;
    messageElement.innerHTML = messageContent;
    return messageElement;
}

document.getElementById("searchInput").addEventListener("keydown", function (event) {
    // Check if the pressed key is Enter
    if (event.key === "Enter") {
        // Prevent the default behavior of the Enter key (e.g., adding a new line)
        event.preventDefault();
        sendMessage();
    }
});

// If you have a "Send" button, you can add an event listener to it as well
// document.getElementById("sendMessageButton").addEventListener("click", sendMessage);
function getCurrentTime() {
    var now = new Date();
    var hours = now.getHours() % 12 || 12;
    var minutes = ('0' + now.getMinutes()).slice(-2);
    var ampm = now.getHours() >= 12 ? 'PM' : 'AM';
    return hours + ':' + minutes + ' ' + ampm;
}

function sendMessage() {
    var userInput = document.getElementById("searchInput");
    var messages = document.getElementById("messages");

    var userMessage = userInput.value.trim();
    if (userMessage === "") return;

    // Create the user message element
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
    fetch('https://localhost:5001/api/completion', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(requestBody),
    })
        .then(response => response.json())
        .then(data => {
            var choices = data.choices;
            if (choices && choices.length > 0) {
                var botMessageContent = choices[0].message.content;
                if (botMessageContent !== "") {
                    // Create the bot message element
                    var botMessageElement = createMessageElement("received", "Bot", botMessageContent);
                    messages.appendChild(botMessageElement);
                }
            }
        })
        .catch(error => {
            // Handle errors
            console.error('Error:', error);
        });
}
function createSendMessage(type, sender, message) {
    var messageElement = document.createElement("div");
    messageElement.classList.add("message", type);
    var messageContent = `
        <p>
            <span class="message-sender"><i class="fa-regular fa-user"></i>${sender}:</span> ${message}
            <span class="message-time">${getCurrentTime()}</span>
        </p>`;
    messageElement.innerHTML = messageContent;
    return messageElement;
}
function createMessageElement(type, sender, message) {
    var messageElement = document.createElement("div");
    messageElement.classList.add("message", type);
    var messageContent = `
        <p>
            <span class="message-sender"><i class="fa-solid fa-robot"></i>${sender}:</span> ${message}
            <span class="message-time">${getCurrentTime()}</span>
        </p>`;
    messageElement.innerHTML = messageContent;
    return messageElement;
}

document.getElementById("searchInput").addEventListener("keydown", function (event) {
    // Check if the pressed key is Enter
    if (event.key === "Enter") {
        // Prevent the default behavior of the Enter key (e.g., adding a new line)
        event.preventDefault();
        sendMessage();
    }
});

// If you have a "Send" button, you can add an event listener to it as well
// document.getElementById("sendMessageButton").addEventListener("click", sendMessage);