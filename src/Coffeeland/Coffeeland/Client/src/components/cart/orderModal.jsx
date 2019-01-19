import React, { Component } from "react";
import Modal from "react-modal";
import OrderDetails from "./orderDetails";
import BillingDetails from "./billingDetails";
import { Button, CloseButton } from "../button";
import { PayPalButton } from "./../paypalButton";
import PropTypes from "prop-types";
import { connect } from "react-redux";
import MessageProcessor from "./../../messageProcessor/messageProcessor";
const mp = MessageProcessor.getInstance();

const customStyles = {
  content: {
    top: "50%",
    left: "50%",
    right: "auto",
    bottom: "auto",
    marginRight: "-50%",
    transform: "translate(-50%, -50%)",
    maxWidth: 600
  }
};

const ORDER_DETAILS = "ORDER DETAILS";
const BILLING_DETAILS = "BILLING DETAILS";
const PAYMENT_DETAILS = "PAYMENT DETAILS";

class OrderModal extends Component {
  componentWillMount() {
    Modal.setAppElement("body");
  }

  state = {
    mode: ORDER_DETAILS,
    selectedAddress: "",
    showMsg: false
  };

  componentWillReceiveProps(nextProps) {
    if (nextProps.addressBook) {
      this.setSelectedAddress(nextProps.addressBook[0]);
    }
  }

  render() {
    const { isActive, onModalClose } = this.props;
    return (
      <Modal
        isOpen={isActive}
        onRequestClose={onModalClose}
        onAfterOpen={this.onModalOpen}
        style={customStyles}
      >
        <div className="col-12">
          <div className="row">
            <div className="col-12 float-right">
              <CloseButton onClick={onModalClose} />
            </div>
          </div>
          <div className="row">
            <div className="col-12">{this.getInnerComponent()}</div>
          </div>
        </div>
      </Modal>
    );
  }

  onModalOpen = () => {
    this.setState({ mode: ORDER_DETAILS });
  };

  setBillingMode = () => {
    this.setState({ mode: BILLING_DETAILS });
  };

  setPaymentMode = () => {
    this.handleOrderRq();
    this.setState({ mode: PAYMENT_DETAILS });
  };

  getInnerComponent = () => {
    switch (this.state.mode) {
      case ORDER_DETAILS:
        return this.getDetailsComponent(
          "Next",
          <OrderDetails cartEntries={this.props.cartEntries} />,
          this.setBillingMode
        );
      case BILLING_DETAILS:
        return this.getDetailsComponent(
          "Buy",
          <BillingDetails
            addresses={this.props.addressBook}
            selectedAddress={this.state.selectedAddress}
            setSelectedAddress={this.setSelectedAddress}
          />,
          this.setPaymentMode
        );
      case PAYMENT_DETAILS:
        return this.getDetailsComponent(
          "Exit",
          <PayPalButton total={this.props.total} />,
          this.onExit
        );
    }
  };

  setSelectedAddress = selectedAddress => {
    this.setState({ selectedAddress });
  };

  getDetailsComponent = (buttonText, child, onClik) => (
    <div className="row">
      <div className="col-12">{child}</div>
      <div className="col-12 text-center p-3">
        <Button onClick={onClik}>{buttonText}</Button>
      </div>
    </div>
  );
  onExit = () => {
    this.props.onModalClose();
    this.props.cleanUpAfterBuy()
  };

  handleOrderRq = () => {
    const orderEntries = this.props.cartEntries.map(
      ce =>
        (ce = {
          key: ce.item.key,
          name: ce.item.name,
          quantity: ce.quantity,
          price: ce.item.price
        })
    );

    const rq = {
      $type: "AddOrderCommand",
      sessionToken: this.props.token,
      orderEntries: orderEntries,
      totalPrice: this.props.total,
      address: this.state.selectedAddress
    };
    mp.processCommand(rq).then(rs => console.log("rs", rs));
  }
}

OrderModal.propTypes = {
  token: PropTypes.string.isRequired
};

const mapStateToProps = state => ({
  token: state.token.token.token
});

export default connect(
  mapStateToProps,
  {}
)(OrderModal);
