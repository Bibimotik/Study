const  canvas = document.getElementById('canvas');
const ctx = canvas.getContext('2d');

const canvas_width = canvas.clientWidth;
const canvas_height = canvas.clientHeight;

const xAxis = Math.round(canvas_width/2)
const yAxis = Math.round(canvas_height/2)

ctx.beginPath();
ctx.strokeStyle = 'black';
ctx.moveTo(xAxis,0);
ctx.lineTo(xAxis,canvas_height);

ctx.moveTo(0,yAxis);
ctx.lineTo(canvas_width,yAxis);

ctx.stroke();
ctx.closePath();

ctx.fillStyle = 'red';




function one() {
    for (let i = 0; i <= canvas_width; i++) {
        const x = (i - xAxis) / 50
        const y = Math.pow(x, 2)
        ctx.fillRect(x * 50 + xAxis, yAxis - y * 50, 6, 6)
    }
}
function two() {
    for (let i = 0; i <= canvas_width; i++) {
        const x = (i - xAxis) / 50
        const y = Math.pow(x, 3)
        ctx.fillRect(x * 50 + xAxis, yAxis - y * 50, 10, 10)
    }
}

function three() {
    for (let i = 0; i <= canvas_width; i++) {
        const x = (i - xAxis) / 50
        const y = Math.sin(x)
        ctx.fillRect(x * 50 + xAxis, yAxis - y * 50, 10, 10)
    }
}

function four() {
    for (let i = 0; i <= canvas_width; i++) {
        const x = (i - xAxis) / 50
        const y = Math.cos()
        ctx.fillRect(x * 50 + xAxis, yAxis - y * 50, 10, 10)
    }
}

 let rad = document.getElementsByName('radio');
// for (let i =0; i < rad.length; i++){
//     if (rad[i].checked){
//        var rad_ch = rad[i].value
//         break
//     }
// }

if (rad.value = 1)





