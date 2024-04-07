// Fetches and displays student information
async function getSinhVien() {
    const url = 'https://localhost:44345/sinhvien/getSv';

    try {
        const response = await fetch(url);
        if (!response.ok) {
            throw new Error(`Error: ${response.status}`);
        }

        const datas = await response.json();
        const email = localStorage.getItem('username'); // Assuming 'username' is the student's email

        // Find the student data that matches the email
        const data = datas.find(d => d.email === email);
        if (data) {
            updateFormWithStudentData(data);
        }
    } catch (error) {
        console.error('Failed to fetch data:', error);
    }
}

// Updates the form with the fetched student data
function updateFormWithStudentData(data) {
    // Fields that are always read-only
    document.getElementById("email").value = data.email || '';
    document.getElementById("email").setAttribute("readonly", true);
    document.getElementById("fname").value = data.full_name || '';
    document.getElementById("fname").setAttribute("readonly", true);

    // Fields that are editable
    document.getElementById("student-number").value = data.mssv || '';
    document.getElementById("major").value = data.major || '';
    document.getElementById("nationality").value = data.nationality || '';
    document.getElementById("address").value = data.address || '';
    document.getElementById("gender").value = data.gender || '';
    document.getElementById("khoa").value = data.faculty || '';
    document.getElementById("religion").value = data.religion || '';
    document.getElementById("id-card-number").value = data.idcard || '';
    document.getElementById("mobNo").value = data.myphone || '';
    document.getElementById("parentMobNo").value = data.parentphone || '';
    document.getElementById("note").value = data.aboutstudent || '';

    // Handling date formatting for the date of birth and date of issue
    setDateInInput("dob", data.birthday);
    setDateInInput("dou", data.dateofissue);
    document.getElementById("Place").value = data.placeofissue || '';
}

// Helper function to set dates in input fields
function setDateInInput(elementId, dateString) {
    if (dateString) {
        const date = new Date(dateString);
        // Adjust for time zone offset
        const offset = date.getTimezoneOffset() * 60000; // Convert minutes to milliseconds
        const localDate = new Date(date.getTime() - offset);
        document.getElementById(elementId).value = localDate.toISOString().split('T')[0];
    } else {
        document.getElementById(elementId).value = '';
    }
}


// Update student information
async function UpdateSinhVien() {
    const request = gatherFormData();

    if (!request.email || !request.mssv || !request.birthday) {
        alert("Email, mssv, and birthday are required");
        return;
    }

    const response = await fetch('https://localhost:44345/sinhvien/update', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json;charset=UTF-8'
        },
        body: JSON.stringify(request)
    });

    if (response.ok) {
        const data = await response.json();
        if (data.updateResult) {
            alert("Update successful");
        } else {
            alert("Update failed. Error message: " + data.errorMessage);
        }
    } else {
        throw new Error(`Network response was not ok: ${response.status}`);
    }
   
}

// Gathers form data into a JSON object
function gatherFormData() {
    return {
        email: document.getElementById("email").value.trim(),
        mssv: document.getElementById("student-number").value.trim(),
        gender: document.getElementById("gender").value.trim(),
        birthday: document.getElementById("dob").value.trim(),
        faculty: document.getElementById("khoa").value.trim(),
        major: document.getElementById("major").value.trim(),
        nationality: document.getElementById("nationality").value.trim(),
        religion: document.getElementById("religion").value.trim(),
        idcard: document.getElementById("id-card-number").value.trim(),
        dateofissue: document.getElementById("dou").value.trim(),
        placeofissue: document.getElementById("Place").value.trim(),
        myphone: document.getElementById("mobNo").value.trim(),
        parentphone: document.getElementById("parentMobNo").value.trim(),
        address: document.getElementById("address").value.trim(),
        aboutstudent: document.getElementById("note").value.trim(),
    };
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
