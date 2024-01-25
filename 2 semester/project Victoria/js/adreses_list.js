async function fillAdresSelect(file_name, id_name, base_text) {
    let response = await fetch("/project/php/" + file_name + "?base_ansver=" + base_text, { method: "GET" });

    let result;
    if (response.ok) {
        result = await response.text();
    } else {
        console.log("Ошибка HTTP: " + response.status);
    }

    document.getElementById(id_name).innerHTML = result;
}

var months = [
   'Январь',
   'Февраль',
   'Март',
   'Апрель',
   'Май',
   'Июнь',
   'Июль',
   'Август',
   'Сентябрь',
   'Октябрь',
   'Ноябрь',
   'Декабрь'
];


function createDateSelector(select_element) {
    let today_date = new Date();
    today_date.setDate(today_date.getDate() + 1);

    let first_element = document.createElement("option");
    first_element.setAttribute("disabled", "");

    /*if (today_date.getMonth() === 1)
        first_element.textContent = months[11];
    else*/
    first_element.textContent = months[today_date.getMonth()];
    select_element.appendChild(first_element);

    for (i = 1; i <= 14; i++) {
        let option_element = document.createElement("option");

        option_element.setAttribute("value", today_date.getFullYear() + "-" + (today_date.getMonth() + 1) + "-" + today_date.getDate());
        option_element.textContent = today_date.getDate();
        select_element.appendChild(option_element);

        if (new Date(today_date.getFullYear(), today_date.getMonth(), today_date.getDate() + 1, 0, 0, 0, 0).getMonth() !== today_date.getMonth()) {
            let option_element = document.createElement("option");
            option_element.setAttribute("disabled", "");
            option_element.textContent = months[today_date.getMonth() + 1];
            select_element.appendChild(option_element);
        }

        today_date.setDate(today_date.getDate() + 1);
    }
}

let time_element = document.getElementsByName("time")[0];
time_element.setAttribute("disabled", "");
let date_element = document.getElementsByName("date")[0];
date_element.setAttribute("disabled", "");

date_element.oninput = function (event) {
    time_element.removeAttribute("disabled");
    for (i = 0; i < time_element.childElementCount; i++) {
        time_element.lastChild.remove();
    }
    createTimeSelector(time_element, document.getElementById("adres").value, event.target.value);
}
document.getElementById("adres").oninput = function (event) {
    date_element.removeAttribute("disabled");
    if (date_element.childElementCount > 1)
        for (i = 0; i < 16; i++) {
            date_element.lastChild.remove();
        }
    createDateSelector(date_element);
}

async function createTimeSelector(select_element, master_id, date) {
    select_element.innerHTML = "";

    var params = new URLSearchParams("master_id=" + master_id + "&date=" + date);
    let response = await fetch("/project/php/get_time_date.php", { method: "POST", body: params });

    let result = "";
    if (response.ok) {
        result = await response.text();
        if (result == "00:00:00 00:00:00 00:00:00") {
            select_element.innerHTML = "";
            return false;
        }
    } else {
        console.log("Ошибка HTTP: " + response.status);
    }

    let full_data = result.split(" orders:");
    let disable_time = full_data[1].split(" ");
    let all_time = full_data[0].split(" ");

    let open_time = all_time[0].split(":");
    let close_time = all_time[1].split(":");
    let step_time = all_time[2].split(":");

    let time = new Date(1, 1, 1, open_time[0], open_time[1], open_time[2]);
    do {
        let option_element = document.createElement("option");
        option_element.textContent = (time.getHours() < 10 ? "0" : "") + time.getHours() + ":" + time.getMinutes();
        for (i = 0; i < disable_time.length; i++) {
            if (disable_time[i] === option_element.textContent + ":00" || disable_time[i] === option_element.textContent + "0:00") {
                option_element.setAttribute("disabled", "");
                break;
            }
        }
        select_element.appendChild(option_element);

        time.setHours(time.getHours() + Number(step_time[0]));
        time.setMinutes(time.getMinutes() + Number(step_time[1]));
    } while (new Date(1, 1, 1, close_time[0], close_time[1], close_time[2]) >= time);

    return true;
}