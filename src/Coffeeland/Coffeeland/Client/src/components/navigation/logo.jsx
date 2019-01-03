import * as React from 'react';
import {NavLink} from 'react-router-dom';
import * as path from '../../constants/paths';
import * as title from '../../constants/titles';
import './../../style.css'
class Logo extends React.Component {
    state = {  }
    render() { 
        return ( 
            <div 
            className="navbar navbar-light pb-3 pt-2">
                <NavLink 
                    className="navbar-brand mx-auto logo"
                    to={path.SHOP}>
                    <h1 className="display-2 text-dark">{title.SHOP_NAME}</h1>
                </NavLink>
            </div>
        );
    }
}
 
export default Logo;