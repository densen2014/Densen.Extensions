let canvas = null;
let log = null;
let file = null;
let dataURL = null;
let image = null;
let startX = null;
let startY = null;
let startDrag = false;
let lastX = null;
let lastY = null;
let avatarDataUrl = null;
let times = 1;
let avatarRef = null;

export async function init(instance, element) {
    canvas = element.querySelector("[data-action=canvas]");
    log = element.querySelector("[data-action=log]");

    let ipnut = document.getElementById("upload");
 

    //获取文件并读取文件
    const handleChange = (event) => {
        let _file = event.target.files[0];
        let fileReader = new FileReader();
        fileReader.onload = (event) => {
            file=_file
            dataURL = event.target.result
            image = new Image;
            let ctx = canvas.getContext("2d");
            image.onload = function () {
                ctx.drawImage(image, 0, 0); // Or at whatever offset you like
            };
            //image.onload = () => drawImage();
            image.src = dataURL;
        };
        fileReader.readAsDataURL(_file);
    };

    //获取裁剪坐标
    const handleMouseDown = (event) => {
        startX = event.clientX;
        startY = event.clientY;
        startDrag = true;
    };

    const handleMouseMove = (event) => {
        if (startDrag) {
            drawImage(
                event.clientX - startX + lastX,
                event.clientY - startY + lastY
            );
        }
    };

    const handleMouseUp = (event) => {
        lastX= event.clientX - startX + lastX;
        lastY= event.clientY - startY + lastY;
        startDrag= false;
    };

    //裁剪图片
    const drawImage = (left = lastX, top = lastY) => {
        let ctx = canvas.getContext("2d");
        ctx.clearRect(0, 0, canvas.width, canvas.height);
        let imageWidth = image.width;
        let imageHeight = image.height;
        if (imageWidth > imageHeight) {
            let scale = canvas.width / canvas.height;
            imageWidth = canvas.width * times;
            imageHeight = imageHeight * scale * times;
        } else {
            let scale = canvas.height / canvas.width;
            imageHeight = canvas.height * times;
            imageWidth = imageWidth * scale * times;
        }
        ctx.drawImage(
            image,
            (canvas.width - imageWidth) / 2 + left,
            (canvas.height - imageHeight) / 2 + top,
            imageWidth,
            imageHeight
        );
    };

    //读取裁剪后的图片并上传
    const confirm = () => {
        let ctx = canvas.getContext("2d");
        const imageData = ctx.getImageData(100, 100, 100, 100);
        let avatarCanvas = document.createElement("canvas");
        avatarCanvas.width = 100;
        avatarCanvas.height = 100;
        let avatarCtx = avatarCanvas.getContext("2d");
        avatarCtx.putImageData(imageData, 0, 0);
        let avatarDataUrl = avatarCanvas.toDataURL();
        avatarDataUrl = avatarDataUrl;
        avatarRef.src = avatarDataUrl;
    };

    ////上传
    //upload = (event) => {
    //    // console.log("文件url", avatarDataUrl);
    //    let bytes = atob(avatarDataUrl.split(",")[1]);
    //    console.log("bytes", bytes);
    //    let arrayBuffer = new ArrayBuffer(bytes.length);
    //    let uInt8Array = new Uint8Array();
    //    for (let i = 0; i < bytes.length; i++) {
    //        uInt8Array[i] = bytes.charCodeAt[i];
    //    }
    //    let blob = new Blob([arrayBuffer], { type: "image/png" });
    //    let xhr = new XMLHttpRequest();
    //    let formData = new FormData();
    //    formData.append("avatar", blob);
    //    xhr.open("POST", "/upload", true);
    //    xhr.send(formData);
    //};

    ipnut.addEventListener("change", handleChange, false);
    canvas.addEventListener("mousedown", handleMouseDown, false);
    canvas.addEventListener("mousemove", handleMouseMove, false);
    canvas.addEventListener("mouseup", handleMouseUp, false);
}

function destroy() {

} 
