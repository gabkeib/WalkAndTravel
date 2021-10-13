import React, { Component, memo, PureComponent } from 'react';
import Map from "./Map";
import Sidebar from './sidebar/Sidebar';
import { FixedSizeList as ReactList, areEqual } from 'react-window';
import memoize from 'memoize-one';

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

const points1 = [
    [54.6866, 25.2880],
    [54.6902, 25.2764]
];


class ItemRenderer extends PureComponent {
    render() {
        const item = this.props.data[this.props.index];
        console.log(this.props.data);
        return (
            <div className={'ListItem'} style={this.props.style} onClick={() => Home.passRoute(item)}>
                {item.name} {this.props.index}
            </div>
        );
    }
}

const Row = memo(({ data, index, style }) => {
    const { items, toggleSelectedRoute } = data;
    const item = items[index];

    return (
        <div onClick={() => toggleSelectedRoute(index)} style = { style }>
            {item.name} {index}
        </div>
    );
}, areEqual);

const createItemData = memoize((items, toggleSelectedRoute) => ({
    items,
    toggleSelectedRoute,
}));

function CreateList({ items, toggleSelectedRoute }) {
    const itemData = createItemData(items, toggleSelectedRoute);
    return (
        <ReactList
            className="RList"
            height={150}
            itemCount={items.length}
            itemData={itemData}
            itemSize={35}
            width={300}
        >
            {Row}
        </ReactList>
    )
}


export class Home extends Component {
    static displayName = Home.name;

    constructor(props) {
        super(props);
        this.state = { routes: [], currentRoute: points1, loading: true };
        this.passRoute = this.passRoute.bind(this);
    }

    componentDidMount() {
        this.populateRouteData();
    }

    passRoute(route) {
        alert(route.name);
        
    }

    handleClick = (route) => {
        this.setState({ currentRoute: route});
    }

    toggleSelectedRoute = index =>
        this.setState(prevState => {
            const item = prevState.routes[index];
            const routess = prevState.routes;
            const loads = prevState.loading
            const currentRouteNew = item.coordinates;
            console.log(currentRouteNew);
            return { routes: routess, currentRoute:currentRouteNew, loading:loads };
        });

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
            : <Sidebar handleClick={this.handleClick} data={this.state.routes} selectedRoute={[]} />

        return (
            <div id = "Home">
                {contents}
                
                <Map handleClick={this.handleClick} waypoints={this.state.currentRoute} />
            </div>
        );
    }

    async populateRouteData() {
        const response = await fetch('routelist');
        const data = await response.json();
        this.setState({ routes: data, currentRoute: points1, loading: false });
    }
}
