import React, { Component } from "react";
import { ORDERS } from "./../../constants/titles";
import { OrderTitle } from "../orderTitle";
import { Button } from "./../button";
import { getOrderStatus } from "./../../helpers/orderHelper";
import SectionTitle from "./sectionTitle";
import OrderEntries from "./orderEntries";
import AddressEntry from "./addressEntry";
import ProductHeader from "./productHeader";
import TotalPriceRow from "./totalPriceRow";

class Orders extends Component {
  state = {};
  render() {
    const { orders } = this.props;
    return (
      <div className="row mb-4 ">
        <div className="col-12">
          <OrderTitle>{ORDERS}</OrderTitle>
        </div>
        <div className="col-12">
          {orders &&
            orders.map(
              ({
                key,
                orderEntries,
                totalPrice,
                address,
                status,
                openDate,
                closeDate
              }) => (
                <div key={key} className="border m-3 p-3 col-12">
                  <ProductHeader />

                  <OrderEntries orderEntries={orderEntries} />

                  <TotalPriceRow totalPrice={totalPrice} />

                  <SectionTitle>Address</SectionTitle>
                  <AddressEntry address={address} />

                  <SectionTitle>Status</SectionTitle>

                  <div className="col-12 pt-2 pr-4 pl-4">
                    {getOrderStatus(status)}
                  </div>

                  <SectionTitle>Open date</SectionTitle>
                  <div className="col-12 pt-2 pr-4 pl-4"> {openDate} </div>

                  {closeDate && <SectionTitle>Close date</SectionTitle>}
                  <div className="col-12 pt-2 pr-4 pl-4"> {closeDate} </div>

                  <div className="col-12 text-center pt-3 pb-2">
                    <Button disabled={!closeDate}>Complain</Button>
                  </div>
                </div>
              )
            )}
        </div>
      </div>
    );
  }
}

export default Orders;
