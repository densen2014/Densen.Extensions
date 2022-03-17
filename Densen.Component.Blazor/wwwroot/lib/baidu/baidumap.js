var map = null;
export function init(key, elementId, dotnetRef, backgroundColor, controlSize) {
    //if (!key || !elementId || !dotnetRef) {
    //    return;
    //}

    //storeElementIdWithDotnetRef(_mapsElementDict, elementId, dotnetRef, backgroundColor, controlSize); //Store map info

    let src = "https://api.map.baidu.com/api?v=3.0&ak=";
    let scriptsIncluded = false;

    let scriptTags = document.querySelectorAll('body > script');
    scriptTags.forEach(scriptTag => {
        if (scriptTag) {
            let srcAttribute = scriptTag.getAttribute('src');
            if (srcAttribute && srcAttribute.startsWith(src)) {
                scriptsIncluded = true;
                return true;
            }
        }
    });

    if (scriptsIncluded) { //Prevent adding JS scripts to page multiple times.
        if (window.BMap) {
            //initMaps(); //Page was navigated
        }
        return true;
    }

    src = src + key + "&callback=initMaps";
    let importedMaps = document.createElement('script');
    importedMaps.src = src;
    //importedMaps.defer = true;
    document.body.appendChild(importedMaps);
    return false;
}

export function initMaps() {
    var map = new BMap.Map("container", {
        coordsType: 5 // coordsType指定输入输出的坐标类型，3为gcj02坐标，5为bd0ll坐标，默认为5。
        // 指定完成后API将以指定的坐标类型处理您传入的坐标
    });          // 创建地图实例
    var point = new BMap.Point(116.47496, 39.77856);  // 创建点坐标
    map.centerAndZoom(point, 15);                 // 初始化地图，设置中心点坐标和地图级别
    map.enableScrollWheelZoom(true);     //开启鼠标滚轮缩放
    map.addControl(new BMap.NavigationControl());
    map.addControl(new BMap.ScaleControl());
    map.addControl(new BMap.OverviewMapControl());
    map.addControl(new BMap.MapTypeControl());
    map.setCurrentCity("北京"); // 仅当设置城市信息时，MapTypeControl的切换功能才能可用
}

export function geolocation() {
    var geolocation = new BMap.Geolocation();
    // 开启SDK辅助定位
    geolocation.enableSDKLocation();
    geolocation.getCurrentPosition(function (r) {
        if (this.getStatus() == BMAP_STATUS_SUCCESS) {
            var mk = new BMap.Marker(r.point);
            map.addOverlay(mk);
            map.panTo(r.point);
            console.log('您的位置：' + r.point.lng + ',' + r.point.lat);
        }
        else {
            alert('failed' + this.getStatus());
        }
    });
}