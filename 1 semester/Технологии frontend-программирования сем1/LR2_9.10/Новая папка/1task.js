
let div1 = document.querySelector('.div-1');
let div2 = document.querySelector('.div-2');
let img = document.getElementById('idiot');

div1.ondragover = canDrop;
div2.ondragover = canDrop;

function canDrop(event){
    event.preventDefault();
}
img.ondragstart = dragImg;

function dragImg(event) {
    event.dataTransfer.setData('id', event.target.id)
}

div1.ondrop = dropImg;
div2.ondrop = dropImg;

function dropImg(event) {
    let imgId = event.dataTransfer.getData('id');
    console.log(imgId);
    event.target.append(document.getElementById(imgId));
}

//////////////////////////////////

    let div3 = document.querySelector('.div-3');
    let div4 = document.querySelector('.div-4');
    let text = document.getElementById('tyler');

    div3.ondragover = canDropText;
    div4.ondragover = canDropText;

    function canDropText(event){
        event.preventDefault();
    }
    text.ondragstart = dragText;

    function dragText(event) {
        event.dataTransfer.setData('id', event.target.id)
    }

    div3.ondrop = dropText;
    div4.ondrop = dropText;

    function dropText(event) {
        let textId = event.dataTransfer.getData('id');

        event.target.append(document.getElementById(textId));
    }
