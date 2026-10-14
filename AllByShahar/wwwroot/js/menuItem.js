var dataTable;
$(document).ready(function () {
    dataTable =$('#DT_load').DataTable({
        "ajax": {
            "url": "/api/MenuItem",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "name", "width": "25%" },
            { "data": "price", "width": "15%" },
            { "data": "category.name", "width": "15%" },
            { "data": "typeItem.name", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="w-75 btn-group dir="ltr" ">
                          
                            
                             <a href="/Admin/MenuItems/Upsert?id=${data}"   class="btn btn-success text-dark mx-2">
                            <i class="  bi bi-pencil-square"></i> </a>
                            <a onClick=Delete('/api/MenuItem/'+${data}) class="btn btn-danger text-dark mx-2">
                             <i class=" bi bi-trash-fill"></i>  </a>
                            </div>`
                    
                },

                "width": "15%"
            }
        ],
        "width": "100%"
    });
});
function Delete(url) {
    Swal.fire({
        title:  "?"+'האם אתה בטוח',
        text: "!" + "אתה לא תוכל לשחזר את זה!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        cancelButtonText: 'ביטול',

        confirmButtonText: '!כן, תמחק'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    if (data.success) {
                        dataTable.ajax.reload();
                        //success notification
                        toastr.success(data.message);
                    }
                    else {
                        //failsure notification
                        toastr.error(data.message);
                    }
                }
            })
        }
    })

}