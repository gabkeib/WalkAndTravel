import React, { Component, memo, PureComponent } from 'react';
import Map from "./map/Map";
import Sidebar from './sidebarMenu/SidebarMenu';
import { FixedSizeList as ReactList, areEqual } from 'react-window';
import memoize from 'memoize-one';
import SidebarProfile from './sidebarProfile/SidebarProfile.js';

import './Home.css';


const points1 = [
    [54.6866, 25.2880],
    [54.6902, 25.2764]
];

const burgerBars = {
    background: 'white'
}

export class Home extends Component {
    static displayName = Home.name;

    constructor(props) {
        super(props);
        this.state = { routes: [], currentRoute: [], routeToSave:[], loading: true };
    }

    componentDidMount() {
        this.populateRouteData();
    }

    handleClick = (route) => {
        this.setState({ currentRoute: route});
    }

    handleRandomRouteRequest = () => {
        this.getRandomRoute();
    }

    handleRandomPOIRouteRequest = () => {
        this.getRandomPOIRoute();
    }

    handleGetRoute = (route) => {
        this.setState({ routeToSave: route });
        console.log(route);
    }

    handleNewRoute = (name) => {
        console.log(name);
        this.sendNewRoute(name);
    }

    handleFarmExp = () => {
        this.gainExp();
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : <Sidebar handleClick={this.handleClick} data={this.state.routes} handleRandomRouteRequest={this.handleRandomRouteRequest} handleFarmExp={this.handleFarmExp} handleRandomPOIRouteRequest={this.handleRandomPOIRouteRequest} handleNewRoute={this.handleNewRoute} selectedRoute={[]} />

        return (
            <div id = "Home">
                {contents}
                <SidebarProfile />
                <Map handleClick={this.handleClick} waypoints={this.state.currentRoute} handleGetRoute={this.handleGetRoute} />
            </div>
        );
    }

    async populateRouteData() {
        const response = await fetch('route/Route');
        const data = await response.json();
        console.log(data);
        this.setState({ routes: data, currentRoute: points1, loading: false });
    }

    async getRandomRoute() {
        const response = await fetch('route/RandomRoute');
        const route = await response.json();
        console.log(route);
        this.setState({ currentRoute: route.coordinates });
    }

    async getRandomPOIRoute() {
        const response = await fetch('route/RandomPOIRoute');
        const route = await response.json();
        console.log(route);
        this.setState({ currentRoute: route.coordinates });
    }

    async gainExp() {
        const response = await fetch('user/farm', {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            }
        }).then(response => {
            //console.log(response.json()
            if (response.ok) {
                console.log("ok");
                console.log(response.json());
            }
            else {
                console.log("oh no");
                alert("Whyy");
            }
        });
    }

    async sendNewRoute(name) {
        console.log(this.state.routeToSave);
        //let routePath = [].concat(...this.state.routeToSave);
        let routePath = [];

        for (var i = 0; i < this.state.routeToSave.length; i++) {
            routePath = routePath.concat(this.state.routeToSave[i].latLng.lat);
            routePath = routePath.concat(this.state.routeToSave[i].latLng.lng);
        }
        let newRoute = {
            name: name,
            route: routePath
        }
        let body = JSON.stringify(newRoute);
        const response = await fetch('route/SaveNewRoute', {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(newRoute)
        }).then(response =>
        {
            //console.log(response.json()
            if (response.ok) {
                console.log("ok");
            }
            else {
                console.log("oh no");
                alert("This route name already exists. Change it!");
            }
        });
        //console.log(JSON.stringify(newRoute));

        //console.log(response);
    }
}
export default Home;