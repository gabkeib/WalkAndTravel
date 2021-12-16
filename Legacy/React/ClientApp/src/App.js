import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';

import './custom.css'
import Form from './components/Sign-up/Form';
import Form2 from './components/Sign-up/Form2';

export default class App extends Component {
    static displayName = App.name;

    render() {
        return (
            <Layout>
                <Route exact path='/' component={Home} />
                <Route exact path='/Sign-up' component={Form} />
                <Route exact path='/Login' component={Form2} />
            </Layout>
        );
    }
}
