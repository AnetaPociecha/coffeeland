import * as React from 'react';
import {NavLink} from 'react-router-dom';
import * as path from '../../constants/paths';
import '../../styles.css';
import * as title from '../../constants/titles';

class Logo extends React.Component {
    state = {  }
    render() { 
        return ( 
            <div 
            className="navbar navbar-light">
                <NavLink 
                    className="navbar-brand mx-auto"
                    to={path.SHOP}>
                    <h3>{title.SHOP_NAME}</h3>
                </NavLink>
            </div>
        );
    }
}
 
export default Logo;