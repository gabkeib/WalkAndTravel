import React from "react"
import { MapContainer, TileLayer } from "react-leaflet";
import RoutingMachine from "./RoutingMachine";

const Map = (props) => {
    return (
        <MapContainer className = "map" center={[54.687157, 25.279652]} zoom={13} height={180} scrollWheelZoom={false}>
            <TileLayer
                attribution='&copy; <a href="http://osm.org/copyright">OpenStreetMap</a> contributors'
                url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
            />
            <RoutingMachine />
        </MapContainer>
    )
}

export default Map;