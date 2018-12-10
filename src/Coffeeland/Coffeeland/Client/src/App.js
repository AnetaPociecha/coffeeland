import React, { Component } from 'react';

class App extends Component {

  constructor(props) {
    super(props)
    this.state  = {hej: 'hej!'}
  }

  fun(){
    return 3;
  }

    render() {
        return (
        <div className="border p-5">
            <h1>{this.state.hej}</h1>
        </div> )
    }
}

export default App;