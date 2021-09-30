import React, { Component } from 'react';
import { Container } from 'reactstrap';
import './Home.css';
import Map from "./Map";

export class Home extends Component {
  static displayName = Home.name;

  render () {
    return (
        <div>
            <Map /> 
        </div>
    );
  }
}
