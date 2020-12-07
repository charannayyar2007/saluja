function ViewAttendace() {
    var error = document.getElementById("error");
    var year = document.getElementById("sessionSelect").value;
    var month = document.getElementById("MonthSelect").value;
    var classId = document.getElementById("classSelect").value;
    var secationid = document.getElementById("sectionSelect").value;

    if (classId == "0") { ErrorModal.style.display = "block"; errormsg.innerHTML = "please select class "; return }
    if (secationid == "0") { ErrorModal.style.display = "block"; errormsg.innerHTML = "please select section "; return }
    if (month == "0") { ErrorModal.style.display = "block"; errormsg.innerHTML = "please select Month "; return }
    if (year == "0") { ErrorModal.style.display = "block"; errormsg.innerHTML = "please select session "; return }



    var url = "/ViewAttendace?classId=" + parseInt(classId) + "&sectionId=" + parseInt(secationid) + "&month=" + parseInt(month) + "&sessionYear=" + year
    AjaxCallWithOutStorage(url, "get").then(att => {

        $.get("/Template/Teacher/ViewAttendaceGrid.html", function (template) {
            // common.TemplateApp(template, oEngineers, 'temwrapper');
            document.getElementById('GridColumn').innerHTML = ''
            $(template).tmpl(att).appendTo('#GridColumn');
            if (att[0].length >= 1) {
                var className = $("#classSelect option:selected").text();
                var SectionName = $("#sectionSelect option:selected").text();
                var MonthName = $("#MonthSelect option:selected").text();
                var Yearname = '';
                if (year != null) {
                    Yearname = year.split('-');
                    if (parseInt(month) <= 3) {
                        Yearname = Yearname[1]
                    }
                    else {
                        Yearname = Yearname[0];
                    }
                }
                var attHeading = document.getElementById('attHeading');
                if (attHeading != null) {
                    document.getElementById('attHeading').innerHTML = `Attendence Sheet Of Class ${className} : Section ${SectionName}  ${MonthName} ${Yearname}`
                }
                var header = '<th class="text-left">Students Name</th><th class="text-left">AdmissionId</th> <th class="text-left">RollNo</th><th class="text-left">Total_Present</th><th class="text-left">Total_Absent</th>';
                for (var i = 1; i <= Object.keys(att[0][0]).length - 5; i++) {
                    header = header + `<th>${i}</th>`
                    document.getElementById('head').innerHTML = header
                }
                att.map((x => {
                    var k = 1;
                    var values = '';
                    x.forEach((item) => {

                        values = values + `<tr id=${"std" + k}></tr>`;
                        document.getElementById('innervalue').innerHTML += values
                        var tdvalue = `<td class="text-left">${item['Name']}</td>
                                        <td class="text-left">${item['admissionId']}</td>
                                        <td class="text-left">${item['RollNo']}</td>
  <td class="text-left">${item['Total_Present']}</td>
  <td class="text-left">${item['Total_Absent']}</td>`;

                        var tdhtml = '';
                        for (var i in item) {
                            if (item[i].trim() !== "Name") {
                                if (item[i] == "P") {
                                    tdhtml = tdhtml + '<td><i class="fa fa-check text-success"></i></td>'

                                }
                                if (item[i] == 'A') {
                                    tdhtml = tdhtml + '<td><i class="fa fa-times text-danger"></i></td>'
                                }
                                if (item[i] == 'S') {
                                    tdhtml = tdhtml + '<td>S</td>'

                                }
                                if (item[i] == 'H') {
                                    tdhtml = tdhtml + '<td>H</td>'

                                }

                            }
                        }
                        document.getElementById('std' + k).innerHTML += tdvalue + tdhtml
                        k++;
                    })
                }))
            }
            else {
                if (classId == "0") { ErrorModal.style.display = "block"; errormsg.innerHTML = "Student Record not found!!"; return }
            }

            //    autocomplete(document.getElementById("NameAuto"), countries);
        })

    })

}

function ViewAttendanceForm() {
    var masters = AjaxCall("/GetAllMaster", "get", SessionKeys.masters);
    document.getElementById('dashboardContent').innerHTML = ''
    document.getElementById('GridColumn').innerHTML = ''
    document.getElementById('breadCrumTitle').innerHTML = "View Student Attendance"

    masters.then(x => {
        $.get("/Template/Teacher/ViewAttendance.html", function (template) {
            window.location.hash = "ViewAttendance"
            // common.TemplateApp(template, oEngineers, 'temwrapper');
            $(template).tmpl(x).appendTo('#dashboardContent');
        })
    })
}

function RestViewAttendace() {
    var masters = AjaxCall("/GetAllMaster", "get", SessionKeys.masters);
    document.getElementById('dashboardContent').innerHTML = ''
    document.getElementById('GridColumn').innerHTML = ''
    document.getElementById('breadCrumTitle').innerHTML = "View Student Attendance"

    masters.then(x => {
        $.get("/Template/Teacher/ViewAttendance.html", function (template) {

            $(template).tmpl(x).appendTo('#dashboardContent');
        })
    })
}