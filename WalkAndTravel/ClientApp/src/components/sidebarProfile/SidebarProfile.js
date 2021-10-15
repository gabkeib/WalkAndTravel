import React from 'react';
import { slide as Menu } from 'react-burger-menu';
import './SidebarProfile.css';
import { BiUserCircle } from "react-icons/bi";
import ProgressBar from 'react-bootstrap/ProgressBar'

var exp = 3505;

const SidebarProfile = (props) => {
    return (
        <Menu noOverlay right width={450} customBurgerIcon={<BiUserCircle />} id={"SidebarProfile"} burgerButtonClassName={"profile-sidebar-button"}>

            <div>
                <ProgressBar animated min={0} max={10000} now={exp} label={`${exp} exp`} />
            </div>
        </Menu>
    );
};

export default SidebarProfile;