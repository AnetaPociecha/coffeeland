import React from "react";
import { getTotalPrice } from './../../helpers/priceHelper'

export default function OrderEntries({ orderEntries }) {
  return (
    <div>
      {orderEntries &&
        orderEntries.map(({ key, name, quantity, price }) => (
          <div className="row border-top" key={key}>
            <div className="col-4 pt-1 pb-1 pr-1 pl-4">{name}</div>
            <div className="col-4 pt-1 pb-1 pr-4 pl-1 text-center">
              {quantity}
            </div>
            <div className="col-4 pt-1 pb-1 pr-4 pl-1 text-right">
              $ {getTotalPrice(price)}
            </div>
          </div>
        ))}
    </div>
  );
}
