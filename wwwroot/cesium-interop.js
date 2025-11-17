/*
    Purpose: JavaScript interop module for CesiumJS 3D globe operations and entity management
    Inputs / Dependencies: CesiumJS library, cesiumService.cs
    Behavior / Notes: Manages globe initialization, marker creation/updates, camera controls, and InfoBox link handling
    Author: Anthony Petrosino
    Last updated: November 2025
*/
let viewer = null;
let markerPositions = [];

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
        selectionIndicator: false,
        fullscreenButton: false,
        creditContainer: document.createElement('div')
    });
    
    viewer.cesiumWidget.creditContainer.style.display = 'none';
    
    // Allow links to open in new tabs
    var iframe = document.getElementsByClassName('cesium-infoBox-iframe')[0];
    iframe.setAttribute('sandbox', 'allow-same-origin allow-scripts allow-popups allow-popups-to-escape-sandbox');
    
    // Intercept link clicks in InfoBox and open in parent window
    iframe.addEventListener('load', function() {
        try {
            var iframeDoc = iframe.contentDocument || iframe.contentWindow.document;
            iframeDoc.addEventListener('click', function(e) {
                if (e.target.tagName === 'A' && e.target.href) {
                    e.preventDefault();
                    window.open(e.target.href, '_blank');
                }
            });
        } catch (e) {
            console.log('Could not access iframe:', e);
        }
    });
    
    // Set camera constraints
    viewer.scene.screenSpaceCameraController.minimumZoomDistance = 2000000;
    viewer.scene.screenSpaceCameraController.maximumZoomDistance = 50000000;
    
    // Disable double-click zoom
    viewer.cesiumWidget.screenSpaceEventHandler.removeInputAction(Cesium.ScreenSpaceEventType.LEFT_DOUBLE_CLICK);
    
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
    
    // Calculate label offset based on existing markers
    const offset = calculateLabelOffset(lat, lon);
    
    // Store position for overlap detection
    markerPositions.push({ lat, lon });
    
    // Add marker point
    const entity = viewer.entities.add({
        id: name,
        position: Cesium.Cartesian3.fromDegrees(lon, lat),
        point: {
            pixelSize: 10,
            color: Cesium.Color.RED,
            outlineColor: Cesium.Color.WHITE,
            outlineWidth: 2
        },
        label: {
            text: name,
            font: '12pt sans-serif',
            style: Cesium.LabelStyle.FILL_AND_OUTLINE,
            outlineWidth: 2,
            verticalOrigin: Cesium.VerticalOrigin.BOTTOM,
            horizontalOrigin: Cesium.HorizontalOrigin.LEFT,
            pixelOffset: new Cesium.Cartesian2(offset.pixelX, offset.pixelY)
        },
        description: description
    });
    
    return entity.id;
}

function calculateLabelOffset(lat, lon) {
    let overlapCount = 0;
    const threshold = 10; // degrees
    
    // Count overlapping markers
    for (const pos of markerPositions) {
        if (Math.abs(pos.lat - lat) < threshold && Math.abs(pos.lon - lon) < threshold) {
            overlapCount++;
        }
    }
    
    // Spread labels in circular pattern
    const angle = overlapCount * 45 * (Math.PI / 180); // 45 degrees apart
    const distance = 15 + (overlapCount * 10); // smaller distances
    
    return {
        pixelX: Math.cos(angle) * distance,
        pixelY: Math.sin(angle) * distance - 10
    };
}

export function addISSMarker(lat, lon, name, description) {
    if (!viewer) return null;
    
    const entity = viewer.entities.add({
        id: 'iss-marker',
        position: Cesium.Cartesian3.fromDegrees(lon, lat),
        billboard: {
            image: '/ISS_spacecraft_model_1.png',
            width: 64,
            height: 64,
            verticalOrigin: Cesium.VerticalOrigin.CENTER,
            horizontalOrigin: Cesium.HorizontalOrigin.CENTER
        },
        label: {
            text: name,
            font: '16pt bold sans-serif',
            style: Cesium.LabelStyle.FILL_AND_OUTLINE,
            fillColor: Cesium.Color.CYAN,
            outlineColor: Cesium.Color.BLACK,
            outlineWidth: 3,
            verticalOrigin: Cesium.VerticalOrigin.TOP,
            pixelOffset: new Cesium.Cartesian2(0, 40)
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

export function resetView() {
    if (!viewer) return;
    
    viewer.camera.flyTo({
        destination: Cesium.Cartesian3.fromDegrees(0, 0, 20000000),
        duration: 1.5
    });
}

export function dispose() {
    if (viewer) {
        viewer.destroy();
        viewer = null;
    }
}
