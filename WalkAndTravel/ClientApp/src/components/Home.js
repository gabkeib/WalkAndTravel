import React, { Component, memo, PureComponent } from 'react';
import Map from "./Map";
import Sidebar from './sidebar/Sidebar';
import { FixedSizeList as ReactList, areEqual } from 'react-window';

import './Home.css';


/*const Row = ({ data, index, style }) => {
    const items = data;
    const item = items[index];

    return (
    <div className={'listItem'} style={style} >
        {item.name} and length is {item.length}
        </div >
        )
};*/

class ItemRenderer extends PureComponent {
    render() {
        const item = this.props.data[this.props.index];
        console.log(this.props.data);
        return (
            <div className={'ListItem'} style={this.props.style} onClick={() => passRoute(item)}>
                {item.name} {this.props.index}
            </div>
        );
    }
}

function passRoute(route) {
    alert(route.name);
}


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


    static renderList(routes) {

        return (
            <ReactList
                className="RList"
                height={150}
                itemCount={routes.length}
                itemData={ routes}
                itemSize={35}
                width={300}
            >
                {ItemRenderer}
            </ReactList>
            )
    }

    render() {
        let contents = this.state.loading
        ? <p><em>Loading...</em></p>
        : Home.renderList(this.state.routes);

        return (
            <div id = "Home">
                {contents}
                <Sidebar />
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
