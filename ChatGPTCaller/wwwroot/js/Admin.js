function fetchPosts() {
    $.get("https://localhost:44345/sinhvien/getSv", function (data, status) {
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
            tr.attr('data-id', item.id); // Assuming item.id is the user ID

            let vals = Object.values(item);
            $.each(vals, (i, elem) => {
                let td = $("<td>");
                td.text(elem);
                tr.append(td);
            });
            // Add buttons for actions (View Detail, Edit, Delete) at the end of each row
            var button1 = $('<button type="button" class="btn btn-primary view-detail" data-id="' + item['id'] + '">').text("View Detail");
            var daucach1 = $('<br><br>');// Add a space between buttons for better spacing
            var button2 = $('<button type="button" class="btn btn-primary edit" data-id="' + item['id'] + '">').text("Edit");
            var daucach2 = $('<br><br>');// Add a space between buttons for better spacing
            var button3 = $('<button type="button" class="btn btn-danger delete" data-id="' + item['id'] + '">').text("Delete");
            let tdAction = $("<td>").append(button1, daucach1, button2, daucach2, button3);
            tr.append(tdAction);
            tbody.append(tr);
        });
        table.append(tbody);
    });
}




function addProduct() {
    var newProduct = {
        title: "He was an good man",
        body: "He was a good man, always ready to lend a helping hand to those in need. His kindness knew no bounds, and his gentle demeanor touched the lives of everyone he met. Whether it was offering a listening ear or offering practical assistance, he never hesitated to support others. He believed in the power of compassion and lived by example, leaving behind a legacy of warmth and generosity that will be remembered for generations to come.",
        userId: 10,
        tags: [
            "french",
            "fiction",
            "english"
        ],
        reactions: 3
    };

    $.post("https://dummyjson.com/posts/add", newProduct)
        .done(function (jsonData) {
            let tbody = $("#tbody");
            let tr = $("<tr>");

            // Dynamically create and append table cells for each product detail
            $('<td>').text(jsonData['id']).appendTo(tr);
            $('<td>').text(jsonData['title']).appendTo(tr);
            $('<td>').text(jsonData['body']).appendTo(tr);
            $('<td>').text(jsonData['userId']).appendTo(tr);
            $('<td>').text(jsonData['tags']).appendTo(tr);
            $('<td>').text(jsonData['reactions']).appendTo(tr);

            tbody.append(tr);

            // Create an alert to notify of the new product addition
            let alert_success = $("#alert-success");
            alert_success.text("A new product " + jsonData['id'] + " has been added to the table.");
            alert_success.attr('class', "alert alert-success");

            setTimeout(() => {
                alert_success.attr('class', "d-none");
            }, 2000);
        });
}


// Using on with event delegation for dynamically created elements
$(document).on('click', '.view-detail', function () {
    var productId = $(this).attr('data-id'); // Get the product id stored in data-id attribute
    $.get("https://dummyjson.com/posts/" + productId, function (data) {
        // Expanded detailHtml to include more details
        var detailHtml = `
            <p><strong>Title:</strong> ${data.title} </p>
            <p><strong>UserId:</strong> ${data.userId}</p>
            <p><strong>Tags:</strong> ${data.tags}</p>
            <p><strong>Reactions:</strong> ${data.reactions}</p>
            <p><strong>Body:</strong> ${data.body}</p>`;

        $('#productDetail').html(detailHtml);
        $('#detailModal').show(); // Show the modal with the product details
    });
});


// Close modal functionality
$(document).on('click', '.close-button', function () {
    $('#detailModal').hide();
});

// Hide the modal when clicking outside of it
$(window).click(function (event) {
    if (event.target.id === 'detailModal') {
        $('#detailModal').hide();
    }
});

//edit product.....
// Function to open the edit modal and populate it with current product details
$(document).on('click', '.edit', function () {
    var productId = $(this).attr('data-id'); // Get the product id stored in data-id attribute
    $.get("https://dummyjson.com/posts/" + productId, function (product) {
        $('#edittitle').val(product.title);
        $('#editbody').val(product.body);
        $('#edituserid').val(product.userId);
        $('#edittags').val(product.tags);
        $('#editreations').val(product.reactions);
        // Select the edit product form

    });
    var form = $("#editProductForm");

    // Create and append the "Save Changes" button
    var button = '<button class="saveEdit" type="button" id="saveEdit" data-id="' + productId + '">Save Changes</button>';
    form.append(button);

    // Show the edit product modal
    $('#editProduct').show();
});


// Close modal functionality
$(document).on('click', '.edit-botton', function () {
    var button = $('#saveEdit')
    // Remove any previously created buttons from
    button.remove();
    $('#editProduct').hide();
});

// Hide the modal when clicking outside of it
$(window).click(function (event) {
    if (event.target.id === 'editProduct') {
        var button = $('#saveEdit')
        // Remove any previously created buttons from
        button.remove();
        $('#editProduct').hide();
    }
});



// Saving changes
$(document).on('click', '.saveEdit', function () {
    var productId = $(this).attr('data-id');
    var updatedProduct = {
        title: $('#edittitle').val(),
        body: $('#editbody').val(),
        userId: $('#edituserid').val(),
        tags: $('#edittags').val(),
        reactions: $('#editreations').val(),
    };

    fetch('https://dummyjson.com/posts/' + productId, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(updatedProduct)
    })
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            return response.json();
        })
        .then(data => {
            //get json data respon fron server
            let jsonData = data;
            alert('Updated product details successfully!!');
            $('#editProduct').hide();

            // Update the corresponding row in the HTML table with the new product details
            var rowToUpdate = $(`#tbody tr[data-id="${jsonData.id}"]`);
            rowToUpdate.find('td:eq(1)').text(jsonData.title);
            rowToUpdate.find('td:eq(2)').text(jsonData.body);
            rowToUpdate.find('td:eq(3)').text(jsonData.userId);
            rowToUpdate.find('td:eq(4)').text(jsonData.tags);
            rowToUpdate.find('td:eq(5)').text(jsonData.reactions);

            // Remove the "Save Changes" button
            $(this).remove();
        })
        .catch(error => {
            console.error('There was a problem with your fetch operation:', error);
            // Handle errors gracefully, display error message to the user, etc.
        });
});

//delete...

// Delete product
$(document).on('click', '.delete', function () {
    var productId = $(this).attr('data-id');

    // Ask for confirmation before deleting
    if (confirm("Are you sure you want to delete this product?")) {
        fetch('https://localhost:44345/admin/delete/' + productId, {
            method: 'DELETE',
        })
            .then(response => {
                if (!response.ok) {
                    throw new Error('Network response was not ok');
                }
                return response.json();
            })
            .then(data => {

                let jsonData = data;
                if (jsonData.updateResult)

                    alert('Product deleted successfully!!');

                else
                    alert('Product deleted Fail!!');
            })
            .catch(error => {
                console.error('There was a problem with your fetch operation:', error);
                // Handle errors gracefully, display error message to the user, etc.
            });
    }
});



