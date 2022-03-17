export function addScript(url) {
    var script = document.createElement('script');
    script.setAttribute('type', 'text/javascript');
    script.setAttribute('src', url);
    document.getElementsByTagName('head')[0].appendChild(script);
}

export function init(className,option) {
    console.info(option);
    KioskBoard.run('.' + className , option);
    return true;
}