import React, { Component } from 'react';
import Map from "./Map";
import SideBar from './sidebar/Sidebar';

import './Home.css';

export class Home extends Component {
    static displayName = Home.name;

    constructor(props) {
        super(props);
        this.state = { routes: [], loading: true };
    }

    componentDidMount() {
        this.populateRouteData();
    }

    static renderRoutesTable(routes) {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Name</th>
                        <th>Length</th>
                    </tr>
                </thead>
                <tbody>
                    {routes.map(routes =>
                        <tr key={routes.name}>
                            <td>{routes.name}</td>
                            <td>{routes.length}</td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    render() {
        let contents = this.state.loading
        ? <p><em>Loading...</em></p>
        : Home.renderRoutesTable(this.state.routes);

        return (
            <div>
                {contents}
                <Map /> 
            </div>
        );
    }

    async populateRouteData() {
        const response = await fetch('routelist');
        const data = await response.json();
        this.setState({ routes: data, loading: false });
    }
}
