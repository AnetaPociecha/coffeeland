import React from "react";
import { getTotalPrice } from "./../../helpers/priceHelper";

export default function TotalPriceRow({ totalPrice }) {
  return (
    <div className="row border-top">
      <div className="col-6 p-2 pl-4 font-weight-bold">Total price:</div>
      <div className="col-6 p-2 text-right pr-4 font-weight-bold">
        $ {getTotalPrice(totalPrice)}
      </div>
    </div>
  );
}
