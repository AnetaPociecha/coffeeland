import React, { Component } from "react";
import OrderDetails from "./orderDetails";
import BillingDetails from "./billingDetails";
import { Button, CloseButton } from "./../button";
import { PaypalButton } from "../paypalButton";

export default class CheckoutDetails extends Component {
  render() {
      const {onModalClose} = this.props
    return (
      <div className="col-12 ml-3 mr-3 mb-3 pt-0">
        <div className="row">
          <div className="col-12 float-right">
            <CloseButton onClick={onModalClose}/>
          </div>
        </div>
        <div className="row pr-4">
          <div className="col-md-7 col-sm-12 p-2 pt-3">
            <OrderDetails />
          </div>
          <div className="col-md-5 col-sm-12 p-2 pt-3 ">
            <BillingDetails />
          </div>
        </div>


        <div className="row">
          <div className="col-12 p-4 text-center">
            <PaypalButton />
          </div>
        </div>


        <div className="row">
          <div className="col-12 p-4 text-center">
            <Button className="btn btn-dark btn-lg">Next</Button>
          </div>
        </div>
      </div>
    );
  }
}
