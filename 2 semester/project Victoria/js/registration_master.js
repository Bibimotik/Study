/*function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            let image = document.getElementById("image");
            image.setAttribute('src', e.target.result);
            image.setAttribute('width', 100);
        }

        reader.readAsDataURL(input.files[0]);
    }
}

document.getElementById("input_image").onchange = function () {
    readURL(this);
};*/

var wrong_password = document.getElementById("wrong_password");
wrong_password.setAttribute("style", "display: none;");
function PasswordCheck() {
    let result = document.getElementsByName("password")[0].value === document.getElementsByName("check_password")[0].value;

    if (result === false) {
        wrong_password.setAttribute("style", " ");
    }

    return result;
}