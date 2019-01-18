import React from "react";
import { getSumPrice } from "./../../helpers/priceHelper";

export default function TotalPriceRow({cartEntries}) {
  return (
    <div className="row">
      <div className="col-6 font-weight-bold lead p-3">Total price: </div>
      <div className="col-6 text-right lead font-weight-bold p-3">
        $ {getSumPrice(cartEntries)}
      </div>
    </div>
  );
}
