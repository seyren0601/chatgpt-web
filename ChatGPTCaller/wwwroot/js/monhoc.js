function fetchmonhoc() {
    $.get("https://localhost:44345/monhoc/getmonhoc", function (data, status) {
        var jsonData = data;

        // Create the table element
        let table = $("#userTable");
        table.empty();

        // Get the keys names
        let cols = Object.keys(jsonData[0]);
        cols.push("Action");

        // Create the header
        let thead = $("<thead>");
        let tbody = $('<tbody id="tbody">');
        let tr = $("<tr>");

        // Loop through the column names and create the header cells
        $.each(cols, function (i, item) {
                item = item[0].toUpperCase() + item.slice(1);

                let th = $('<th scope="col">');
                th.text(item);
                tr.append(th);
            
        });
        thead.append(tr);
        table.append(thead);

        // Populate table rows with user data
        $.each(jsonData, function (i, item) {
            let tr = $("<tr>");
            tr.attr('data-id', item.IdMonhoc);

            // Loop through the values and populate cells, excluding "salt" and "hashed_pw"
            $.each(item, function (key, val) {
               
                    let td = $("<td>");
                    td.text(val);
                    tr.append(td);
                
            });

            // Add buttons for actions (View Detail, Edit, Delete) at the end of each row
            var button1 = $('<button type="button" class="btn btn-primary view-detailmonhoc" data-id="' + item['IdMonhoc'] + '">').text("View Detail");
            var daucach1 = $('<br><br>'); // Add a space between buttons for better spacing
            var button2 = $('<button type="button" class="btn btn-primary editmonhoc" data-id="' + item['IdMonhoc'] + '">').text("Edit");
            var daucach2 = $('<br><br>'); // Add a space between buttons for better spacing
            var button3 = $('<button type="button" class="btn btn-danger deletemonhoc" data-id="' + item['IdMonhoc'] + '">').text("Delete");
            let tdAction = $("<td>").append(button1, daucach1, button2, daucach2, button3);
            tr.append(tdAction);
            tbody.append(tr);
        });
        table.append(tbody);
    });
}


// Using on with event delegation for dynamically created elements
$(document).on('click', '.view-detailmonhoc', function () {
    var productId = $(this).attr('data-id'); // Get the product id stored in data-id attribute

    $.get("https://localhost:44345/monhoc/getmonhoc/" + productId, function (data) {
        // Expanded detailHtml to include more details
        var detailHtml = `
            <p><strong>IdMonhoc:</strong> ${data[0].IdMonhoc} </p>
            <p><strong>Tên Môn Học:</strong> ${data[0].TitleMonhoc}</p>
            <p><strong>Nội Dung:</strong> ${data[0].ContentMonhoc}</p>`;

        $('#MONHOCDetail').html(detailHtml);
        $('#detailMonhoc').show(); // Show the modal with the product details
    });
});

// Close modal functionality
$(document).on('click', '.close-button', function () {
    $('#detailMonhoc').hide();
});

// Hide the modal when clicking outside of it
$(window).click(function (event) {
    if (event.target.id === 'detailMonhoc') {
        $('#detailMonhoc').hide();
    }
});

// Edit product functionality
$(document).on('click', '.editmonhoc', function () {
    var productId = $(this).attr('data-id');
    $.get("https://localhost:44345/monhoc/getmonhoc/" + productId, function (product) {
        $('#editIdMonhoc').val(product[0].IdMonhoc);
        $('#editTitleMonhoc').val(product[0].TitleMonhoc);
        $('#editContentMonhoc').val(product[0].ContentMonhoc);
    });
    var form = $("#editMonhocForm");

    // Create and append the "Save Changes" button
    var button = '<button class="saveChange btn btn-primary" type="button" id="saveChange" data-id="' + productId + '">Save Changes</button>';
    form.append(button);

    // Show the edit product modal
    $('#editMonhoc').show();
});

// Close modal functionality
$(document).on('click', '.edit-botton', function () {
    var button = $('#saveChange');
    button.remove();
    $('#editMonhoc').hide();
});

// Hide the modal when clicking outside of it
$(window).click(function (event) {
    if (event.target.id === 'editMonhoc') {
        var button = $('#saveChange');
        button.remove();
        $('#editMonhoc').hide();
    }
});

// Save changes
$(document).on('click', '.saveChange', function () {
    var productId = $(this).attr('data-id');
    var updateduser = {
        "IdMonhoc": $('#editIdMonhoc').val(),
        "TitleMonhoc": $('#editTitleMonhoc').val(),
        "ContentMonhoc": $('#editContentMonhoc').val(),
    };

    fetch('https://localhost:44345/monhoc/update/' + productId, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(updateduser)
    })
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            return response.json();
        })
        .then(data => {
            let jsonData = data;
            alert('Updated User details successfully!!');
            $('#editMonhoc').hide();
            fetchmonhoc();

            // Remove the "Save Changes" button
            $(this).remove();
        })
        .catch(error => {
            console.error('There was a problem with your fetch operation:', error);
        });
});

// Delete product
$(document).on('click', '.deletemonhoc', function () {
    var productId = $(this).attr('data-id');

    var updateduser = {
        "isdeleted": true,
    };
    if (confirm("Are you sure you want to delete this product?")) {
        fetch('https://localhost:44345/monhoc/delete/' + productId, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(updateduser)
        })
            .then(response => {
                if (!response.ok) {
                    throw new Error('Network response was not ok');
                }
                return response.json();
            })
            .then(data => {
                let jsonData = data;
                if (jsonData.updateResult) {
                    alert('Product deleted successfully!!');
                    fetchUsers();
                }
                else
                    alert('Product deleted Fail!!');
            })
            .catch(error => {
                console.error('There was a problem with your fetch operation:', error);
            });
    }
});
