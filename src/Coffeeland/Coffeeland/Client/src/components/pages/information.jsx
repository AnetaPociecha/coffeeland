import React, { Component } from 'react';
import * as title from '../../constants/titles';

class Information extends Component {
    state = {  }
    render() { 
        return ( 
            <div className="p-5 mx-auto" style={this.props.style}>
                <h5 className="text-center p-1">
                    About {title.SHOP_NAME}
                </h5>
                <div className="text-justify">
                    <p>
                        Lorem ipsum dolor sit amet, consectetur adipiscing elit. In eget velit id arcu tristique luctus. Nunc sed risus eget neque varius congue. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Nulla egestas velit vel purus convallis sagittis. Nulla quis urna mollis, porttitor lectus et, venenatis massa. Nam a porta libero, eget blandit sapien. In at dignissim enim. Nulla ac urna vestibulum, consectetur sapien a, ultricies felis. Suspendisse sit amet lacinia ante. Quisque magna sapien, pharetra non elit in, scelerisque ullamcorper dui. Nunc faucibus ipsum ac sagittis ullamcorper. Pellentesque ac porttitor purus, sed lobortis nisi. Ut mattis elementum risus, id fermentum urna tincidunt elementum.
                    </p>
                    <p>
                        Aliquam varius tellus in ornare accumsan. Cras eget leo eget nulla bibendum semper. Vestibulum dapibus in sapien sit amet maximus. Cras imperdiet lectus at diam suscipit auctor. Suspendisse potenti. Sed fringilla viverra velit at blandit. Donec consectetur blandit facilisis. In volutpat vulputate velit, at posuere elit finibus a. Phasellus pharetra auctor felis, et lacinia nisi lobortis sagittis. Praesent faucibus congue tellus. Aliquam suscipit mauris et urna varius, ut varius augue dapibus.
                    </p>
                </div>
            </div> );
    }
}
 
export default Information;