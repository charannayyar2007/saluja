var ErrorModal;
var errormsg;
    function AjaxCall(url, methodTpe, itemType) {
        return new Promise(function (res, rej) { 
    $(document).ready(function () {
        if (sessionStorage.getItem(SessionKeys.token) == null) {
            window.location.href = "/";
        }

  
        if (sessionStorage.getItem(itemType) == null) {
            $.ajax({
                url: url,
                method: methodTpe,
                headers: {
                    'Authorization': 'Bearer '
                        + sessionStorage.getItem(SessionKeys.token)
                },
                success: function (data) {
                    sessionStorage.setItem(itemType, JSON.stringify(data));
                    res(data);
                },
                error: function (jQXHR) {
                    // If status code is 401, access token expired, so
                    // redirect the user to the login page
                    if (jQXHR.status == "401") {
                       
                    }
                    else {
                        rej(jqXHR.responseText);
                       
                    }
                }
            });
        }
        else {
            res(JSON.parse(sessionStorage.getItem(itemType)))
        }
    });
        })
   
}
function AjaxCallWithOutStorage(url, methodTpe) {
    return new Promise(function (res, rej) {
        $(document).ready(function () {
            if (sessionStorage.getItem(SessionKeys.token) == null) {
                window.location.href = "/";
            }

           

           
                $.ajax({
                    url: url,
                    method: methodTpe,
                    headers: {
                        'Authorization': 'Bearer '
                            + sessionStorage.getItem(SessionKeys.token)
                    },
                    success: function (data) {
                      
                        res(data);
                    },
                    error: function (jQXHR) {
                        // If status code is 401, access token expired, so
                        // redirect the user to the login page
                        if (jQXHR.status == "401") {
                            
                        }
                        else {
                            rej(jqXHR.responseText);
                           
                        }
                    }
                });
      
        });
    })

}

function AjaxCallWithOutStoragePost(url,dataval) {
    return new Promise(function (res, rej) {
        $(document).ready(function () {
            if (sessionStorage.getItem(SessionKeys.token) == null) {
                window.location.href = "/";
            }

          


            $.ajax({
                url: url,
                method: "Post",
                contentType: 'application/json',
                data: JSON.stringify(dataval),
                headers: {
                    'Authorization': 'Bearer '
                        + sessionStorage.getItem(SessionKeys.token)
                },
                success: function (data) {

                    res(data);
                },
                error: function (jQXHR) {
                    // If status code is 401, access token expired, so
                    // redirect the user to the login page
                    if (jQXHR.status == "401") {
                      
                    }
                    else {
                        rej(jqXHR.responseText);
                       
                    }
                }
            });

        });
    })

}

function AjaxCallWithOutStoragePostFile(url, dataval) {
    return new Promise(function (res, rej) {
        $(document).ready(function () {
            if (sessionStorage.getItem(SessionKeys.token) == null) {
                window.location.href = "/";
            }




            $.ajax({
                url: url,
                method: "Post",
                contentType: false,
                processData: false,
                data: dataval,
                headers: {
                    'Authorization': 'Bearer '
                        + sessionStorage.getItem(SessionKeys.token)
                },
                success: function (data) {

                    res(data);
                },
                error: function (jQXHR) {
                    // If status code is 401, access token expired, so
                    // redirect the user to the login page
                    if (jQXHR.status == "401") {

                    }
                    else {
                        rej(jqXHR.responseText);

                    }
                }
            });

        });
    })

}


/// popup windo
$(document).ready(function () {
    ErrorModal = document.getElementById("error");
 
    // Get the button that opens the modal
    errormsg = document.getElementById("ModelError");
    // Get the <span> element that closes the modal
    var span = document.getElementsByClassName("close")[0];

    // When the user clicks the button, open the modal 

    // When the user clicks on <span> (x), close the modal
    span.onclick = function () {
        ErrorModal.style.display = "none";
    }

    window.onclick = function (event) {
        if (event.target == ErrorModal) {
            ErrorModal.style.display = "none";
        }
        if (document.getElementById("namedatalist")!=null&&!document.getElementById("namedatalist").contains(event.target)) {
            $("#namedatalist").css("display", "none");
        }
        if (document.getElementById("Erndatalist") != null &&!document.getElementById("Erndatalist").contains(event.target)) {
            $("#Erndatalist").css("display", "none");
        }
       
    }
    // When the user clicks anywhere outside of the modal, close it
    
});
