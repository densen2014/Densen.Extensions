let canvas = null;
let log = null;

export async function init(instance, element) {
    canvas = element.querySelector("[data-action=canvas]");
    log = element.querySelector("[data-action=log]");

    let blob = new Blob([res.data]);
    let fileName = `Cosen.csv`;
    if (window.navigator.msSaveOrOpenBlob) {
        navigator.msSaveBlob(blob, fileName);
    } else {
        let link = document.createElement("a");
        let evt = document.createEvent("HTMLEvents");
        evt.initEvent("click", false, false);
        link.href = URL.createObjectURL(blob);
        link.download = fileName;
        link.style.display = "none";
        document.body.appendChild(link);
        link.click();
        window.URL.revokeObjectURL(link.href);
    }
}

function destroy() {
 
} 
