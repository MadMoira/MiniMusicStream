import React from 'react';
import './NavBar.css';

function NavBar() {
    return (
        <div className="app-head test items-center">
            <div className="order-first ml-2 test">Icon</div>
            <div className="menu-item">Artists</div>
            <div className="menu-item">Albums</div>
            <div className="menu-item">Songs</div>
        </div>
    )
}

export default NavBar;
