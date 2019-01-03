import React, { Component } from "react";
import { ImageLink } from "./../image";
import { Price } from "./../price";
import { Button } from "./../button";
import { Info } from "./../info";
import { InputSpinner } from "./../inputSpinner";
import * as config from "./../../constants/config";
import { getItemPath } from './../../helpers/pathHelper';

class CartEntry extends Component {
  state = {
    item: this.props.item,
    quantity: this.props.quantity
  };
  render() {
    const { item, quantity } = this.state
    const { onCartRemove } = this.props
    const path = getItemPath(item.name)
    return (
      <div className="row p-1 m-2 border">

        <div className="pt-2 col-md-4 col-sm-12 my-auto">
          <ImageLink src={item.img} path={path} />
        </div>

        <div className="pt-2 col-md-3 col-sm-6 my-auto">
          <Info>
            Name: <Info className=''>{item.name}</Info>
          </Info>
          <Info>
            Type: <Info className="">{item.type}</Info>
          </Info>
          <Info>
            Weight: <Info className="">{config.WEIGTH} kg</Info>
          </Info>
        </div>

        <div className="pt-2 col-md-3 col-sm-6 my-auto">
          <Info>
            Price: <Price className="" price={item.price} />
          </Info>
          <Info>
            Quantity:{" "}
            <InputSpinner
              value={quantity}
              onMinus={this.onMinus}
              onPlus={this.onPlus}
              min={config.MIN_QUANTITY}
              max={config.MAX_QUANTITY}
            />
          </Info>
          <Info>
            Total price:{" "}
            <Price className="" quantity={quantity} price={item.price} />
          </Info>
        </div>
        <div className="col-sm-6 col-md-2 my-auto">
          <Button onClick={()=>onCartRemove(item)}>Remove</Button>
        </div>

      </div>
    );
  }

  onMinus = () => {
    if (this.state.quantity > config.MIN_QUANTITY) {
      this.setState(prevState => ({ quantity: prevState.quantity - 1 }));
      this.props.onCartUpdate(this.state.item, this.state.quantity - 1);
    }
  };

  onPlus = () => {
    if (this.state.quantity < config.MAX_QUANTITY) {
      this.setState(prevState => ({ quantity: prevState.quantity + 1 }));
      this.props.onCartUpdate(this.state.item, this.state.quantity + 1);
    }
  };
}

export default CartEntry;
