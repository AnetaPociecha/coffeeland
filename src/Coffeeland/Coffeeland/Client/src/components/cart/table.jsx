import React, { Component } from "react";
import CartEntry from "./cartEntry";
import OrderModal from "./orderModal";
import { Button } from "./../button";
import { BUY } from "../../constants/titles";
import PropTypes from "prop-types";
import TotalPriceRow from "./totalPriceRow";

export default class Table extends Component {
  state = {
    isActive: false
  };

  render() {
    const { cartEntries, onCartUpdate, onCartRemove } = this.props;
    const { isActive } = this.state;
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

        <TotalPriceRow cartEntries={cartEntries} />

        <div className="col-12 text-center p-3">
          <Button onClick={this.toggleModal} className="btn btn-dark btn-lg">
            {BUY}
          </Button>
        </div>
        <div>
          <OrderModal
            cartEntries={cartEntries}
            isActive={isActive}
            onModalClose={this.onModalClose}
          />
        </div>
      </div>
    );
  }

  toggleModal = () => {
    this.setState({ isActive: true });
  };

  onModalClose = () => {
    this.setState({ isActive: false });
  };
}

Table.propTypes = {
  cartEntries: PropTypes.any.isRequired,
  onCartUpdate: PropTypes.func.isRequired,
  onCartRemove: PropTypes.func.isRequired
};
