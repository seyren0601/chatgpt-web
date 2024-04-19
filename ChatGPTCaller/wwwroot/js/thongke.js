﻿
function fetchtruyvan() {
    $.get("https://localhost:44345/admin/gettruyvan", function (data, status) {
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
            tr.attr('data-id', item.TruyVanId);

            // Loop through the values and populate cells, excluding "salt" and "hashed_pw"
            $.each(item, function (key, val) {

                let td = $("<td>");
                td.text(val);
                tr.append(td);

            });
            // Add buttons for actions (View Detail, Edit, Delete) at the end of each row
            
            var button4 = $('<button type="button" class="btn btn-danger delete" data-id="' + item['TruyVanId'] + '">').text("Delete");
            let tdAction = $("<td>").append(button4);
            tr.append(tdAction);
            tbody.append(tr);
           
        });
        table.append(tbody);
    });
}

// Delete product
$(document).on('click', '.delete', function () {
    var productId = $(this).attr('data-id');

    var updateduser = {
        "isdeleted": true,
    };
    if (confirm("Are you sure you want to delete this Truy Van permanently?")) {
        fetch('https://localhost:44345/admin/deletetruyvan/' + productId, {
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
                    alert('truy van  deleted permanently successfully!!');
                    fetchtruyvan();
                }
                else
                    alert('Product deleted Fail!!');
            })
            .catch(error => {
                console.error('There was a problem with your fetch operation:', error);
            });
    }
});



$(document).on('click', '.thongketruyvan', function () {
    var Ngay = $('#ngay').val().trim();
    $.get("https://localhost:44345/admin/gettruyvan/" + Ngay, function (data, status) {
        var jsonData = data;

        // Create the table element
        let table = $("#userTable");
        table.empty();

        // Get the keys names
        let cols = Object.keys(jsonData[0]);

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
            tr.attr('data-id', item.TruyVanId);

            // Loop through the values and populate cells, excluding "salt" and "hashed_pw"
            $.each(item, function (key, val) {
                let td = $("<td>");
                if (key === "ThoiGian") {
                    // Check if val needs to be converted to a Date object
                    if (typeof val === 'string') {
                        val = new Date(val).toISOString().split('T')[0];
                    } else if (val instanceof Date) {
                        val = val.toISOString().split('T')[0];
                    }
                    td.text(val);
                } else {
                    td.text(val);
                }
                tr.append(td);
            });
            tbody.append(tr);
        });
        table.append(tbody);
    });
});


//thong ke dang nhap


function fetchdangnhap() {
    $.get("https://localhost:44345/admin/getdangnhap", function (data, status) {
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
            tr.attr('data-id', item.DangNhapId);

            // Loop through the values and populate cells, excluding "salt" and "hashed_pw"
            $.each(item, function (key, val) {

                let td = $("<td>");
                td.text(val);
                tr.append(td);

            });
            // Add buttons for actions (View Detail, Edit, Delete) at the end of each row

            var button4 = $('<button type="button" class="btn btn-danger delete1" data-id="' + item['DangNhapId'] + '">').text("Delete");
            let tdAction = $("<td>").append(button4);
            tr.append(tdAction);
            tbody.append(tr);

        });
        table.append(tbody);
    });
}

// Delete product
$(document).on('click', '.delete1', function () {
    var productId = $(this).attr('data-id');

    var updateduser = {
        "isdeleted": true,
    };
    if (confirm("Are you sure you want to delete this Dang Nhap permanently?")) {
        fetch('https://localhost:44345/admin/deletedangnhap/' + productId, {
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
                    alert('Dang Nhap  deleted permanently successfully!!');
                    fetchdangnhap();
                }
                else
                    alert('Product deleted Fail!!');
            })
            .catch(error => {
                console.error('There was a problem with your fetch operation:', error);
            });
    }
});



$(document).on('click', '.thongkedangnhap', function () {
    var Ngay = $('#ngay1').val().trim();
    $.get("https://localhost:44345/admin/getdangnhap/" + Ngay, function (data, status) {
        var jsonData = data;

        // Create the table element
        let table = $("#userTable");
        table.empty();

        // Get the keys names
        let cols = Object.keys(jsonData[0]);

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
            tr.attr('data-id', item.DangNhapId);

            // Loop through the values and populate cells, excluding "salt" and "hashed_pw"
            $.each(item, function (key, val) {
                let td = $("<td>");
                if (key === "login_day") {
                    // Check if val needs to be converted to a Date object
                    if (typeof val === 'string') {
                        val = new Date(val).toISOString().split('T')[0];
                    } else if (val instanceof Date) {
                        val = val.toISOString().split('T')[0];
                    }
                    td.text(val);
                } else {
                    td.text(val);
                }
                tr.append(td);
            });
            tbody.append(tr);
        });
        table.append(tbody);
    });
});
