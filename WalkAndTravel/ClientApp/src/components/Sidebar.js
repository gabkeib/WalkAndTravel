import React from 'react';
import { slide as Menu } from 'react-burger-menu';
import { Button } from 'react-bootstrap';
import { Form } from 'react-bootstrap';
import { Dropdown } from 'react-bootstrap';
import { DropdownButton } from 'react-bootstrap';
import { ButtonGroup } from 'react-bootstrap';
import './Sidebar.css';

export default props => {
    return (
        <Menu right width={450}>
            <Form.Control size="lg" type="text" placeholder="Search trail by street name" />
            
            <div className="mb-2">
                {[DropdownButton].map((DropdownType, idx) => (
                    <DropdownType
                        className="DropDownButton1"
                        as={ButtonGroup}
                        key={idx}
                        id={`dropdown-button-drop-${idx}`}
                        title="Trail Length"
                    >
                        <Dropdown.Item eventKey="1">Long</Dropdown.Item>
                        <Dropdown.Item eventKey="2">Medium</Dropdown.Item>
                        <Dropdown.Item eventKey="3">Short</Dropdown.Item>
                        
                    </DropdownType>
                ))}

                {[DropdownButton].map((DropdownType, idx) => (
                    <DropdownType
                        className="DropDownButton2"
                        as={ButtonGroup}
                        key={idx}
                        id={`dropdown-button-drop-${idx}`}
                        title="Include sites worth visitting?"
                    >
                        <Dropdown.Item eventKey="1">Yes</Dropdown.Item>
                        <Dropdown.Item eventKey="2">No</Dropdown.Item>

                    </DropdownType>
                ))}
            </div>

            <div className="d-grid gap-2">
                <Button className="VerticalBlock" variant="primary" vertical block>
                    Show POI near me
                </Button>
            </div>

            <div>
                <a href="https://www.youtube.com/watch?v=dQw4w9WgXcQ" target="_blank">
                    <Button className="Button"> Link Button </Button>
                </a>
            </div>
        </Menu>
    );
};