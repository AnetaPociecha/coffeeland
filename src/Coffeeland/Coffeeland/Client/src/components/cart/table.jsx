import React, { Component } from "react";
import CartEntry from "./cartEntry";
import OrderModal from "./orderModal";
import { Button } from "./../button";
import { BUY } from "../../constants/titles";
import PropTypes from "prop-types";
import TotalPriceRow from "./totalPriceRow";
import { connect } from "react-redux";
import { WarningAlert } from "./../alert";
import { getSumPrice } from "./../../helpers/priceHelper";
import { fetchAddressBook } from "./../../actions/addressBookActions";
import { isArrayEmpty } from "../../helpers/arrayHelper";

class Table extends Component {
  state = {
    isActive: false,
    isAddressPresent: false
  };

  componentWillMount() {
    if (this.props.isSignIn && this.props.token) {
      this.props.fetchAddressBook(this.props.token);
    }
  }

  componentWillReceiveProps(nextProps) {
    if (
      nextProps.addressBook &&
      !isArrayEmpty(nextProps.addressBook.addressBook)
    ) {
      this.setState({ isAddressPresent: true });
    } else if (
      nextProps.addressBook &&
      isArrayEmpty(nextProps.addressBook.addressBook)
    ) {
      this.setState({ isAddressPresent: false });
    }
  }

  render() {
    const {
      cartEntries,
      onCartUpdate,
      onCartRemove,
      isSignIn,
      sumPrice,
    } = this.props;
    const { addressBook } = this.props.addressBook;
    const { isActive, isAddressPresent } = this.state;

    return (
      <div className="col-12 m-3">
        {!isSignIn && (
          <div className="pb-3 pl-1 pr-1 col-12">
            <WarningAlert>Please sign in before you buy</WarningAlert>
          </div>
        )}

        {isSignIn && !isAddressPresent && (
          <div className="pb-3 pl-1 pr-1 col-12">
            <WarningAlert>
              Please add address to address book before you buy
            </WarningAlert>
          </div>
        )}

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
          <Button
            onClick={this.toggleModal}
            className="btn btn-dark btn-lg"
            disabled={!isSignIn || !isAddressPresent}
          >
            {BUY}
          </Button>
        </div>
        <div>
          <OrderModal
            addressBook={addressBook}
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
  isSignIn: PropTypes.bool.isRequired,
  addressBook: PropTypes.object.isRequired
};

const mapStateToProps = state => ({
  token: state.token.token.token,
  isSignIn: state.token.token.isSignIn,
  addressBook: state.addressBook.addressBook
});

export default connect(
  mapStateToProps,
  { fetchAddressBook }
)(Table);
