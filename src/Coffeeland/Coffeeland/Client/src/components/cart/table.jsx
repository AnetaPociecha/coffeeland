import React, { Component } from "react";
import CartEntry from "./cartEntry";
import OrderModal from "./orderModal";
import { Button } from "./../button";
import { getSumPrice } from "./../../helpers/priceHelper";
import { BUY } from "../../constants/titles";
import PropTypes from "prop-types";


export default class Table extends Component {
  state = {
    isActive: false
  };


  render() {
    const { cartEntries, onCartUpdate, onCartRemove, sumPrice} = this.props;
    const { isActive } = this.state
    return (
      <div className="col-12 m-3">
        {cartEntries.map(el => (
          <CartEntry
            key={el.item.name}
            item={el.item}
            quantity={el.quantity}
            onCartUpdate={onCartUpdate}
            onCartRemove={onCartRemove}
          />
        ))}

        <div className="row">
          <div className="col-6 font-weight-bold lead p-3">Total price: </div>
          <div className="col-6 text-right lead font-weight-bold p-3">
            $ {getSumPrice(cartEntries)}
          </div>
        </div>
        <div className="col-12 text-center p-3">
          <Button onClick={this.toggleModal} className="btn btn-dark btn-lg">
            {BUY}
          </Button>
        </div>
        <div>
          <OrderModal isActive={isActive} onModalClose={this.onModalClose}/>
        </div>
      </div>
    );
  }

  toggleModal = () => {
    this.setState({ isActive: true });
  };

  onModalClose = () => {
    this.setState({ isActive: false });
  }
}

Table.propTypes = {
  cartEntries: PropTypes.any.isRequired,
  onCartUpdate: PropTypes.func.isRequired,
  onCartRemove: PropTypes.func.isRequired,
};
