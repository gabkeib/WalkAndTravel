import React, { useEffect, useState, useRef } from "react"
import { MapContainer, TileLayer, useMap } from "react-leaflet";
import RoutingMachine from "./RoutingMachine";
import { GeoSearchControl, OpenStreetMapProvider } from 'leaflet-geosearch';
import "leaflet-geosearch/dist/geosearch.css";
import "leaflet-contextmenu";
import "leaflet-contextmenu/dist/leaflet.contextmenu.css";
import { LatLng, point } from "leaflet";

function SearchField() {

    const map = useMap();

    useEffect(() => {
        const provider = new OpenStreetMapProvider();
        const searchControl = new GeoSearchControl({
            provider,
            style: 'bar',
            showMarker: true,
            showPopuo: false,
            autoClose: true,
            retainZoomLevel: false,
            animateZoom: true,
            keepResult: false,
            searchLabel: 'search'
        });

        map.addControl(searchControl);

        return () => map.removeControl(searchControl)
    }, []);
    return null;
}

/*function showCoordinates(e) {
    alert(e.latlng);
    addElement(e.latlng);
}*/

const points1 = [
    [54.6866, 25.2865],
    [54.6902, 25.2764]
];

const points2 = [
    [54.6902, 25.2764],
    [54.6866, 25.2865]
];

const Map = (props) => {
    const rMachine = useRef();
    const [points, setPoints] = useState(points1);
    
    useEffect(() => {
        if (rMachine.current) {
            rMachine.current.setWaypoints(points);
        }
    }, [points, rMachine]);

    function addWaypoint(e) {
        addElement(e.latlng);
    }

    function changeStartingElement(e) {
        changeStartingPoint(e.latlng);
    }

    function changeEndingElement(e) {
        changeEndingPoint(e.latlng);
    }

    const changeStartingPoint = (latlng) => {
        let waypoints = rMachine.current.getWaypoints();
        let newPoints = [...waypoints];
        let pointt = newPoints[0];
       
        pointt = latlng;
        newPoints[0] = pointt;
        setPoints(prevv => newPoints);
    };

    const changeEndingPoint = (latlng) => {
        let waypoints = rMachine.current.getWaypoints();
        let pos = waypoints.length - 1;
        let newPoints = [...waypoints];
        let pointt = newPoints[pos];

        pointt = latlng;
        newPoints[pos] = pointt;
        setPoints(prevv => newPoints);
    };

    const addElement = (latlng) => {
        let waypoints = rMachine.current.getWaypoints();
        setPoints([...waypoints, latlng])
    };

    return (
        <MapContainer className="map"
            center={[54.687157, 25.279652]}
            zoom={13}
            height={180}
            scrollWheelZoom={true}
            contextmenu={true}
            contextmenuItems={[
                {
                    text: "Add waypoint",
                    callback: addWaypoint
                       
                },
                {
                    text: "Start from here",
                    callback: changeStartingElement
                },
                {
                    text: "End here",
                    callback: changeEndingElement
                }
       
            ]}
            >
            <TileLayer
                attribution='&copy; <a href="http://osm.org/copyright">OpenStreetMap</a> contributors'
                url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
            />
            <RoutingMachine ref={rMachine} waypoints={points} />
            <SearchField />
        </MapContainer>
    )
}

export default Map;