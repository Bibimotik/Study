var order_list = document.getElementById("order_list");
var tr_order_list = order_list.getElementsByTagName("tr");

var active_orders = new Array();

function cheack() {
    let active_orders_element = document.getElementById("active_orders");
    
    for (i = 0; i < tr_order_list.length; i++) {
        let element = tr_order_list[i];

        let date = new Date();
        let user_time_text = element.getElementsByTagName("td")[2].textContent.split(":");

        if (new Date(date.getFullYear(), date.getMonth(), date.getDate(), user_time_text[0], user_time_text[1], 0, 0).getTime() < date.getTime()) {
            if (!active_orders.includes(element)) {
                active_orders.push(element);

                let new_tr_element = document.createElement("tr");
                for (j = 0; j < 3; j++) {
                    let td = document.createElement("td");
                    td.textContent = element.getElementsByTagName("td")[j].textContent;
                    new_tr_element.appendChild(td);
                }

                let complete_button = document.createElement("button");
                complete_button.textContent = "Выполнен";
                complete_button.className = "compleate_button";
                complete_button.id = new_tr_element.getElementsByTagName("td")[1].textContent + " " + new_tr_element.getElementsByTagName("td")[2].textContent;
                new_tr_element.id = "tr_" + complete_button.id;
                complete_button.onclick = function (event) {
                    document.getElementById("tr_" + event.target.id).remove();
                    let buffer = String(event.target.id).split(" ");
                    let time = buffer[1];
                    let user_telephone = buffer[0];

                    deleteOrder(date.getFullYear() + "-" + (date.getMonth() + 1) + "-" + date.getDate(), time);
                    event.target.remove();
                }
                active_orders_element.appendChild(complete_button);

                active_orders_element.appendChild(new_tr_element);

                element.remove();
            }
        }
    }
}

async function clientCompleate(time) {
    let params = new URLSearchParams("time=" + time);
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

async function deleteOrder(date, time) {
    let params = new URLSearchParams("date=" + date + "&time=" + time);
    let response = await fetch("/project/php/complete_order.php", { method: "POST", body: params });

    let result;
    if (response.ok) {
        result = await response.text();
        console.log(result);
    } else {
        console.log("Ошибка HTTP: " + response.status);
    }
}

async function deleteOldOrders(date) {
    let params = new URLSearchParams("date=" + date.getFullYear() + "-" + (date.getMonth() + 1) + "-" + date.getDate());
    let response = await fetch("/project/php/delete_old_orders.php", { method: "POST", body: params });

    let result;
    if (response.ok) {
        result = await response.text();
    } else {
        console.log("Ошибка HTTP: " + response.status);
    }
}