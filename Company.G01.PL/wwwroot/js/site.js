// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function checkPasswordStrength() {
    const password = document.querySelector('input[name="password"]').value;
    const minLength = document.getElementById('min-length');
    const uppercase = document.getElementById('uppercase');
    const lowercase = document.getElementById('lowercase');
    const number = document.getElementById('number');
    const specialChar = document.getElementById('special-char');
    const strengthBar = document.getElementById('password-strength');
    const strengthText = document.getElementById('strength-text');
    const requirementsList = document.getElementById('password-requirements');
    if (password.length > 0) {
        requirementsList.style.display = "block";
    } else {
        requirementsList.style.display = "none";
    }


    let strength = 0;
    if (password.length >= 8) strength++; // طول مناسب
    if (/[A-Z]/.test(password)) strength++; // حروف كبيرة
    if (/[a-z]/.test(password)) strength++; // حروف صغيرة
    if (/[0-9]/.test(password)) strength++; // أرقام
    if (/[^A-Za-z0-9]/.test(password)) strength++; // رموز خاصة

    // تحديث الشريط والنص بناءً على القوة
    if (strength <= 2) {
        strengthBar.className = "strength-bar low";
        strengthText.textContent = "Weak";
    } else if (strength <= 4) {
        strengthBar.className = "strength-bar medium";
        strengthText.textContent = "Medium";
    } else {
        strengthBar.className = "strength-bar high";
        strengthText.textContent = "Strong";
    }
}


function checkPasswordStrength() {
    const password = document.querySelector('input[name="password"]').value;
    const strengthBar = document.getElementById('password-strength');
    const strengthText = document.getElementById('strength-text');


    let strength = 0;
    if (password.length >= 8) {
        strength++;
        document.getElementById('min-length').classList.remove('invalid');
        document.getElementById('min-length').classList.add('valid');
    } else {
        document.getElementById('min-length').classList.remove('valid');
        document.getElementById('min-length').classList.add('invalid');
    }

    if (/[A-Z]/.test(password)) {
        strength++;
        document.getElementById('uppercase').classList.remove('invalid');
        document.getElementById('uppercase').classList.add('valid');
    } else {
        document.getElementById('uppercase').classList.remove('valid');
        document.getElementById('uppercase').classList.add('invalid');
    }

    if (/[a-z]/.test(password)) {
        strength++;
        document.getElementById('lowercase').classList.remove('invalid');
        document.getElementById('lowercase').classList.add('valid');
    } else {
        document.getElementById('lowercase').classList.remove('valid');
        document.getElementById('lowercase').classList.add('invalid');
    }

    if (/[0-9]/.test(password)) {
        strength++;
        document.getElementById('number').classList.remove('invalid');
        document.getElementById('number').classList.add('valid');
    } else {
        document.getElementById('number').classList.remove('valid');
        document.getElementById('number').classList.add('invalid');
    }

    if (/[^A-Za-z0-9]/.test(password)) {
        strength++;
        document.getElementById('special-char').classList.remove('invalid');
        document.getElementById('special-char').classList.add('valid');
    } else {
        document.getElementById('special-char').classList.remove('valid');
        document.getElementById('special-char').classList.add('invalid');
    }

    // تحديث شريط القوة والنص بناءً على القوة
    if (strength <= 2) {
        strengthBar.className = "strength-bar low";
        strengthText.textContent = "Weak";
    } else if (strength <= 4) {
        strengthBar.className = "strength-bar medium";
        strengthText.textContent = "Medium";
    } else {
        strengthBar.className = "strength-bar high";
        strengthText.textContent = "Strong";
    }
}

//let element = document.getElementById("id");

//element.addEventListener("keyup", () => {
    
//    let xhr = new XMLHttpRequest();

//    let url = `https://localhost:44385/Employee/Index?InputSearch=${element.value}`;
//    xhr.open("GET", url, true);

//    xhr.onreadystatechange = function () {
//        if (this.readyState == 4 && this.status == 200) {
//            console.log(this.responseText);
//        }
//    }
    
//    xhr.send();
//});
