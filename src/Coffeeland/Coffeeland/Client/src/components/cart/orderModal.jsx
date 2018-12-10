import React, { Component } from "react";
import { Button } from "./../button";
import Modal from "react-modal";
import CheckoutDetails from './checkoutDetails'

export default class OrderModal extends Component {

  componentWillMount() {
    Modal.setAppElement("body");
  }

  render() {
    const { isActive, onModalClose } = this.props
    return (
      <Modal isOpen={isActive} onRequestClose={onModalClose}>
        <CheckoutDetails  onModalClose={onModalClose} />
      </Modal>
    );
  }
}
