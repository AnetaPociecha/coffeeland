import React, { Component } from "react";
import CartEntry from "./cartEntry";
import OrderModal from "./orderModal";
import { Button } from "./../button";
import { BUY } from "../../constants/titles";
import PropTypes from "prop-types";
import TotalPriceRow from "./totalPriceRow";
import { connect } from "react-redux";
import { SecondaryAlert } from "./../alert";
import { getSumPrice } from "./../../helpers/priceHelper";

class Table extends Component {
  state = {
    isActive: false
  };

  render() {
    const { cartEntries, onCartUpdate, onCartRemove, isSignIn, sumPrice } = this.props;
    const { isActive } = this.state;

    return (
      <div className="col-12 m-3">
        
        { !isSignIn && <div className="pb-3 pl-1 pr-1 col-12">
          <SecondaryAlert>Please sign in before you buy</SecondaryAlert>
        </div> }

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
          <Button onClick={this.toggleModal} className="btn btn-dark btn-lg" disabled={!isSignIn}>
            {BUY}
          </Button>
        </div>
        <div>
          <OrderModal
            cartEntries={cartEntries}
            isActive={isActive}
            onModalClose={this.onModalClose}
            total={getSumPrice(cartEntries)}
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
  onCartRemove: PropTypes.func.isRequired,
  token: PropTypes.string.isRequired,
  isSignIn: PropTypes.bool.isRequired
};

const mapStateToProps = state => ({
  token: state.token.token.token,
  isSignIn: state.token.token.isSignIn
});

export default connect(
  mapStateToProps,
  {}
)(Table);
