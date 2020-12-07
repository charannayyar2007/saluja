var row_count = 0;
$(document).ready(function () {

});
//get subject list 
function GetSubjectforUploadHW()
{
    var url = "/GetSubjectBasedClassId?ClassId=" + $('#txtClass option:selected').val();

    AjaxCallWithOutStorage(url, "get").then(data => {
        $('#txtSubject').empty();
        $('#txtSubject').append(new Option("Select Subject", 0));
        for (var i = 0; i < data.length; i++) {
            
            $('#txtSubject').append(new Option(data[i].Subject, data[i].Id));
        }
    });
}

function GetSubjectforShowHW() {
    var url = "/GetSubjectBasedClassId?ClassId=" + $('#txtclass option:selected').val();

    AjaxCallWithOutStorage(url, "get").then(data => {
        $('#txtsub').empty();
        $('#txtsub').append(new Option("All Subject", 0));
        for (var i = 0; i < data.length; i++) {

            $('#txtsub').append(new Option(data[i].Subject, data[i].Id));
        }
    });
}

function GetSubjectforViewHW() {
    var url = "/GetSubjectBasedClassId?ClassId=" + $('#txtclass option:selected').val();

    AjaxCallWithOutStorage(url, "get").then(data => {
        $('#txtsub').empty();
        $('#txtsub').append(new Option("All Subject", 0));
        for (var i = 0; i < data.length; i++) {

            $('#txtsub').append(new Option(data[i].Subject, data[i].Id));
        }
    });
}

//for alert box
function CheckClassvalueforUploadHW() {
        if ($('#txtClass').val() == 0) {
          return alert("Please select Class First");
        }
      
}
function CheckClassvalueforshowHW() {
    if ($('#txtclass').val() == 0) {
        return alert("Please select Class First");
    }

}
function CheckClassvalueforViewHW() {
    if ($('#txtclass').val() == 0) {
        return alert("Please select Class First");
    }

}

function Change() {
    var today = new Date();
    var dd = String(today.getDate()).padStart(2, '0');
    var mm = String(today.getMonth() + 1).padStart(2, '0'); //January is 0!
    var yyyy = today.getFullYear();

    today = yyyy + '-' + mm + '-' + dd;
   
    if ($('#txtLastDate').val() != null) {
        $('#txtLastDate').attr('min', today);
    }
    
  
}

function UploadHomework(fl, Id) {
    if (sessionStorage.getItem('accessToken') == null) {
        window.location.href = "/";
    }
   
    var flag = Validation();
    if (flag == null) {
        document.getElementById("loaders").style.display = "block"
        if (fl == 0) {
            SaveHWData("/AssignmentDetails", 0);
        }
        else {
            SaveHWData("/EditAssignmentDetails", Id);
        }
    }
    else {
        alert(flag);
      
    }
}

function SaveHWData(Url, Iden) {
    var file = document.getElementById("txtFile").files;

    var form = new FormData();
    var filename = '';
    for (var i = 0; i < file.length; i++) {
        form.append("file" + i, file[i]);
        filename += file[i].name + ',';
    }
    var roleid = '';
    if (sessionStorage.getItem('roleId') == 'teacher')
        roleid = 3;
    $.ajax({
        // Post username, password & the grant type to /token
        url: '/UploadFile',
        method: 'POST',
        contentType: false,
        processData: false,
        data: form,
        headers: {
            'Authorization': 'Bearer '
                + sessionStorage.getItem(SessionKeys.token)
        },
        success: function (response) {
            var filepath = '';
            for (var i = 0; i < response.length; i++)
                filepath += response[i] + ',';
            sessionStorage.setItem("filename", response);
            $.ajax({
                url: Url,
                method: 'POST',
                contentType: 'application/json',
                data: JSON.stringify({
                    classid: $('#txtClass option:selected').val(),
                    sectionId: $("#txtSection option:selected").val(),
                    subjectId: $("#txtSubject option:selected").val(),
                   /// uploadeOn: $("#txtDate").val(),
                    deadlineDate: $("#txtLastDate").val(),
                    filepath: filepath.substring(0, filepath.length - 1),
                    FileName: filename.substring(0, filename.length - 1),
                    uploadedby: roleid,
                    enrollmentCode: sessionStorage.getItem("userId"),
                    Id:Iden
                }),
                 headers: {
                    'Authorization': 'Bearer '
                        + sessionStorage.getItem(SessionKeys.token)
                },
                success: function (resp) {
                  
                    Reset();
                    ShowUploadHW();
                    document.getElementById("loaders").style.display = "none"
                }

            });
        },
        // Display errors if any in the Bootstrap alert <div>
        error: function (jqXHR) {
            document.getElementById("loaders").style.display = "none"
            $('#divErrorText').text(jqXHR.responseText);
            $('#divError').show('fade');

        }
    });
}

function SearchHW() {
    var flag = false;
    if (($("#txtclass option:selected").val() == 0) && ($("#txtsub option:selected").val() == 0) && ($("#txtdate").val() == "")) {
        flag = true;
    }
    if (!flag) {
        document.getElementById("loaders").style.display = "block"
        var cl = $("#txtclass option:selected").val();
        var sub = $("#txtsub option:selected").val();
        var dat = $("#txtdate").val();
    //   var date=new Date
        var url = "/SearchHWByTeacher?ClassId=" + cl + "&SubId=" + sub + "&Date=" + dat
       
        AjaxCallWithOutStorage(url, "get").then(data => {
            $("#DataTables_Table_1").dataTable().fnDestroy();
            $('#DataTables_Table_1').DataTable({

                // sDom: 'lrtip',
                //bLengthChange: false,
                stateSave: true,
                "bDestroy": true,
                //  bLengthChange: false, //thought this line could hide the LengthMenu
                data: data,
                columns: [{
                    data: "ClassName"
                },
                { data: "SectionName" },
                { data: "Subject" },

                { data: "uploadOn" },
                { data: "LastUploadDate" },
                {
                    "data": "weblink",
                    "render": function (data, type, row, meta) {
                        var file = row.fileName.split(",");
                        var path = row.filepath.split(",");
                        var value = '';
                        for (var i = 0; i < row.fileName.split(",").length; i++) {

                            value += '<a target="_blank" href="' + path[i] + '" > ' + file[i] + '</a > ';


                        }
                        data = value;
                        return data;
                    }
                },
                {
                    "data": "weblink",
                    "render": function (data, type, row, meta) {
                        data = '<span onclick="EditHW(' + row.Id + ')" > <i class="fa fa-edit text-success actionbtn"></i></span>  <span onclick="DeleteHW(' + row.Id + ')" > <i class="fa fa-times text-danger actionbtn"></i></span>';



                        return data;
                    }
                },
                ]
            });
            $("#DataTables_Table_1_info").append('(filtered from ' + row_count + ' total entries)');
            document.getElementById("loaders").style.display = "none"
        });
        
   //    })
    }
    else {
        ShowUploadHW();
    }
        }

function SearchViewHW() {
    
    var flag = false;
    document.getElementById("loaders").style.display = "block";
    if (($("#txtclass option:selected").val() == 0) && ($("#txtsub option:selected").val() == 0) && ($("#txtdate").val() == "")) {
        flag = true;
    }
    if (!flag) {
        
        var cl = $("#txtclass option:selected").val();
        var sub = $("#txtsub option:selected").val();
        var dat = $("#txtdate").val();
        var url = "/SearchViewHW?ClassId=" + cl + "&SubId=" + sub + "&Date=" + dat
        CommonSearchViewHW(url)
    }
    else {
        var url = "/ViewAllAssignment"
        CommonSearchViewHW(url)
       
    }
    document.getElementById("loaders").style.display = "none"
}

function CommonSearchViewHW(url) {
   
    document.getElementById("loaders").style.display = "block"
    AjaxCallWithOutStorage(url, "get").then(data => {
        $("#DataTables_Table_2").dataTable().fnDestroy();
          $('#DataTables_Table_2').DataTable({
                 //   sDom: 'lrtip',
                 //   bLengthChange: false, //thought this line could hide the LengthMenu
              stateSave: true,
              "bDestroy": true,
              data: data,
            
          
                     columns: [
                         {
                             "data": "weblink",
                             "render": function (data, type, row, meta) {
                                 if(row.Status)
                                     return '<input type="checkbox" name="id" value="' + row.Id + '">';

                                 return '<input type="checkbox" name="id" value="' + row.Id + '" disabled>';

                             }
                         },
                        {
                            "data": "weblink",
                            "render": function (data, type, row, meta) {
                                // data = '<a href="/Download?FilePath=' + row.FilePath + '" > ' + row.FileName + '</a > ';
                                data = row.FirstName + ' ' + row.LastName;
                                return data;
                            }
                        },
                        {

                            data: "ClassName"
                        },
                        { data: "SectionName" },
                        { data: "Subject" },

                        { data: "uploadOn" },
                        { data: "LastUploadDate" },
                        //{
                        //    "data": "weblink",
                        //    "render": function (data, type, row, meta) {
                        //        var file = row.fileName.split(",");
                        //        var path = row.filepath.split(",");
                        //        var value = '';
                        //        for (var i = 0; i < row.fileName.split(",").length; i++) {

                        //            value += '<a target="_blank" href="' + path[i] + '" > ' + file[i] + '</a > ';


                        //        }
                        //        data = value;
                        //        return data;
                        //    }
                        //},
                        {
                            render: function (data, type, row) {
                                data = row.Status == true ? '<span class=" text-success ">Completed</span>' : '<span class=" text-danger">Pending</span>'
                                return data;
                            }
                        },
                        {
                            render: function (data, type, row) {
                                // data = (row.Remark != null && row.Remark!="") ? row.Remark : '-'
                                var remark = '';
                                for (var i = 0; i < row.Remarks.length; i++)
                                    if (row.Remarks[i] != null && row.Remarks[i]!="")
                                    remark = remark +" "+row.Remarks[i] ;

                                data = remark;
                                return data;
                            }
                        },
                        {
                            "data": "weblink",
                            "render": function (data, type, row, meta) {
                                var NotDownloadFiles = 0, downloadFiles = 0;
                                for (var i = 0; i < row.DownloadCheck.length; i++)
                                {
                                    if (row.DownloadCheck[i] == false) {
                                        NotDownloadFiles++;
                                    }
                                    else
                                        downloadFiles++;
                                }
                                if (row.Status && NotDownloadFiles != row.DownloadCheck.length && NotDownloadFiles!=0) {
                                    data = '<span onclick="DeleteStudentHW(' + row.Id + ')" class=\'dele\' style=\'pointer-events:auto;cursor:pointer;\'> <i class=\'fa fa-times text-danger actionbtn\'></i></span>  <span class=\'dele\' onclick="DownlaodHW(' + row.Id + ')" style=\'pointer-events:auto;color: blue;cursor:pointer;\'> <i class=\'fa fa-download text-edit actionbtn\'></i><b id="notific_' + row.Id + '">Total ' + row.DownloadCheck.length + ' files(' + NotDownloadFiles + 'New file) </b ></span > ';
                                    return data;
                                }
                                 if (row.Status && NotDownloadFiles == row.DownloadCheck.length)
                                {
                                     data = '<span onclick="DeleteStudentHW(' + row.Id + ')" class=\'dele\' style=\'pointer-events:auto;cursor:pointer;\'> <i class=\'fa fa-times text-danger actionbtn\'></i></span>  <span class=\'dele\' onclick="DownlaodHW(' + row.Id + ')" style=\'pointer-events:auto;color: blue;cursor:pointer;\'> <i class=\'fa fa-download text-edit actionbtn\'></i><b id="notific_' + row.Id + '">Total ' + row.DownloadCheck.length + ' files </b ></span > ';
                                     return data;

                                }
                                 if (row.Status && downloadFiles == row.DownloadCheck.length) {
                                     data = '<span onclick="DeleteStudentHW(' + row.Id + ')" class=\'dele\' style=\'pointer-events:auto;cursor:pointer;\'> <i class=\'fa fa-times text-danger actionbtn\'></i></span>  <span class=\'dele\' onclick="DownlaodHW(' + row.Id + ')" style=\'pointer-events:auto;color: blue;cursor:pointer;\'> <i class=\'fa fa-download text-edit actionbtn\'></i></span>';
                                     return data;
                                 }
                                
                                    
                                

                                data = '<span onclick="DeleteStudentHW(' + row.Id + ')" class=\'dele\' style=\'pointer-events:none\'> <i class=\'fa fa-times text-danger actionbtn\'></i></span>  <span class=\'dele\' onclick=DownlaodHW(' + row.Id + ') style=\'pointer-events:none\'> <i class=\'fa fa-download text-edit actionbtn\'></i></span > ';

                                return data;
                            }
                        },

                     ],
                    
                  
                    
                  
                });

               
            });


}

function ShowUploadHW() {
    
    AjaxCallWithOutStorage("/ViewAssignmentByTeacher?UserId=" + sessionStorage.getItem('userId'), "get").then(data => {
        row_count = data.length;
        $("#DataTables_Table_1").dataTable().fnDestroy();
            $('#DataTables_Table_1').DataTable({
              //  sDom: 'lrtip',
                //bLengthChange: false, //thought this line could hide the LengthMenu
               // bInfo: false,   
                stateSave: true,
                "bDestroy": true,
                data: data,
                columns: [
                    {
                    data: "ClassName"
                },
                { data: "SectionName" },
                { data: "Subject" },

                { data: "uploadOn" },
                { data: "LastUploadDate" },
                {
                    "data": "weblink",
                    "render": function (data, type, row, meta) {
                          var file = row.fileName.split(",");
                        var path = row.filepath.split(",");
                        var value = '';
                        for (var i = 0; i < row.fileName.split(",").length; i++) {

                           value  +='<a target="_blank" href="' + path[i] + '" > ' + file[i] + '</a > ' ;

                           
                        }
                        data = value;
                        return data;
                    }
                },
                {
                    "data": "weblink",
                    "render": function (data, type, row, meta) {
                        data = '<span onclick="EditHW(' + row.Id + ')" > <i class="fa fa-edit text-success actionbtn"></i></span>  <span onclick="DeleteHW(' + row.Id + ')" > <i class="fa fa-times text-danger actionbtn"></i></span>';
                       
                          

                        return data;
                    }
                },

                ]
                  
            });
        })

    
}

function CheckValue() {
    if ($("#txtclass").val().length == 0 && $("#txtsubject").val().length == 0 && $("#txtdate").val() == '') {
        ShowUploadHW();
    }
  
}

function DeleteHW(Id) {
   var con= confirm("Do you want to delete the Assignment");
    if (con) {
        document.getElementById("loaders").style.display = "block"
        var url = "/DeleteHW?FileId=" + Id
        AjaxCallWithOutStorage(url, "get").then(data => {
            ShowUploadHW();
            document.getElementById("loaders").style.display = "none"
        })

    }
}

function DeleteStudentHW(Id) {
    var con = confirm("Do you want to delete Students Assignment");
    if (con) {
        document.getElementById("loaders").style.display = "block"
        var url = "/DeleteHW?FileId=" + Id
        AjaxCallWithOutStorage(url, "get").then(data => {
            SearchViewHW();
            document.getElementById("loaders").style.display = "none"
        })

    }
}

function DeleteHWWithCheckBox()
{
    var flag = 0;
    document.getElementById("loaders").style.display = "block"
    
    $.each($("input[name='id']:checked"), function () {
        var url = "/DeleteHW?FileId=" + $(this).val()
        AjaxCallWithOutStorage(url, "get").then(data => { flag = 1; });
    });
    if (flag = 1)
        SearchViewHW();
    else
        alert("Please Select Any Row");
        document.getElementById("loaders").style.display = "none"
}

function EditHW(Id) {
   var url = "/EditHW?FileId=" + Id
   AjaxCallWithOutStorage(url, "get").then(data => {

       var url = "/GetSubjectBasedClassId?ClassId=" + data.classid;
  AjaxCallWithOutStorage(url, "get").then(d => {
           $('#txtSubject').empty();
           $('#txtSubject').append(new Option("Select Subject", 0));
           for (var i = 0; i < d.length; i++) {

               $('#txtSubject').append(new Option(d[i].Subject, d[i].Id));
           }

           var dat = DateFormat(data.uploadeOn);
           var lastdat = DateFormat(data.deadlineDate);

           $("#txtClass").val(data.classid);
           $("#txtSection").val(data.sectionId);
           $("#txtSubject").val(data.subjectId);
           $("#txtDate").val(dat);
           $("#txtLastDate").val(lastdat);

           $("#SaveBtt").replaceWith(' <span type="submit" class="btn-fill-lg btn-gradient-yellow btn-hover-bluedark" onclick="UploadHomework(1,' + Id + ')" id="SaveBtt">Update</span>');
           $("#Resetbtt").replaceWith(' <span type="reset" class="btn-fill-lg bg-blue-dark btn-hover-yellow" onclick="Reset()" id="Resetbtt">Cancel</span>');
           document.body.scrollTop = 0; // For Safari
           document.documentElement.scrollTop = 0; // For Chrome, Firefox, IE and Opera
           $(".dashboard-content-one").animate({ scrollTop: 0 }, "slow");
       });
        
        
    })
}

function DateFormat(date) {
    var d = new Date(date);
    var day = d.getDate() < 10 ? "0" + d.getDate() : d.getDate();
    var month = (d.getMonth() + 1) < 10 ? "0" + (d.getMonth() + 1) : (d.getMonth() + 1);
    var year = d.getFullYear();
    return year + "-" + month + "-" + day;
}
//function DownlaodHW(FilePath,FileName,Id) {
//    $('#notific_'+Id).attr('hidden', true);
//    var filepath = FilePath.split(',');
//    var filename = FileName.split(',');
//    var a = document.createElement('a');

//    for (var i = 0; i < filepath.length; i++) {

//        a.setAttribute('href', filepath[i]);
//        a.setAttribute('download', filename[i]);
//        a.setAttribute('target', "_blank");
//        a.style.display = 'none';
//        document.body.appendChild(a);
//        a.click();
//        document.body.removeChild(a);
       
//    }
//}

function DownlaodHW(Id)
{
    document.getElementById("loaders").style.display = "block"
    var url = "/DownloadHW?FileId=" + Id
    AjaxCallWithOutStorage(url, "get").then(data => {
        console.log(data);

        $('#notific_' + Id).attr('hidden', true);
     var a = document.createElement('a');

     for (var i = 0; i < data.length; i++) {

         a.setAttribute('href', data[i].filepath);
         a.setAttribute('download', data[i].fileName);
        a.setAttribute('target', "_blank");
        a.style.display = 'none';
        document.body.appendChild(a);
        a.click();
        document.body.removeChild(a);

     }
     document.getElementById("loaders").style.display = "none";
    });
}

function Validation() {
    var flag = false;
    var msg = "";
    if ($("#txtClass option:selected").val() == 0) {
        return "Please select Class";
    }
    if ($("#txtSection option:selected").val() == 0) {
        return "Please select Section";
    }
    if ($("#txtSubject option:selected").val() == 0) {
        return "Please select Subject";
    }
    if ($("#txtDate").val() == '') {
        return "Please select Upload Date";
    }
    if ($("#txtLastDate").val() == '') {
        return "Please select Last Submission Date";
    }
    if (document.getElementById("txtFile").files.length != 0 && document.getElementById("txtFile").files.length <= 3) {
        var flag = 0;
        for (var i = 0; i < document.getElementById("txtFile").files.length; i++) {
            var filename = document.getElementById("txtFile").files[i].name;
            var ext = filename.split(".").pop();
            if (ext == "jpg" || ext == "pdf" || ext == "mp4" || ext == "mp3" || ext == "png") {
                continue;
            }
            else {
                return "Please select correct format";
            }
        }
        return null
    }
    else
        return "Please select maximum 3 Files";

}

function Reset() {
    $("#txtClass").val(0);
    $("#txtSection").val(0);
    $("#txtSubject").val(0);
    $("#txtDate").val('');
    $("#txtLastDate").val('');
    $("#txtFile").val('');
    $("#SaveBtt").replaceWith(' <span type="submit" class="btn-fill-lg btn-gradient-yellow btn-hover-bluedark" onclick="UploadHomework(0,0)" id="SaveBtt">Save</span>');
    $("#Resetbtt").replaceWith(' <span type="reset" class="btn-fill-lg bg-blue-dark btn-hover-yellow" onclick="Reset()" id="Resetbtt">Reset</span>');

}
