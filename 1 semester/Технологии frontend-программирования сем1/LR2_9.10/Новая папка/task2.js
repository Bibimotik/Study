let idiot_get= document.getElementById('idiot')
idiot_get.onclick = function() {
    let start = Date.now();

    let timer = setInterval(function() {
        let time_passed = Date.now() - start;
        idiot_get.style.left = time_passed  + 'px';

        if (time_passed > 1000){
            clearInterval(timer);
        }

    }, 1);

}