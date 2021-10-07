import L from "leaflet"
import { createControlComponent } from "@react-leaflet/core";
import "leaflet-routing-machine";
import "leaflet-control-geocoder";

const createRoutingMachineLayer = (props) => {
    const instance = L.Routing.control({
        waypoints: [
            L.latLng(54.6866, 25.2865),
            L.latLng(54.6902, 25.2764)
        ],
        lineOptions: {
            styles: [{ color: "#6FA1EC", weight: 4 }]
        },
        routeWhileDragging: true,
        geocoder: L.Control.Geocoder.nominatim()
    });

    return instance;
    };

    const RoutingMachines = createControlComponent(createRoutingMachineLayer);

    export default RoutingMachines;