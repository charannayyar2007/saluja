var admissionId = "";

$(document).ready(function () {

    userName = sessionStorage.getItem("userId")
    //var data = common.AjaxCall("/Getnav?userId=" + userName,"get");
    var teacherDetail = AjaxCall("/GetTeacherDetail", "get", SessionKeys.teacherDetail);


    teacherDetail.then(x => {
        userNameHead
        document.getElementById("userNameHead").innerHTML = x.FirstName + ' ' + x.Lastname;
        document.getElementById("userName").innerHTML = x.FirstName + ' ' + x.Lastname;
        document.getElementById("roleType").innerHTML = "Teacher";
    })
    // Dashboard call
    window.location.hash=""
    Dashboard();
    document.querySelector("#TeacherDashboard").addEventListener('click', function () {
        $(".nav-link").removeClass("menu-active");
        $(".nav-item").removeClass("active");
        $(this).addClass("menu-active");
        document.getElementById('dashboardContent').innerHTML = ''
        document.getElementById('GridColumn').innerHTML = ''
        $.get("/Template/Teacher/TeacherDashboard.html", function (template)
        {
            window.location.hash = ""
            // common.TemplateApp(template, oEngineers, 'temwrapper');
            $(template).tmpl().appendTo('#dashboardContent');
        })
    })
    /// mark Attendance 
    document.querySelector("#MarkeAtt").addEventListener('click', function () {
        $(".nav-link").removeClass("menu-active");
        
        $("#Att").addClass("menu-active");
        $(this).addClass("menu-active");
        MarkAttendance();
    })
    // view Attendance

    document.querySelector("#ViewAtt").addEventListener('click', function () {
        $(".nav-link").removeClass("menu-active");
        $("#Att").addClass("menu-active");
        $(this).addClass("menu-active");
        ViewAttendanceForm();
    })
    /// student list using data table   <h3>Student Management</h3>
    document.querySelector("#StudentView").addEventListener("click", function () {
        $(".nav-link").removeClass("menu-active");
        $(".nav-item").removeClass("active");
        $(".sub-group-menu").css("display", "none");
        $(this).addClass("menu-active");

        GetAllStudent();
    })
    //Upload Homework
    document.querySelector("#UploadHW").addEventListener('click', function () {
        $(".nav-link").removeClass("menu-active");
        $("#HW").addClass("menu-active");
        $(this).addClass("menu-active");
        var masters = AjaxCall("/GetAllMaster", "get", SessionKeys.masters);
        document.getElementById('dashboardContent').innerHTML = ''
        document.getElementById('GridColumn').innerHTML = ''
        document.getElementById('breadCrumTitle').innerHTML = "Upload Homework"
        document.getElementById("loaders").style.display = "block";
        masters.then(x => {

            $.get("/Template/Teacher/HomeworkUpload.html", function (template) {
                window.location.hash = "UploadHomework"
                $(template).tmpl(x).appendTo('#dashboardContent');

                $.get("/Template/Teacher/ShowUploadHW.html", function (template) {
                    document.getElementById('GridColumn').innerHTML = ''
                    $(template).tmpl(x).appendTo('#GridColumn');


                    ShowUploadHW();
                    document.getElementById("loaders").style.display = "none";
                });

            })
           

            if (x.SectionsMasters != null) {

            }
        })
  
   

    })

    //View Homework
    document.querySelector("#ViewHW").addEventListener('click', function () {
        $(".nav-link").removeClass("menu-active");
        $("#HW").addClass("menu-active");
        $(this).addClass("menu-active");
        var masters = AjaxCall("/GetAllMaster", "get", SessionKeys.masters);
        document.getElementById('dashboardContent').innerHTML = ''
        document.getElementById('GridColumn').innerHTML = ''
        document.getElementById('breadCrumTitle').innerHTML = "View Homework"
       
        masters.then(x => {
            $.get("/Template/Teacher/ViewHomework.html", function (template) {
                window.location.hash = "ViewHomework"
                // common.TemplateApp(template, oEngineers, 'temwrapper');
                $(template).tmpl(x).appendTo('#dashboardContent');
                document.getElementById("loaders").style.display = "block"

          var url = "/ViewAllAssignment"
                CommonSearchViewHW(url);
                document.getElementById("loaders").style.display = "none"
        })
        })
    })
    // onchage 
    if (document.querySelector("#NameAuto") != null) {
        document.querySelector("#NameAuto").addEventListener('onchange', function () {
            var txtvalue = document.getElementById("NameAuto").value;
            if (txtvalue == "") {
                var txtvalue = document.getElementById("enrollmentId").value = "";
            }
        })
    }

    if (document.querySelector("#enrollmentId") != null) {
        document.querySelector("#enrollmentId").addEventListener('onchange', function () {
            var txtvalue = document.getElementById("enrollmentId").value;
            if (txtvalue == "") {
                var txtvalue = document.getElementById("NameAuto").value = "";
            }
        })
    }
});
// GetStudent by name AutoSearch
function Export() {
    $("input[name='GridHtml']").val($("#Grid").html());
    var data = $("input[name='GridHtml']").val();
    AjaxCallWithOutStorage(url, "get").then(att => {
        $.ajax({
            // Post username, password & the grant type to /token
            url: '/Teacher/PrintPDF',
            method: 'POST',
            contentType: 'application/json',
            data: JSON.stringify({
                token: att
            }),
            success: function (response) {

            }
        })
    })
    //AjaxCallWithOutStoragePost('/Teacher/Export', data).then(x => {
    //    console.log("sdd")
    //})
}
function GetStudent() {
    var typesvalues = document.getElementById("NameAuto").value;
    var selectedCls = document.getElementById("clsList").value;
    var selectedSec = document.getElementById("sectionlist").value;
    
    if (selectedCls == "0") { ErrorModal.style.display = "block"; errormsg.innerHTML = "please select class"; return }
    if (selectedSec == "0") { ErrorModal.style.display = "block"; errormsg.innerHTML = "please select section"; return }
   
        var url = "/GetStudentbyName?classId=" + parseInt(selectedCls) + "&sectionId=" + parseInt(selectedSec) + "&name=" + typesvalues
        AjaxCallWithOutStorage(url, "get").then(x => {
            $("#namedatalist").css("display", "block");
            autocomplete(x, "name", "namedatalist");

        })

}
// GetStudent by AdmissionId AutoSearch
function GetStudentByAppId() {
    var selectedCls = document.getElementById("clsList").value;
    var selectedSec = document.getElementById("sectionlist").value;

    if (selectedCls == "0") { ErrorModal.style.display = "block"; errormsg.innerHTML = "please select class"; return }
    if (selectedSec == "0") { ErrorModal.style.display = "block"; errormsg.innerHTML = "please select section"; return }

    var typesvalues = document.getElementById("enrollmentId").value;
    var url = "/GetStudentbyAdmissionId?AddmistionId=" + typesvalues +""
    AjaxCallWithOutStorage(url, "get").then(x => {
        $("#Erndatalist").css("display", "block");
        var list = x.filter(function (x) { return x.ClassId == selectedCls && x.SectionId == selectedSec })
        autocomplete(list, "AdmissionId", "Erndatalist");

    })

}
function selectStudent(appId,type,name) {
    if (type == "name") {
        document.getElementById("NameAuto").value = name;
        document.getElementById("enrollmentId").value = appId;
        $("#namedatalist").css("display", "none");
    }
    if (type == "AdmissionId") {
        document.getElementById("enrollmentId").value = appId;
        document.getElementById("NameAuto").value = name;
        $("#Erndatalist").css("display", "none");
    }
    admissionId = appId;
   
}

// save student adendence 
function MarkAttendace() {
    var error = document.getElementById("error");
    var markType = document.getElementById("attType").value;
    var selectedCls = document.getElementById("clsList").value;
    var selectedSec = document.getElementById("sectionlist").value;

    if (selectedCls == "0") { ErrorModal.style.display = "block"; errormsg.innerHTML = "please select class"; return }
    if (selectedSec == "0") { ErrorModal.style.display = "block"; errormsg.innerHTML = "please select section"; return }
    if (admissionId == "") { ErrorModal.style.display = "block"; errormsg.innerHTML = "please select student Name or AdmissionId "; return }
    if (markType == "0") { ErrorModal.style.display = "block"; errormsg.innerHTML = "please select Attendance status "; return } 
    data = { EnrollmentId: admissionId, MarkeType: markType}
    AjaxCallWithOutStoragePost('/MarkAttendace', data).then(x => {

        if (x.AdmissionId != null) {
            GetAllStudentByClassAndSection();
            return
        }
        if (x.ErrorMsg != null)
        ErrorModal.style.display = "block"; errormsg.innerHTML = x.ErrorMsg; 
       
        
    })
    
}

//Get All StudentList by Class and section there Attendace status
function GetAllStudentByClassAndSection() {
    var selectedCls = document.getElementById("clsList").value;
    var selectedSec = document.getElementById("sectionlist").value;

    if (selectedCls == "0") { ErrorModal.style.display = "block"; errormsg.innerHTML = "please select class"; return }
    if (selectedSec == "0") { ErrorModal.style.display = "block"; errormsg.innerHTML = "please select section"; return }

    var url = "/ViewAttendaceByClass?classId=" + parseInt(selectedCls) + "&sectionId=" + parseInt(selectedSec)
    AjaxCallWithOutStorage(url, "get").then(data => {
      
        $.get("/Template/Teacher/MarkStudent.html", function (template) {
            document.getElementById('GridColumn').innerHTML = ''
            // common.TemplateApp(template, oEngineers, 'temwrapper');
            var className = $("#clsList option:selected").text();
            var SectionName = $("#sectionlist option:selected").text();
            $(template).tmpl().appendTo('#GridColumn');
            var today = new Date();
            var date = today.getDate() + '-' + (today.getMonth() + 1) + '-' + today.getFullYear();
            document.getElementById('headforMarkAtt').innerHTML = `Class : ${className} Section : ${SectionName} Date:${date}`
            headforMarkAtt
            setTimeout(function () {
                $('#DataTables_Table_StudentList').DataTable({
                    stateSave: true,
                    "bDestroy": true,
                    data: data[0],
                    "createdRow": function (row, data, dataIndex) {
                        if (data["PRESENT_STATUS"] == "A") {
                            $('td', row).css('background-color', 'Red');
                            $('td', row).css('color', 'white')
                        }
                    },
                    columns: [{
                        data: "admissionId"
                    },
                        { data: "NAME" },
                    { data: "RollNo" },
                    { data: "PRESENT_STATUS" }
                    
                    ]
                }
                   
                );
                document.getElementById("loaders").style.display = "none"
            }, 200)
            return;


            //    autocomplete(document.getElementById("NameAuto"), countries);
        })

    })
}
/// get student Attendace 
//function ViewAttendace() {
//    var error = document.getElementById("error");
//    var year = document.getElementById("sessionSelect").value;
//    var month = document.getElementById("MonthSelect").value;
//    var classId = document.getElementById("classSelect").value;
//    var secationid = document.getElementById("sectionSelect").value;
   
//    if (classId == "0") { ErrorModal.style.display = "block"; errormsg.innerHTML = "please select class "; return }
//    if (secationid == "0") { ErrorModal.style.display = "block"; errormsg.innerHTML = "please select section "; return }
//    if (month == "0") { ErrorModal.style.display = "block"; errormsg.innerHTML = "please select Month "; return }
//    if (year == "0") { ErrorModal.style.display = "block"; errormsg.innerHTML = "please select session "; return }
    
 
 
//    var url = "/ViewAttendace?classId=" + parseInt(classId) + "&sectionId=" + parseInt(secationid) + "&month=" + parseInt(month) + "&sessionYear=" + year
//    AjaxCallWithOutStorage(url, "get").then(att => {
        
//        $.get("/Template/Teacher/ViewAttendaceGrid.html", function (template) {
//            // common.TemplateApp(template, oEngineers, 'temwrapper');
//            document.getElementById('GridColumn').innerHTML = ''
//            $(template).tmpl(att).appendTo('#GridColumn');
//            if (att[0].length >= 1) {
//                var className = $("#classSelect option:selected").text();
//                var SectionName = $("#sectionSelect option:selected").text();
//                var MonthName = $("#MonthSelect option:selected").text();
//                var Yearname = '';
//                if (year != null) {
//                    Yearname = year.split('-');
//                    if (parseInt(month) <= 3) {
//                        Yearname = Yearname[1]
//                    }
//                    else {
//                        Yearname = Yearname[0];
//                    }
//                }
//                var attHeading = document.getElementById('attHeading');
//                if (attHeading != null) {
//                    document.getElementById('attHeading').innerHTML = `Attendence Sheet Of Class ${className} : Section ${SectionName}  ${MonthName} ${Yearname}`
//                }
//                var header = '<th class="text-left">Students Name</th><th class="text-left">AdmissionId</th> <th class="text-left">RollNo</th>';
//                for (var i = 1; i <= Object.keys(att[0][0]).length - 3; i++) {
//                    header = header + `<th>${i}</th>`
//                    document.getElementById('head').innerHTML = header
//                }
//                att.map((x => {
//                    var k = 1;
//                    var values = '';
//                    x.forEach((item) => {

//                        values = values + `<tr id=${"std" + k}></tr>`;
//                        document.getElementById('innervalue').innerHTML += values
//                        var tdvalue = `<td class="text-left">${item['Name']}</td>
//                                        <td class="text-left">${item['admissionId']}</td>
//                                        <td class="text-left">${item['RollNo']}</td>`;
//                        var tdhtml = '';
//                        for (var i in item) {
//                            if (item[i].trim() !== "Name") {
//                                if (item[i] == "P") {
//                                    tdhtml = tdhtml + '<td><i class="fas fa-check text-success"></i></td>'


//                                }
//                                if (item[i] == 'A') {
//                                    tdhtml = tdhtml + '<td><i class="fas fa-times text-danger"></i></td>'

//                                }
//                                if (item[i] == 'S') {
//                                    tdhtml = tdhtml + '<td>S</td>'

//                                }
//                                if (item[i] == 'H') {
//                                    tdhtml = tdhtml + '<td>H</td>'

//                                }

//                            }
//                        }
//                        document.getElementById('std' + k).innerHTML += tdvalue + tdhtml
//                        k++;
//                    })
//                }))
//            }
//            else {
//                if (classId == "0") { ErrorModal.style.display = "block"; errormsg.innerHTML = "Student Record not found!!"; return }
//            }
         
//            //    autocomplete(document.getElementById("NameAuto"), countries);
//        })
        
//    })

//}
// form Logic for Attendace

function MarkAttendance() {
    var masters = AjaxCall("/GetAllMaster", "get", SessionKeys.masters);
    document.getElementById('dashboardContent').innerHTML = ''
    document.getElementById('breadCrumTitle').innerHTML = "Mark Student Attendance"


    masters.then(x => {
        document.getElementById('dashboardContent').innerHTML = ''
        document.getElementById('GridColumn').innerHTML = ''
        $.get("/Template/Teacher/MarkAttendance.html", function (template) {
            window.location.hash ="MarkAttendance"
            // common.TemplateApp(template, oEngineers, 'temwrapper');
            $(template).tmpl(x).appendTo('#dashboardContent');

        })
    })
}



function GetAllStudent() {
    document.getElementById('dashboardContent').innerHTML = ''
    document.getElementById('GridColumn').innerHTML = ''
    document.getElementById('breadCrumTitle').innerHTML = "Student Management"
    $.get("/Template/student/StudentList.html", function (template) {
        document.getElementById('dashboardContent').innerHTML = ''
        document.getElementById('GridColumn').innerHTML = ''
        // common.TemplateApp(template, oEngineers, 'temwrapper');
        window.location.hash = "StudentList"
        $(template).tmpl().appendTo('#dashboardContent');
        document.getElementById("loaders").style.display = "block"
        AjaxCallWithOutStorage("/GetAllStudent?start=1&end=300", "get").then(data => {
            setTimeout(function () { 
            $('#DataTables_Table_0').DataTable({
                stateSave: true,
                "bDestroy": true,
                data: data,
                columns: [{
                    data: "AdmissionId"
                },
                { data: "FirstName" },
                { data: "LastName" },
                { data: "Gender" },
                { data: "Dob" },
                { data: "FatherName" },
                { data: "MotherName" },
                { data: "ClassName" },
                { data: "Section" },
                { data: "RollNo" },
                ]
            });
                document.getElementById("loaders").style.display = "none"
            },200)
        });
    })
}
// Rest Logic



/// rest logic for mark att form 
function RestmarkAttendace() {
    var masters = AjaxCall("/GetAllMaster", "get", SessionKeys.masters);
    document.getElementById('dashboardContent').innerHTML = ''
    document.getElementById('GridColumn').innerHTML = ''
    document.getElementById('breadCrumTitle').innerHTML = "Mark Student Attendance"


    masters.then(x => {
        document.getElementById('dashboardContent').innerHTML = ''
        $.get("/Template/Teacher/MarkAttendance.html", function (template) {
            // common.TemplateApp(template, oEngineers, 'temwrapper');
        
            $(template).tmpl(x).appendTo('#dashboardContent');

        })
    })
}
/// Dashboard 
function Dashboard() {
    $.get("/Template/Teacher/TeacherDashboard.html", function (template) {
        // common.TemplateApp(template, oEngineers, 'temwrapper');
        $(template).tmpl().appendTo('#dashboardContent');
    })
}
window.onhashchange = function () {
    //blah blah blah
    var hasSet = this.window.location.hash;
    if (hasSet =='#StudentList') {
        GetAllStudent();
        return
    }
    if (hasSet == '#ViewAttendance') {
        ViewAttendanceForm();
        return
    }
    if (hasSet == '#MarkAttendance') {
        MarkAttendance();
        return
    } if (hasSet == "") {
        window.location.reload()

    }
 
}
