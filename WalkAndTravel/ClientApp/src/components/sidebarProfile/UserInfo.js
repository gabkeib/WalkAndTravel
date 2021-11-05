import React, { Component } from 'react';
import User1 from './user1.json';
import Container from 'react-bootstrap/Container';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import ProgressBar from 'react-bootstrap/ProgressBar';
import { Form } from 'react-bootstrap';
import './SidebarProfile.css';

class UserInfo extends Component {

    render() {
        return (
        <div>
            {User1.map((userDetail, index) => {
                    return <div>
                        <Container className='profile-container1' style={{ color: 'black' }}>
                            <Row>
                                <Col>Username:</Col>
                                <Col>{`${userDetail.username}`}</Col>
                            </Row>
                        </Container>

                        <Container className='profile-container2' style={{ color: 'white' }}>
                            <Row>
                                <Col>Level:</Col>
                                <Col>{`${userDetail.level}`}</Col>
                            </Row>
                        </Container>
                 
                        <div>
                            <ProgressBar animated className='Progress-bar' min={0} max={10000} now={userDetail.experience} label={`${userDetail.experience} exp`} />
                        </div>

                        <Container className='profile-container3' style={{ color: 'white' }}>
                            <Row>
                                <Col>Friends online:</Col>
                                <Col>{`${userDetail.friendsOnline} / ${userDetail.totalFriends}`}</Col>
                            </Row>
                            <Form.Control size="sm" type="text" placeholder="Add friend..." />
                        </Container>
                    </div>
            })}
        </div>
        )
    }
}
export default UserInfo