let viewer = null;

export function initializeGlobe(containerId) {
    viewer = new Cesium.Viewer(containerId, {
        baseLayer: Cesium.ImageryLayer.fromProviderAsync(
            Cesium.SingleTileImageryProvider.fromUrl('/world_map.jpg')
        ),
        baseLayerPicker: false,
        geocoder: false,
        homeButton: false,
        sceneModePicker: false,
        navigationHelpButton: false,
        animation: false,
        timeline: false,
        creditContainer: document.createElement('div')
    });
    
    viewer.cesiumWidget.creditContainer.style.display = 'none';
    
    // Set camera constraints
    viewer.scene.screenSpaceCameraController.minimumZoomDistance = 2000000;
    viewer.scene.screenSpaceCameraController.maximumZoomDistance = 50000000;
    
    return true;
}

export function updateMarker(entityId, lat, lon) {
    if (!viewer) return;
    
    const entity = viewer.entities.getById(entityId);
    if (entity) {
        entity.position = Cesium.Cartesian3.fromDegrees(lon, lat);
    }
}

export function addMarker(lat, lon, name, description) {
    if (!viewer) return null;
    
    const entity = viewer.entities.add({
        position: Cesium.Cartesian3.fromDegrees(lon, lat),
        point: {
            pixelSize: 10,
            color: Cesium.Color.RED,
            outlineColor: Cesium.Color.WHITE,
            outlineWidth: 2
        },
        label: {
            text: name,
            font: '14pt sans-serif',
            style: Cesium.LabelStyle.FILL_AND_OUTLINE,
            outlineWidth: 2,
            verticalOrigin: Cesium.VerticalOrigin.BOTTOM,
            pixelOffset: new Cesium.Cartesian2(0, -9)
        },
        description: description
    });
    
    return entity.id;
}

// Not functional atm
export function addISSMarker(lat, lon, name, description) {
    if (!viewer) return null;
    
    const entity = viewer.entities.add({
        id: 'iss-marker',
        position: Cesium.Cartesian3.fromDegrees(lon, lat),
        point: {
            pixelSize: 20,  // Much bigger
            color: Cesium.Color.CYAN,
            outlineColor: Cesium.Color.WHITE,
            outlineWidth: 3
        },
        label: {
            text: name,
            font: '16pt bold sans-serif',
            style: Cesium.LabelStyle.FILL_AND_OUTLINE,
            fillColor: Cesium.Color.CYAN,
            outlineColor: Cesium.Color.BLACK,
            outlineWidth: 3,
            verticalOrigin: Cesium.VerticalOrigin.TOP,
            pixelOffset: new Cesium.Cartesian2(0, 15)
        },
        description: description
    });
    
    return entity.id;
}

export function flyToLocation(lat, lon) {
    if (!viewer) return;
    
    viewer.camera.flyTo({
        destination: Cesium.Cartesian3.fromDegrees(lon, lat, 15000000),
        duration: 2
    });
}

export function dispose() {
    if (viewer) {
        viewer.destroy();
        viewer = null;
    }
}
