//get the student information
async function getSinhVien() {
    const url = 'https://localhost:44345/sinhvien/getSv';

    try {
        const response = await fetch(url);
        // Check if the request was successful
        if (!response.ok) {
            throw new Error(`Error: ${response.status}`);
        }

        const datas = await response.json();
        const email = localStorage.getItem('username');
        for (const data of datas) { // Fixed the iteration here
            if (data.email === email) {
                updateFormWithStudentData(data);
                break; // If you only need to find one match then break
            }
        }
    } catch (error) {
        console.error('Failed to fetch data:', error);
    }
}

function updateFormWithStudentData(data) {
    // Update text inputs
    document.getElementById("email").setAttribute("readonly", true);
    document.getElementById("fname").setAttribute("readonly", true);
    document.querySelector("#fname").value = data.full_name || '';
    document.querySelector("#student-number").value = data.mssv || '';
    document.querySelector("#dob").value = data.birthday || '';
    document.querySelector("#major").value = data.major || '';
    document.querySelector("#nationality").value = data.nationality || '';
    document.querySelector("#address").value = data.address || '';
    document.querySelector("#email").value = data.email || '';

    // Update selects. This example assumes the value is an exact match.
    if (data.gender) {
        document.querySelector("#gender").value = data.gender;
    }

    if (data.faculty) {
        // This assumes you have set the value attributes of the options to match the faculty names exactly.
        document.querySelector("#khoa").value = data.faculty;
    }

    if (data.religion) {
        document.querySelector("#religion").value = data.religion;
    }

    if (data.idcard) {
        document.querySelector("#id-card-number").value = data.idcard;
    }
    if (data.dateofissue) {
        document.querySelector("#dou").value = data.dateofissue;
    }
    if (data.placeofissue) {
        document.querySelector("#Place").value = data.placeofissue;
    }
    if (data.aboutstudent) {
        document.querySelector("#note").value = data.aboutstudent;
    }
    if (data.myphone) {
        document.querySelector("#mobNo").value = data.myphone;
    }
    if (data.parentphone) {
        document.querySelector("#parentMobNo").value = data.parentphone;
    }
}


function UpdateSinhVien() {
    // Get the input elements
    var email = document.getElementById("email").value.trim();
    var mssv = document.getElementById("student-number").value.trim();
    var gender = document.getElementById("gender").value.trim();
    var birthday = document.getElementById("dob").value.trim();
    var faculty = document.getElementById("khoa").value.trim();
    var major = document.getElementById("major").value.trim();
    var nationality = document.getElementById("nationality").value.trim();
    var religion = document.getElementById("religion").value.trim();
    var idcard = document.getElementById("id-card-number").value.trim();
    var dateofissue = document.getElementById("dou").value.trim();
    var placeofissue = document.getElementById("Place").value.trim();
    var myphone = document.getElementById("mobNo").value.trim();
    var parentphone = document.getElementById("parentMobNo").value.trim();
    var address = document.getElementById("address").value.trim();
    var aboutstudent = document.getElementById("note").value.trim();

    // Check if required fields are not empty
    if (!email || !mssv || !birthday) {
        alert("Email, mssv, and birthday are required");
        return;
    }

    // Create the JSON payload
    var request = {
        "email": email,
        "mssv": mssv,
        "gender": gender,
        "birthday": birthday,
        "faculty": faculty,
        "major": major,
        "nationality": nationality,
        "religion": religion,
        "idcard": idcard,
        "dateofissue": dateofissue,
        "placeofissue": placeofissue,
        "myphone": myphone,
        "parentphone": parentphone,
        "address": address,
        "aboutstudent": aboutstudent
    };

    // Make an AJAX request using the Fetch API
    fetch('https://localhost:44345/update/sinhvien', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json; charset=utf-8'
        },
        body: JSON.stringify(request)
    })
        .then(response => {
            if (response.ok) {
                return response.json();
            } else {
                // Log or alert the status code for more information
                console.error('Error status code:', response.status);
                alert('Network response was not ok. Status code: ' + response.status);
                return Promise.reject('Error ' + response.status);
            }
        })
        .then(data => {
            // Check if updation was successful
            if (data.UpdateResult == true) {
                // Updation successful
                alert("Updation successful");
            } else {
                // Updation failed, handle the error message
                alert("Updation failed. Error message: " + data.errorMessage);
            }
        })
        .catch(error => {
            console.error('Error during updation request:', error.message);
            alert('Error during updation request: ' + error.message);
        });
}

window.onload = function () {
    getSinhVien();
};

function viewStudentDetails() {
    // Navigate to the student details page
    window.location.href = './Student_Infor'; // Change this URL to the correct page for your application
}
function addStudentDetails() {
    // Navigate to the student details page
    window.location.href = './Student_Update_Data'; // Change this URL to the correct page for your application

}
