import React, { useEffect } from "react"
import { MapContainer, TileLayer, useMap } from "react-leaflet";
import RoutingMachine from "./RoutingMachine";
import { GeoSearchControl, OpenStreetMapProvider } from 'leaflet-geosearch';
import "leaflet-geosearch/dist/geosearch.css";
import "leaflet-contextmenu";
import "leaflet-contextmenu/dist/leaflet.contextmenu.css";

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

function showCoordinates(e) {
    alert(e.latlng);
}


const Map = (props) => {
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
                    callback: showCoordinates
                },
                {
                    text: "Set starting point"

                },
                {
                    text: "Set ending point"

                }
       
            ]}
            >
            <TileLayer
                attribution='&copy; <a href="http://osm.org/copyright">OpenStreetMap</a> contributors'
                url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
            />
            <RoutingMachine />
            <SearchField />
        </MapContainer>
    )
}

export default Map;