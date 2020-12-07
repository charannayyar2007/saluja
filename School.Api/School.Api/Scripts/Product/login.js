$(document).ready(function () {
    var modal = document.getElementById("error");
    $('#linkClose').click(function () {
        $('#divError').hide('fade');
    });
   
    $('#btnLogin').click(function () {
        $.ajax({
            // Post username, password & the grant type to /token
            url: '/Login',
            method: 'POST',
            contentType: 'application/json',
            data: JSON.stringify({
                UserId: $('#txtUsername').val(),
                Password: $('#txtPassword').val(),
            }),
            // When the request completes successfully, save the
            // access token in the browser session storage and
            // redirect the user to Data.html page. We do not have
            // this page yet. So please add it to the
            // EmployeeService project before running it
            success: function (response) {
                sessionStorage.setItem("accessToken", response.token);
                sessionStorage.setItem("userId", response.userId);
                sessionStorage.setItem("roleId", response.roleId);
                if (response.roleId == "admin") {
                    $.ajax({
                    // Post username, password & the grant type to /token
                        url: '/Admin/index',
                    method: 'POST',
                    contentType: 'application/json',
                    data: JSON.stringify({
                        token: response.token
                    }),
                        success: function (response) {
                            window.location.href = "/Admin/index";
                        }
                })
       
                   
                }
                else if (response.roleId == "teacher") {

                    window.location.href = "/Teacher/index";
                }

            },
            // Display errors if any in the Bootstrap alert <div>
            error: function (jqXHR) {
               
                var span = document.getElementsByClassName("close")[0];
                modal.style.display = "block";
                span.onclick = function () {
                    modal.style.display = "none";
                }
                $('#divErrorText').text(jqXHR.responseText);
                $('#errormsg').show('fade');
            }
        });
    });

    /// sent opt

    $('#sendOtp').click(function () {
        $.ajax({
            // Post username, password & the grant type to /token
            url: '/ForgotPassword',
            method: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(
                $('#txtUsernameotp').val()
            ),
            // When the request completes successfully, save the
            // access token in the browser session storage and
            // redirect the user to Data.html page. We do not have
            // this page yet. So please add it to the RestPassword
            // EmployeeService project before running it
            success: function (response) {
                alert(response)
            },
            // Display errors if any in the Bootstrap alert <div>
            error: function (jqXHR) {
                alert(jqXHR.responseJSON.Message)
                var span = document.getElementsByClassName("close")[0];
                modal.style.display = "block";
                span.onclick = function () {
                    modal.style.display = "none";
                }
                $('#divErrorText').text(jqXHR.responseText);
                $('#errormsg').show('fade');
            }
        });
    });
    ///
    //// 
    $('#RestPassword').click(function () {
        $.ajax({
            // Post username, password & the grant type to /token
            url: '/ResetPassword',
            method: 'POST',
            contentType: 'application/json',
            data: JSON.stringify({
                UserId: $('#txtUsernameotp').val(),
                Password: $('#txtPasswordFogot').val(),
                Repassword: $('#txtPasswordFogot').val(),
                Token: $('#txtOPT').val()
            }
              
            ),
            // When the request completes successfully, save the
            // access token in the browser session storage and
            // redirect the user to Data.html page. We do not have
            // this page yet. So please add it to the RestPassword
            // EmployeeService project before running it
            success: function (response) {
                alert(response)
            },
            // Display errors if any in the Bootstrap alert <div>
            error: function (jqXHR) {
                alert(jqXHR.responseJSON.Message)
                var span = document.getElementsByClassName("close")[0];
                modal.style.display = "block";
                span.onclick = function () {
                    modal.style.display = "none";
                }
                $('#divErrorText').text(jqXHR.responseText);
                $('#errormsg').show('fade');
            }
        });
    });
    /// 

    window.onclick = function (event) {
        if (event.target == modal) {
            modal.style.display = "none";
        }
    }

    document.getElementById('forgotpassword').addEventListener('click', function () {
        document.getElementById('forgotpaswordpage').style.display = "block";
        document.getElementById('loginpage').style.display = "none";
        
    })
    $('#forgotsub').click(function () {
        $.ajax({
            // Post username, password & the grant type to /token
            url: '/ForgotPassword?Username=' + $('#txtfUsername').val(),
            method: 'GET',
          
          
            success: function (response) {

                document.getElementById('urlmsg').innerHTML='Password rest link has been sent on register mail or phone number'
            },
            // Display errors if any in the Bootstrap alert <div>
            error: function (jqXHR) {
                alert(jqXHR.responseJSON.Message)
                var span = document.getElementsByClassName("close")[0];
                modal.style.display = "block";
                span.onclick = function () {
                    modal.style.display = "none";
                }
                $('#divErrorText').text(jqXHR.responseText);
                $('#errormsg').show('fade');
            }
        });
    });
});

