$(document).ready(function () {
    $("#customerDatatable").DataTable({
        "processing": true,
        "serverSide": true,
        "filter": true,
        "ajax": {
            "url": "/api/customer",
            "type": "POST",
            "datatype": "json"
        },
        "columnDefs": [{
            "targets": [0],
            "visible": false,
            "searchable": false
        }],
        "columns": [
            { "data": "id", "name": "ID", "autoWidth": true },
            { "data": "bankID", "name": "Bank ID", "autoWidth": true },
            { "data": "bankName", "name": "Bank Name", "autoWidth": true },
            { "data": "bankInfo", "name": "Bank Info", "autoWidth": true },
            { "data": "bankAccount", "name": "Bank Account", "autoWidth": true },
            { "data": "bankNumber", "name": "Bank Number", "autoWidth": true },
            { "data": "status", "name": "Status", "autoWidth": true },
            { "data": "channel", "name": "Channel", "autoWidth": true },
        ]
    });
});  