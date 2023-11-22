
function getCurrentTime() {
    var now = new Date();
    var hours = now.getHours();
    var minutes = now.getMinutes();
    var ampm = hours >= 12 ? 'PM' : 'AM';
    hours = hours % 12;
    hours = hours ? hours : 12; // 12 hours for 0 AM
    minutes = minutes < 10 ? '0' + minutes : minutes;
    var timeString = hours + ':' + minutes + ' ' + ampm;
    return timeString;
}

function sendMessage(event) {
    if (event.type === "click" || (event.type === "keydown" && event.key === "Enter")) {
        var userInput = document.getElementById("userInput");
        var messages = document.getElementById("messages");

        var userMessage = userInput.value;
        if (userMessage.trim() === "") return;

        var userMessageElement = document.createElement("div");
        userMessageElement.classList.add("message", "sent");
        var messageContent = `
            <p>
                <span class="message-sender">User:</span> ${userMessage}
                <span class="message-time">${getCurrentTime()}</span>
            </p>`;
        userMessageElement.innerHTML = messageContent;
        messages.appendChild(userMessageElement);

        userInput.value = "";

        // Use AJAX request to fetch an API
        fetch("https://dummyjson.com/products")
            .then(response => response.json())
            .then(data => {
                var jsonData = data.products;
                let Mess = Object.values(jsonData[0]['description']);
                var Mess_received = Mess.join(""); // Join strings together
                receiveMessage(Mess_received);
            })
            .catch(error => console.error("Error fetching data:", error));
    }
}

function receiveMessage(message) {
    var messages = document.getElementById("messages");

    var botMessageElement = document.createElement("div");
    botMessageElement.classList.add("message", "received");
    var messageContent = `
        <p>
            <span class="message-sender">Bot:</span> ${message}
            <span class="message-time">${getCurrentTime()}</span>
        </p>`;
    botMessageElement.innerHTML = messageContent;
    messages.appendChild(botMessageElement);
}

document.querySelector("#sendMessageButton").addEventListener("click", sendMessage);
document.getElementById("userInput").addEventListener("keydown", sendMessage);

/*
// index.js

function sendMessage(event) {
    // Prevent the default form submission behavior
    event.preventDefault();

    // Get the user input value
    var userInput = document.getElementById("userInput").value;

    // Create the JSON payload
    var requestBody = { "requestBody": userInput };

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
            // Handle the response data (update the UI, etc.)
            console.log('Response from server:', data);
            updateChatUI(data.responseBody); // Assuming there's a function to update the chat UI
        })
        .catch(error => {
            // Handle errors
            console.error('Error:', error);
        });
}

function updateChatUI(message) {
    // Update the chat UI with the received message
    var messagesContainer = document.getElementById('messages');

    var messageElement = document.createElement('div');
    messageElement.classList.add('message', 'received');
    messageElement.innerHTML = '<p>' + message + '</p>';

    messagesContainer.appendChild(messageElement);

    // Clear the user input
    document.getElementById('userInput').value = '';
}
*/