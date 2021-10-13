import L from "leaflet"
import { createControlComponent } from "@react-leaflet/core";
import "leaflet-routing-machine";
import 'leaflet-routing-machine/dist/leaflet-routing-machine.css'
import "leaflet-control-geocoder";

const createRoutingMachineLayer = (props) => {
    const { waypoints } = props;
    const instance = L.Routing.control({
        waypoints,
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