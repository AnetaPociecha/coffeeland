import React, { Component } from "react";
import { OrderTitle } from "../orderTitle";
import { SHOPPING_CART } from "../../constants/titles";
import { CART_IS_EMPTY } from "../../constants/messages";
import { isArrayEmpty } from "../../helpers/arrayHelper";
import Table from "./table";
import { withMessage } from "./../../with/withMessage";

class CartPage extends Component {
  render() {
    const { cartEntries, onCartUpdate, onCartRemove, style } = this.props;
    return (
      <div className="row mx-auto m-4" style={style}>
        <div className="col-12 p-3">
          <OrderTitle>{SHOPPING_CART}</OrderTitle>
        </div>
        <TableWithMessage
          shouldDisplayMsg={isArrayEmpty(cartEntries)}
          msg={CART_IS_EMPTY}
          cartEntries={cartEntries}
          onCartUpdate={onCartUpdate}
          onCartRemove={onCartRemove}
        />
      </div>
    );
  }
}

const TableWithMessage = withMessage(Table);

export default CartPage;
