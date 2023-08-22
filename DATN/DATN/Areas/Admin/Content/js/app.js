$(document).ready(function () {
    $("#tinhID").change(function () {
        $.get("/Admin/QLDiaBanCN/GetHuyen", { tinhID: $("#tinhID").val() }, function (data) {
            $("tinhID").empty();
            $each(data, function (index, row) {
                $("#idHuyen").append("<option value='" + row.ID + "'>" + row.TenHuyen + " <option/>")
            });
        });
    })   
