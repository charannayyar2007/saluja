$(document).ready(function () {

   
    document.getElementById('addTeacher').addEventListener('click', function () {
        var masters = AjaxCall("/GetAllMaster", "get", SessionKeys.masters);
        document.getElementById('dashboardContent').innerHTML = ''
        document.getElementById('breadCrumTitle').innerHTML = "Create Teacher"


        masters.then(x => {
            document.getElementById('dashboardContent').innerHTML = ''
            document.getElementById('GridColumn').innerHTML = ''
            $.get("/Template/Admin/Teacher/CreateTeacherTemp.html", function (template) {
                // common.TemplateApp(template, oEngineers, 'temwrapper');
                $(template).tmpl(x).appendTo('#dashboardContent');
            })
        })
    })
    document.getElementById('addAccount').addEventListener('click', function () {

        $.get("/Template/Admin/Account/AccountTemplate.html", function (template) {
            // common.TemplateApp(template, oEngineers, 'temwrapper');
            $(template).tmpl().appendTo('#dashboardContent');
        })
    });
  
});