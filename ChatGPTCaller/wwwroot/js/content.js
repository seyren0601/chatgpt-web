
function updateHeaderOnLogin(username) {
    const usernameSpan = document.getElementById('usernameSpan');
    const logoutButton = document.getElementById('logoutButton');

    if (usernameSpan && logoutButton) {
        // Display the username in the header
        usernameSpan.textContent = username;

        // Store the username in localStorage
        localStorage.setItem('username', username);

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
        usernameSpan.textContent = 'Sign Up';

        // Change the link back to the original (Sign Up)
        logoutButton.parentElement.setAttribute('href', './Register');

        // Redirect to the Register page
        window.location.href = "./Register";
    }
}

function loadUsernameFromStorage() {
    const storedUsername = localStorage.getItem('username');
    if (storedUsername) {
        // If the username is stored, update the header
        updateHeaderOnLogin(storedUsername);
    } else {
        // If the username is not stored, check if the current URL is the specified one
        if (window.location.href === "https://localhost:44345/Home") {
            // Redirect to the Register page
            window.location.href = "./Register";
        }
    }
}

document.addEventListener('DOMContentLoaded', loadUsernameFromStorage);



