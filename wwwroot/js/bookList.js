﻿var dataTable;

$(document).ready(function () {
    loadDataTable();
});


function loadDataTable() {
    dataTable = $('#DT_load').DataTable({
        "ajax": {
            "url": "/api/book",
            "type": "GET",
            "datatype":"json"
        },
        "columns": [
            {"data":"name", "width": "30%"},
            {"data":"author", "width": "30%"},
            { "data": "isbn", "width": "30%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="text-center">
                            <a href="/BookList/Upsert?id=${data}" class='btn btn-success text-white' style='cursor:pointer;width:100px;'>
                                Edit
                            </a>
                            &nbsp;
                            <a class='btn btn-danger text-white' style='cursor:pointer;width:100px;'
                            onclick=Delete('api/book?id='+${data})>
                                Delete
                            </a>
                        </div>
                    `
                }, "width": "30%"
            }
        ],
        "language": {
            "emptyTable": "no data found"
        },
        "width":"100%"

    });
}

function Delete(url) {
    swal({
        title: "Are you shure?",
        text: "Once deleted, you will not be able to recove",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then(willDelete => {
        if (willDelete) {
            $.ajax({
                type: "DELETE",
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    }
                    else {
                        toaster.error(data.message);
                    }
                }
            })
        }
    });

}