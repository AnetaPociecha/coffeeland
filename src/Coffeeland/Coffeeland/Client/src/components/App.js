import React, { Component } from 'react';

class App extends Component {

    state = {
      hej: 'hej !'
    }

    render() {
        return (
        <div>
            <h1>{this.state.hej}</h1>
        </div> )
    }
}

export default App;