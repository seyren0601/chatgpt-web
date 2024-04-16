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

// Function to display sub-chapters
function displayAllSubChapters(chapterId, chapters) {
    const subChapters = chapters.filter(chapter => chapter.ParentId === chapterId);
    if (subChapters.length === 0) {
        return null; // If there are no sub-chapters, return null to stop the recursion
    }
    const subChapterList = document.createElement('ul');
    subChapters.forEach(subChapter => {
        const listItem = document.createElement('li');
        listItem.innerHTML = `<a href="#${subChapter.Id}" data-toggle="tab" class="chapter-link">${subChapter.Id}: ${subChapter.Title}</a>`;
        const nestedSubChapters = displayAllSubChapters(subChapter.Id, chapters); // Recursively display sub-chapters of sub-chapters
        if (nestedSubChapters) {
            listItem.appendChild(nestedSubChapters);
        }
        subChapterList.appendChild(listItem);
    });
    return subChapterList;
}

document.addEventListener('DOMContentLoaded', function () {
    // Fetch chapters
    $.get("https://localhost:44345/monhoc/getChuong/CTGT01")
        .done(function (chapters) {
            const toc = document.getElementById('toc');
            if (!toc) return;

            const mainChapters = chapters.filter(chapter => chapter.ParentId === null);
            mainChapters.forEach(chapter => {
                const listItem = document.createElement('li');
                listItem.innerHTML = `<a href="#${chapter.Id}" data-toggle="tab" class="chapter-link">${chapter.Id}: ${chapter.Title}</a>`;
                listItem.addEventListener('click', function (event) {
                    event.preventDefault();
                    const subChapterList = displayAllSubChapters(chapter.Id, chapters);
                    if (subChapterList) {
                        if (!this.querySelector('ul')) {
                            this.appendChild(subChapterList);
                        } else {
                            this.removeChild(this.querySelector('ul'));
                        }
                    }
                });
                toc.appendChild(listItem);
            });
        })
        .fail(function (xhr, status, error) {
            console.error('Failed to fetch chapters:', status, error);
        });
});



document.addEventListener('DOMContentLoaded', loadUsernameFromStorage);
