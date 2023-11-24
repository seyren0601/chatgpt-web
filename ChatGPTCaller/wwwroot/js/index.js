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
    // Check if the event is a click or Enter key press
    if (event.type === "click" || (event.type === "keydown" && event.key === "Enter")) {
        var userInput = document.getElementById("userInput");
        var messages = document.getElementById("messages");

        var userMessage = userInput.value.trim();
        if (userMessage === "") return;

        // Create the user message element
        var userMessageElement = createMessageElement("sent", "User", userMessage);
        messages.appendChild(userMessageElement);

        // Clear the user input field after sending the message
        userInput.value = "";

        // Create the JSON payload
        var requestBody = { "requestBody": userMessage };

        // Make an AJAX request using the Fetch API
        fetch('https://localhost:44345/api/completion', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(requestBody),
        })
            .then(response => response.text())
            .then(data => {
                var Mess_received = data.trim();
                if (Mess_received !== "") {
                    // Create the bot message element
                    var botMessageElement = createMessageElement("received", "Bot", Mess_received);
                    messages.appendChild(botMessageElement);
                }
            })
            .catch(error => {
                // Handle errors
                console.error('Error:', error);
            });
    }
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

document.getElementById("sendMessageButton").addEventListener("click", sendMessage);

document.getElementById("userInput").addEventListener("keydown", function (event) {
    // Check if the pressed key is Enter
    if (event.type === "click" || (event.type === "keydown" && event.key === "Enter")) {
        // Prevent the default behavior of the Enter key (e.g., adding a new line)
        event.preventDefault();
        sendMessage(event);
    }
});