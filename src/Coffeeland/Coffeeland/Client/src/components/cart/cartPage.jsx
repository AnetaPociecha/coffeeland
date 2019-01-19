import React, { Component } from "react";
import { OrderTitle } from "../orderTitle";
import { SHOPPING_CART } from "../../constants/titles";
import { CART_IS_EMPTY } from "../../constants/messages";
import { isArrayEmpty } from "../../helpers/arrayHelper";
import Table from "./table";
import { withMessage } from "./../../with/withMessage";

class CartPage extends Component {
  state = {
    showBuyMsg: false
  };

  render() {
    const { cartEntries, onCartUpdate, onCartRemove, style, onCartEmpty } = this.props;
    const { showBuyMsg } = this.state;
    return (
      <div className="row mx-auto m-4" style={style}>
        <div className="col-12 p-3">
          <OrderTitle>{SHOPPING_CART}</OrderTitle>
        </div>

        {showBuyMsg ? (
          <div className="text-success text-center font-weight-bold col-12">
            Thank you for buying our coffee! We will send you an email
            confirmation.
          </div>
        ) : (
          <div className="row mx-auto ">
            <TableWithMessage
              shouldDisplayMsg={isArrayEmpty(cartEntries)}
              msg={CART_IS_EMPTY}
              cartEntries={cartEntries}
              onCartUpdate={onCartUpdate}
              onCartRemove={onCartRemove}
              cleanUpAfterBuy={this.cleanUpAfterBuy}
            />
          </div>
        )}
      </div>
    );
  }

  cleanUpAfterBuy = () => {
    this.props.onCartEmpty();
    this.setState({ showBuyMsg: true });
    setTimeout(() => this.setState({ showBuyMsg: false }), 5000);
  };
}

const TableWithMessage = withMessage(Table);

export default CartPage;
