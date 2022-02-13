import 'https://cdnjs.cloudflare.com/ajax/libs/viewerjs/1.10.3/viewer.min.js';
var gallery = null;
export function initOptions (options) {
    options.title = function (image) {
        return image.alt + ' (' + (this.index + 1) + '/' + this.length + ')';
    };
    if (undefined !== gallery && null !== gallery) {
        gallery.destroy();
        console.log('destroy');
   }
    gallery = new Viewer(document.getElementById(options.id), options);
    console.log(gallery);
}
