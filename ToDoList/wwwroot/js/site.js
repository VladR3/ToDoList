// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

    function myFunction() {
    var checkBox = document.getElementById("myCheck");
    if (checkBox.checked == true){
        //const target = 'https://localhost:5001/Task/Completed/'

        //target.searchParams.set('idTaskComplete', 1)
        console.log(checkBox.className)
    document.location.href = 'https://localhost:5001/Task/Completed/?idTaskComplete='+checkBox.className

    } 
    }
    let dateInput = document.getElementById("date");
dateInput.min = new Date().toISOString().split(".")[0];
dateInput.value = new Date().toISOString().split(".")[0];