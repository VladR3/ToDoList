// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function FunctionDone() {
    var checkBoxes = document.querySelectorAll("#CheckNotDone");
    for (let i = 0; i < checkBoxes.length; i++) {
        if (checkBoxes[i].checked == true) {
            console.log(checkBoxes[i].className)
            document.location.href = 'https://localhost:5001/Task/Completed/?idTaskComplete=' + checkBoxes[i].className

        }
    }
}
function FunctionNotDone() {
    var checkBoxes = document.querySelectorAll("#CheckDone");
    for (let i = 0; i < checkBoxes.length; i++) {
        if (checkBoxes[i].checked == false) {
            console.log(checkBoxes[i].className)
            document.location.href = 'https://localhost:5001/Task/Completed/?idTaskComplete=' + checkBoxes[i].className

        }
    }
}
let dateInput = document.getElementById("date");
//dateInput.min = new Date().toISOString().split(".")[0];
//dateInput.value = new Date().toISOString().split(".")[0];
dateInput.min = new DateTime.now.strftime("%Y-%m-%d")
//let dateInputUpdate = document.getElementById("dateUpdate");


//function FunctionDone() {
//    var checkBox = document.getElementById("CheckNotDone");
//    if (checkBox.checked == true) {
//        console.log(checkBox.className)
//        document.location.href = 'https://localhost:5001/Task/Completed/?idTaskComplete=' + checkBox.className

//    }
//}