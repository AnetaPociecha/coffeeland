import React, { Component } from "react";
import OrderDetails from "./orderDetails";
import BillingDetails from "./billingDetails";
import { Button, CloseButton } from "./../button";
import PropTypes from "prop-types";
import { connect } from "react-redux";
import {
  fetchAddressBook
} from "./../../actions/addressBookActions";


class CheckoutDetails extends Component {

  state = {
    isSubmitButtonDisabled: false
  }

  componentWillMount() {
    this.props.fetchAddressBook(this.props.token);
  }

  render() {
      const {onModalClose, cartEntries} = this.props
      const {isSubmitButtonDisabled} = this.state
      const { addressBook } = this.props.addressBook

    return (
      <div className="col-12 ml-3 mr-3 mb-3 pt-0">

        <div className="row">
          <div className="col-12 float-right">
            <CloseButton onClick={onModalClose}/>
          </div>
        </div>


        <div className="row pr-4">
          <div className="col-md-6 col-sm-12 p-2 pt-3">
            <OrderDetails cartEntries={cartEntries}/>
          </div>
          <div className="col-md-6 col-sm-12 p-2 pt-3 ">
            <BillingDetails addresses={addressBook} disableSubmitButton={this.disableSubmitButton}/> 
          </div>
        </div>


        <div className="row">
          <div className="col-12 p-4 text-center">
            <Button className="btn btn-dark btn-lg" disabled={isSubmitButtonDisabled}>Next</Button>
          </div>
        </div>
      </div>
    );
  }

  disableSubmitButton = disable => {
    console.log("disableSubmitButton", disable)
    this.setState({isSubmitButtonDisabled: disable})
  }
}

CheckoutDetails.propTypes = {
  fetchAddressBook: PropTypes.func.isRequired,
  addressBook: PropTypes.object.isRequired,
  token: PropTypes.string.isRequired,
  isSignIn: PropTypes.bool.isRequired
};

const mapStateToProps = state => ({
  addressBook: state.addressBook.addressBook,
  token: state.token.token.token,
  isSignIn: state.token.token.isSignIn
});

export default connect(
  mapStateToProps,
  { fetchAddressBook }
)(CheckoutDetails);