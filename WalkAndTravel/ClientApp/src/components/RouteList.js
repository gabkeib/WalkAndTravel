import React, { memo, useState } from 'react';
import { FixedSizeList as ReactList, areEqual } from 'react-window';
import memoize from 'memoize-one';

import './RouteList.css';

const Row = memo(({ data, index, style }) => {
    const { items, toggleSelectedRoute } = data;
    const item = items[index];

    return (
        <div className={'ListItem'} onClick={() => toggleSelectedRoute(index)} style={style}>
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

const RouteList = (props) => {

    const [routes, setRoutes] = useState(props.data);

    const toggleSelectedRoute = index => {
        props.sendRoute(routes[index].coordinates);
        setRoutes(prevState => routes);
    };

    return (
        <CreateList
            items={routes}
            toggleSelectedRoute={toggleSelectedRoute}
        />
    )
};

export default RouteList;