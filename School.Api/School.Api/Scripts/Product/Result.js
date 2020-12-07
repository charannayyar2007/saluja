var row_count = 0;
$(document).ready(function () {



document.querySelector("#UploadRslt").addEventListener('click', function () {
    
    $(".nav-link").removeClass("menu-active");
    $("#Rslt").addClass("menu-active");
    $(this).addClass("menu-active");
    var masters = AjaxCall("/GetAllMaster", "get", SessionKeys.masters);
    document.getElementById('dashboardContent').innerHTML = ''
    document.getElementById('GridColumn').innerHTML = ''
    document.getElementById('breadCrumTitle').innerHTML = "Upload Result"
    document.getElementById("loaders").style.display = "block";
    masters.then(x => {

        $.get("/Template/Result/ResultUpload.html", function (template) {
            window.location.hash = "UploadResult"
            $(template).tmpl(x).appendTo('#dashboardContent');

            //$.get("/Template/Teacher/ShowUploadHW.html", function (template) {
            //    document.getElementById('GridColumn').innerHTML = ''
            //    $(template).tmpl(x).appendTo('#GridColumn');


            //    ShowUploadHW();
            //    document.getElementById("loaders").style.display = "none";
            //});
            document.getElementById("loaders").style.display = "none";
        })


        if (x.SectionsMasters != null) {

        }
    })



    })

});