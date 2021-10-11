import React, { Component } from 'react';
import Map from "./Map";
import SideBar from './sidebar/Sidebar';

import './Home.css';

export class Home extends Component {
    static displayName = Home.name;

    render() {
        return (
            <div id="Home">
                <SideBar />
                <Map />
            </div>
        );
    }
}
