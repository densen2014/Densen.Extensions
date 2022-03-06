export function GetBattery(wrapper, addListener = true) {
    navigator.getBattery().then(function (battery) {

        console.log("Battery charging? " + (battery.charging ? "Yes" : "No"));
        console.log("Battery level: " + battery.level * 100 + "%");
        console.log("Battery charging time: " + battery.chargingTime + " seconds");
        console.log("Battery discharging time: " + battery.dischargingTime + " seconds");

        if (addListener) {
            battery.addEventListener('chargingchange', function () {
                console.log("Battery charging? " + (battery.charging ? "Yes" : "No"));
                logbatteryitem();
            });

            battery.addEventListener('levelchange', function () {
                console.log("Battery level: " + battery.level * 100 + "%");
                logbatteryitem();
            });

            battery.addEventListener('chargingtimechange', function () {
                console.log("Battery charging time: " + battery.chargingTime + " seconds");
                logbatteryitem();
            });

            battery.addEventListener('dischargingtimechange', function () {
                console.log("Battery discharging time: " + battery.dischargingTime + " seconds");
                logbatteryitem();
            });
        }

        function logbatteryitem() {

            var batteryitem = {
                "charging": battery.charging,
                "level": battery.level * 100,
                "chargingTime": battery.chargingTime == 'Infinity' ? null : battery.chargingTime,
                "dischargingTime": battery.dischargingTime == 'Infinity' ? null : battery.dischargingTime
            };
            wrapper.invokeMethodAsync('GetBatteryResult', batteryitem);
        }

        logbatteryitem();
    });
}

export function GetNetworkInfo(wrapper) {
    navigator.connection.addEventListener('change', logNetworkInfo);
    function logNetworkInfo() {
        // Network type that browser uses
        console.log('         type: ' + navigator.connection.type);

        // Effective bandwidth estimate
        console.log('     downlink: ' + navigator.connection.downlink + ' Mb/s');

        // Effective round-trip time estimate
        console.log('          rtt: ' + navigator.connection.rtt + ' ms');

        // Upper bound on the downlink speed of the first network hop
        console.log('  downlinkMax: ' + navigator.connection.downlinkMax + ' Mb/s');

        // Effective connection type determined using a combination of recently
        // observed rtt and downlink values: ' +
        console.log('effectiveType: ' + navigator.connection.effectiveType);

        // True if the user has requested a reduced data usage mode from the user
        // agent.
        console.log('     saveData: ' + navigator.connection.saveData);

        // Add whitespace for readability
        console.log('');

        var networkInfo = {
            "type": navigator.connection.type,
            "downlink": navigator.connection.downlink == undefined ? null : navigator.connection.downlink,
            "rtt": navigator.connection.rtt,
            "downlinkMax": navigator.connection.downlinkMax == undefined ? null : navigator.connection.downlinkMax,
            "effectiveType": navigator.connection.effectiveType,
            "saveData": navigator.connection.saveData,
        };
        wrapper.invokeMethodAsync('GetNetworkInfoResult', networkInfo);

    }

    logNetworkInfo();
}