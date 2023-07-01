

 $(function () {

    $('#TBLCreated').DataTable({
        'paging': true,
        'lengthChange': true,
        'searching': true,
        'ordering': true,
        'info': true,
        'autoWidth': true
    });
    $('#CloseAdvanceSearch').hide();
    $('#UpdateDialogSave').hide();
    
    loadDetails();
    getstatus();
    getAssignTo();
    getTags();
    getdepartment();
    getstickertype();
})

 $(document).on('click', '#btnattachmentlist', function () {

     $("#messageBody").toggleClass("direct-chat-contacts-open");
   
 });
 var GlobalSubticket = "";

 function getFilename(elm) {
     var renderFileList;
     fileList = [];
     for (var i = 0; i < fileInput.files.length; i++) {
         fileList.push(fileInput.files[i]);
     }

     $("#tblDialogAttachment").DataTable().clear().draw();
     fileList.forEach(function (file, index) {
         $("#tblDialogAttachment").DataTable().row.add([
          file.name
         ]).draw();
         fileList = '';

     });
 }


 function getAttachement(elm) {
     var renderFileList;
     fileList = [];
     for (var i = 0; i < dialogattachment.files.length; i++) {
         fileList.push(dialogattachment.files[i]);
     }

     $("#tblAttachment").DataTable().clear().draw();
     fileList.forEach(function (file, index) {
         $("#tblAttachment").DataTable().row.add([
          file.name
         ]).draw();
         fileList = '';

     });
 }


 
 $(document).on('click', '#BtnAttachmentSave', function () {
     var fileList = [];
     var files ='' ;


     if ($('#dialogattachment').get(0).files.length === 0) {
         files;
     } else {
        
         for (var i = 0; i < dialogattachment.files.length; i++) {
             fileList.push(dialogattachment.files[i]);
         }
     

         fileList.forEach(function (file, index) {
             files += file.name + ','
         });
  
     }
 
 

     var empObj = {
         TicketNo: $('#DialogTicketNo').val(),
         TargetCompletionDate: ' ',
         TAGS: ' ',
         TicketStatusCode:' ',
         AssignedTo: ' ',
         DIALOGMSG: ' ',
         AttachmentFile: files.slice(0,-1),

     };



     $.ajax({
         url: "/Common/saveDialog",
         data: JSON.stringify(empObj),
         type: "POST",
         contentType: "application/json;charset=utf-8",
         dataType: "json",
         success: function (result) {
             if (result == true) {
                 saveDialogattachment();
                 GetDialogAttachment($('#DialogTicketNo').val());
                 GetDialog($('#DialogTicketNo').val());
             }
         },
         error: function (errormessage) {
             alert(errormessage.responseText);
         }
     });
     
     
 });







 $(document).on('click', '#btnCloseModSuccess', function () {
     $('#saveDialogSave').show();
     $('#UpdateDialogSave').hide();
    
     loadDetails();
   
});

$(document).on('click', '#AdvanceSearch', function () {
    $('#CloseAdvanceSearch').show();
    $('#AdvanceSearch').hide();
    $('#TBLCreated tfoot th').each(function () {
        $('#TBLCreated tfoot th').each(function () {
            var title = $(this).text();
            $(this).html('<input type="text" placeholder="Search ' + title + '" />');
        });

        // DataTable
        var table = $('#TBLCreated').DataTable();

        // Apply the search
        table.columns().every(function () {
            var that = this;

            $('input', this.footer()).on('keyup change', function () {
                if (that.search() !== this.value) {
                    that
                        .search(this.value)
                        .draw();
                }
            });
        });
    });

});
$(document).on('click', '#CloseAdvanceSearch', function () {
    $('#CloseAdvanceSearch').hide();
    $('#AdvanceSearch').show();
    $('#TBLCreated tfoot th').each(function () {
        var title = $(this).text();
        $(this).html('');
    });

});

   

    
var fileName = "";

function getSubTicketType() {
 
    var empObj = {
        TicketTypeCode: $('#Stickertype').val()
    };


    $.ajax({
        url: "/HomePage/SubTicketType",
        data: JSON.stringify(empObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {

            $('#SubStickertype').empty();
            $('#SubStickertype')
            $('#SubStickertype').append('<option selected="" value=""></option>')
            $.each(result, function (val, text) {

                $('#SubStickertype')
                     .append(new Option(this['Text'], this['Value'])
                 );
            });

            $('#SubStickertype').select2().select2('val', [GlobalSubticket]);
            GlobalSubticket = "";
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });

}
function getdepartment() {
    $.ajax({
        url: "/HomePage/getDepartmentPerUser",
        data: '{}',
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {

            $('#Department').append('<option selected="" value=""></option>')
            $.each(result, function (val, text) {
                $('#Department')
                     .append(new Option(this['Text'], this['Value'])
                 );
            });
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
function getstickertype() {
    $.ajax({
        url: "/HomePage/getStickertype",
        data: '{}',
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            $('#Stickertype').append('<option selected="" value=""></option>')
            $.each(result, function (val, text) {
                $('#Stickertype')
                     .append(new Option(this['Text'], this['Value'])
                 );
            });

        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function loadDetails() {
    $.ajax({
        url: "/Homepage/TicketList",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var edithtml='',deleteHtml = '';
            var html = '';
         
            $("#TBLCreated").DataTable().clear().draw();
            $.each(result, function (key, item) {
                
             
                if (item.TicketStatus.toUpperCase() == 'PENDING') {
                   
                 
                    edithtml = ' <button type="button"  onclick="return getTicketByID(' + item.TicketNo + ')" class="btn btn-primary btn-sm a-btn-slide-text">  <span class="glyphicon glyphicon-edit" aria-hidden="true"></span></button>'
                    deleteHtml = '<button type="button"   onclick="Delete(' + item.TicketNo + ')" class="btn btn-danger btn-sm a-btn-slide-text">  <span class="glyphicon glyphicon-remove" aria-hidden="true"></span></button>'
                } else {
                    edithtml = '';
                    deleteHtml = '';
                }

                $("#TBLCreated").DataTable().row.add([
                   '<button type="button" onclick="return ticketModal(' + item.TicketNo + ')"  class="btn btn-link">' + item.TicketID + '</button>',
                   item.CreatedDate,
                    item.DepartmentName,
                    item.Title,
                    item.TicketType,
                    item.SubTicketType,
                    item.Customer,
                    '<font color="' + item.HtmlTextColor + '">' + item.TicketStatus + '</font>',
                     item.AssignedTo,
                    item.TargetCompletionDate,
                    edithtml,
                    deleteHtml
                   

                ]).draw();
            });


        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
$(document).on('click', '#save', function () {

    saveticketDataDetails();
});

$(document).on('click', '#AddAttachment', function () {
    $('#DialogAttachmentModal').modal({
        backdrop: 'static',
        keyboard: false
    });
});


$(document).on('click', '#update', function () {
    updateticketDataDetails();
});

$(document).on('click', '#CreateTicket', function () {
    clear();
    clearErr();
    $('#CreateTicketModal a:first').tab('show');
    $('#CreateTicketModal').modal({
        backdrop: 'static',
        keyboard: false
    });

    $('#save').show();
    $('#update').hide();
});




function saveticketDataDetails() {
    var _isvalid = validate();
    if (_isvalid == false) {
        return false;
    }

    var files = '';

    if ($('#fileInput').get(0).files.length === 0) {
        files;
    } else {
        fileList = [];
        for (var i = 0; i < fileInput.files.length; i++) {
            fileList.push(fileInput.files[i]);
        }

        fileList.forEach(function (file, index) {
            files += file.name+','
        });
    }

   
 


    var empObj = {
        TicketTypeCode: $('#Stickertype').val(),
        BriefDescription: $('#BriefDescription').val(),
        DepartmentCode: $('#Department').val(),
        AttachmentFile: files,
        SubTicketType: $('#SubStickertype').val(),
        Title: $('#title').val()
     

    };


    $.ajax({
        url: "/Homepage/saveTicket",
        data: JSON.stringify(empObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result == true) {
                saveattachment();
                $('#CreateTicketModal').modal('hide');
                $('#lblmesgsuccess').html('Successfully Save');
                $('#modsuccess').modal({
                    backdrop: 'static',
                    keyboard: false
                });
            }
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}


function saveattachment() {
    var files = $("#fileInput").get(0).files;
    var fileData = new FormData();
    for (var i = 0; i < files.length; i++) {
        fileData.append("fileInput", files[i]);
    }
    $.ajax({
        type: "POST",
        url: "/HomePage/UploadFiles",
        dataType: "application/json; charset=utf-8",
        contentType: false,
        processData: false,
        data: fileData,
        success: function (result, status, xhr) {

        },
        error: function (xhr, status, error) {

        }
    });
}


function updateticketDataDetails() {
    var _isvalid = validate();
    if (_isvalid == false) {
        return false;
    }
    var file = '';

    if ($('#fileInput').get(0).files.length === 0) {
        file;
    } else {
        file = $("#fileInput").prop('files')[0]["name"];
    }

    var empObj = {
        TicketNo: $('#HiddenticketNo').val(),
        TicketTypeCode: $('#Stickertype').val(),
        BriefDescription: $('#BriefDescription').val(),
        DepartmentCode: $('#Department').val(),
        AttachmentFile: file,
        SubTicketType: $('#SubStickertype').val(),
        Title: $('#title').val()
    };



    $.ajax({
        url: "/Homepage/UpdateTicket",
        data: JSON.stringify(empObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",

        success: function (result) {
            if (result == true) {
                saveattachment();
                $('#CreateTicketModal').modal('hide');
                $('#lblmesgsuccess').html('Successfully Updated');
                $('#modsuccess').modal({
                    backdrop: 'static',
                    keyboard: false
                });
            }
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}



function clearErr() {
    $("#TitleErr").html("<small>*</small>");
    $("#StickerTypeErr").html("<small>* </small>");
    $("#BriefDescriptionErr").html("<small>* </small>");
    $("#SubTicketTypeErr").html("<small>* </small>");
    $("#DepartmentErr").html("<small>* </small>");
}
function clear() {

    $('#Stickertype').val('');
    $('#BriefDescription').val('');
    $('#title').val('');
    $('#SubStickertype').empty();
    $('#Department').select2().select2('val', ['']);
    $('#Stickertype').select2().select2('val', ['']);
    $("#attachmentLI").html('');
}


function getTicketByID(TicketID) {

    clearErr();
    $.ajax({
        url: "/Homepage/getbyID/" + TicketID,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            var Attachment = result.AttachmentFile;
            $('#Department').select2().select2('val', [result.DepartmentCode]);
            $('#Stickertype').select2().select2('val', [result.TicketTypeCode]);
            $('#title').val(result.Title);
            GlobalSubticket = result.SubTicketType;
            $('#BriefDescription').val(result.BriefDescription);
            $('#HiddenticketNo').val(result.TicketNo);

            //$("#attachmentLI").html('<a href="/HomePage/uploadedFile?fileName=' + result.AttachmentFile + '">' + Attachment.substr(Attachment.indexOf("_") + 1) + '</a>');
        
     
        },


        error: function (errormessage) {
            alert(errormessage.responseText);
        }


    });
    $('#CreateTicketModal a:first').tab('show');
    $('#CreateTicketModal').modal('show');
    $('#update').show();
    $('#save').hide();


    return false;
}





function validate() {
    var isValid = true;
    var msg = "";

    if ($('#title').val() == "") {
        $("#TitleErr").html("<small>* required </small>");
        isValid = false;
    }

    if ($('#Stickertype').val() == "") {
        $("#StickerTypeErr").html("<small>* required </small>");
        isValid = false;
    }



    if ($('#BriefDescription').val() == "") {
        $("#BriefDescriptionErr").html("<small>* required </small>");
        isValid = false;
    }
    if ($('#SubStickertype').val() == "" || $('#SubStickertype').val() == null) {
        $("#SubTicketTypeErr").html("<small>* required </small>");
        isValid = false;
    }
    if ($('#Department').val() == "") {
        $("#DepartmentErr").html("<small>* required </small>");
        isValid = false;
    }
    return isValid;

}


function ticketModal(id) {

    $('#DialogTicketModal').modal({
        backdrop: 'static',
        keyboard: false
    });
    GetDialog(id);
    getBYID(id);
  
       GetDialogAttachment(id);

}

function getBYID(id) {
    $.ajax({
        url: "/Homepage/getbyID/" + id,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            var DialogTagsList = result.Dialogtags;
            var List = DialogTagsList.split(',');
            $('#DialogTags').val(List);
            $('#DialogTags').trigger('change');
            $('#DialogStatus').select2().select2('val', [result.TicketStatusCode]);
            $("#Dialoginformation").text("Title:" + result.Title + "/" + "Department:" + result.DepartmentName + "/" + "Ticketype:" + result.TicketType + "/" + "SubTicketype:" + result.SubTicketType);
            $("#DialogDescription").html(result.BriefDescription.replace(/[\r\n]+/g, '<br/>'));
            $('#DialogAssignto').select2().select2('val', [result.AssignToCode]);
            $('#datepicker').datepicker({ dateFormat: 'yy-mm-dd' }).datepicker('setDate', result.TargetCompletionDate);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }


    });
}

function GetDialogAttachment(id) {
    var empObj = {
        TicketNo: id
    };
    var htmlLine = "";

    $.ajax({
        url: "/Common/GetDialogAttachment/",
        data: JSON.stringify(empObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
          
            htmlLine += "<ul class='contacts-list'>";
            $.each(result, function (key, item) {
                var attach = "'" + item.AttachmentFile + "'";
       
                htmlLine += "<li><a href=/HomePage/uploadedFile?fileName=" + item.AttachmentFile.replace(/ /g, "%20") + ">" + item.AttachmentFile.substr(item.AttachmentFile.indexOf("_") + 1) + "</a><span class='pull-right'> <button type='button' onclick='DeleteAttachment(" + item.AttachementID + ")'  class='btn btn-danger btn-sm a-btn-slide-text'>  <span class='glyphicon glyphicon-remove' aria-hidden='true'></span></button></span></li>";
               
            });
            //htmlLine += " <li> <a href='#'> <div class='contacts-list-info'>";
            htmlLine += "</ul>";
            $("#DialogAttachmentList").html(htmlLine);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });


}


function DeleteAttachment(id) {


    var ans = confirm("Are you sure you want to delete this attachment?");
    if (ans) {
        var empObj = {
            AttachementID: id,
            TicketNo: $('#DialogTicketNo').val()
        };
        $.ajax({
            url: "/Common/DeleteAttachement/",
            data: JSON.stringify(empObj),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                GetDialogAttachment($('#DialogTicketNo').val());
                GetDialog($('#DialogTicketNo').val());
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}

function GetDialog(id) {
    $('#DialogTicketNo').val(id);
    
    var fullname = $("#MasterPagerUserName").text();
    var htmlLine = "";

    htmlLine += " <div class='direct-chat-info clearfix'>";
    htmlLine += " <span class='direct-chat-name pull-left'>Description:</span> </div>";
    htmlLine += "<div class='direct-chat-text'> <p id='DialogDescription'></p> </div> ";

    var empObj = {
        TicketNo: id
    };

    $.ajax({
        url: "/Common/GetDialoglist/",
        data: JSON.stringify(empObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
        
            $.each(result, function (key, item) {



                if (fullname == item.FullName) {
                    htmlLine += "<div class='direct-chat-msg right'>";
                    htmlLine += "<div class='direct-chat-info clearfix'>";
                    htmlLine += "<span class='direct-chat-name pull-right'>"
                    if (parseInt(item.ConvoDateExp) < 24 && fullname == item.FullName && item.Editable == 'True') {
                        htmlLine += "<button class='btn btn-link' onclick=editDialogByID('" + item.DialogID + "')>edit</button>"
                    }
                    htmlLine += " "+item.FullName +"</span>"
                    htmlLine += " <span class='direct-chat-timestamp pull-left'>" + item.CreatedDate + "</span>"
                    htmlLine += "</div>";
                    htmlLine += "<div class='direct-chat-text'>" + item.Dialog + "</div>"
                    htmlLine += "</div>"
                } else {
                    htmlLine += "<div class='direct-chat-msg'>";
                    htmlLine += "<div class='direct-chat-info clearfix'>";
                    htmlLine += "<span class='direct-chat-name pull-left'>" + item.FullName + " "
                    if (parseInt(item.ConvoDateExp) < 24 && fullname == item.FullName && item.Editable == 'True' ) {
                        htmlLine += "<a href='#' onclick='editDialogByID(" + item.DialogID + ")'> Edit <a/>"
                    }
                    htmlLine += "</span>"

                    htmlLine += " <span class='direct-chat-timestamp pull-right'>" + item.CreatedDate + "</span>"
                    htmlLine += "</div>";
                    htmlLine += "<div class='direct-chat-text'>" + item.Dialog + "</div>"

                    htmlLine += "</div>"
                }



                //$("#DialogmessageID").html(htmlLine);

            });
            $("#DialogmessageID").html(htmlLine);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });







}

function getTags() {


    $.ajax({
        type: "POST",
        url: "/Common/getTags",
        data: '{}',
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            $.each(result, function (val, text) {
                $('#DialogTags')
                     .append(new Option(this['Text'], this['text'])
                 );
            });
        }
    });
}

function getstatus() {
    $.ajax({
        type: "POST",
        url: "/Common/getstatus",
        data: '{}',
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            //$('#DialogStatus').append('<option selected="" value=""></option>')
            $.each(result, function (val, text) {
                $('#DialogStatus')
                     .append(new Option(this['Text'], this['Value'])
                 );
            });
        }
    });
}

function getAssignTo() {

    $.ajax({
        type: "POST",
        url: "/Common/getAssignto",
        data: '{}',
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            //$('#DialogAssignto').append('<option selected="" value=""></option>')
            $.each(result, function (val, text) {
                $('#DialogAssignto')
                     .append(new Option(this['Text'], this['Value'])
                 );
            });
        }
    });
}



function addTicketDialog() {
    var DialogTags = $('#DialogTags').val();
    var TagsDialog = '';
    var msg = '';
    if (DialogTags == null) {
        TagsDialog = '';
    } else {
        for (i = 0; i < DialogTags.length; i++) {
            TagsDialog += (DialogTags[i]) + ',';
        }
    }
    if (!$.trim($("#dialogMsg").val())) {
        msg = " ";
    } else {
        msg = $('#dialogMsg').val();
    }

    var empObj = {
        TicketNo: $('#DialogTicketNo').val(),
        TargetCompletionDate: $('#datepicker').val(),
        TAGS: TagsDialog,
        TicketStatusCode: $('#DialogStatus').val(),
        AssignedTo: $('#DialogAssignto').val(),
        DIALOGMSG: msg,
        AttachmentFile: ' ',
     
    };

  

    $.ajax({
        url: "/Common/saveDialog",
        data: JSON.stringify(empObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result == true) {
             
                ticketModal($('#DialogTicketNo').val())
                GetDialog($('#DialogTicketNo').val());
                saveDialogattachment();
                GetDialogAttachment($('#DialogTicketNo').val());
                dialogClear();
            }
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function dialogClear() {
    $('#dialogMsg').val('');
    $("#dialogattachment").val('');;
}

$(document).on('click', '#saveDialogSave', function () {
  
    var _isvalid = DialogValidate();
   
    if (_isvalid == false) {
        return false;
    } 
        addTicketDialog();
        GetDialog($('#DialogTicketNo').val());
        GetDialogAttachment($('#DialogTicketNo').val());
        getBYID($('#DialogTicketNo').val());
    
  
});


function Delete(ID) {
    var ans = confirm("Are you sure you want to delete this Ticket?");

    if (ans) {
        $.ajax({
        
            url: "/HomePage/DeleteTicket/" + ID,
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                if (result == true) {

                    loadDetails();
                }
               
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}

function DialogValidate() {
    var isvalid = true;
    if ($('#DialogStatus').val() == null) {
        $("#StatusDialogErr").html("<small>* required </small>");
        isvalid = false;
    }

    if ($('#DialogAssignto').val() == null) {
        $("#AssignToDialogErr").html("<small>* required </small>");
        isvalid = false;
    }

    if ($('#datepicker').val() == "" || $('#datepicker').val() == null) {
        $("#TargetDateDialogErr").html("<small>* required </small>");
       
        isvalid = false;
    }


    return isvalid;
}




function saveDialogattachment() {
    var files = $("#dialogattachment").get(0).files;
    var fileData = new FormData();
    for (var i = 0; i < files.length; i++) {
        fileData.append("dialogattachment", files[i]);
    }
    $.ajax({
        type: "POST",
        url: "/HomePage/UploadFiles",
        dataType: "application/json; charset=utf-8",
        contentType: false,
        processData: false,
        data: fileData,
        success: function (result, status, xhr) {

        },
        error: function (xhr, status, error) {

        }
    });
}

function editDialogByID(id) {
    var obj = {
        DialogID:id
    }
    $('#UpdateDialogSave').show();
    $('#saveDialogSave').hide();
    $.ajax({
        url: "/Homepage/DialogByID",
        data: JSON.stringify(obj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
          
            $.each(result, function (key, item) {
                $("#dialogMsg").val(item.Dialog);
                $("#DialogConvoID").val(item.DialogID);
                
            });
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    }


$(document).on('click', '#UpdateDialogSave', function () {

    UpdateDialogDetails();
});

function UpdateDialogDetails() {
    var obj = {
        DialogID: $("#DialogConvoID").val(),
        Dialog: $("#dialogMsg").val()
    }
    
    $.ajax({
        url: "/Homepage/UpdateDialogDetails",
        data: JSON.stringify(obj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            $('#UpdateDialogSave').hide();
            $('#saveDialogSave').show();
            GetDialogAttachment($('#DialogTicketNo').val());
            GetDialog($('#DialogTicketNo').val());
            $("#dialogMsg").val("");
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
