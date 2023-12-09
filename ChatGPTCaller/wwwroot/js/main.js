﻿function getCurrentTime() {
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
        .then(response => {
            return response.json();
        })
        .then(data => {
            // Handle successful response data
            var choices = data.choices;
            if (choices && choices.length > 0) {
                var botMessageContent = choices[0].message.content;
                if (botMessageContent !== "") {
                    // Create the bot message element
                    var botMessageElement = createMessageElement("received", "Bot_Assistant", botMessageContent);
                    messages.appendChild(botMessageElement);
                }
            }
            else { 
                // If the response status is not OK, throw an error
                throw new Error(`HTTP error! Status: ${data.status} Title: ${data.title}`);
            }
        })
        .catch(error => {
            // Handle errors

            var errorElement = showErorrToast(error.message);
        });
}
function displayErrorMessage(message) {
    // Update the UI to display the received message
    var messagesContainer = document.getElementById('messages');
    var newMessage = document.createElement('div');
    newMessage.className = 'message received';
    newMessage.innerHTML = message;
    messagesContainer.appendChild(newMessage);
}
function createSendMessage(type, sender, message) {
    var messageElement = document.createElement("div");
    messageElement.classList.add("message", type);
    var messageContent = `
        <p class="message-sender">
            <i class="fa-regular fa-user"></i>${sender}_${getCurrentTime()}:</p>
        <p class="message-text">${message}</p>`;
    messageElement.innerHTML = messageContent;
    return messageElement;
}

function createMessageElement(type, sender, message) {
    var messageElement = document.createElement("div");
    messageElement.classList.add("message", type);
    var messageContent = `
        <p class="message-sender">
            <i class="fa-solid fa-robot"></i>${sender}_${getCurrentTime()}:</p>
        <p class="message-text">${message}</p>`;
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

//error show
// Hàm toast nhận vào một đối tượng có các thuộc tính title, message, type, và duration.
function toast({ title = "", message = "", type = "", duration = "" }) {
    // Lấy phần tử có id là 'toast' từ DOM
    const main = document.getElementById("toast");

    // Kiểm tra nếu phần tử main tồn tại
    if (main) {
        // Tạo một phần tử div mới có class 'toast'
        const toast = document.createElement("div");

        // Tạo một timeout để tự động loại bỏ toast sau thời gian duration + 1000 miligiây
        const autoRomeveId = setTimeout(function () {
            main.removeChild(toast);
        }, duration + 1000);

        // Xử lý sự kiện khi click vào toast
        toast.onclick = function (e) {
            if (e.target.closest(".toast__close")) {
                main.removeChild(toast);
                clearTimeout(autoRomeveId);
            }
        };

        // Định nghĩa các biểu tượng tương ứng với type
        const icons = {
            success: "fas fa-check-circle",
            info: "fas fa-check-circle",
            warning: "fas fa-exclamation-circle",
            error: "fa-solid fa-circle-exclamation",
        };
        const icon = icons[type];

        // Tính toán delay trong giây
        const delay = (duration / 1000).toFixed(2);

        // Thêm class và định nghĩa animation cho toast
        toast.classList.add("toast", `toast--${type}`);
        toast.style.animation = `slideInLeft ease .3s ,fadeOut linear 1s ${delay}s forwards`;

        // Tạo nội dung cho toast
        toast.innerHTML = ` <div class="toast__icon">
                <i class="${icon}"></i>
            </div>
            <div class="toast__body">
                <h3 class="toast__title">${title}</h3>
                <p class="toast__msg">${message}</p>
            </div>
            <div class="toast__close">
                   <i class="fas fa-times"></i>
            </div>
       `;

        // Thêm toast vào phần tử main
        main.appendChild(toast);
    }
}


// Hàm showErrorToast sử dụng hàm toast để hiển thị một toast thất bại
function showErorrToast(message) {
    toast({
        title: "Thất Bại",
        message: message,
        type: "error",
        duration: 5000,
    });
}
