function getCurrentTime() {
    var now = new Date();
    var hours = now.getHours() % 12 || 12;
    var minutes = ('0' + now.getMinutes()).slice(-2);
    var ampm = now.getHours() >= 12 ? 'PM' : 'AM';
    return hours + ':' + minutes + ' ' + ampm;
}
async function getsinhvienbyemail(email) {

    const url = 'https://localhost:44345/sinhvien/getByEmail/' + email;

    try {
        const response = await fetch(url);
        // Check if the request was successful
        if (!response.ok) {
            throw new Error(`Error: ${response.status}`);
        }

        const data = await response.json();
        return data[0].id;
    } catch (error) {
        console.error('Failed to fetch data:', error);
    }
}

async function sendMessage() {
    const userInput = document.getElementById("searchInput");
    const messages = document.getElementById("messages");
    const loadingIndicator = document.getElementById("loading");

    // Disable user input field during loading
    userInput.disabled = true;

    const userMessage = userInput.value.trim();
    if (userMessage === "") {
        // Enable user input field if message is empty
        userInput.disabled = false;
        return;
    }

    // Create user message element
    const userMessageElement = createMessageElement("sent", "User", userMessage);
    messages.appendChild(userMessageElement);

    // Clear user input field after sending the message
    userInput.value = "";

    try {
        const email = localStorage.getItem('username');
        const ID = await getsinhvienbyemail(email);
        const requestBody = {
            "ConversationID": ID,
            "message": {
                "role": "user",
                "content": userMessage
            }
        };

        // Show loading indicator
        loadingIndicator.style.display = "block";

        // Make AJAX request using Fetch API
        const response = await fetch('https://localhost:44345/api/completion', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json;charset=UTF-8',
            },
            body: JSON.stringify(requestBody),
        });

        if (!response.ok) {
            throw new Error(`HTTP error! Status: ${response.status}`);
        }

        const data = await response.json();
        const choices = data.choices;

        if (choices && choices.length > 0) {
            const botMessageContent = choices[0].message.content;
            if (botMessageContent !== "") {
                // Create bot message element
                const botMessageElement = createMessageElement("received", "Bot_Assistant", botMessageContent);
                messages.appendChild(botMessageElement);

                // Create new ThongKeTruyVan entry
                const newThongKeTruyVan = {
                    "id": ID,
                    "TruyVanText": userMessage,
                    "TraLoiText": botMessageContent
                };
                await createThongKeTruyVan(newThongKeTruyVan);
            }
        } else {
            throw new Error(`No choices received from server.`);
        }
    } catch (error) {
        showErrorToast(error.message);
    } finally {
        // Hide loading indicator
        loadingIndicator.style.display = "none";
        // Enable user input field
        userInput.disabled = false;
    }
}

async function createThongKeTruyVan(newThongKeTruyVan) {
    try {
        const response = await fetch('https://localhost:44345/admin/themtruyvan', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json;charset=UTF-8'
            },
            body: JSON.stringify(newThongKeTruyVan)
        });

        if (!response.ok) {
            throw new Error('Network response was not ok');
        }

        const data = await response.json();

        if (data.updateResult) {
            console.log("Add thong ke truy van successful");
        } else {
            console.error("Add thong ke truy van failed. Error message: " + data.errorMessage);
        }
    } catch (error) {
        if (error instanceof TypeError) {
            console.error('Error: Could not connect to the server.');
        } else {
            console.error('Error during add thong ke truy van request: ' + error.message);
        }
    }
}



document.addEventListener('keydown', function (event) {
    
    if (event.key === 'Enter') {
        // Execute your code here
        sendMessage();
    }
});

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


async function loginRequest() {
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
    const ID = await getsinhvienbyemail(email);

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
                const newThongKeDangNhap = {
                    "id": ID
                };
                createThongKeDangNhap(newThongKeDangNhap);
                showSuccessToast("Login successful");
                localStorage.setItem('username', email);
                localStorage.setItem('name', respond.full_name);
                localStorage.setItem('role', respond.userRole);

               
                updateHeaderOnLogin(email,respond.full_name);
                // Redirect to '/Home' after a successful login
                if (data.userRole == "user") {
                    window.location.href = '/Home';
                    
                }
                else {
                    window.location.href = '/Home';
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

//then thong ke dang nhap
async function createThongKeDangNhap(newThongKeDangNhap) {
    try {
        const response = await fetch('https://localhost:44345/admin/themdangnhap', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json;charset=UTF-8'
            },
            body: JSON.stringify(newThongKeDangNhap)
        });

        if (!response.ok) {
            throw new Error('Network response was not ok');
        }

        const data = await response.json();

        if (data.updateResult) {
            console.log("Add thong ke dang nhap successful");
        } else {
            console.error("Add thong ke truy van failed. Error message: " + data.errorMessage);
        }
    } catch (error) {
        if (error instanceof TypeError) {
            console.error('Error: Could not connect to the server.');
        } else {
            console.error('Error during add thong ke truy van request: ' + error.message);
        }
    }
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
        localStorage.removeItem('role');
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
    const role = localStorage.getItem('role');
    if (storedUsername) {
        // If the username is stored, update the header
        updateHeaderOnLogin(storedUsername, name);
        updateNavigationForUserRole(role);
    } else {
        // If the username is not stored, check if the current URL is the specified one
        if (window.location.href === "https://localhost:44345/Home") {
            // Redirect to the Register page
            window.location.href = "./Register";
        }
    }
}
function updateNavigationForUserRole(role) {
    var administratorMenuItem = document.querySelector('.navbar-header.admin');

    if (administratorMenuItem) {
        // Check if the element exists before trying to access its properties
        if (role === "user") {
            // Hide Administrator link for regular users
            administratorMenuItem.style.display = 'none';
        } else if (role === "admin") {
            // Show Administrator link for admins
            administratorMenuItem.style.display = 'block';
        } else {
            // Hide Administrator link by default if the role is unknown
            administratorMenuItem.style.display = 'none';
        }
    } else {
        console.error("Error: Admin menu item not found.");
    }
}

document.addEventListener('DOMContentLoaded', loadUsernameFromStorage);

