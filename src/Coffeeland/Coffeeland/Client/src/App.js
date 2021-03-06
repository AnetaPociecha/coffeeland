import React, { Component } from "react";
import NavigationComponent from "./components/navigation/navigationComponent";
import { BrowserRouter as Router, Route } from "react-router-dom";
import { MAX_QUANTITY } from "./constants/config";
import { routes } from "./routes";
import { Provider } from "react-redux";
import store from "./store";

class App extends Component {
  state = {
    cartEntries: []
  }; 

  render() {

    const { cartEntries } = this.state;
    return (
      <Provider store={store}>
        <div className="container-fluid">
          <Router>
            <div>
              <NavigationComponent />
              {routes.map(({ path, component: C }) => (
                <Route
                  path={path}
                  exact
                  strict
                  render={props => (
                    <C
                      {...props}
                      onAddToCart={this.onAddToCart}
                      cartEntries={cartEntries}
                      onCartUpdate={this.onCartUpdate}
                      onCartRemove={this.onCartRemove}
                      onCartEmpty={this.onCartEmpty}
                      style={{ maxWidth: 1000 }}
                    />
                  )}
                  key={path}
                />
              ))}
            </div>
          </Router>
        </div>
      </Provider>
    );
  }

  onAddToCart = (item, quantity) => {
    const currentQuantity = this.howManyItemInCart(item);
    if (currentQuantity === 0) {
      this.setState(prevState => ({
        cartEntries: [
          ...prevState.cartEntries,
          { item: item, quantity: quantity }
        ]
      }));
    } else if (currentQuantity < MAX_QUANTITY) {
      const sumQuantity = quantity + currentQuantity;
      const newQuantity =
        sumQuantity > MAX_QUANTITY ? MAX_QUANTITY : sumQuantity;
      this.onCartUpdate(item, newQuantity);
    }
  };

  onCartUpdate = (item, newQuantity) => {
    const oldCartEntries = [...this.state.cartEntries];
    const newCartEntries = oldCartEntries.map(el =>
      el.item == item ? { item: item, quantity: newQuantity } : el
    );
    this.setState({
      cartEntries: newCartEntries
    });
  };

  howManyItemInCart = item => {

    const q = this.state.cartEntries.find(entry => entry.item == item);    
    return q ? q.quantity : 0;
  };

  onCartRemove = item => {
    const oldCartEntries = [...this.state.cartEntries];
    const newCartEntries = oldCartEntries.filter(entry => entry.item !== item);
    this.setState({
      cartEntries: newCartEntries
    });
  };

  onCartEmpty = () => {
    this.setState({cartEntries: []})
  }

}

export default App;
