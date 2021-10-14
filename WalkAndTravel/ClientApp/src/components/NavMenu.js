import React, { Component } from 'react';
import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';
import './NavMenu.css';
import { BiHome } from "react-icons/bi";

export class NavMenu extends Component {
  static displayName = NavMenu.name;

  constructor (props) {
    super(props);

    this.toggleNavbar = this.toggleNavbar.bind(this);
    this.state = {
      collapsed: true
    };
  }

  toggleNavbar () {
    this.setState({
      collapsed: !this.state.collapsed
    });
  }

  render () {
    return (
        <header>
            <Navbar className="navbar-light navbar-expand-sm navbar-toggleable-sm">
            <Container>
                <NavbarBrand tag={Link} size="40px" to="/">WalkAndTravel</NavbarBrand>
                <NavbarToggler onClick={this.toggleNavbar} className="mr-2" />
                <Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen={!this.state.collapsed} navbar>
                    <ul className="navbar-nav flex-grow">
                        <NavItem>
                            <NavLink tag={Link} to="/"> <BiHome color= "black" size="20px" /> </NavLink>
                        </NavItem>
                        <NavItem>
                            <NavLink tag={Link} className="text-dark" to="/Login">Login </NavLink>
                        </NavItem>
                        <NavItem>
                            <NavLink tag={Link} className="text-dark"to="/Sign-up">Sign-up </NavLink>
                        </NavItem>
                    </ul>
                </Collapse>
            </Container>
          </Navbar>
      </header>
    );
  }
}
