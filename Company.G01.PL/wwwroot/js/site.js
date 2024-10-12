// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


let element = document.getElementById("id");

element.addEventListener("keyup", () =>{
    // send request to the back end

    // Creating our XMLHttpRequest  object
    let xhr = new XMLHttpRequest();

    let url = `https://localhost:44385/Employee/Index?InputSearch=${element.value}`;
    xhr.open("GET", url, true);

    // function execute after request is successful

    xhr.onreadystatechange = function () {
        if (th.readystate == 4 && this.status == 200) {
            console.log(this.responseText);
        }
    }
    // sending our request
    xhr.send();
});
