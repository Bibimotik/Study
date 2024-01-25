//
// function crug_on (){
//     let crug = document.getElementById('idiot');
//     let time = Date.now(),
//     left = '',
//     top = '',
//     alpha = 2* 3.13;
//
//     function draw_position(k) {
//     left = 150+ r*Math.cos(k*alpha);
//     top = 150+ r*Math.sin(-alpha*k);
//     crug.css({'left': left,'top': top })
//     }
//     let start = setInterval(function (){
//         let k = Date.now() - time;
//         if(t>10000){
//             clearInterval(start)
//         }
//         draw_position(k);
//     },1)
//
// }
//
// crug_on ();


// let idiot_get= document.getElementById('idiot')
// idiot_get.onclick = function() {
//     let time = Date.now(),
//         r = 100;
//         alpha = 2* 3.13;
//     let k = Date.now() - time;
//     let timer = setInterval(function(k) {
//
//         idiot_get.style.left = 150+ r*Math.cos(k*alpha) + 'px';
//         idiot_get.style.top = 150+ r*Math.sin(k*(-alpha)) + 'px';
//
//
//         if (k > 1000){
//             clearInterval(timer);
//         }
//
//     }, 1);
//
// }


// let container = document.querySelector(".container");
// let ball = document.querySelector(".ball");
//
// let maxX = container.clientWidth - ball.offsetWidth;
// let maxY = container.clientHeight - ball.offsetHeight;
//
// let size = 30;
// let start = null;
// let duration = 1;
//
// function step(timestamp) {
//     let progress, x, y;
//     if (start === null) start = timestamp;
//
//     progress = (timestamp - start) / duration / 2000;
//
//     x = progress * maxX / size;
//     y = 2 * Math.sin(x);
//
//     ball.style.left = Math.min(maxX, size * x) + "px";
//     ball.style.top = maxY / 2 + (size * y) + "px";
//
//     if (progress >= 1) start = null;
//     requestAnimationFrame(step);
// }
// requestAnimationFrame(step);


//
// function movement( xid,yexpr, xexpr, x0, y0,dx,t,time){
//     /*
//       xid - id перемещаемого объекта
//       yexpr - выражение с переменной x для вертикальной координаты
//       xexpr - выражение с переменной x для горизонтальной координаты
//       x0, y0 - координаты начала, px
//       dx - шаг изменения координат, px
//       t - шаг времени, мс
//       time - длительность движения, с (если не указан, то бесконечная)
//    */
//
//     if (!yexpr) {
//         return null;
//     }
//     /* Значения параметров по умолчанию */
//     if (!xexpr) {
//         xexpr="x";
//     }
//     if (!x0) {
//         x0=0;
//     }
//     if (!y0) {
//         y0=0;
//     }
//     if (!dx) {
//         dx=0.1
//     }
//     if(!t) {
//         t=100;
//     }
//     if(!time) {
//         time=0;
//     }
//
//     let xobj=document.getElementById(xid);
//
//     if (!xobj.style.position) {
//         xobj.style.position="absolute";
//     }
//     x=0;
//     sumtime=0;
//     my_interval=0;
//     my_interval = setInterval("move2('"+xid+"','"+yexpr+"','"+xexpr+ "',"+x0+","+y0+","+t+","+dx+","+time+",my_interval)",t);
//     return my_interval
// }
// function  move2(xid,yexpr, xexpr, x0, y0,t, dx,time,my_interval){
//     x=x+dx;
//     document.getElementById(xid).style.top=parseInt(y0+eval(yexpr))+"px";
//     document.getElementById(xid).style.left=parseInt(x0+eval(xexpr))+"px";
//     if (time>0){
//         sumtime+=t;
//         if (sumtime>time*1000)clearInterval(my_interval); //остановка
//     }
// }
// /* Пример вызова */
// movement("img1",
//     "100*Math.sin(x)-50*Math.sin(2*x)",
//     "100*Math.cos(x)+50*Math.cos(2*x)",
//     200,250,0.15,100,60);


let idiot_get= document.getElementById('img1')
idiot_get.onclick = function() {
    let start = Date.now();

    let timer = setInterval(function () {
        let x = Date.now() - start;

            idiot_get.style.left = 100 * Math.cos(x / 100) + 'px';
            idiot_get.style.top = x / 10 + 'px';
            if (x > 2000) {
                clearInterval(timer);
            }
        },1)


        //  if (x > 2000){
        //      let start_second = Date.now();
        //      let x_second  = Date.now() - start_second;
        //      idiot_get.style.right = 100*Math.cos(x_second/100) + 'px';
        //      idiot_get.style.bottom = x_second/10 + 'px';
        // }
    function uno() {
        // transform: translate(500px);

        idiot_get.style.transform = 'translate(-38px,-200px)'


    }

    setTimeout(uno,4000)

}



