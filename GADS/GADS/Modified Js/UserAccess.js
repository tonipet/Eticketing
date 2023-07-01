$(function () {
 
    $('#Userlist').DataTable({
        'paging': true,
        'lengthChange': true,
        'searching': true,
        'ordering': true,
        'info': true,
        'autoWidth': true
        
    });


    
   
})

$(function () {
    LoadUsers();
    getDivision();
    getPosition();
    AccessList();
    SelectAllDept();
 
   
})

function LoadUsers() {
    $.ajax({
        url: "/UserAccess/Userlist",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var edithtml = '', deleteHtml = '';
            var html = '';
            $("#TBLCreated").DataTable().clear().draw();
            $.each(result, function (key, item) {
                $("#TBLCreated").DataTable().row.add([
                 
                   item.FirstName,
                    item.LastName,
                    item.DivisionName,
                    item.DepartmentName,
                    item.DesignationName,
                    item.Email,
                    item.Access,
                   '<button type="button"  onclick="return getUsersID(' + item.Usercode + ')" class="btn btn-primary btn-sm a-btn-slide-text">  <span class="glyphicon glyphicon-edit" aria-hidden="true"></span></button>',
                     '<button type="button"   onclick="DeleteUser(' + item.Usercode + ')" class="btn btn-danger btn-sm a-btn-slide-text">  <span class="glyphicon glyphicon-remove" aria-hidden="true"></span></button>'
                ]).draw();
            });
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });

}


function DeleteUser(id){
    var ans = confirm("Are you sure you want to delete this User?");
    if (ans) {
        $.ajax({

            url: "/UserAccess/DeleteUsers/" + id,
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                if (result == true) {
                    LoadUsers();
                }

            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}


function getUsersID(id) {
    $('#ModEditUsers').modal({
        backdrop: 'static',
        keyboard: false
    });
   
    $.ajax({
        url: "/UserAccess/GetUserbyID/" + id,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#Uusercode').val(result.Usercode);
            $('#UFName').val(result.FirstName);
            $('#ULName').val(result.LastName);
            $('#UEmail').val(result.Email);
            $('#UDept').select2().select2('val', [result.DepartmentCode]);
            $('#UAccess').select2().select2('val', [result.UserAccessCode]);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });


}






function getDivision() {
    $.ajax({
        type: "POST",
        url: "/Registration/getDivision",
        data: '{}',
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {

            $.each(result, function (val, text) {
                $('#UDivision')
                     .append(new Option(this['Text'], this['Value'])
                 );
            });
        }
    });
}


function getPosition() {
    $.ajax({
        type: "POST",
        url: "/Registration/getPosition",
        data: '{}',
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            $.each(result, function (val, text) {
                $('#UPosition')
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
                     .append(new Option(this['Text'], this['Value'])
                 );
            });
        }
    });
}



function clearUserErr() {
    $("#UFNameErr").html("<small>*</small>");
    $("#ULNameErr").html("<small>* </small>");
    $("#UDivisionErr").html("<small>* </small>");
    $("#UDeptErr").html("<small>* </small>");
    $("#UPositionErr").html("<small>* </small>");
    $("#UEmailErr").html("<small>* </small>");
    $("#UAccessErr").html("<small>* </small>");

}



function SelectAllDept() {

    //var empObj = {
    //    DivisionCode: Div
    //};
    $.ajax({
        url: "/UserAccess/AllDeptList",
        data: {},
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
    
            $.each(result, function (val, text) {
                $('#UDept').append($('<option>', {
                    value: this['Value'],
                    text: this['Text']
                })).select2();
            });

        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });


}




function getdepartment(Div) {

    var empObj = {
        DivisionCode: Div
    };
    $.ajax({
        url: "/Registration/getDeparment",
        data: JSON.stringify(empObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
             $('#UDept').html('<option></option>');
            $.each(result, function (val, text) {
                $('#UDept').append($('<option>', {
                    value: this['Value'],
                    text: this['Text']
                })).select2();
            });

        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
   

}


$(document).on('click', '#btnCloseUser', function () {
    clearUserErr();
});
$(document).on('click', '#btnEditUser', function () {
    var validate = Uservalidate();
    if( validate==false){
        return false;
    }
    UpdateUsers();
});

function UpdateUsers() {

    var empObj = {
        LastName: $('#ULName').val(),
        FirstName: $('#UFName').val(),
        DivisionCode: $('#UDivision').val(),
        DepartmentCode: $('#UDept').val(),
        Position: $('#UPosition').val(),
        Email: $('#UEmail').val(),
        UserAccessCode: $('#UAccess').val(),
        Usercode: $('#Uusercode').val()
    };

    $.ajax({
        url: "/UserAccess/UpdateUsers",
        data: JSON.stringify(empObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",

        success: function (result) {
            if (result == true) {
               
                $('#EditUsers').modal('hide');
                LoadUsers();
            }
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });

}



function Uservalidate() {
    var isValid = true;
    var msg = "";

    if ($('#UFName').val() == "") {
        $("#UFNameErr").html("<small>* required </small>");
        isValid = false;
    }
    if ($('#ULName').val() == "") {
        $("#ULNameErr").html("<small>* required </small>");
        isValid = false;
    }
    if ($('#UDivision').val() == "") {
        $("#UDivisionErr").html("<small>* required </small>");
        isValid = false;
    }
        if ($('#UDept').val() == "") {
            $("#UDeptErr").html("<small>* required </small>");
            isValid = false;
        }

        if ($('#UPosition').val() == "") {
            $("#UPositionErr").html("<small>* required </small>");
            isValid = false;
        }


        if ($('#UEmail').val() == "") {
            $("#UEmailErr").html("<small>* required </small>");
            isValid = false;
        }
        if ($('#UAccess').val() == "") {
            $("#UAccessErr").html("<small>* required </small>");
            isValid = false;
        }

        return isValid;

  
}



