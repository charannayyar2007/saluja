
function autocomplete(value,type,id) {
    document.getElementById(id).innerHTML = '';
    //setting datalist empty at the start of function 
    //if we skip this step, same name will be repeated 

    var list = '';
    value.map((x) => {

        var ul = document.getElementById(id);
     //   list =`<li onClick(${x})>${x.FirstName+' ' +x.LastName}</li>`
        var li = document.createElement("li");
        li.setAttribute("class","list-group-item")
        if (type == "name") {
            x.LastName =   x.LastName != null ? x.LastName : '';
            li.appendChild(document.createTextNode(x.FirstName + ' ' + x.LastName));
            li.setAttribute("onclick", `selectStudent('${x.AdmissionId}','name','${x.FirstName + ' ' + x.LastName }')`);
        }
        if (type == "AdmissionId") {
            li.appendChild(document.createTextNode(x.AdmissionId));
            li.setAttribute("onclick", `selectStudent('${x.AdmissionId}','AdmissionId','${x.FirstName + ' ' + x.LastName }')`);
        }
        // added line
        ul.appendChild(li);

    })
    //input query length 
   
}