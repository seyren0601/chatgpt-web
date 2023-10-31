function getCurrentTime() {
    var now = new Date();
    var hours = now.getHours();
    var minutes = now.getMinutes();
    var ampm = hours >= 12 ? 'PM' : 'AM';
    hours = hours % 12;
    hours = hours ? hours : 12; // 12 giờ đối với 0 AM
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

        // Gọi hàm để xử lý và hiển thị câu trả lời ở đây
        // Ví dụ:
        receiveMessage("Anh Thuong Dep Trai");
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

document.getElementById("userInput").addEventListener("keydown", sendMessage);
document.querySelector("button").addEventListener("click", sendMessage);
