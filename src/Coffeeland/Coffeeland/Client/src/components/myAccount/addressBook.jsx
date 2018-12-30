import React, { Component } from "react";
import AddressEntry from "./addressEntry";
import { ADDRESS_BOOK, REMOVE } from "./../../constants/titles";
import { OrderTitle } from "./../orderTitle";
import { Button } from "../button";
import AddressForm from "./addressForm";
import { isAddressValid } from "./../../isValid";
import { getZIPCodeForDB } from "./../../helpers/addressHelper";
import PropTypes from "prop-types";
import { connect } from "react-redux";
import {
  fetchAddressBook,
  updateAddressBook,
  removeAddress
} from "./../../actions/addressBookActions";

class AddressBook extends Component {
  constructor(props) {
    super(props);
    this.state = this.getState();
  }

  state = {};

  componentWillMount() {
    this.props.fetchAddressBook(this.props.token);
  }

  render() {
    const {
      isAddNewAddressMode,
      country,
      city,
      ZIPCode,
      street,
      buildingNumber,
      apartmentNumber,
      isChecked,
    } = this.state;
    const { addressBook } = this.props.addressBook;

    return (
      <div>
        <OrderTitle>{ADDRESS_BOOK}</OrderTitle>
        <div className="pt-1">
          {addressBook &&
            addressBook.map(address => (
              <div key={address.key} className="pt-2 border m-3">
                <AddressEntry address={address} />
                <div className="row p-3">
                  <div className="col-12 text-right pt-3 pr-4">
                    <Button onClick={() => this.remove(address)}>
                      {REMOVE}
                    </Button>
                  </div>
                </div>
              </div>
            ))}
        </div>
        <div>
          {isAddNewAddressMode ? (
            <div className="pb-5 pr-3 pl-3 pt-4">
              <div>
                <AddressForm
                  country={country}
                  city={city}
                  ZIPCode={ZIPCode}
                  street={street}
                  buildingNumber={buildingNumber}
                  apartmentNumber={apartmentNumber}
                  onChange={this.onChange}
                  isChecked={isChecked}
                />
              </div>
              <div className="row">
                <div className="col-6 text-left">
                  <Button
                    onClick={this.saveNewAddress}
                    disabled={this.isSaveButtonDisabled()}
                  >
                    Save
                  </Button>
                </div>
                <div className="col-6 text-right">
                  <Button onClick={this.cancelNewAddress}>Cancel</Button>
                </div>
              </div>
            </div>
          ) : (
            <div className="pb-5 pr-3 pl-3 pt-4 text-center">
              <Button onClick={this.addNewAddress}>Add new address</Button>
            </div>
          )}
        </div>
      </div>
    );
  }

  isSaveButtonDisabled = () => {
    return (this.state.isChecked &&
    !isAddressValid(
      this.state.country,
      this.state.city,
      this.state.ZIPCode,
      this.state.street,
      this.state.buildingNumber,
      this.state.apartmentNumber
    ))
  }


  addNewAddress = () => {
    this.setState({
      isAddNewAddressMode: true
    });
  };

  saveNewAddress = () => {
    const {
      country,
      city,
      street,
      ZIPCode,
      buildingNumber,
      apartmentNumber
    } = this.state;
    // dont worry about key, you will get it from server

    this.setState({isChecked: true})

    if (
      isAddressValid(
        country,
        city,
        ZIPCode,
        street,
        buildingNumber,
        apartmentNumber
      )
    ) {
      this.props.updateAddressBook({
        key: 5,
        country,
        city,
        street,
        ZIPCode: getZIPCodeForDB(ZIPCode),
        buildingNumber,
        apartmentNumber
      });
      this.setState(this.getState());
    }
  };

  cancelNewAddress = () => {
    this.setState(this.getState());
  };

  onChange = event => {
    const target = event.target;
    const value = target.value;
    const name = target.name;
    this.setState({
      [name]: value
    });
  };

  getState = () => ({
    isAddNewAddressMode: false,
    country: "",
    city: "",
    ZIPCode: "",
    street: "",
    buildingNumber: "",
    apartmentNumber: "",
    isChecked: false
  });

  remove = address => {
    this.props.removeAddress(address);
  };
}

AddressBook.propTypes = {
  fetchAddressBook: PropTypes.func.isRequired,
  updateAddressBook: PropTypes.func.isRequired,
  removeAddress: PropTypes.func.isRequired,
  addressBook: PropTypes.object.isRequired,
  token: PropTypes.string.isRequired
};

const mapStateToProps = state => ({
  addressBook: state.addressBook.addressBook,
  token: state.token.token.token
});

export default connect(
  mapStateToProps,
  { fetchAddressBook, updateAddressBook, removeAddress }
)(AddressBook);
