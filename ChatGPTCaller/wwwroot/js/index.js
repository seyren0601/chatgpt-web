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

        // Dùng AJAX request để lấy API
        fetch("https://dummyjson.com/products")
            .then(response => response.json())
            .then(data => {
                var jsonData = data.products;
                let Mess = Object.values(jsonData[0]['description']);
                 var Mess_received = Mess.join(""); //nối chuỗi lại Nha
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

document.querySelector("button").addEventListener("click", sendMessage);
document.getElementById("userInput").addEventListener("keydown", sendMessage);