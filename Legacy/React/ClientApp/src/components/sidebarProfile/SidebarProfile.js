import React from 'react';
import { slide as Menu } from 'react-burger-menu';
import './SidebarProfile.css';
import { BiUserCircle } from "react-icons/bi";
import UserInfo from './UserInfo.js';

const SidebarProfile = (props) => {
    return (
        <Menu noOverlay right width={450} customBurgerIcon={<BiUserCircle />} id={"SidebarProfile"} burgerButtonClassName={"profile-sidebar-button"}>

            <UserInfo/>
           
        </Menu>
    );
};

export default SidebarProfile;