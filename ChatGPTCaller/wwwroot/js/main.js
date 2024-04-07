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
    var loadingIndicator = document.getElementById("loading");
    // Disable the user input field during loading
    userInput.disabled = true;

    var userMessage = userInput.value.trim();
    if (userMessage === "") {
        // Enable the user input field if the message is empty
        userInput.disabled = false;
        return;
    }
    // Create the user message element
    var userMessageElement = createSendMessage("sent", "User", userMessage);
    messages.appendChild(userMessageElement);

    // Clear the user input field after sending the message
    userInput.value = "";

    // Show the loading indicator
    loadingIndicator.style.display = "block";

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

            // Hide the loading indicator when the response is received
            loadingIndicator.style.display = "none";
            userInput.disabled = false;

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
            // Hide the loading indicator in case of an error
            loadingIndicator.style.display = "none";
            userInput.disabled = false;

            // Handle errors
            showErrorToast(error.message);
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

document.getElementById("search_enter").addEventListener("keydown", function (event) {
    // Check if the pressed key is Enter
    if (event.key === "Enter") {
// Prevent the default behavior of the Enter key 
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
function showErrorToast(message) {
    toast({
        title: "Thất Bại",
        message: message,
        type: "error",
        duration: 3000,
    });
}
// Hàm showSuccessToast sử dụng hàm toast để hiển thị một toast thành công
function showSuccessToast(message) {
    toast({
        title: "Thành Công",
        message: message,
        type: "success",
        duration: 2000,
    });
}


function registerRequest() {
    // Get the email and password input elements
    var emailInput = document.getElementById("email");
    var passwordInput = document.getElementById("password");
    var fullnameInput = document.getElementById("fullname");

    // Extract values and trim whitespace
    var email = emailInput.value.trim();
    var password = passwordInput.value.trim();
    var fullname = fullnameInput.value.trim();

    // Check if email and password are not empty
    if (!email || !password) {
        showErrorToast("Email and password are required");
        return;
    }

    // Create the JSON payload
    var request = {
        "full_name": fullname,
        "email": email,
        "password": password
    };

    // Make an AJAX request using the Fetch API
    fetch('https://localhost:44345/register/request', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json;charset=UTF-8'
        },
        body: JSON.stringify(request)
    })
        .then(response => {
            // Check if the response status is in the range 200-299
            if (response.ok) {
                return response.json();
            } else {
                throw new Error('Network response was not ok');
            }
        })
        .then(data => {
            // Check if registration was successful
            if (data.registerResult) {
                // Registration successful
                showSuccessToast("Registration successful");
            } else {
                // Registration failed, handle the error message
                showErrorToast("Registration failed. Error message: " + data.errorMessage);
            }
        })
        .catch(error => {
            // Handle errors that occur during the fetch
            showErrorToast('Error during registration request:', error);
        });
}

function loginRequest() {
    // Get the email and password input elements
    const emailInput = document.getElementById("email_login");
    const passwordInput = document.getElementById("password_login");

    // Extract values and trim whitespace
    const email = emailInput.value.trim();
    const password = passwordInput.value.trim();

    // Check if email and password are not empty
    if (!email || !password) {
        showErrorToast("Email and password are required");
        return;
    }

    // Create the JSON payload
    const request = {
        "email": email,
        "password": password
    };

    // Make an AJAX request using the Fetch API
    fetch('https://localhost:44345/login/request', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json;charset=UTF-8'
        },
        body: JSON.stringify(request)
    })
        .then(response => {
            // Check if the response status is in the range 200-299
            if (response.ok) {
                return response.json();
            } else {
                throw new Error('Network response was not ok');
            }
        })
        .then(data => {
            // Check if login was successful
            if (data.logInResult) {
                var respond = data;
                showSuccessToast("Login successful");
                localStorage.setItem('username', email);
                localStorage.setItem('name', respond.full_name);
               
                updateHeaderOnLogin(email,respond.full_name);
                // Redirect to '/Home' after a successful login
                if (data.userRole == "user") {
                    window.location.href = '/Home';
                }
                else {
                    window.location.href = '/Admin';
                }
                    ;
            } else {
                showErrorToast("Login failed. Error message: " + data.errorMessage);
            }
        })
        .catch(error => {
            // Handle errors that occur during the fetch
            showErrorToast('Error during login request:', error);
        });
}


function updateHeaderOnLogin(username, name) {
    const usernameSpan = document.getElementById('usernameSpan');
    const logoutButton = document.getElementById('logoutButton');

    if (usernameSpan && logoutButton) {
        // Display the username in the header
        usernameSpan.textContent = name;

        // Store the username in localStorage
        localStorage.setItem('username', username);
        localStorage.setItem('name', name);

        // Display the logout button
        logoutButton.style.display = 'inline';
    }
}

function logout() {
    // Implement your logout logic here

    // After successful logout
    const usernameSpan = document.getElementById('usernameSpan');
    const logoutButton = document.getElementById('logoutButton');

    if (usernameSpan && logoutButton) {
        // Hide the username in the header
        localStorage.removeItem('username');
        localStorage.removeItem('name');
        usernameSpan.textContent = 'Sign Up';

        // Change the link back to the original (Sign Up)
        logoutButton.parentElement.setAttribute('href', './Register');

        // Redirect to the Register page
        window.location.href = "./Register";
    }
}

function loadUsernameFromStorage() {
    const storedUsername = localStorage.getItem('username');
    const name = localStorage.getItem('name');
    if (storedUsername) {
        // If the username is stored, update the header
        updateHeaderOnLogin(storedUsername, name);
    } else {
        // If the username is not stored, check if the current URL is the specified one
        if (window.location.href === "https://localhost:44345/Home") {
            // Redirect to the Register page
            window.location.href = "./Register";
        }
    }
}

document.addEventListener('DOMContentLoaded', loadUsernameFromStorage);

