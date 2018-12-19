import * as React from 'react';
import {NavLink} from 'react-router-dom';
import * as path from '../../constants/paths';
import * as title from '../../constants/titles';

const linkClass = 'nav-link text-light font-weight-bold title'

class ShopNav extends React.Component {
    render() { 
        return (
           <div >
                <ul className="nav bg-success justify-content-center border-bottom mb-2">
                    <li className="nav-item">
                        <NavLink 
                            className={linkClass}
                            to={path.SHOP} exact>
                            {title.SHOP}
                        </NavLink>
                    </li>
                    <li className="nav-item">
                        <NavLink 
                            className={linkClass}
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