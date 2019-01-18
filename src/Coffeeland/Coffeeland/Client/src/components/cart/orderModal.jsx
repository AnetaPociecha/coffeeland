import React, { Component } from "react";
import Modal from "react-modal";
import OrderDetails from "./orderDetails";
import BillingDetails from "./billingDetails";
import { Button, CloseButton } from "../button";
import { PayPalButton } from "./../paypalButton";

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

export default class OrderModal extends Component {
  componentWillMount() {
    Modal.setAppElement("body");
  }

  state = {
    mode: ORDER_DETAILS,
    selectedAddress: ''
  };

  componentWillReceiveProps(nextProps) {
    console.log('nextProps',nextProps)
    if(nextProps.addressBook) {
      this.setSelectedAddress(nextProps.addressBook[0])
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
    this.setState({ mode: PAYMENT_DETAILS });
  };

  getInnerComponent = () => {
    switch (this.state.mode) {
      case ORDER_DETAILS:
        return this.getDetailsComponent(
          "Next",
          <OrderDetails cartEntries={this.props.cartEntries}/>,
          this.setBillingMode
        );
      case BILLING_DETAILS:
        return this.getDetailsComponent(
          "Next",
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
          this.props.onModalClose
      );
    }
  };

  setSelectedAddress = (selectedAddress) => {
    this.setState({ selectedAddress })
  }

  getDetailsComponent = (buttonText, child, onClik) => (
    <div className="row">
      <div className="col-12">{child}</div>
      <div className="col-12 text-center p-3">
        <Button onClick={onClik}>{buttonText}</Button>
      </div>
    </div>
  );
}
