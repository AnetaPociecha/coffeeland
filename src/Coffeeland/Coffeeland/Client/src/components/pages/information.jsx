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
                        There are infinite ways to ruin a good cup of coffee—the grind’s not right, the pour wasn’t steady enough, water’s too chilly—but the most surefire way to make a bad cup of coffee is to use bad beans. All the fancy coffee-brewing devices in the world can’t turn trash into something delicious. The cash spent outfitting a kitchen with fancy gadgetry would be for naught if you’re going to bring home beans from the supermarket. It’s something I learned the hard way since falling in love with coffee after a roommate busted a Chemex out when I was in college almost a [shivers] decade ago.
                    </p>
                    <p>
                        Contact us via email coffeeland1234@gmail.com.
                    </p>
                </div>
            </div> );
    }
}
 
export default Information;