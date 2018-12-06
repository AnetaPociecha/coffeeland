import * as React from 'react';
import {NavLink} from 'react-router-dom';
import * as path from '../../constants/paths';
import * as title from '../../constants/titles';

class ShopNav extends React.Component {
    render() { 
        return (
           <div >
                <ul className="nav nav-tabs justify-content-center">
                    <li className="nav-item">
                        <NavLink 
                            className="nav-link" 
                            to={path.SHOP} exact>
                            {title.SHOP}
                        </NavLink>
                    </li>
                    <li className="nav-item">
                        <NavLink 
                            className="nav-link"
                            to={path.INFORMATION} exact>
                            {title.INFORMATION}
                        </NavLink>
                    </li>
                </ul>
            </div>
        );
    }
}
 
export default ShopNav;