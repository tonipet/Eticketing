$(function () {
    $('#TBLStatus').DataTable({
        'paging': true,
        'lengthChange': true,
        'searching': true,
        'ordering': true,
        'info': true,
        'autoWidth': true
    });
    LoadStatus();
    GetMasterStatus();
    AccessList();
})

$(document).on('click', '#AddStatus', function () {
    $('#UAUpdate').hide();
    $('#ModAddStatus').modal({
        backdrop: 'static',
        keyboard: false
    });
  
});

$(document).on('click', '#UAClose', function () {
    clearUserAccess();
});

$(document).on('click', '#UAUpdate', function () {
    var validate = ValidateUserAccess();
    if (validate == false){
        return false;
    }
    UpdateUserAccess();
});


$(document).on('click', '#UASave', function () {
    var validate = ValidateUserAccess();
    if (validate==false) {
        return false;
    }
   AddUserAccess();
});



function LoadStatus() {
    $.ajax({
        url: "/UserAccess/UserStatuslist",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var edithtml = '', deleteHtml = '';
            var html = '';

            $("#TBLStatus").DataTable().clear().draw();
            $.each(result, function (key, item) {
                $("#TBLStatus").DataTable().row.add([

                   item.Access,
                    item.StatusList,
                   '<button type="button"  onclick="return getStatusID(' + item.UserAccessCode + ')" class="btn btn-primary btn-sm a-btn-slide-text">  <span class="glyphicon glyphicon-edit" aria-hidden="true"></span></button>',
                   '<button type="button"   onclick="DeleteUser(' + item.UserAccessCode + ')" class="btn btn-danger btn-sm a-btn-slide-text">  <span class="glyphicon glyphicon-remove" aria-hidden="true"></span></button>'
                ]).draw();
            });
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });

}


function getStatusID(id) {
    $('#UASave').hide();

    $('#ModAddStatus').modal({
        backdrop: 'static',
        keyboard: false
    });

    $.ajax({
        url: "/UserAccess/GetStatus/" + id,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#UACode').val(result.UserAccessCode);
            $('#UAccess').select2().select2('val', [result.Access]);
    
            
            var UAStatusList = result.TicketStatusCode; 
            var List = UAStatusList.split(',');
            $('#UAStatus').val(List);
            //$('#UAStatus').val(List);
            $('#UAStatus').trigger('change');
           
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
function GetMasterStatus() {
    $.ajax({
        type: "POST",
        url: "/UserAccess/GetMasterStatus",
        data: '{}',
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
       
            $.each(result, function (val, text) {
                $('#UAStatus')
                     .append(new Option(this['Text'], this['Value'])
                 );
            });
        }
    });
}


function AccessList() {
    $.ajax({
        type: "POST",
        url: "/UserAccess/AccessList",
        data: '{}',
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            $.each(result, function (val, text) {
                $('#UAccess')
                     .append(new Option(this['Text'], this['Text'])
                 );
            });
        }
    });
}

function UpdateUserAccess() {

    var UAStatus = $('#UAStatus').val();
    var UAStatuslist = "";
    var a=0;
    for (i = 0; i < UAStatus.length; i++) {
        UAStatuslist += (UAStatus[i]) + ',';
    }
    var UAObj = {
        UserAccessCode: $('#UACode').val(),
        Access: $('#UAccess').val(),
        StatusList: UAStatuslist.slice(0,-1)

    }
   
    
    $.ajax({
        type: "POST",
        url: "/UserAccess/UpdateUserAccess",
        data: JSON.stringify(UAObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result == true) {
                LoadStatus();
                $('#ModAddStatus').modal('hide');
            }
            
        }
    });

}


function AddUserAccess() {

    var UAStatus = $('#UAStatus').val();
    var UAStatuslist = "";
    var a = 0;
    for (i = 0; i < UAStatus.length; i++) {
        UAStatuslist += (UAStatus[i]) + ',';
    }
    var UAObj = {
        UserAccessCode: $('#UACode').val(),
        Access: $('#UAccess').val(),
        StatusList: UAStatuslist.slice(0, -1)

    }

    $.ajax({
        type: "POST",
        url: "/UserAccess/AddUserAccess",
        data: JSON.stringify(UAObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result == true) {
                LoadStatus();
                $('#ModAddStatus').modal('hide');
            }
            else {
                $('#lblmesgerror').html('User Group Already Exist!');
                $('#moderror').modal({
                    backdrop: 'static',
                    keyboard: false
                });

            }

        }
    });

}


function clearUserAccess() {
    $("#UAccessErr").html("<small>*</small>");
    $("#UAStatussErr").html("<small>* </small>");

   
    $('#UAccess').select2().select2('val', ['']);
    $('#UAStatus').select2().select2('val', ['']);
    $('#UAUpdate').show();
    $('#UASave').show();

}

function ValidateUserAccess() {
    var isvalid = true;

    if ($('#UAccess').val() == "" || $('#UAccess').val() == null) {
        $("#UAccessErr").html("<small>* required </small>");
        isValid = false;
    }
    if ($('#UAStatus').val() == "" || $('#UAStatus').val() == null) {
        $("#UAStatussErr").html("<small>* required </small>");
        isValid = false;
    }

    return isvalid;


}








