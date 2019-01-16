import React, { Component } from "react";
import Modal from "react-modal";
import CheckoutDetails from "./checkoutDetails";
import "./style.css";

export default class OrderModal extends Component {
  componentWillMount() {
    Modal.setAppElement("body");
  }

  render() {
    const { isActive, onModalClose, cartEntries } = this.props;
    console.log("hej4");
    return (
      <Modal
        bsSize="small"
        dialogClassName="modal"
        isOpen={isActive}
        onRequestClose={onModalClose}
      >
        <CheckoutDetails
          cartEntries={cartEntries}
          onModalClose={onModalClose}
          total={this.props.total}
        />
      </Modal>
    );
  }
}
