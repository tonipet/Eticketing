$(function () {

    $('#TBLUserGroup').DataTable({
        'paging': true,
        'lengthChange': true,
        'searching': true,
        'ordering': true,
        'info': true,
        'autoWidth': true

    });

    LoadGroups();


})
$(document).on('click', '#AddUserGroup', function () {

    $('#UGUpdate').hide();
    $('#ModAddUserGroup').modal({
        backdrop: 'static',
        keyboard: false
    });
});






function LoadGroups() {
    $.ajax({
        url: "/UserAccess/AccessList",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var edithtml = '', deleteHtml = '';
            var html = '';
            $("#TBLUserGroup").DataTable().clear().draw();
            $.each(result, function (key, item) {
                $("#TBLUserGroup").DataTable().row.add([
                   item.Text,
                    '<button type="button"  onclick="return getGroupsID(' + item.Value + ')" class="btn btn-primary btn-sm a-btn-slide-text">  <span class="glyphicon glyphicon-edit" aria-hidden="true"></span></button>',
                ]).draw();
            });
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });

}

function getGroupsID(id) {
    $('#UGSave').hide();
    $('#ModAddUserGroup').modal({
        backdrop: 'static',
        keyboard: false
    });

    $.ajax({
        url: "/UserAccess/GetGroupsbyID/" + id,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#UACode').val(result.Value);
            $('#UGroup').val(result.Text);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

$(document).on('click', '#UGUpdate', function () {
    var empObj = {
        Access: $('#UGroup').val(),
        UserAccessCode: $('#UACode').val()
    };

    $.ajax({
        url: "/UserAccess/UpdateUserGroup",
        data: JSON.stringify(empObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",

        success: function (result) {
            if (result == true) {
                $('#ModAddUserGroup').modal('hide');
                LoadGroups();
            }
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
});