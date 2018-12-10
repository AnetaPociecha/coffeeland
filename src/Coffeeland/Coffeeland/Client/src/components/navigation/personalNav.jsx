import * as React from 'react';
import {NavLink} from 'react-router-dom';
import * as path from '../../constants/paths';
import * as title from '../../constants/titles';

class PersonalNav extends React.Component {
    render() { 
        const 
        {
            isSignIn,
            handleSignOut
        } = this.props;

        return ( 
            <div>
                <ul className="nav justify-content-end small">
                    { isSignIn ? 
                    <li className="nav-item">
                        <NavLink
                            className="nav-link"
                            to={path.MY_ACCOUNT} exact>
                            {title.MY_ACCOUNT}
                        </NavLink>
                    </li> : null }
                    <li>
                        <NavLink 
                            className="nav-link"
                            to={path.SIGN_IN} exact
                            onClick={e => {
                                if(isSignIn) {
                                    e.preventDefault();
                                    handleSignOut();
                                }
                            }}
                        >
                            { isSignIn ? title.SIGN_OUT
                                : title.SIGN_IN_REGISTER }
                            
                        </NavLink>
                    </li>
                    <li>
                        <NavLink 
                            className="nav-link"
                            to={path.CART} exact>
                            {title.CART}
                        </NavLink>
                    </li>
                </ul>
            </div>
            );
    }
}
 
export default PersonalNav;