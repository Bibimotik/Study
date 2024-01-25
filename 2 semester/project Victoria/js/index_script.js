let record = document.getElementById('record-button');
let change = document.getElementById('nav-record');

record.onmouseover = function (){
    change.style.background = '#BA936B';
}
record.onmouseout = function (){
    change.style.background = '#000000';
}
let submit = document.getElementById('submit-button');

submit.onclick = function (){
    document.window.alert('Спасибо что остаетесь с нами!');
}