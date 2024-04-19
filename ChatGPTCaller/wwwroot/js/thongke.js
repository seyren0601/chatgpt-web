
function fetchtruyvan() {
    $.get("https://localhost:44345/admin/gettruyvan", function (data, status) {
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
                td.text(val);
                tr.append(td);

            });
            tbody.append(tr);
        });
        table.append(tbody);
    });
}

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
