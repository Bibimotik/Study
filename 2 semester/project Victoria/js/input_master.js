//onsubmit="return CheckMaster();"
//method="get" action="/project/php/input_master.php"   pattern="+375([0-9]{2})[0-9]{3}-[0-9]{2}-[0-9]{2}"

function checkMaster() {
    console.log(Boolean(check(document.getElementsByName("telephone")[0], document.getElementsByName("password")[0])));
    return false;
}

async function check(telephone, password) {

    var params = new URLSearchParams("telephone=" + telephone.value + "&password=" + password.value);
    let response = await fetch("/project/php/get_master.php", { method: "POST", body: params });

    let result;
    if (response.ok) {
        result = await response.text();

        if (Number(result) <= 0) {
            console.log("Неверный логин или пароль");
        }

        return Number(result) > 0;
    } else {
        console.log("Ошибка HTTP: " + response.status);
    }
}