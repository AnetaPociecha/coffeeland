import React, { Component } from "react";
import SectionTitle from "./../orderTitle/sectionTitle";
import { getTotalPrice } from "./../../helpers/priceHelper";
import TotalPriceRow from './totalPriceRow'

export default class OrderDetails extends Component {
  render() {
    const { cartEntries } = this.props;
    return (
      <div className="col-12 p-3 border">
        <SectionTitle>Order Details</SectionTitle>

        <div className="row pt-2">
          <div className="col-4 text-muted p-1 pl-4 ">Name:</div>
          <div className="col-4 text-muted pt-1 pb-1">Quantity:</div>
          <div className="col-4 text-muted p-1 pr-4">Price:</div>
        </div>

        {cartEntries.map(({ key, item, quantity }) => (
          <div className="row" key={key}>
            <div className="col-4 pt-1 pb-1 pr-1 pl-4">{item.name}</div>
            <div className="col-4 pt-1 pb-1 pr-4 pl-1 text-center">
              {quantity}
            </div>
            <div className="col-4 pt-1 pb-1 pr-4 pl-1 text-right">
              $ {getTotalPrice(item.price, quantity)}
            </div>
          </div>
        ))}

        <TotalPriceRow cartEntries={cartEntries} small={true}/>
      </div>
    );
  }
}
