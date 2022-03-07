import '//api.map.baidu.com/api?type=webgl&v=1.0&ak=DD279b2a90afdf0ae7a3796787a0742e';
var map = null;
export function init() {
    map = new BMapGL.Map('container');
    map.centerAndZoom(new BMapGL.Point(116.47496, 39.77856), 15);
    map.enableScrollWheelZoom(true);
    map.enableKeyboard();
    //map.setHeading(64.5);

    //点覆盖物
    var marker = new BMapGL.Marker(new BMapGL.Point(116.404, 39.915));
    map.addOverlay(marker);
    //自定义的图层
    var isTilesPng = true;
    var tileSize = 256;
    var tstyle = 'pl';
    var udtVersion = '20190102';
    var tilelayer = new BMapGL.TileLayer({
        transparentPng: isTilesPng
    });
    tilelayer.zIndex = 110;
    tilelayer.getTilesUrl = function (point, level) {
        if (!point || level < 0) {
            return null;
        }
        var row = point.x;
        var col = point.y;
        var url = '//mapsv0.bdimg.com/tile/?udt=' + udtVersion
            + '&qt=tile&styles=' + tstyle + '&x=' + row + '&y=' + col + '&z=' + level;
        return url;
    };
    map.addTileLayer(this.tilelayer);
    //设置地图显示元素
    map.setDisplayOptions({
        overlay: false,   //是否显示覆盖物
        layer: false,     //是否显示叠加图层，地球模式暂不支持
        building: false,   //是否显示3D建筑物（仅支持WebGL方式渲染的地图）
    })
    map.setDisplayOptions({
        poi: true       //是否显示POI信息
    })
}

export function showOverlay() {
    map.setDisplayOptions({
        overlay: true
    })
}
export function hideOverlay() {
    map.setDisplayOptions({
        overlay: false
    })
}
export function showTilelay() {
    map.setDisplayOptions({
        layer: true
    })
}
export function hideTilelay() {
    map.setDisplayOptions({
        layer: false
    })
}
export function show3Dbuild() {
    map.setDisplayOptions({
        building: true
    })
}
export function hide3Dbuild() {
    map.setDisplayOptions({
        building: false
    })
}
export function setNewCenter() {
    var lng = 116.514 + Math.floor(Math.random() * 589828) / 1e6;
    var lat = 39.416 + Math.floor(Math.random() * 514923) / 1e6;
    var point = new BMapGL.Point(lng, lat);
    map.setCenter(point); // 设置地图中心点
}
export function getMapCenter() {
    var cen = map.getCenter(); // 获取地图中心点
    alert('地图中心点: (' + cen.lng.toFixed(5) + ', ' + cen.lat.toFixed(5) + ')');
}
export function showText() {
    map.setDisplayOptions({
        poiText: true
    })
}
export function hideText() {
    map.setDisplayOptions({
        poiText: false
    })
}
export function showIcon() {
    map.setDisplayOptions({
        poiIcon: true
    })
}
export function hideIcon() {
    map.setDisplayOptions({
        poiIcon: false
    })
}
export function showRoadNet() {
    map.setDisplayOptions({
        street: true,     //是否显示路网（只对卫星图和地球模式有效）
    })
}
export function hideRoadNet() {
    map.setDisplayOptions({
        street: false,     //是否显示路网（只对卫星图和地球模式有效）
    })
}
export function showEARTH_MAP() {
    //地球模式
    map.setMapType(BMAP_EARTH_MAP);
}
export function hideEARTH_MAP() {
    map.setMapType(BMAP_NORMAL_MAP);
}