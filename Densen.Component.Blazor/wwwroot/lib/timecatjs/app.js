import '/_content/Densen.Component.Blazor/lib/timecatjs/timecat.global.prod.js';

export function init(wrapperc, element, alertText, backgroundColor) {
    const recorder = new Recorder();
    const player = new Player();
    // receive data on callback
    recorder.onData(data)
    console.log('ss')
    recorder.next()

    // stop record and clean all watchers
    recorder.destroy()

    // clear all records in db
    recorder.clearDB()
}