let canvas = null;
let log = null;
let cropper=null;
export async function init(instance, element) {
    canvas = element.querySelector("[data-action=canvas]");
    log = element.querySelector("[data-action=log]");

    var image = document.getElementById('image');
    var button = document.getElementById('button');
    var result = document.getElementById('result');
    var croppable = false;
    cropper = new Cropper(image, {
        viewMode: 1, // 只能在裁剪的图片范围内移动
        dragMode: 'move', // 画布和图片都可以移动
        aspectRatio: 1, // 裁剪区默认正方形
        autoCropArea: 1, // 自动调整裁剪图片
        cropBoxMovable: false, // 禁止裁剪区移动
        cropBoxResizable: false, // 禁止裁剪区缩放
        background: false, // 关闭默认背景 
        movable: true,
        zoomable: true,
        rotatable: true,
        scalable: true,
        guides: true,
        center: true,
        highlight: true,
        cropBoxMovable: true,
        cropBoxResizable: true,
        ready: function () {            
            cropper.zoomTo(1); // Zoom the image to its natural size
            croppable = true;
        },
    });

    button.onclick = function () {
        result.innerHTML = '';
        result.appendChild(cropper.getCroppedCanvas());
    };

    document.getElementById('replace').onclick = function () {
        cropper.replace('./_content/DemoShared/icon-512.png');
    };

    //button.onclick = function () {
    //    var croppedCanvas;
    //    var maskedCanvas;
    //    var maskedImage;

    //    if (!croppable) {
    //        return;
    //    }

    //    // Crop
    //    croppedCanvas = cropper.getCroppedCanvas();

    //    // Mask
    //    maskedCanvas = getMaskedCanvas(croppedCanvas, image, cropper);

    //    // Show
    //    maskedImage = document.createElement('img');
    //    maskedImage.src = maskedCanvas.toDataURL();
    //    result.innerHTML = '';
    //    result.appendChild(maskedImage);
    //};

    function getMaskedCanvas(sourceCanvas, sourceImage, cropper) {
        var canvas = document.createElement('canvas');
        var context = canvas.getContext('2d');
        var maskWidth = cropper.getData().width;
        var maskHeight = cropper.getData().height;
        var maskTop = cropper.getData().y;
        var maskLeft = cropper.getData().x;
        var imageWidth = cropper.getImageData().naturalWidth;
        var imageHeight = cropper.getImageData().naturalHeight;
        var imageLeft = cropper.getImageData().left;
        var imageTop = cropper.getImageData().top;
        var imageAspect = cropper.getImageData().aspectRatio;

        canvas.width = imageWidth;
        canvas.height = imageHeight;

        // Debug
        console.log('Image Width: ' + imageWidth + ' Image Height: ' + imageHeight + ' Image Aspect Ratio: ' + imageAspect);
        console.log('Image Left: ' + imageLeft + ' Image Top: ' + imageTop);
        console.log('Mask Width: ' + maskWidth + ' Mask Height: ' + maskHeight + ' Mask Left: ' + maskLeft + ' Mask Top: ' + maskTop);

        context.imageSmoothingEnabled = true;
        context.drawImage(image, 0, 0, imageWidth, imageHeight);
        context.fillRect(maskLeft, maskTop, maskWidth, maskHeight);
        return canvas;
    }

    // 5. 点击确定事件
    function onConfirm(){
        this.cropper.getCroppedCanvas().toBlob(async blob => {
 
        })
    }
 

}

export async function changeAvatar(instance, element) {
    var avatar = document.getElementById('avatar');
    var image = document.getElementById('newAvatar');
    var input = document.getElementById('input');
    var $progress = $('.progress');
    var $progressBar = $('.progress-bar');
    var $alert = $('.alert');
    var $modal = $('#modal');
    var cropper;

    $('[data-toggle="tooltip"]').tooltip();

    input.addEventListener('change', function (e) {
        var files = e.target.files;
        var done = function (url) {
            input.value = '';
            image.src = url;
            $alert.hide();
            $modal.modal('show');
        };
        var reader;
        var file;
        var url;

        if (files && files.length > 0) {
            file = files[0];

            if (URL) {
                done(URL.createObjectURL(file));
            } else if (FileReader) {
                reader = new FileReader();
                reader.onload = function (e) {
                    done(reader.result);
                };
                reader.readAsDataURL(file);
            }
        }
    });

    $modal.on('shown.bs.modal', function () {
        cropper = new Cropper(image, {
            aspectRatio: 1,
            viewMode: 3,
        });
    }).on('hidden.bs.modal', function () {
        cropper.destroy();
        cropper = null;
    });

    document.getElementById('crop').addEventListener('click', function () {
        var initialAvatarURL;
        var canvas;

        $modal.modal('hide');

        if (cropper) {
            canvas = cropper.getCroppedCanvas({
                width: 160,
                height: 160,
            });
            initialAvatarURL = avatar.src;
            avatar.src = canvas.toDataURL();
            $progress.show();
            $alert.removeClass('alert-success alert-warning');
            canvas.toBlob(function (blob) {
                var formData = new FormData();

                formData.append('avatar', blob, 'avatar.jpg');
                $.ajax('https://jsonplaceholder.typicode.com/posts', {
                    method: 'POST',
                    data: formData,
                    processData: false,
                    contentType: false,

                    xhr: function () {
                        var xhr = new XMLHttpRequest();

                        xhr.upload.onprogress = function (e) {
                            var percent = '0';
                            var percentage = '0%';

                            if (e.lengthComputable) {
                                percent = Math.round((e.loaded / e.total) * 100);
                                percentage = percent + '%';
                                $progressBar.width(percentage).attr('aria-valuenow', percent).text(percentage);
                            }
                        };

                        return xhr;
                    },

                    success: function () {
                        $alert.show().addClass('alert-success').text('Upload success');
                    },

                    error: function () {
                        avatar.src = initialAvatarURL;
                        $alert.show().addClass('alert-warning').text('Upload error');
                    },

                    complete: function () {
                        $progress.hide();
                    },
                });
            });
        }
    });
}
function destroy() {
 
} 
