import React, { Component, memo, PureComponent } from 'react';
import Map from "./Map";
import Sidebar from './sidebar/Sidebar';

import './Home.css';

const points1 = [
    [54.6866, 25.2880],
    [54.6902, 25.2764]
];


export class Home extends Component {
    static displayName = Home.name;

    constructor(props) {
        super(props);
        this.state = { routes: [], currentRoute: points1, loading: true };
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
        console.log(route);
    }

    render() {
        let contents = this.state.loading
        ? <p><em>Loading...</em></p>
            : <Sidebar handleClick={this.handleClick} data={this.state.routes} handleRandomRouteRequest={this.handleRandomRouteRequest} handleRandomPOIRouteRequest={this.handleRandomPOIRouteRequest} selectedRoute={[]} />

        return (
            <div id = "Home">
                {contents}

                <Map handleClick={this.handleClick} waypoints={this.state.currentRoute} handleGetRoute={this.handleGetRoute} />
            </div>
        );
    }

    async populateRouteData() {
        const response = await fetch('routelist');
        const data = await response.json();
        console.log(data);
        this.setState({ routes: data, currentRoute: points1, loading: false });
    }

    async getRandomRoute() {
        const response = await fetch('routelist/GetRandomRoute');
        const route = await response.json();
        console.log(route);
        this.setState({ currentRoute: route.coordinates });
    }

    async getRandomPOIRoute() {
        const response = await fetch('routelist/GetRandomPOIRoute');
        const route = await response.json();
        console.log(route);
        this.setState({ currentRoute: route.coordinates });
    }
}
export default Home;