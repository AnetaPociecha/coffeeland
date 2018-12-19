import * as React from 'react';
import {NavLink} from 'react-router-dom';
import * as path from '../../constants/paths';
import * as title from '../../constants/titles';
import PropTypes from "prop-types";
import { connect } from "react-redux";
import { signOut } from "./../../actions/signInActions";

const linkClass = 'nav-link font-weight-bold title text-success'

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
                            className={linkClass}
                            to={path.MY_ACCOUNT} exact>
                            {title.MY_ACCOUNT}
                        </NavLink>
                    </li> : null }
                    <li>
                        <NavLink 
                            className={linkClass}
                            to={path.SIGN_IN} exact
                            onClick={e => {
                                if(isSignIn) {
                                    e.preventDefault();
                                    handleSignOut();
                                    this.props.signOut();
                                }
                            }}
                        >
                            { isSignIn ? title.SIGN_OUT
                                : title.SIGN_IN_REGISTER }
                            
                        </NavLink>
                    </li>
                    <li>
                        <NavLink 
                            className={linkClass}
                            to={path.CART} exact>
                            {title.CART}
                        </NavLink>
                    </li>
                </ul>
            </div>
            );
    }
}
 

PersonalNav.propTypes = {
    signOut: PropTypes.func.isRequired,
    token: PropTypes.object.isRequired
  };
  
  const mapStateToProps = state => ({
    token: state.token.token
  });
  
  export default connect(
    mapStateToProps,
    { signOut }
  )( PersonalNav);
  