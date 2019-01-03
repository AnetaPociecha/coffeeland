import * as React from 'react';
import ShopNav from './shopNav';
import Logo from './logo';
import PersonalNav from './personalNav';

class NavigationComponent extends React.Component {
    render() { 
        const 
        {
            isSignIn, 
            handleSignOut
        } = this.props;
        
        return ( 
            <div className="">
                <PersonalNav 
                    isSignIn={isSignIn}
                    handleSignOut={handleSignOut}
                />
                <Logo />
                <ShopNav />
            </div>
         );
    }
}

export default NavigationComponent;