import React from 'react';
import { slide as Menu } from 'react-burger-menu';
import './SidebarProfile.css';
import { BiUserCircle } from "react-icons/bi";
import Container from 'react-bootstrap/Container'
import Row from 'react-bootstrap/Row'
import Col from 'react-bootstrap/Col'
import ProgressBar from 'react-bootstrap/ProgressBar'
import { Form } from 'react-bootstrap';

var exp = 3505;
var username = 'DFg11'
var level = '15'
var count = "3"
var outOf = "5"

const SidebarProfile = (props) => {
    return (
        <Menu noOverlay right width={450} customBurgerIcon={<BiUserCircle />} id={"SidebarProfile"} burgerButtonClassName={"profile-sidebar-button"}>

            
            <Container className='profile-container1' style={{ color: 'black' }}>
                 <Row>
                     <Col>Username:</Col>
                     <Col>{`${username}`}</Col>
                 </Row>
           </Container>
           
           <Container className='profile-container2' style={{ color: 'white' }}>
                  <Row>
                     <Col>Level:</Col>
                     <Col>{`${level}`}</Col>
                  </Row>
           </Container>
           <div>
               <ProgressBar animated min={0} max={10000} now={exp} label={`${exp} exp`}  />
           </div>

            <Container className='profile-container3' style={{ color: 'white' }}>
                <Row>
                    <Col>Friends:</Col>
                    <Col>{`${count} / ${outOf}`}</Col>
                </Row>
                <Form.Control size="sm" type="text" placeholder="Add friend..." />
            </Container>
        </Menu>
    );
};

export default SidebarProfile;