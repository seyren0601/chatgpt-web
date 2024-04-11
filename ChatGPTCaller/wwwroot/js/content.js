

function updateHeaderOnLogin(username,name) {
    const usernameSpan = document.getElementById('usernameSpan');
    const logoutButton = document.getElementById('logoutButton');

    if (usernameSpan && logoutButton) {
        // Display the username in the header
        usernameSpan.textContent = name;

        // Store the username in localStorage
        localStorage.setItem('username', username);
        localStorage.setItem('name',name);

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

document.addEventListener('DOMContentLoaded', loadUsernameFromStorage);

// Hàm đệ quy để hiển thị tất cả các chương con của một chương mẹ
function displayAllSubChapters(chapterId, chapters) {
    const subChapters = chapters.filter(chapter => chapter.ParentId === chapterId);
    if (subChapters.length === 0) {
        return null; // Nếu không có chương con, trả về null để dừng đệ quy
    }
    const subChapterList = document.createElement('ul');
    subChapters.forEach(subChapter => {
        const listItem = document.createElement('li');
        listItem.innerHTML = `<a href="#${subChapter.Id}" data-toggle="tab" class="chapter-link-con">${subChapter.Id}: ${subChapter.Title}</a>`;
        const nestedSubChapters = displayAllSubChapters(subChapter.Id, chapters); // Đệ quy để hiển thị các chương con của chương con
        if (nestedSubChapters) {
            listItem.appendChild(nestedSubChapters);
        }
        subChapterList.appendChild(listItem);
    });
    return subChapterList;
}

// Tạo Chương mục
document.addEventListener('DOMContentLoaded', function () {
    $.get("https://localhost:44345/monhoc/getChuong/CTGT01", function (chapters) {
        const toc = document.getElementById('toc');

        // Chỉ hiển thị các chương lớn ban đầu
        const mainChapters = chapters.filter(chapter => chapter.ParentId === null);
        mainChapters.forEach(chapter => {
            const listItem = document.createElement('li');
            listItem.innerHTML = `<a href="#${chapter.Id}" data-toggle="tab" class="chapter-link">${chapter.Id}: ${chapter.Title}</a>`;
            listItem.addEventListener('click', function (event) {
                event.preventDefault();
                // Khi chương lớn được click, hiển thị tất cả các chương con
                const subChapterList = displayAllSubChapters(chapter.Id, chapters);
                if (subChapterList) {
                    if (!this.querySelector('ul')) {
                        this.appendChild(subChapterList);
                    } else {
                        // Nếu đã có danh sách chương con, xóa nó đi để 'toggle' hiển thị
                        this.removeChild(this.querySelector('ul'));
                    }
                }
            });
            toc.appendChild(listItem);
        });
    });
});
